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

namespace Reactor.Types
{
    public enum RPixelFormat : int
    {
        UnsignedShort = ((int)0x1403),
        UnsignedInt = ((int)0x1405),
        ColorIndex = ((int)0x1900),
        StencilIndex = ((int)0x1901),
        DepthComponent = ((int)0x1902),
        Red = ((int)0x1903),
        RedExt = ((int)0x1903),
        Green = ((int)0x1904),
        Blue = ((int)0x1905),
        Alpha = ((int)0x1906),
        Rgb = ((int)0x1907),
        Rgba = ((int)0x1908),
        Luminance = ((int)0x1909),
        LuminanceAlpha = ((int)0x190A),
        AbgrExt = ((int)0x8000),
        CmykExt = ((int)0x800C),
        CmykaExt = ((int)0x800D),
        Bgr = ((int)0x80E0),
        Bgra = ((int)0x80E1),
        Ycrcb422Sgix = ((int)0x81BB),
        Ycrcb444Sgix = ((int)0x81BC),
        Rg = ((int)0x8227),
        RgInteger = ((int)0x8228),
        R5G6B5IccSgix = ((int)0x8466),
        R5G6B5A8IccSgix = ((int)0x8467),
        Alpha16IccSgix = ((int)0x8468),
        Luminance16IccSgix = ((int)0x8469),
        Luminance16Alpha8IccSgix = ((int)0x846B),
        DepthStencil = ((int)0x84F9),
        RedInteger = ((int)0x8D94),
        GreenInteger = ((int)0x8D95),
        BlueInteger = ((int)0x8D96),
        AlphaInteger = ((int)0x8D97),
        RgbInteger = ((int)0x8D98),
        RgbaInteger = ((int)0x8D99),
        BgrInteger = ((int)0x8D9A),
        BgraInteger = ((int)0x8D9B),
    }
}

