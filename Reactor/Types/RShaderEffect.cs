using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reactor.Types
{
    public class RShaderEffect : IDisposable
    {
        public int Id { get; internal set; }
        public string EffectSource;
        public RShaderEffectType Type;

        public RShaderEffect(string source, int type, string[] defines)
        {
            Type = (RShaderEffectType)type;

                StringBuilder defineSource = new StringBuilder();
                defineSource.Append("#version 330\r\n");

                if(defines != null)
                    foreach(string define in defines){
                        defineSource.AppendFormat("#{0};\r\n", define);
                    }
            //if(Type == RShaderEffectType.VERTEX)
                //defineSource.Append(RShaderResources.Headers);
            EffectSource = defineSource.ToString() + source;

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
            REngine.CheckGLError();
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
            if (GL.IsShader(Id))
            {
                GL.DeleteShader(Id);
                REngine.CheckGLError();
            }
        }

        #region IDisposable implementation

        public void Dispose()
        {

        }

        #endregion
    }
}
