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
