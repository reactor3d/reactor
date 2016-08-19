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
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Reactor.Types
{
    public class RShaderEffect : IDisposable
    {
        public int Id { get; internal set; }
        public string EffectSource;
        public RShaderEffectType Type;
        internal RShaderSemantics Semantics;

        public RShaderEffect(string source, int type, string[] defines)
        {
            Type = (RShaderEffectType)type;

                StringBuilder defineSource = new StringBuilder();
                defineSource.Append("#version 330\r\n");

                if(defines != null)
                    foreach(string define in defines){
                        defineSource.AppendFormat("#{0};\r\n", define);
                    }
                                                      //regex for finding the file referenced inside double-quotes...
            var parsedSource = Regex.Replace(source, @"#include ""(.*?)""", delegate(Match match){
                if(match.Success)
                {
                    string fileInclude = match.Groups[1].Value;
                    if(fileInclude.ToLower().Equals("headers.glsl"))
                        return RShaderResources.Headers;
                    if(fileInclude.ToLower().Equals("lighting.glsl"))
                        return RShaderResources.Lighting;
                    StreamReader reader = new StreamReader(RFileSystem.Instance.GetFile(match.ToString()));
                    string inc = reader.ReadToEnd();
                    return inc;
                }
                return "";
            });
                //source = source.Replace("#include \"headers.glsl\"", RShaderResources.Headers);
                //source = source.Replace("#include \"lighting.glsl\"", RShaderResources.Lighting);
            //if(Type == RShaderEffectType.VERTEX)
                //defineSource.Append(RShaderResources.Headers);
            
            EffectSource = defineSource.ToString() + parsedSource;
            Semantics = new RShaderSemantics(ref EffectSource);
            //RLog.Info(EffectSource);
            switch (type)
            {
                case ((int)RShaderEffectType.GEOMETRY):
                    Id = GL.CreateShader(ShaderType.GeometryShader);
                    break;
                case ((int)RShaderEffectType.FRAGMENT):
                    Id = GL.CreateShader(ShaderType.FragmentShader);
                    break;
                case ((int)RShaderEffectType.VERTEX):
                    Id = GL.CreateShader(ShaderType.VertexShader);
                    break;
                case ((int)RShaderEffectType.TESS_CONTROL):
                    Id = GL.CreateShader(ShaderType.TessControlShader);
                    break;
                case ((int)RShaderEffectType.TESS_EVAL):
                    Id = GL.CreateShader(ShaderType.TessEvaluationShader);
                    break;
                case ((int)RShaderEffectType.COMPUTE):
                    Id = GL.CreateShader(ShaderType.ComputeShader);
                    break;
                default:
                    Id = GL.CreateShader(ShaderType.ComputeShader);
                    break;


            }
            GL.ShaderSource(Id, EffectSource);
            REngine.CheckGLError();
            GL.CompileShader(Id);
            REngine.CheckGLError();
            int compile_status;
            GL.GetShader(Id, ShaderParameter.CompileStatus, out compile_status);
            if (compile_status == (int)All.False)
            {
                var log = GL.GetShaderInfoLog(Id);
                RLog.Error(log);
                REngine.CheckGLError();
                if (GL.IsShader(Id))
                {
                    GL.DeleteShader(Id);
                    REngine.CheckGLError();
                }
                Id = -1;

                throw new InvalidOperationException("Shader Compilation Failed");
            }
        }

        ~RShaderEffect()
        {
            
            
        }

        #region IDisposable implementation

        public void Dispose()
        {
            try
            {
                if (GL.IsShader(Id))
                {
                    GL.DeleteShader(Id);
                    REngine.CheckGLError();
                }
            }
            catch { }
        }

        #endregion
    }
}
