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

using Reactor.Math;

namespace Reactor.Types
{
    public class RDisplayMode
    {
        #region Constructors

        public RDisplayMode(int width, int height, int refreshRate)
        {
            Width = width;
            Height = height;
            RefreshRate = refreshRate;
            //this.format = format;
        }

        #endregion Constructors

        #region Fields

        //private RSurfaceFormat format;

        #endregion Fields

        #region Properties

        public float AspectRatio => Width / (float)Height;

        /*public RSurfaceFormat Format
        {
            get { return format; }
        }*/

        public int Height { get; }

        public int RefreshRate { get; }

        public int Width { get; }

        public Rectangle TitleSafeArea => new Rectangle(0, 0, Width, Height);

        #endregion Properties

        #region Operators

        public static bool operator !=(RDisplayMode left, RDisplayMode right)
        {
            return !(left == right);
        }

        public static bool operator ==(RDisplayMode left, RDisplayMode right)
        {
            if (ReferenceEquals(left, right)) //Same object or both are null
                return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
            return
                left.Height == right.Height &&
                left.RefreshRate == right.RefreshRate &&
                left.Width == right.Width;
        }

        #endregion Operators

        #region Public Methods

        public override bool Equals(object obj)
        {
            return obj is RDisplayMode && this == (RDisplayMode)obj;
        }

        public override int GetHashCode()
        {
            return Width.GetHashCode() ^ Height.GetHashCode() ^ RefreshRate.GetHashCode();
        }

        public override string ToString()
        {
            return "{{Width:" + Width + " Height:" + Height + " RefreshRate:" + RefreshRate + "}}";
        }

        #endregion Public Methods
    }
}