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
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK;
namespace Reactor
{
    public sealed class RGameWindow : GameWindow
    {


        public RGameWindow():base(800,600, new GraphicsMode(32,24,8,8), "Reactor", GameWindowFlags.Default, DisplayDevice.GetDisplay(DisplayIndex.Primary), 4, 0, GraphicsContextFlags.ForwardCompatible){
            Width = 800;
            Height = 600;
        }

        public RGameWindow(int width, int height)
            : base(width, height, new GraphicsMode(32, 24, 8, 8), "Reactor", GameWindowFlags.Default, DisplayDevice.GetDisplay(DisplayIndex.Primary),4, 0, GraphicsContextFlags.ForwardCompatible)
        {
            Width = width;
            Height = height;
        }

        public void DoExit()
        {
            base.Exit();
        }

        public bool Focused
        {
            get
            {
                return base.Focused;
            }
        }

        public int Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }
        public int Height
        {
            get { return base.Height; }
            set { base.Width = value; }
        }

    }
}

