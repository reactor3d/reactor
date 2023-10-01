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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactor.Platform.GLFW;
using Reactor.Types;

namespace Reactor.Platform
{
    public class GameWindowRenderControl : RenderControl
    {
        public RGameWindow GameWindow { get; internal set; }

        public GameWindowRenderControl() : base()
        {
            GameWindow = new RGameWindow(1280, 720);
        }
        public GameWindowRenderControl(RDisplayMode displayMode, RWindowStyle style, string title) : base()
        {
            GameWindow = REngine.RGame.GameWindow;
            Context = GameWindow;
            GameWindow.WindowStyle = style;
            GameWindow.SetMode(displayMode);

        }
        public override void Init()
        {
            Context = GameWindow;
        }

        public override void Dispose()
        {
            GameWindow.Close();
        }

        public override void MakeCurrent()
        {
            Context.MakeCurrent();
        }

        public override void SwapBuffers()
        {
            Context.SwapBuffers();
        }
    }
}