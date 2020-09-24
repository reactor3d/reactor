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
using Reactor.Types;
using OpenTK.Input;
using System.Collections.Generic;

namespace Reactor
{
    public class RInput : RSingleton<RInput>
    {

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
            var cursor = Mouse.GetCursorState();

            if(cursor.IsConnected){
                X = cursor.X;
                Y = cursor.Y;
                Wheel = cursor.Wheel;
            } else {
                X = -1;
                Y = -1;
                Wheel = -1;
            }
        }

        public MouseState GetMouseState ()
        {
            return Mouse.GetState ();
        }

        public bool IsMouseButtonDown(MouseButton button)
        {
            return Mouse.GetState().IsButtonDown(button);
        }

        public void CenterMouse()
        {
            RGameWindow window = REngine.RGame.GameWindow;
            var x = window.Location.X + (window.ClientSize.Width / 2);
            var y = window.Location.Y + (window.ClientSize.Height / 2);
            RLog.Info (String.Format ("Set MouseCenter to {0} {1}", x, y));
            Mouse.SetPosition(x, y);
        }



        public bool IsMouseButtonUp(MouseButton button)
        {
            return Mouse.GetState().IsButtonUp(button);
        }
    }
}

