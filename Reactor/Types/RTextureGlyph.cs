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
    internal class RTextureGlyph : RTextureSprite
    {

        internal char keyCode;
        internal float bearingX;
        internal float bearingY;
        internal int bitmapLeft;
        internal int bitmapTop;
        internal Vector2 advance;
        internal GlyphSlot glyph;

        public RTextureGlyph(GlyphSlot glyph, char c) : base()
        {
            this.glyph = glyph;
            bearingX = (float)glyph.Metrics.HorizontalBearingX;
            bearingY = (float)glyph.Metrics.HorizontalBearingY;
            bitmapLeft = glyph.BitmapLeft;
            bitmapTop = glyph.BitmapTop;
            advance = new Vector2((float)(glyph.Advance.X >> 6), (float)(glyph.Advance.Y >> 6));
            Bounds = new Rectangle(0,0,glyph.Bitmap.Width, glyph.Bitmap.Rows);
            textureTarget = TextureTarget.Texture2D;
            pixelFormat = RPixelFormat.Alpha;
            PixelInternalFormat internalFormat = PixelInternalFormat.Alpha;
            switch(glyph.Bitmap.PixelMode)
            {
                case PixelMode.Bgra:
                    pixelFormat = RPixelFormat.Bgra;
                    internalFormat = PixelInternalFormat.Rgb;
                    break;
                case PixelMode.Gray:
                    pixelFormat = RPixelFormat.Red;
                    internalFormat = PixelInternalFormat.R8;
                    break;
                case PixelMode.Lcd:
                    pixelFormat = RPixelFormat.Rgb;
                    internalFormat = PixelInternalFormat.Rgb8;
                    break;
                default:
                    pixelFormat = RPixelFormat.Red;
                    internalFormat = PixelInternalFormat.R8;
                    break;
            }
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.GenTextures(1, out Id);
            GL.BindTexture(TextureTarget.Texture2D, Id);
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            REngine.CheckGLError();
            GL.TexImage2D<byte>(textureTarget, 0,internalFormat, glyph.Bitmap.Width, glyph.Bitmap.Rows, 0, (PixelFormat)pixelFormat, PixelType.UnsignedByte, glyph.Bitmap.BufferData);
            REngine.CheckGLError();
            GL.PixelStore(PixelStoreParameter.PackAlignment, 1);
            REngine.CheckGLError();
            GL.TexParameter(textureTarget, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(textureTarget, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            REngine.CheckGLError();
            GL.TexParameter(textureTarget, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(textureTarget, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            REngine.CheckGLError();
            //GL.TexEnv( TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int) TextureLoaderParameters.EnvMode );
            REngine.CheckGLError();
            GL.BindTexture(textureTarget, 0);
            REngine.CheckGLError();

            keyCode = c;

        }

        public RTextureGlyph(Rectangle bounds, char c)
        {
            this.Bounds = bounds;
            this.ScaledBounds = bounds;
            this.keyCode = c;
            this.Offset = Vector2.Zero;


        }

        public Vector2 GetSize(char c)
        {
            return new Vector2(Bounds.Width, Bounds.Height);
        }

        
            
    }
}

