//
// RPixelFormat.cs
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

namespace Reactor.Types
{
    public enum RPixelFormat
    {
        UnsignedShort = 5123,
        UnsignedInt = 5125,
        ColorIndex = 6400,
        StencilIndex,
        DepthComponent,
        Red,
        RedExt = 6403,
        Green,
        Blue,
        Alpha,
        Rgb,
        Rgba,
        Luminance,
        LuminanceAlpha,
        AbgrExt = 32768,
        CmykExt = 32780,
        CmykaExt,
        Bgr = 32992,
        Bgra,
        Ycrcb422Sgix = 33211,
        Ycrcb444Sgix,
        Rg = 33319,
        RgInteger,
        R5G6B5IccSgix = 33894,
        R5G6B5A8IccSgix,
        Alpha16IccSgix,
        Luminance16IccSgix,
        Luminance16Alpha8IccSgix = 33899,
        DepthStencil = 34041,
        RedInteger = 36244,
        GreenInteger,
        BlueInteger,
        AlphaInteger,
        RgbInteger,
        RgbaInteger,
        BgrInteger,
        BgraInteger
    }
}

