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

namespace Reactor.Types
{
    public enum RPixelFormat
    {
        UnsignedShort = 0x1403,
        UnsignedInt = 0x1405,
        ColorIndex = 0x1900,
        StencilIndex = 0x1901,
        DepthComponent = 0x1902,
        Red = 0x1903,
        RedExt = 0x1903,
        Green = 0x1904,
        Blue = 0x1905,
        Alpha = 0x1906,
        Rgb = 0x1907,
        Rgba = 0x1908,
        Luminance = 0x1909,
        LuminanceAlpha = 0x190A,
        AbgrExt = 0x8000,
        CmykExt = 0x800C,
        CmykaExt = 0x800D,
        Bgr = 0x80E0,
        Bgra = 0x80E1,
        Ycrcb422Sgix = 0x81BB,
        Ycrcb444Sgix = 0x81BC,
        Rg = 0x8227,
        RgInteger = 0x8228,
        R5G6B5IccSgix = 0x8466,
        R5G6B5A8IccSgix = 0x8467,
        Alpha16IccSgix = 0x8468,
        Luminance16IccSgix = 0x8469,
        Luminance16Alpha8IccSgix = 0x846B,
        DepthStencil = 0x84F9,
        RedInteger = 0x8D94,
        GreenInteger = 0x8D95,
        BlueInteger = 0x8D96,
        AlphaInteger = 0x8D97,
        RgbInteger = 0x8D98,
        RgbaInteger = 0x8D99,
        BgrInteger = 0x8D9A,
        BgraInteger = 0x8D9B
    }
}