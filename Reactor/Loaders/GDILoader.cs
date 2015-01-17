//
// GDILoader.cs
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
using System.IO;
using Reactor.Types;


#region --- License ---
/* Licensed under the MIT/X11 license.
 * Copyright (c) 2006-2008 the OpenTK Team.
 * This notice may not be removed from any source distribution.
 * See license.txt for licensing details.
 */
#endregion

// TODO: Find paint program that can properly export 8/16-bit Textures and make sure they are loaded correctly.

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Reactor
{
    static class ImageGDI
    {
        public static void LoadFromData( byte[] data, out uint texturehandle, out TextureTarget dimension, out RPixelFormat format, out PixelType type)
        {
            dimension = (TextureTarget) 0;
            texturehandle = TextureLoaderParameters.OpenGLDefaultTexture;
            MemoryStream stream = new MemoryStream(data);
            Bitmap b = new Bitmap(stream);
            LoadFromBitmap(ref b, out texturehandle, out dimension, out format, out type);
        }

        public static void LoadFromDisk( string filename, out uint texturehandle, out TextureTarget dimension, out RPixelFormat format, out PixelType type )
        {
            dimension = (TextureTarget) 0;
            texturehandle = TextureLoaderParameters.OpenGLDefaultTexture;
            Bitmap b = new Bitmap(filename);
            LoadFromBitmap(ref b, out texturehandle, out dimension, out format, out type);
        }
        public static void LoadFromBitmap( ref Bitmap bitmap, out uint texturehandle, out TextureTarget dimension, out RPixelFormat format, out PixelType type )
        {
            dimension = (TextureTarget) 0;
            texturehandle = TextureLoaderParameters.OpenGLDefaultTexture;
            ErrorCode GLError = ErrorCode.NoError;

            Bitmap CurrentBitmap = null;

            try // Exceptions will be thrown if any Problem occurs while working on the file. 
            {
                CurrentBitmap = bitmap;
                if ( TextureLoaderParameters.FlipImages )
                    CurrentBitmap.RotateFlip( RotateFlipType.RotateNoneFlipY );

                if ( CurrentBitmap.Height > 1 )
                    dimension = TextureTarget.Texture2D;
                else
                    dimension = TextureTarget.Texture1D;

                GL.GenTextures( 1, out texturehandle );
                GL.BindTexture( dimension, texturehandle );

                #region Load Texture
                OpenTK.Graphics.OpenGL.PixelInternalFormat pif;
                OpenTK.Graphics.OpenGL.PixelFormat pf;
                OpenTK.Graphics.OpenGL.PixelType pt;


                switch ( CurrentBitmap.PixelFormat )
                {
                    case System.Drawing.Imaging.PixelFormat.Format8bppIndexed: // misses glColorTable setup
                        pif = OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgb8;
                        pf = OpenTK.Graphics.OpenGL.PixelFormat.ColorIndex;
                        pt = OpenTK.Graphics.OpenGL.PixelType.Bitmap;
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format16bppArgb1555:
                    case System.Drawing.Imaging.PixelFormat.Format16bppRgb555: // does not work
                        pif = OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgb5A1;
                        pf = OpenTK.Graphics.OpenGL.PixelFormat.Bgr;
                        pt = OpenTK.Graphics.OpenGL.PixelType.UnsignedShort5551Ext;
                        break;
                        /*  case System.Drawing.Imaging.PixelFormat.Format16bppRgb565:
                    pif = OpenTK.Graphics.OpenGL.PixelInternalFormat.R5G6B5IccSgix;
                    pf = OpenTK.Graphics.OpenGL.PixelFormat.R5G6B5IccSgix;
                    pt = OpenTK.Graphics.OpenGL.PixelType.UnsignedByte;
                    break;
*/
                    case System.Drawing.Imaging.PixelFormat.Format24bppRgb: // works
                        pif = OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgb8;
                        pf = OpenTK.Graphics.OpenGL.PixelFormat.Bgr;
                        pt = OpenTK.Graphics.OpenGL.PixelType.UnsignedByte;
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format32bppRgb: // has alpha too? wtf?
                    case System.Drawing.Imaging.PixelFormat.Canonical:
                    case System.Drawing.Imaging.PixelFormat.Format32bppArgb: // works
                        pif = OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgba;
                        pf = OpenTK.Graphics.OpenGL.PixelFormat.Bgra;
                        pt = OpenTK.Graphics.OpenGL.PixelType.UnsignedByte;
                        break;
                    default:
                        throw new ArgumentException( "ERROR: Unsupported Pixel Format " + CurrentBitmap.PixelFormat );
                }
                format = (RPixelFormat)pf;
                type = pt;
                BitmapData Data = CurrentBitmap.LockBits( new System.Drawing.Rectangle( 0, 0, CurrentBitmap.Width, CurrentBitmap.Height ), ImageLockMode.ReadOnly, CurrentBitmap.PixelFormat );

                if ( Data.Height > 1 )
                { // image is 2D
                    if (TextureLoaderParameters.BuildMipmapsForUncompressed)
                    {
                        throw new Exception("Cannot build mipmaps, Glu is deprecated.");
                        //  Glu.Build2DMipmap(dimension, (int)pif, Data.Width, Data.Height, pf, pt, Data.Scan0);
                    }
                    else
                        GL.TexImage2D(dimension, 0, pif, Data.Width, Data.Height, TextureLoaderParameters.Border, pf, pt, Data.Scan0);
                } else
                { // image is 1D
                    if (TextureLoaderParameters.BuildMipmapsForUncompressed)
                    {
                        throw new Exception("Cannot build mipmaps, Glu is deprecated.");
                        //  Glu.Build1DMipmap(dimension, (int)pif, Data.Width, pf, pt, Data.Scan0);
                    }
                    else
                        GL.TexImage1D(dimension, 0, pif, Data.Width, TextureLoaderParameters.Border, pf, pt, Data.Scan0);
                }

                GL.Finish( );
                GLError = GL.GetError( );
                if ( GLError != ErrorCode.NoError )
                {
                    throw new ArgumentException( "Error building TexImage. GL Error: " + GLError );
                }

                CurrentBitmap.UnlockBits( Data );
                #endregion Load Texture

                #region Set Texture Parameters
                GL.TexParameter( dimension, TextureParameterName.TextureMinFilter, (int) TextureLoaderParameters.MinificationFilter );
                GL.TexParameter( dimension, TextureParameterName.TextureMagFilter, (int) TextureLoaderParameters.MagnificationFilter );

                GL.TexParameter( dimension, TextureParameterName.TextureWrapS, (int) TextureLoaderParameters.WrapModeS );
                GL.TexParameter( dimension, TextureParameterName.TextureWrapT, (int) TextureLoaderParameters.WrapModeT );

                GL.TexEnv( TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int) TextureLoaderParameters.EnvMode );

                GLError = GL.GetError( );
                if ( GLError != ErrorCode.NoError )
                {
                    throw new ArgumentException( "Error setting Texture Parameters. GL Error: " + GLError );
                }
                #endregion Set Texture Parameters

                return; // success
            } catch ( Exception e )
            {
                dimension = (TextureTarget) 0;
                texturehandle = TextureLoaderParameters.OpenGLDefaultTexture;
                throw new ArgumentException( "Texture Loading Error: Failed to read data.\n" + e );
                // return; // failure
            } finally
            {
                CurrentBitmap = null;
            }
        }

    }
}
