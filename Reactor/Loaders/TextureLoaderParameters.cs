//
// TextureLoaderParameters.cs
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
using OpenTK.Graphics.OpenGL;

namespace Reactor
{

    /// <summary>The parameters in this class have only effect on the following Texture loads.</summary>
    static class TextureLoaderParameters
    {
        /// <summary>(Debug Aid, should be set to false) If set to false only Errors will be printed. If set to true, debug information (Warnings and Queries) will be printed in addition to Errors.</summary>
        public static bool Verbose = true;

        /// <summary>Always-valid fallback parameter for GL.BindTexture (Default: 0). This number will be returned if loading the Texture failed. You can set this to a checkerboard texture or similar, which you have already loaded.</summary>
        public static uint OpenGLDefaultTexture = 0;

        /// <summary>Compressed formats must have a border of 0, so this is constant.</summary>
        public const int Border = 0;

        /// <summary>false==DirectX TexCoords, true==OpenGL TexCoords (Default: true)</summary>
        public static bool FlipImages = false;

        /// <summary>When enabled, will use Glu to create MipMaps for images loaded with GDI+ (Default: false)</summary>
        public static bool BuildMipmapsForUncompressed = false;

        /// <summary>Selects the Magnification filter for following Textures to be loaded. (Default: Nearest)</summary>
        public static TextureMagFilter MagnificationFilter = TextureMagFilter.Linear;

        /// <summary>Selects the Minification filter for following Textures to be loaded. (Default: Nearest)</summary>
        public static TextureMinFilter MinificationFilter = TextureMinFilter.LinearMipmapLinear;

        /// <summary>Selects the S Wrapping for following Textures to be loaded. (Default: Repeat)</summary>
        public static TextureWrapMode WrapModeS = TextureWrapMode.Repeat;

        /// <summary>Selects the T Wrapping for following Textures to be loaded. (Default: Repeat)</summary>
        public static TextureWrapMode WrapModeT = TextureWrapMode.Repeat;

        /// <summary>Selects the Texture Environment Mode for the following Textures to be loaded. Default: Modulate)</summary>
        public static TextureEnvMode EnvMode = TextureEnvMode.Blend;
    }

}