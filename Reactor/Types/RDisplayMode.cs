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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Reactor.Types
{
    public class RDisplayMode
    {
        #region Fields

        //private RSurfaceFormat format;
        private int height;
        private int refreshRate;
        private int width;

        #endregion Fields

        #region Properties

        public float AspectRatio
        {
            get { return (float)width / (float)height; }
        }

        /*public RSurfaceFormat Format
        {
            get { return format; }
        }*/

        public int Height
        {
            get { return this.height; }
        }

        public int RefreshRate
        {
            get { return this.refreshRate; }
        }

        public int Width
        {
            get { return this.width; }
        }

        public Rectangle TitleSafeArea
        {
            get { return new Rectangle(0, 0, Width, Height); }
        }

        #endregion Properties

        #region Constructors

        public RDisplayMode(int width, int height, int refreshRate)
        {
            this.width = width;
            this.height = height;
            this.refreshRate = refreshRate;
            //this.format = format;
        }

        #endregion Constructors

        #region Operators

        public static bool operator !=(RDisplayMode left, RDisplayMode right)
        {
            return !(left == right);
        }

        public static bool operator ==(RDisplayMode left, RDisplayMode right)
        {
            if (ReferenceEquals(left, right)) //Same object or both are null
            {
                return true;
            }
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }
            return
                (left.height == right.height) &&
                (left.refreshRate == right.refreshRate) &&
                (left.width == right.width);
        }

        #endregion Operators

        #region Public Methods

        public override bool Equals(object obj)
        {
            return obj is RDisplayMode && this == (RDisplayMode)obj;
        }

        public override int GetHashCode()
        {
            return (this.width.GetHashCode() ^ this.height.GetHashCode() ^ this.refreshRate.GetHashCode());
        }

        public override string ToString()
        {
            return "{{Width:" + this.width + " Height:" + this.height + " RefreshRate:" + this.refreshRate + "}}";
        }

        #endregion Public Methods
    }
}
