using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reactor.Types
{
    public class RTexture2D : RTexture
    {
        public RTexture2D():base()
        {

        }
        internal RTexture2D(bool defaultWhite):base()
        {

                Create(1, 1, RPixelFormat.Rgba);
                SetData<RColor>(new RColor[] { new RColor(1f, 1f, 1f, 1f) }, RPixelFormat.Rgba, 0, 0, 1, 1, true);
                REngine.CheckGLError();

        }
        public void Create(int width, int height, RPixelFormat format, bool multisample = false)
        {

            GL.GenTextures(1, out Id);
            if (multisample){
                textureTarget = TextureTarget.Texture2DMultisample;
                GL.BindTexture(TextureTarget.Texture2DMultisample, Id);
                GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, 4, PixelInternalFormat.Rgba, width, height, true);
                REngine.CheckGLError();
                CreateProperties(TextureTarget.Texture2DMultisample, true);
                REngine.CheckGLError();
            }
            else
            {
                textureTarget = TextureTarget.Texture2D;
                GL.BindTexture(TextureTarget.Texture2D, Id);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, (PixelFormat)format, PixelType.UnsignedByte, IntPtr.Zero);
                REngine.CheckGLError();
                CreateProperties(TextureTarget.Texture2D, false);
                REngine.CheckGLError();
            }

        }
    }
}
