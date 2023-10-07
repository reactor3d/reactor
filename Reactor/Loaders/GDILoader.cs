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
using System.IO;
using Reactor.Math;
using Reactor.Platform.OpenGL;
using Reactor.Types;
using Reactor.Utilities;
using SD = System.Drawing;
using SI = System.Drawing.Imaging;

#region --- License ---

/* Licensed under the MIT/X11 license.
 * Copyright (c) 2006-2008 the OpenTK Team.
 * This notice may not be removed from any source distribution.
 * See license.txt for licensing details.
 */

#endregion

// TODO: Find paint program that can properly export 8/16-bit Textures and make sure they are loaded correctly.

namespace Reactor
{
    internal static class ImageGDI
    {
        public static void LoadFromData(byte[] data, out uint texturehandle, out TextureTarget dimension,
            out RPixelFormat format, out PixelType type, out Rectangle bounds)
        {
            dimension = TextureTarget.Texture2D;
            texturehandle = TextureLoaderParameters.OpenGLDefaultTexture;
            var stream = new MemoryStream(data);
            var b = new SD.Bitmap(stream);
            GL.GenTextures(1, Allocator.UInt32_1);
            texturehandle = Allocator.UInt32_1[0];
            GL.BindTexture(dimension, texturehandle);
            var width = b.Width;
            var height = b.Height;
            var d = new RColor[width * height];
            var i = 0;
            for (var y = 0; y < b.Height; y++)
            for (var x = 0; x < b.Width; x++)
            {
                var color = b.GetPixel(x, y);
                d[i] = new RColor(color.R, color.G, color.B, color.A);
                i++;
            }

            bounds = new Rectangle(0, 0, b.Width, b.Height);
            format = RPixelFormat.Rgba;
            type = PixelType.UnsignedByte;
            REngine.CheckGLError();
            unsafe
            {
                fixed (RColor* ptr = &d[0])
                {
                    var p = new IntPtr(ptr);
                    GL.TexImage2D(dimension, 0, PixelInternalFormat.Rgba, width, height, 0, (PixelFormat)format, type,
                        p);
                }
            }

            REngine.CheckGLError();
            GL.BindTexture(TextureTarget.Texture2D, 0);
            //LoadFromBitmap(ref b, out texturehandle, out dimension, out format, out type, out bounds);
        }

        public static void LoadFromDisk(string filename, out uint texturehandle, out TextureTarget dimension,
            out RPixelFormat format, out PixelType type, out Rectangle bounds)
        {
            dimension = 0;
            texturehandle = TextureLoaderParameters.OpenGLDefaultTexture;
            var b = new SD.Bitmap(filename);
            LoadFromBitmap(ref b, out texturehandle, out dimension, out format, out type, out bounds);
        }

