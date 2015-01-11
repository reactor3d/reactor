using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Reflection;
using Reactor.Math;
using System;
using System.Text;
using System.Runtime.InteropServices;
using Reactor.Geometry;


namespace Reactor.Types
{
    public class RShader : IDisposable
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct Attribute
        {
            public ActiveAttribType type;
            public int index;
            public string name;
            public int location;
        }

        private struct Uniform
        {
            public ActiveUniformType type;
            public int index;
            public string name;
            public int location;
        }
        private Attribute[] _attributes;
        private Uniform[] _uniforms;

        private readonly Dictionary<string, int> _uniformLocations = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _attributeLocations = new Dictionary<string, int>();
        public int Id { get; internal set; }
        RShaderEffect[] effects = new RShaderEffect[6];


        public void Load(string vertSource, string fragSource, string[] defines)
        {
            effects[(int)RShaderEffectType.VERTEX] = new RShaderEffect(vertSource, (int)RShaderEffectType.VERTEX, defines);
            effects[(int)RShaderEffectType.FRAGMENT] = new RShaderEffect(fragSource, (int)RShaderEffectType.FRAGMENT, defines);
            Id = GL.CreateProgram();
            foreach (RShaderEffect effect in effects)
            {
                if(effect!=null)
                    GL.AttachShader(Id, effect.Id);
            }

            GL.LinkProgram(Id);
            int linkStatus;
            GL.GetProgram(Id, GetProgramParameterName.LinkStatus, out linkStatus);
            if(linkStatus == (int)All.False){
                var log = GL.GetProgramInfoLog(Id);
                RLog.Error(log);
                REngine.CheckGLError();
                if (GL.IsProgram(Id))
                {
                    GL.DeleteProgram(Id);
                    REngine.CheckGLError();
                    Id = 0;
                }
                return;
            }

#if DEBUG && WINDOWS
            
            int status;
            GL.ValidateProgram(Id);
            GL.GetProgram(Id, GetProgramParameterName.ValidateStatus, out status);
            if (status != 1) throw new Exception(GL.GetProgramInfoLog(Id));
#endif
            REngine.CheckGLError();
            int numAttributes;
            GL.GetProgram(Id, GetProgramParameterName.ActiveAttributes, out numAttributes);
            REngine.CheckGLError();
            _attributes = new Attribute[numAttributes];
            for (int i = 0; i < numAttributes; i++)
            {
                int size;
                ActiveAttribType type;
                int length;
                StringBuilder name = new StringBuilder();
                GL.GetActiveAttrib(Id, i, 4096, out length, out size, out type, name);
                Attribute attrib = new Attribute();
                attrib.name = name.ToString().ToLower();  //ToLower() so that we can do matching with RVertexElementFormat's
                attrib.index = i;
                attrib.location = GL.GetAttribLocation(Id, attrib.name);
                attrib.type = type;
                _attributes[i] = attrib;
                _attributeLocations.Add(attrib.name, attrib.location);
            }

            int numUniforms;
            GL.GetProgram(Id, GetProgramParameterName.ActiveUniforms, out numUniforms);
            _uniforms = new Uniform[numUniforms];
            for (int i = 0; i < numUniforms; i++)
            {
                int size;
                ActiveUniformType type;
                int length;
                StringBuilder name = new StringBuilder();
                GL.GetActiveUniform(Id, i, 4096, out length, out size, out type, name);
                Uniform uni = new Uniform();
                uni.index = i;
                uni.name = name.ToString();
                uni.location = GL.GetUniformLocation(Id, uni.name);
                uni.type = type;
                _uniforms[i] = uni;
                _uniformLocations.Add(uni.name, uni.location);

            }

        }

