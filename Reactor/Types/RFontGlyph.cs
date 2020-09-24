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
using SD = System.Drawing;
using Reactor.Math;
using SharpFont;
using OpenTK.Graphics.OpenGL;
using System.IO;


namespace Reactor.Types
{
    internal class RFontGlyph
    {
        public int CharIndex;
        public Rectangle Bounds;
        public Vector4 UVBounds;
        public Vector2 Offset;
        public int Advance;
        public System.Drawing.Bitmap bitmap;
        internal void Save(ref BinaryWriter stream)
        {
            stream.Write(CharIndex);
            stream.Write(Bounds.X);
            stream.Write(Bounds.Y);
            stream.Write(Bounds.Width);
            stream.Write(Bounds.Height);

            stream.Write(UVBounds.X);
            stream.Write(UVBounds.Y);
            stream.Write(UVBounds.Z);
            stream.Write(UVBounds.W);

            stream.Write(Offset.X);
            stream.Write(Offset.Y);

            stream.Write(Advance);
        }

        internal void Load(ref BinaryReader stream)
        {
            CharIndex = stream.ReadInt32();
            Bounds = new Rectangle();
            Bounds.X = stream.ReadInt32();
            Bounds.Y = stream.ReadInt32();
            Bounds.Width = stream.ReadInt32();
            Bounds.Height = stream.ReadInt32();

            UVBounds = new Vector4();
            UVBounds.X = stream.ReadSingle();
            UVBounds.Y = stream.ReadSingle();
            UVBounds.Z = stream.ReadSingle();
            UVBounds.W = stream.ReadSingle();

            Offset = new Vector2();
            Offset.X = stream.ReadSingle();
            Offset.Y = stream.ReadSingle();

            Advance = stream.ReadInt32();
        }
        
    }
}

