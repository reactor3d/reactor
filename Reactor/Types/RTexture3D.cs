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
using Reactor.Platform.OpenGL;

namespace Reactor.Types
{
    public class RTexture3D : RTexture
    {
        private static uint[] uint1 = new uint[] { 0 };
        public void Create(RPixelFormat format, ref RTexture2D posX, ref RTexture2D posY, ref RTexture2D posZ, ref RTexture2D negX, ref RTexture2D negY, ref RTexture2D negZ)
        {
            PixelInternalFormat inf = PixelInternalFormat.Rgba;
            PixelFormat pf = PixelFormat.Rgba;
            if(format == RPixelFormat.Bgr)
            {
                inf = PixelInternalFormat.Rgb;
                pf = PixelFormat.Bgr;
            }
            if (format == RPixelFormat.Rgb)
            {
                inf = PixelInternalFormat.Rgb;
                pf = PixelFormat.Rgb;
            }
            if (format == RPixelFormat.Bgra)
            {
                inf = PixelInternalFormat.Rgba;
                pf = PixelFormat.Bgra;
            }
            if (format == RPixelFormat.Rgba)
            {
                inf = PixelInternalFormat.Rgba;
                pf = PixelFormat.Rgba;
            }
            GL.ActiveTexture(TextureUnit.Texture0);
            var posXcolors = posX.GetData<RColor>();
            var posYcolors = posY.GetData<RColor>();
            var posZcolors = posZ.GetData<RColor>();
            var negXcolors = negX.GetData<RColor>();
            var negYcolors = negY.GetData<RColor>();
            var negZcolors = negZ.GetData<RColor>();

            Bounds = posX.Bounds;
            GL.GenTextures(1, uint1);
            Id = uint1[0];
            textureTarget = TextureTarget.TextureCubeMap;
            GL.BindTexture(textureTarget, Id);
            SetTextureMagFilter(RTextureMagFilter.Nearest);
            SetTextureMinFilter(RTextureMinFilter.Nearest);
            SetTextureWrapMode(RTextureWrapMode.ClampToBorder, RTextureWrapMode.ClampToBorder);
            unsafe
            {
                fixed (RColor* ptr = &posXcolors[0])
                {
                    var p = new IntPtr(ptr);
                    GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX, 0, inf, Bounds.Width, Bounds.Height, 0, pf, PixelType.UnsignedByte, p);
                }
                fixed (RColor* ptr = &posYcolors[0])
                {
                    var p = new IntPtr(ptr);
                    GL.TexImage2D(TextureTarget.TextureCubeMapPositiveY, 0, inf, Bounds.Width, Bounds.Height, 0, pf, PixelType.UnsignedByte, p);
                }
                fixed (RColor* ptr = &posZcolors[0])
                {
                    var p = new IntPtr(ptr);
                    GL.TexImage2D(TextureTarget.TextureCubeMapPositiveZ, 0, inf, Bounds.Width, Bounds.Height, 0, pf, PixelType.UnsignedByte, p);
                }
                fixed (RColor* ptr = &negXcolors[0])
                {
                    var p = new IntPtr(ptr);
                    GL.TexImage2D(TextureTarget.TextureCubeMapNegativeX, 0, inf, Bounds.Width, Bounds.Height, 0, pf, PixelType.UnsignedByte, p);
                }
                fixed (RColor* ptr = &negYcolors[0])
                {
                    var p = new IntPtr(ptr);
                    GL.TexImage2D(TextureTarget.TextureCubeMapNegativeY, 0, inf, Bounds.Width, Bounds.Height, 0, pf, PixelType.UnsignedByte, p);
                }
                fixed (RColor* ptr = &negZcolors[0])
                {
                    var p = new IntPtr(ptr);
                    GL.TexImage2D(TextureTarget.TextureCubeMapNegativeZ, 0, inf, Bounds.Width, Bounds.Height, 0, pf, PixelType.UnsignedByte, p);
                }
            }
        }
    }
}
