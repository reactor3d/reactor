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

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Reactor.Geometry;
using Reactor.Math;
using Reactor.Platform.OpenGL;
using Reactor.Utilities;

namespace Reactor.Types
{
    public class RShader : IDisposable
    {
        private const string r_position = "r_Position";
        private const string r_color = "r_Color";
        private const string r_normal = "r_Normal";
        private const string r_bitangent = "r_Bitangent";
        private const string r_tangent = "r_Tangent";
        private const string r_texcoord = "r_TexCoord";
        private const string r_blendindices = "r_BlendIndices";
        private const string r_blendweight = "r_BlendWeight";
        private const string r_tessellatefactor = "r_TessellateFactor";

        internal static RShader basicShader;
        private readonly Dictionary<string, int> _attributeLocations = new Dictionary<string, int>();

        private readonly Dictionary<RShaderSemanticDefinition, RShaderSemantic> _semantics =
            new Dictionary<RShaderSemanticDefinition, RShaderSemantic>();

        private readonly Dictionary<string, int> _uniformLocations = new Dictionary<string, int>();
        private Attribute[] _attributes;
        private Uniform[] _uniforms;
        private readonly RShaderEffect[] effects = new RShaderEffect[6];
        public uint Id { get; internal set; }


        public void Dispose()
        {
            if (GL.IsProgram(Id))
            {
                GL.DeleteProgram(Id);
                Id = 0;
            }
        }

        public void Load(string vertSource, string fragSource)
        {
            Load(vertSource, fragSource, null, null);
        }

        public void Load(string vertSource, string fragSource, string geomSource)
        {
            Load(vertSource, fragSource, geomSource, null);
        }

