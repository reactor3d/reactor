using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reactor.Types
{
    public class RTexture2D : RTexture
    {
        public void Create(int width, int height, RSurfaceFormat format, bool multisample = false)
        {
            GL.GenTextures(1, out Id);
            if (multisample){
                GL.BindTexture(TextureTarget.Texture2DMultisample, Id);
                REngine.CheckGLError();
                CreateProperties(TextureTarget.Texture2DMultisample, true);
                REngine.CheckGLError();
            }
            else
            {
                GL.BindTexture(TextureTarget.Texture2D, Id);
                REngine.CheckGLError();
                CreateProperties(TextureTarget.Texture2D, true);
                REngine.CheckGLError();
            }
            
        }
    }
}