        public void SetUniformValue(string name, int value)
        {
            GL.Uniform1(GetUniformLocation(name), value);
            REngine.CheckGLError();
        }
        public void SetUniformValue(string name, double value)
        {
            GL.Uniform1(GetUniformLocation(name), value);
            REngine.CheckGLError();
        }
        public void SetUniformValue(string name, float value)
        {
            GL.Uniform1(GetUniformLocation(name), value);
            REngine.CheckGLError();
        }
        public void SetUniformValue(string name, Vector2 value)
        {
            GL.Uniform2(GetUniformLocation(name), value);
            REngine.CheckGLError();
        }
        public void SetUniformValue(string name, Vector3 value)
        {
            GL.Uniform3(GetUniformLocation(name), value);
            REngine.CheckGLError();
        }
        public void SetUniformValue(string name, Vector4 value)
        {
            GL.Uniform4(GetUniformLocation(name), value);
            REngine.CheckGLError();
        }
        public void SetUniformValue(string name, Matrix value)
        {
            OpenTK.Matrix4 matrix = value;
            GL.UniformMatrix4(GetUniformLocation(name), false, ref matrix);
            REngine.CheckGLError();
        }

        public void SetSamplerValue(RTextureLayer layer, RTexture2D texture)
        {
            GL.ActiveTexture((TextureUnit)(int)layer);
            GL.BindTexture(TextureTarget.Texture2D, texture.Id);
        }

        public void SetSamplerValue(RTextureLayer layer, RTexture3D texture)
        {
            GL.ActiveTexture((TextureUnit)(int)layer);
            GL.BindTexture(TextureTarget.Texture3D, texture.Id);
        }
        internal int GetUniformLocation(string name)
        {
            if (_uniformLocations.ContainsKey(name))
            {
                return _uniformLocations[name];
            }
            else
            {
                int location = GL.GetUniformLocation(Id, name);
                if(location != -1){
                    _uniformLocations.Add(name, location);
                    return location;
                } else {
                    return -1;
                }
            }
        }
        internal int GetAttribLocation(string name)
        {
            for(int i=0; i<_attributes.Length; i++){
                if(_attributes[i].name == name){
                    return _attributes[i].location;
                }
            }
            return GL.GetAttribLocation(Id, name);

        }
        internal int GetAttribLocation(RVertexElementUsage rVertexElementUsage)
        {
            string name = "";
            switch(rVertexElementUsage)
            {
                case RVertexElementUsage.Position:
                    name = "position";
                    break;
                case RVertexElementUsage.Color:
                    name = "color";
                    break;
                case RVertexElementUsage.Normal:
                    name = "normal";
                    break;
                case RVertexElementUsage.Bitangent:
                    name = "bitangent";
                    break;
                case RVertexElementUsage.Tangent:
                    name = "tangent";
                    break;
                case RVertexElementUsage.TextureCoordinate:
                    name = "texcoord";
                    break;
                case RVertexElementUsage.BlendIndices:
                    name = "blendindices";
                    break;
                case RVertexElementUsage.BlendWeight:
                    name = "blendweight";
                    break;
                case RVertexElementUsage.TessellateFactor:
                    name = "tessellatefactor";
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
            return GetAttribLocation(name);
        }
        internal int GetAttribLocation(RVertexElementUsage rVertexElementUsage, int usageIndex)
        {
            int size;
            ActiveAttribType type;
            string name = GL.GetActiveAttrib(Id, usageIndex, out size, out type);
            return GL.GetAttribLocation(Id, name);
        }
        internal void Bind()
        {
            if (Id != 0)
            {
                GL.UseProgram(Id);
            }
        }

        internal void Unbind()
        {
            GL.UseProgram(0);
        }


        public void Dispose()
        {
            if (GL.IsProgram(Id))
            {
                GL.DeleteProgram(Id);
                Id = -1;
            }
        }
    }

    internal static class RShaderResources
    {
        internal static Assembly Assembly = Assembly.GetAssembly(typeof(RShaderResources));
        internal static string GetResourceString(string resource){
            System.IO.StreamReader reader = new System.IO.StreamReader(Assembly.GetManifestResourceStream(resource));
            string contents = reader.ReadToEnd();
            reader.Close();
            return contents;
        }
        internal static string Headers = GetResourceString("Reactor.Shaders.headers.glsl");

        internal static string BasicEffectVert = GetResourceString("Reactor.Shaders.basicEffect.vert.glsl");
        internal static string BasicEffectFrag = GetResourceString("Reactor.Shaders.basicEffect.frag.glsl");
    }
}