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
using System.Collections.Generic;
using Reactor.Math;
using Reactor.Platform.GLFW;
using Reactor.Types;
using Exception = System.Exception;
using Point = System.Drawing.Point;

namespace Reactor
{
    public enum RMouseButton
    {
        Left = 0,
        Right = 1,
        Middle = 2,
        X1 = 3,
        X2 = 4
    }

    public class RKeyboard
    {
    }

    public class RInput : RSingleton<RInput>
    {
        private readonly Dictionary<MouseButton, bool> _mouseButtons = new Dictionary<MouseButton, bool>();
        private Vector2 _mousePosition;
        private double _mouseScroll;
        private readonly Dictionary<RKey, bool> _pressed = new Dictionary<RKey, bool>();
        public Action<RKey> KeyDown;
        public Action<RKey> KeyUp;
        public Action<RMouseButton> MouseButtonDown;
        public Action<RMouseButton> MouseButtonUp;
        public Action<Vector2> MouseMove;

        public void Init()
        {
            if (REngine.RGame == null) throw new Exception("RGame must be initialized first!");
            Bind();
        }

        private void Bind()
        {
            REngine.RGame.GameWindow.KeyPress += (s, k) =>
            {
                _pressed[k.Key] = true;
                RLog.Info("KEY DOWN: " + k.Key);
            };
            REngine.RGame.GameWindow.KeyRelease += (s, k) => { _pressed[k.Key] = false; };
            REngine.RGame.GameWindow.MouseScroll += (s, v) => { _mouseScroll = v.Y; };
            REngine.RGame.GameWindow.MouseMoved += (s, v) => { _mousePosition = new Vector2((float)v.X, (float)v.Y); };
            REngine.RGame.GameWindow.MouseButton += (s, b) =>
            {
                _mouseButtons[b.Button] = b.Action == InputState.Press;
            };
        }

        public bool IsKeyDown(RKey key)
        {
            _pressed.TryGetValue(key, out var down);
            return down;
        }

        public bool IsKeyUp(RKey key)
        {
            _pressed.TryGetValue(key, out var value);
            return !value;
        }

        public bool IsMouseButtonDown(MouseButton button)
        {
            if (_mouseButtons.ContainsKey(button))
                return _mouseButtons[button];
            return false;
        }

        public bool IsMouseButtonUp(MouseButton button)
        {
            if (_mouseButtons.ContainsKey(button))
                return !_mouseButtons[button];
            return true;
        }

        public void GetMouse(out int X, out int Y, out int Wheel)
        {
            X = (int)System.Math.Floor(_mousePosition.X);
            Y = (int)System.Math.Floor(_mousePosition.Y);
            Wheel = (int)System.Math.Floor(_mouseScroll);
        }

        public void GetMouse(out float X, out float Y, out float Wheel)
        {
            X = _mousePosition.X;
            Y = _mousePosition.Y;
            Wheel = (float)_mouseScroll;
        }

        public void CenterMouse()
        {
            var window = REngine.RGame.GameWindow;
            var x = window.ClientBounds.X + window.ClientSize.Width / 2;
            var y = window.ClientBounds.Y + window.ClientSize.Height / 2;
            //RLog.Info (String.Format ("Set MouseCenter to {0} {1}", x, y));
            REngine.RGame.GameWindow.MousePosition = new Point(x, y);
        }
    }
}