        public static void LoadFromBitmap(ref SD.Bitmap bitmap, out uint texturehandle, out TextureTarget dimension,
            out RPixelFormat format, out PixelType type, out Rectangle bounds)
        {
            dimension = 0;
            texturehandle = TextureLoaderParameters.OpenGLDefaultTexture;
            var GLError = ErrorCode.NoError;

            SD.Bitmap CurrentBitmap = null;

            try // Exceptions will be thrown if any Problem occurs while working on the file. 
            {
                CurrentBitmap = bitmap;
                if (TextureLoaderParameters.FlipImages)
                    CurrentBitmap.RotateFlip(SD.RotateFlipType.RotateNoneFlipY);

                if (CurrentBitmap.Height > 1)
                    dimension = TextureTarget.Texture2D;
                else
                    dimension = TextureTarget.Texture1D;

                GL.GenTextures(1, Allocator.UInt32_1);
                texturehandle = Allocator.UInt32_1[0];
                GL.BindTexture(dimension, texturehandle);

                #region Load Texture

                PixelInternalFormat pif;
                PixelFormat pf;
                PixelType pt;
                switch (CurrentBitmap.PixelFormat)
                {
                    case SI.PixelFormat.Format8bppIndexed: // misses glColorTable setup
                        pif = PixelInternalFormat.R8;
                        pf = PixelFormat.Red;
                        pt = PixelType.UnsignedByte;
                        break;
                    case SI.PixelFormat.Format16bppArgb1555:
                    case SI.PixelFormat.Format16bppRgb555: // does not work
                        pif = PixelInternalFormat.Rgb5A1;
                        pf = PixelFormat.Rgb;
                        pt = PixelType.UnsignedShort5551Ext;
                        break;
                    /*  case System.Drawing.Imaging.PixelFormat.Format16bppRgb565:
                pif = Reactor.Graphics.OpenGL.PixelInternalFormat.R5G6B5IccSgix;
                pf = Reactor.Graphics.OpenGL.PixelFormat.R5G6B5IccSgix;
                pt = Reactor.Graphics.OpenGL.PixelType.UnsignedByte;
                break;
*/
                    case SI.PixelFormat.Format24bppRgb:
                        pif = PixelInternalFormat.Rgba;
                        pf = PixelFormat.Rgba;
                        pt = PixelType.UnsignedByte;
                        break;
                    case SI.PixelFormat.Format32bppRgb:
                        pif = PixelInternalFormat.Rgba;
                        pf = PixelFormat.Rgba;
                        pt = PixelType.UnsignedByte;
                        break;
                    case SI.PixelFormat.Canonical:
                    case SI.PixelFormat.Format32bppArgb:
                        pif = PixelInternalFormat.Rgba;
                        pf = PixelFormat.Rgba;
                        pt = PixelType.UnsignedByte;
                        break;
                    default:
                        throw new ArgumentException("ERROR: Unsupported Pixel Format " + CurrentBitmap.PixelFormat);
                }

                format = (RPixelFormat)pf;
                type = pt;
                var width = CurrentBitmap.Width;
                var data = new RColor[CurrentBitmap.Width * CurrentBitmap.Height];
                for (var y = 0; y < CurrentBitmap.Height; ++y)
                for (var x = 0; x < CurrentBitmap.Width; ++x)
                {
                    var color = CurrentBitmap.GetPixel(x, y);
                    data[x + width * y] = new RColor(color.R, color.G, color.B, color.A);
                }

                //SI.BitmapData Data = CurrentBitmap.LockBits( new System.Drawing.Rectangle( 0, 0, CurrentBitmap.Width, CurrentBitmap.Height ), SI.ImageLockMode.ReadOnly, CurrentBitmap.PixelFormat );

                if (CurrentBitmap.Height > 1)
                    // image is 2D
                    unsafe
                    {
                        fixed (RColor* ptr = &data[0])
                        {
                            var p = new IntPtr(ptr);
                            GL.TexImage2D(dimension, 0, pif, CurrentBitmap.Width, CurrentBitmap.Height, 0, pf, pt, p);
                        }
                    }
                else
                    // image is 1D
                    unsafe
                    {
                        fixed (RColor* ptr = &data[0])
                        {
                            var p = new IntPtr(ptr);
                            GL.TexImage1D(dimension, 0, pif, CurrentBitmap.Width, TextureLoaderParameters.Border, pf,
                                pt, p);
                        }
                    }

                //GL.Finish( );
                REngine.CheckGLError();
                GLError = GL.GetError();
                if (GLError != ErrorCode.NoError)
                    throw new ArgumentException("Error building TexImage. GL Error: " + GLError);

                //CurrentBitmap.UnlockBits( Data );

                #endregion Load Texture

                Setup(dimension);

                bounds = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            }
            catch (Exception e)
            {
                dimension = 0;
                texturehandle = TextureLoaderParameters.OpenGLDefaultTexture;
                throw new ArgumentException("Texture Loading Error: Failed to read data.\n" + e);
                // return; // failure
            }
            finally
            {
                CurrentBitmap = null;
            }
        }

        private static void Setup(TextureTarget target)
        {
            #region Set Texture Parameters

            GL.TexParameteri(target, TextureParameterName.TextureMinFilter,
                (int)TextureLoaderParameters.MinificationFilter);
            GL.TexParameteri(target, TextureParameterName.TextureMagFilter,
                (int)TextureLoaderParameters.MagnificationFilter);
            REngine.CheckGLError();
            GL.TexParameteri(target, TextureParameterName.TextureWrapS, (int)TextureLoaderParameters.WrapModeS);
            GL.TexParameteri(target, TextureParameterName.TextureWrapT, (int)TextureLoaderParameters.WrapModeT);

            //float maxAniso=GL.GetFloat(GetPName.MaxTextureMaxAnisotropyExt);
            //GL.TexParameterf(target, TextureParameterName.MaxAnisotropyExt, maxAniso);
            REngine.CheckGLError();

            #endregion Set Texture Parameters
        }

        private static GenerateMipmapTarget GetMipmapTargetForTextureTarget(TextureTarget target)
        {
            switch (target)
            {
                case TextureTarget.Texture1D:
                    return GenerateMipmapTarget.Texture1D;
                case TextureTarget.Texture1DArray:
                    return GenerateMipmapTarget.Texture1DArray;
                case TextureTarget.Texture2D:
                case TextureTarget.TextureCubeMapNegativeX:
                case TextureTarget.TextureCubeMapNegativeY:
                case TextureTarget.TextureCubeMapNegativeZ:
                case TextureTarget.TextureCubeMapPositiveX:
                case TextureTarget.TextureCubeMapPositiveY:
                case TextureTarget.TextureCubeMapPositiveZ:
                    return GenerateMipmapTarget.Texture2D;
                case TextureTarget.Texture2DArray:
                    return GenerateMipmapTarget.Texture2DArray;
                case TextureTarget.Texture2DMultisample:
                    return GenerateMipmapTarget.Texture2DMultisample;
                case TextureTarget.Texture2DMultisampleArray:
                    return GenerateMipmapTarget.Texture2DMultisampleArray;
                case TextureTarget.Texture3D:
                    return GenerateMipmapTarget.Texture3D;
                case TextureTarget.TextureCubeMap:
                    return GenerateMipmapTarget.TextureCubeMap;
                case TextureTarget.TextureCubeMapArray:
                    return GenerateMipmapTarget.TextureCubeMapArray;
                default:
                    return GenerateMipmapTarget.Texture2D;
            }
        }
    }
}