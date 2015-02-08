using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reactor.Types
{
    public class RTexture2D : RTexture
    {
        public void Create(int width, int height, RPixelFormat format, bool multisample = false)
        {

            GL.GenTextures(1, out Id);
            if (multisample){
                textureTarget = TextureTarget.Texture2DMultisample;
                GL.BindTexture(TextureTarget.Texture2DMultisample, Id);
                GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, 4, PixelInternalFormat.Alpha, width, height, true);
                REngine.CheckGLError();
                CreateProperties(TextureTarget.Texture2DMultisample, true);
                REngine.CheckGLError();
            }
            else
            {
                textureTarget = TextureTarget.Texture2D;
                GL.BindTexture(TextureTarget.Texture2D, Id);
                REngine.CheckGLError();
                CreateProperties(TextureTarget.Texture2D, false);
                REngine.CheckGLError();
            }

        }
    }
}
