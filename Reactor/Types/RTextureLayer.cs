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
using Reactor.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reactor.Types
{
    public enum RTextureLayer : int
    {
        DIFFUSE = TextureUnit.Texture0,
        NORMAL = TextureUnit.Texture1,
        AMBIENT = TextureUnit.Texture2,
        SPECULAR = TextureUnit.Texture3,
        GLOW = TextureUnit.Texture4,
        HEIGHT = TextureUnit.Texture5,
        DETAIL = TextureUnit.Texture6,
        TEXTURE7 = TextureUnit.Texture7,
        TEXTURE8 = TextureUnit.Texture8,
        TEXTURE9 = TextureUnit.Texture9,
        TEXTURE10 = TextureUnit.Texture10,
        TEXTURE11 = TextureUnit.Texture11,
        TEXTURE12 = TextureUnit.Texture12,
        TEXTURE13 = TextureUnit.Texture13,
        TEXTURE14 = TextureUnit.Texture14,
        TEXTURE15 = TextureUnit.Texture15
    }
    public enum RTextureUnit : int
    {
        DIFFUSE = 0,
        NORMAL = 1,
        AMBIENT = 2,
        SPECULAR = 3,
        GLOW = 4,
        HEIGHT = 5,
        DETAIL = 6,
        TEXTURE7 = 7,
        TEXTURE8 = 8,
        TEXTURE9 = 9,
        TEXTURE10 = 10,
        TEXTURE11 = 11,
        TEXTURE12 = 12,
        TEXTURE13 = 13,
        TEXTURE14 = 14,
        TEXTURE15 = 15
    }
}
