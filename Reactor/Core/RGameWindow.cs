//
// RGameWindow.cs
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
using Reactor.Platform;
using Reactor.Math;

namespace Reactor
{
    public sealed class RGameWindow : RNativeWindow, IEquatable<RGameWindow>
    {
        private double r = 1.0f / 60.0f;
        private double u = 1.0f / 60.0f;
        public double RenderFrequency { get => r; set { r = value; } }
        public double UpdateFrequency { get => u; set { u = value; } }


        public Action Render;
        public Action Update;
        public Action Resize;
        public Action Closed;

        public RGameWindow():base()
        {
            init();
        }

        public RGameWindow(int width, int height) : base(width, height, null)
        {
            init();
        }
        void init()
        {
            base.Closing += OnClosing;
            base.SizeChanged += OnSizeChanged;
        }
        void OnClosing(object sender, EventArgs e)
        {
            if (Closed != null) Closed();
        }

        void OnSizeChanged(object sender, EventArgs e)
        {
            if (Resize != null) Resize();
        }

        public void Run()
        {
            while(true)
            {
                Threading.BlockOnUIThread(Update);
                Threading.BlockOnUIThread(Render);
            }
        }

        public bool Equals(RGameWindow other)
        {
            return Handle == other.Handle;
        }
    }
}

