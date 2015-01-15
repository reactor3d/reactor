//
// RTextureGlyph.cs
//
// Author:
//       Gabriel Reiser <gabriel@reisergames.com>
//
// Copyright (c) 2015 2014
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
using SD = System.Drawing;
using Reactor.Math;
using SharpFont;
using OpenTK.Graphics.OpenGL;

namespace Reactor.Types
{
    internal class RTextureGlyph : RTexture
    {

        char keyCode;
        float bearingX;
        float bearingY;
        int bitmapLeft;
        int bitmapTop;
        Vector2 advance;
        public RTextureGlyph(GlyphSlot glyph, char c)
        {
            bearingX = (float)glyph.Metrics.HorizontalBearingX / 72;
            bearingY = (float)glyph.Metrics.HorizontalBearingY / 72;
            bitmapLeft = glyph.BitmapLeft;
            bitmapTop = glyph.BitmapTop;
            advance = new Vector2((float)glyph.Advance.X / 72, (float)glyph.Advance.Y / 72);
            Bounds = new Rectangle(0,0,glyph.Bitmap.Width, glyph.Bitmap.Rows);

            GL.GenTextures(1, out Id);
            GL.BindTexture(TextureTarget.Texture2D, Id);
            REngine.CheckGLError();
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            REngine.CheckGLError();
            GL.TexImage2D(TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgb8,
                Bounds.Width,
                Bounds.Height,
                0,
                PixelFormat.Red,
                PixelType.UnsignedByte,
                glyph.Bitmap.Buffer);
            GL.PixelStore(PixelStoreParameter.PackAlignment, 1);
            REngine.CheckGLError();
            GL.TexParameter( TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) RTextureMagFilter.Linear );
            REngine.CheckGLError();

            keyCode = c;

        }
            
    }
}