        public void Load(string vertSource, string fragSource, string geomSource, string[] defines)
        {
            effects[(int)RShaderEffectType.VERTEX] =
                new RShaderEffect(vertSource, (int)RShaderEffectType.VERTEX, defines);
            effects[(int)RShaderEffectType.FRAGMENT] =
                new RShaderEffect(fragSource, (int)RShaderEffectType.FRAGMENT, defines);
            if (geomSource != null)
                effects[(int)RShaderEffectType.GEOMETRY] =
                    new RShaderEffect(geomSource, (int)RShaderEffectType.GEOMETRY, defines);
            Id = GL.CreateProgram();
            foreach (var effect in effects)
                if (effect != null)
                    GL.AttachShader(Id, effect.Id);

            GL.LinkProgram(Id);
            int[] buff = { 0 };
            GL.GetProgramiv(Id, ProgramParameter.LinkStatus, Allocator.Int32_1);
            if (Allocator.Int32_1[0] == 0)
            {
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


            //int status;
            //GL.ValidateProgram(Id);
            //GL.GetProgram(Id, GetProgramParameterName.ValidateStatus, out status);
            //if (status != 1) throw new Exception(GL.GetProgramInfoLog(Id));

            REngine.CheckGLError();
            GL.GetProgramiv(Id, ProgramParameter.ActiveAttributes, Allocator.Int32_1);
            REngine.CheckGLError();
            _attributes = new Attribute[Allocator.Int32_1[0]];
            for (var i = 0; i < Allocator.Int32_1[0]; i++)
            {
                int[] size = { 0 };
                ActiveAttribType[] type = { ActiveAttribType.Float };
                int[] length = { 0 };
                var name = new StringBuilder(64);
                GL.GetActiveAttrib(Id, i, 64, length, size, type, name);
                var attrib = new Attribute();
                attrib.name = name.ToString();
                attrib.index = i;
                attrib.location = GL.GetAttribLocation(Id, attrib.name);
                attrib.type = type[0];
                _attributes[i] = attrib;
                _attributeLocations.Add(attrib.name, attrib.location);
            }


            GL.GetProgramiv(Id, ProgramParameter.ActiveUniforms, Allocator.Int32_1);
            _uniforms = new Uniform[Allocator.Int32_1[0]];
            for (var i = 0; i < Allocator.Int32_1[0]; i++)
            {
                int[] size = { 0 };
                ActiveUniformType[] type = { ActiveUniformType.Float };
                int[] length = { 0 };
                var name = new StringBuilder(64);
                GL.GetActiveUniform(Id, i, 64, length, size, type, name);
                var uni = new Uniform();
                uni.index = i;
                uni.name = name.ToString();
                uni.location = GL.GetUniformLocation(Id, uni.name);
                uni.type = type[0];
                _uniforms[i] = uni;
                _uniformLocations.Add(uni.name, uni.location);
            }

            foreach (var effect in effects)
                if (effect != null)
                {
                    if (effect.Semantics != null)
                        foreach (var keyPair in effect.Semantics)
                            _semantics.Add(keyPair.Key, keyPair.Value);

                    effect.Dispose();
                }

            REngine.CheckGLError();
        }

        public void SetUniformValue(string name, bool value)
        {
            if (value)
                GL.Uniform1i(GetUniformLocation(name), 1);
            else
                GL.Uniform1i(GetUniformLocation(name), 0);
            REngine.CheckGLError();
        }

        public void SetUniformValue(string name, int value)
        {
            GL.Uniform1i(GetUniformLocation(name), value);
            REngine.CheckGLError();
        }

        public void SetUniformValue(string name, double value)
        {
            GL.Uniform1f(GetUniformLocation(name), (float)value);
            REngine.CheckGLError();
        }

        public void SetUniformValue(string name, float value)
        {
            GL.Uniform1f(GetUniformLocation(name), value);
            REngine.CheckGLError();
        }

        public void SetUniformValue(string name, Vector2 value)
        {
            GL.Uniform2f(GetUniformLocation(name), value.X, value.Y);
            REngine.CheckGLError();
        }

        public void SetUniformValue(string name, Vector3 value)
        {
            GL.Uniform3f(GetUniformLocation(name), value.X, value.Y, value.Z);
            REngine.CheckGLError();
        }

        public void SetUniformValue(string name, Vector4 value)
        {
            GL.Uniform4f(GetUniformLocation(name), value.X, value.Y, value.Z, value.W);
            REngine.CheckGLError();
        }

        public void SetUniformValue(string name, RColor value)
        {
            SetUniformValue(name, value.ToVector4());
        }

        public void SetUniformValue(string name, Matrix value)
        {
            var v = Matrix.ToFloatArray(value);
            GL.UniformMatrix4fv(GetUniformLocation(name), 1, false, v);
            REngine.CheckGLError();
        }

        public void SetSamplerValue(RTextureLayer layer, RTexture texture)
        {
            texture.SetActive(layer);
            texture.Bind();
            var loc = GetTexUniformLocation(layer);
            var sampler = (int)layer - (int)RTextureLayer.TEXTURE0;
            GL.Uniform1i(loc, sampler);
            REngine.CheckGLError();
        }

        public int GetUniformBySemantic(RShaderSemanticDefinition semantic)
        {
            return GetUniformLocation(_semantics[semantic].name);
        }

        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, bool value)
        {
            if (_semantics.ContainsKey(semantic))
                if (_semantics[semantic].type == "bool")
                    SetUniformValue(_semantics[semantic].name, value);
        }

        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, int value)
        {
            if (_semantics.ContainsKey(semantic))
                if (_semantics[semantic].type == "int")
                    SetUniformValue(_semantics[semantic].name, value);
        }

        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, double value)
        {
            if (_semantics.ContainsKey(semantic))
                if (_semantics[semantic].type == "double")
                    SetUniformValue(_semantics[semantic].name, value);
        }

        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, float value)
        {
            if (_semantics.ContainsKey(semantic))
                if (_semantics[semantic].type == "float")
                    SetUniformValue(_semantics[semantic].name, value);
        }

        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, Vector2 value)
        {
            if (_semantics.ContainsKey(semantic))
                if (_semantics[semantic].type == "vec2")
                    SetUniformValue(_semantics[semantic].name, value);
        }

        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, Vector3 value)
        {
            if (_semantics.ContainsKey(semantic))
                if (_semantics[semantic].type == "vec3")
                    SetUniformValue(_semantics[semantic].name, value);
        }

        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, Vector4 value)
        {
            if (_semantics.ContainsKey(semantic))
                if (_semantics[semantic].type == "vec4")
                    SetUniformValue(_semantics[semantic].name, value);
        }

        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, Matrix value)
        {
            if (_semantics.ContainsKey(semantic))
                if (_semantics[semantic].type == "mat4")
                    SetUniformValue(_semantics[semantic].name, value);
        }

        public void SetUniformBySemantic(RShaderSemanticDefinition semantic, RColor value)
        {
            if (_semantics.ContainsKey(semantic))
                if (_semantics[semantic].type == "vec4")
                    SetUniformValue(_semantics[semantic].name, value);
        }

        internal int GetTexUniformLocation(RTextureLayer layer)
        {
            var v = (int)layer - (int)RTextureLayer.TEXTURE0;
            var name = $"texture{v}";
            return GetUniformLocation(name);
        }

        internal int GetUniformLocation(string name)
        {
            if (_uniformLocations.ContainsKey(name)) return _uniformLocations[name];

            var location = GL.GetUniformLocation(Id, name);
            REngine.CheckGLError();
            if (location != -1)
            {
                _uniformLocations.Add(name, location);
                return location;
            }

            return -1;
        }

        internal int GetAttribLocation(string name)
        {
            for (var i = 0; i < _attributes.Length; i++)
                if (_attributes[i].name == name)
                    return _attributes[i].location;
            return GL.GetAttribLocation(Id, name);
        }

        internal int GetAttribLocation(RVertexElementUsage rVertexElementUsage)
        {
            var name = "";
            switch (rVertexElementUsage)
            {
                case RVertexElementUsage.Position:
                    name = r_position;
                    break;
                case RVertexElementUsage.Color:
                    name = r_color;
                    break;
                case RVertexElementUsage.Normal:
                    name = r_normal;
                    break;
                case RVertexElementUsage.Bitangent:
                    name = r_bitangent;
                    break;
                case RVertexElementUsage.Tangent:
                    name = r_tangent;
                    break;
                case RVertexElementUsage.TextureCoordinate:
                    name = r_texcoord;
                    break;
                case RVertexElementUsage.BlendIndices:
                    name = r_blendindices;
                    break;
                case RVertexElementUsage.BlendWeight:
                    name = r_blendweight;
                    break;
                case RVertexElementUsage.TessellateFactor:
                    name = r_tessellatefactor;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return GetAttribLocation(name);
        }

        internal int GetAttribLocation(RVertexElementUsage rVertexElementUsage, int usageIndex)
        {
            var size = 0;
            var type = ActiveAttribType.Float;
            var name = new StringBuilder();
            GL.GetActiveAttrib(Id, usageIndex, size, new[] { 1 }, new[] { 1 }, new[] { type }, name);
            return GL.GetAttribLocation(Id, name.ToString());
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

        internal void BindSemantics(Matrix Model, Matrix View, Matrix Projection)
        {
            foreach (var keyPair in _semantics)
                switch (keyPair.Key)
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

        public static RShader GetBasicShader()
        {
            return basicShader;
        }

        internal static void InitShaders()
        {
            basicShader = new RShader();
            basicShader.Load(RShaderResources.BasicEffectVert, RShaderResources.BasicEffectFrag, null);
        }

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
    }

    internal static class RShaderResources
    {
        internal static Assembly Assembly = Assembly.GetAssembly(typeof(RShaderResources));
        internal static string Headers = GetResourceString("Reactor.Shaders.headers.glsl");
        internal static string Lighting = GetResourceString("Reactor.Shaders.lighting.glsl");
        internal static string Noise = GetResourceString("Reactor.Shaders.noise.glsl");

        internal static string BasicEffectVert = GetResourceString("Reactor.Shaders.basicEffect.vert.glsl");
        internal static string BasicEffectFrag = GetResourceString("Reactor.Shaders.basicEffect.frag.glsl");

        internal static string Basic2dEffectVert = GetResourceString("Reactor.Shaders.basic2dEffect.vert.glsl");
        internal static string Basic2dEffectFrag = GetResourceString("Reactor.Shaders.basic2dEffect.frag.glsl");

        internal static string HDRVert = GetResourceString("Reactor.Shaders.hdr.vert.glsl");
        internal static string HDRFrag = GetResourceString("Reactor.Shaders.hdr.frag.glsl");

        internal static string GetResourceString(string resource)
        {
            var reader = new StreamReader(Assembly.GetManifestResourceStream(resource));
            var contents = reader.ReadToEnd();
            reader.Close();
            return contents;
        }
    }
}