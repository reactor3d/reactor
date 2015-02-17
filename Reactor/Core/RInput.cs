//
// RInput.cs
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
using Reactor.Types;
using OpenTK.Input;

namespace Reactor
{
    public class RInput : RSingleton<RInput>
    {

        public RInput()
        {

        }

        public bool IsKeyDown(RKey key)
        {
            return Keyboard.GetState().IsKeyDown((Key)key);
        }

        public bool IsKeyUp(RKey key)
        {
            return Keyboard.GetState().IsKeyUp((Key)key);
        }

        public void GetMouse(out int X, out int Y, out int Wheel)
        {
            MouseState state = Mouse.GetCursorState();
            X = state.X;
            Y = state.Y;
            Wheel = state.Wheel;
        }

        public bool IsMouseButtonDown(MouseButton button)
        {
            return Mouse.GetState().IsButtonDown(button);
        }

        public void CenterMouse()
        {
            Mouse.SetPosition(REngine.Instance._viewport.Width / 2, REngine.Instance._viewport.Height / 2);
        }



        public bool IsMouseButtonUp(MouseButton button)
        {
            return Mouse.GetState().IsButtonUp(button);
        }
    }
}

