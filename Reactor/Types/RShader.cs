// Author:
//       Gabriel Reiser <gabe@reisergames.com>
//
// Copyright (c) 2010-2016 Reiser Games, LLC.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
        
        private readonly Dictionary<RShaderSemanticDefinition,RShaderSemantic> _semantics = new Dictionary<RShaderSemanticDefinition,RShaderSemantic>();

        private readonly Dictionary<string, int> _uniformLocations = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _attributeLocations = new Dictionary<string, int>();
        public int Id { get; internal set; }
        RShaderEffect[] effects = new RShaderEffect[6];

        public void Load(string vertSource, string fragSource) {
            Load(vertSource, fragSource, null, null);
        }
        public void Load(string vertSource, string fragSource, string geomSource) {
            Load(vertSource, fragSource, geomSource, null);
        }
        public void Load(string vertSource, string fragSource, string geomSource, string[] defines)
        {
            effects[(int)RShaderEffectType.VERTEX] = new RShaderEffect(vertSource, (int)RShaderEffectType.VERTEX, defines);
            effects[(int)RShaderEffectType.FRAGMENT] = new RShaderEffect(fragSource, (int)RShaderEffectType.FRAGMENT, defines);
            if(geomSource != null) {
                effects[(int)RShaderEffectType.GEOMETRY] = new RShaderEffect(geomSource, (int)RShaderEffectType.GEOMETRY, defines);
            }
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


            
            int status;
            GL.ValidateProgram(Id);
            GL.GetProgram(Id, GetProgramParameterName.ValidateStatus, out status);
            if (status != 1) throw new Exception(GL.GetProgramInfoLog(Id));

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
                attrib.name = name.ToString();
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
            foreach(var effect in effects)
            {
                if(effect!=null)
                {
                    if (effect.Semantics != null)
                    {
                        foreach (var keyPair in effect.Semantics)
                        {
                            _semantics.Add(keyPair.Key, keyPair.Value);
                        }
                    }

                    effect.Dispose();
                }
                
            }
            REngine.CheckGLError();
        }
        public void SetUniformValue(string name, bool value)
        {
            if(value)
                GL.Uniform1(GetUniformLocation(name), 1);
            else
                GL.Uniform1(GetUniformLocation(name), 0);
            REngine.CheckGLError();
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
        public void SetUniformValue(string name, RColor value)
        {
            SetUniformValue(name, value.ToVector4());
        }
        public void SetUniformValue(string name, Matrix value)
        {
            OpenTK.Matrix4 matrix = value;
            GL.UniformMatrix4(GetUniformLocation(name), false, ref matrix);
            REngine.CheckGLError();
        }

        public void SetSamplerValue(RTextureLayer layer, RTexture texture)
        {
            int unival = GetTexUniformValue(layer);
            int loc = GetTexUniformLocation(layer);
            GL.Uniform1(loc, unival);
            REngine.CheckGLError();
            TextureUnit unit = (TextureUnit)(int)layer;
            GL.ActiveTexture(unit);
            REngine.CheckGLError();
            texture.Bind();
            REngine.CheckGLError();

        }

        public int GetUniformBySemantic(RShaderSemanticDefinition semantic)
        {
            return GetUniformLocation(_semantics[semantic].name);
        }
        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, bool value)
        {
            if (_semantics.ContainsKey(semantic))
            {
                if (_semantics[semantic].type == "bool")
                    SetUniformValue(_semantics[semantic].name, value);
            }
        }
        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, int value)
        {
            if(_semantics.ContainsKey(semantic))
            {
                if (_semantics[semantic].type == "int")
                    SetUniformValue(_semantics[semantic].name, value);
            }
        }
        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, double value)
        {
            if (_semantics.ContainsKey(semantic))
            {
                if (_semantics[semantic].type == "double")
                    SetUniformValue(_semantics[semantic].name, value);
            }
        }
        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, float value)
        {
            if (_semantics.ContainsKey(semantic))
            {
                if (_semantics[semantic].type == "float")
                    SetUniformValue(_semantics[semantic].name, value);
            }
        }
        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, Vector2 value)
        {
            if (_semantics.ContainsKey(semantic))
            {
                if (_semantics[semantic].type == "vec2")
                    SetUniformValue(_semantics[semantic].name, value);
            }
        }
        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, Vector3 value)
        {
            if (_semantics.ContainsKey(semantic))
            {
                if (_semantics[semantic].type == "vec3")
                    SetUniformValue(_semantics[semantic].name, value);
            }
        }
        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, Vector4 value)
        {
            if (_semantics.ContainsKey(semantic))
            {
                if (_semantics[semantic].type == "vec4")
                    SetUniformValue(_semantics[semantic].name, value);
            }
        }
        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, Matrix value)
        {
            if (_semantics.ContainsKey(semantic))
            {
                if (_semantics[semantic].type == "mat4")
                    SetUniformValue(_semantics[semantic].name, value);
            }
        }
        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, RColor value)
        {
            if (_semantics.ContainsKey(semantic))
            {
                if (_semantics[semantic].type == "vec4")
                    SetUniformValue(_semantics[semantic].name, value);
            }
        }
        internal int GetTexUniformLocation(RTextureLayer layer)
        {
            string name = "";
            switch(layer)
            {
                case RTextureLayer.AMBIENT:
                    name = "ambient";
                    break;
                case RTextureLayer.DETAIL:
                    name = "detail";
                    break;
                case RTextureLayer.DIFFUSE:
                    name = "diffuse";
                    break;
                case RTextureLayer.GLOW:
                    name = "glow";
                    break;
                case RTextureLayer.HEIGHT:
                    name = "height";
                    break;
                case RTextureLayer.NORMAL:
                    name = "normal";
                    break;
                case RTextureLayer.SPECULAR:
                    name = "specular";
                    break;
                case RTextureLayer.TEXTURE7:
                    name = "texture7";
                    break;
                case RTextureLayer.TEXTURE8:
                    name = "texture8";
                    break;
                case RTextureLayer.TEXTURE9:
                    name = "texture9";
                    break;
                case RTextureLayer.TEXTURE10:
                    name = "texture10";
                    break;
                case RTextureLayer.TEXTURE11:
                    name = "texture11";
                    break;
                case RTextureLayer.TEXTURE12:
                    name = "texture12";
                    break;
                case RTextureLayer.TEXTURE13:
                    name = "texture13";
                    break;
                case RTextureLayer.TEXTURE14:
                    name = "texture14";
                    break;
                case RTextureLayer.TEXTURE15:
                    name = "texture15";
                    break;
                 
            }
            return GetUniformLocation(name);
        }
        internal int GetTexUniformValue(RTextureLayer layer)
        {
            RTextureUnit name = RTextureUnit.DIFFUSE;
            switch(layer)
            {
                case RTextureLayer.AMBIENT:
                    name = RTextureUnit.AMBIENT;
                    break;
                case RTextureLayer.DETAIL:
                    name = RTextureUnit.DETAIL;
                    break;
                case RTextureLayer.DIFFUSE:
                    name = RTextureUnit.DIFFUSE;
                    break;
                case RTextureLayer.GLOW:
                    name = RTextureUnit.GLOW;
                    break;
                case RTextureLayer.HEIGHT:
                    name = RTextureUnit.HEIGHT;
                    break;
                case RTextureLayer.NORMAL:
                    name = RTextureUnit.NORMAL;
                    break;
                case RTextureLayer.SPECULAR:
                    name = RTextureUnit.SPECULAR;
                    break;
                case RTextureLayer.TEXTURE7:
                    name = RTextureUnit.TEXTURE7;
                    break;
                case RTextureLayer.TEXTURE8:
                    name = RTextureUnit.TEXTURE8;
                    break;
                case RTextureLayer.TEXTURE9:
                    name = RTextureUnit.TEXTURE9;
                    break;
                case RTextureLayer.TEXTURE10:
                    name = RTextureUnit.TEXTURE10;
                    break;
                case RTextureLayer.TEXTURE11:
                    name = RTextureUnit.TEXTURE11;
                    break;
                case RTextureLayer.TEXTURE12:
                    name = RTextureUnit.TEXTURE12;
                    break;
                case RTextureLayer.TEXTURE13:
                    name = RTextureUnit.TEXTURE13;
                    break;
                case RTextureLayer.TEXTURE14:
                    name = RTextureUnit.TEXTURE14;
                    break;
                case RTextureLayer.TEXTURE15:
                    name = RTextureUnit.TEXTURE15;
                    break;

            }
            return (int)name;
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
                REngine.CheckGLError();
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
                    name = "r_Position";
                    break;
                case RVertexElementUsage.Color:
                    name = "r_Color";
                    break;
                case RVertexElementUsage.Normal:
                    name = "r_Normal";
                    break;
                case RVertexElementUsage.Bitangent:
                    name = "r_Bitangent";
                    break;
                case RVertexElementUsage.Tangent:
                    name = "r_Tangent";
                    break;
                case RVertexElementUsage.TextureCoordinate:
                    name = "r_TexCoord";
                    break;
                case RVertexElementUsage.BlendIndices:
                    name = "r_BlendIndices";
                    break;
                case RVertexElementUsage.BlendWeight:
                    name = "r_BlendWeight";
                    break;
                case RVertexElementUsage.TessellateFactor:
                    name = "r_TessellateFactor";
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
        public void Bind()
        {
            if (Id != 0)
            {
                GL.UseProgram(Id);
                REngine.CheckGLError();
            }
            else
            {
                throw new EngineGLException("You must first compile a shader program before you can bind it");
            }
        }

        public void Unbind()
        {
            GL.UseProgram(0);
            REngine.CheckGLError();
        }


        public void Dispose()
        {
            if (GL.IsProgram(Id))
            {
                GL.DeleteProgram(Id);
                Id = -1;
            }
        }

        internal void BindSemantics(Matrix Model, Matrix View, Matrix Projection)
        {
            foreach(var keyPair in _semantics)
            {
                switch(keyPair.Key)
                {
                    case RShaderSemanticDefinition.VIEW:
                        SetUniformBySemantic(RShaderSemanticDefinition.VIEW, View);
                        break;
                    case RShaderSemanticDefinition.PROJECTION:
                        SetUniformBySemantic(RShaderSemanticDefinition.PROJECTION, Projection);
                        break;
                    case RShaderSemanticDefinition.VIEWPROJECTION:
                        SetUniformBySemantic(RShaderSemanticDefinition.VIEWPROJECTION, View * Projection);
                        break;
                    case RShaderSemanticDefinition.MODEL:
                        SetUniformBySemantic(RShaderSemanticDefinition.MODEL, Model);
                        break;
                    case RShaderSemanticDefinition.WORLD:
                        SetUniformBySemantic(RShaderSemanticDefinition.WORLD, Model);
                        break;
                    case RShaderSemanticDefinition.MODELVIEW:
                        SetUniformBySemantic(RShaderSemanticDefinition.MODELVIEW, Model * View);
                        break;
                    case RShaderSemanticDefinition.MODELVIEWPROJECTION:
                        SetUniformBySemantic(RShaderSemanticDefinition.MODELVIEWPROJECTION, Model * View * Projection);
                        break;
                    case RShaderSemanticDefinition.INVERSE_VIEW:
                        SetUniformBySemantic(RShaderSemanticDefinition.INVERSE_VIEW, Matrix.Invert(View));
                        break;
                    case RShaderSemanticDefinition.INVERSE_PROJECTION:
                        SetUniformBySemantic(RShaderSemanticDefinition.INVERSE_PROJECTION, Matrix.Invert(Projection));
                        break;
                    case RShaderSemanticDefinition.FAR_PLANE:
                        SetUniformBySemantic(RShaderSemanticDefinition.FAR_PLANE, REngine.camera.Far);
                        break;
                    case RShaderSemanticDefinition.NEAR_PLANE:
                        SetUniformBySemantic(RShaderSemanticDefinition.NEAR_PLANE, REngine.camera.Near);
                        break;
                }
            }
        }

        internal static RShader basicShader;
        public static RShader GetBasicShader()
        {
            return basicShader;
        }
        internal static void InitShaders()
        {
            basicShader = new RShader();
            basicShader.Load(RShaderResources.BasicEffectVert, RShaderResources.BasicEffectFrag, null);
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
        internal static string Lighting = GetResourceString("Reactor.Shaders.lighting.glsl");
        internal static string Noise = GetResourceString("Reactor.Shaders.noise.glsl");

        internal static string BasicEffectVert = GetResourceString("Reactor.Shaders.basicEffect.vert.glsl");
        internal static string BasicEffectFrag = GetResourceString("Reactor.Shaders.basicEffect.frag.glsl");

        internal static string Basic2dEffectVert = GetResourceString("Reactor.Shaders.basic2dEffect.vert.glsl");
        internal static string Basic2dEffectFrag = GetResourceString("Reactor.Shaders.basic2dEffect.frag.glsl");
    }
}