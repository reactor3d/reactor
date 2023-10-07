// Author:
//       Gabriel Reiser <gabe@reisergames.com>
//
// Copyright (c) 2010-2023 Reiser Games, LLC.
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

using System.Collections.Generic;
using Reactor.Platform.GLFW;
using Reactor.Types;

namespace Reactor.Platform
{
    internal class DummyContext : IGraphicsContext
    {
        private static readonly RDisplayMode mode = new RDisplayMode(1, 1, 60);

        public RDisplayMode CurrentMode()
        {
            return mode;
        }

        public void MakeCurrent()
        {
            Glfw.MakeContextCurrent(Window.None);
        }

        public void MakeNoneCurrent()
        {
            Glfw.MakeContextCurrent(Window.None);
        }

        public void SetMode(RDisplayMode mode)
        {
        }

        public RDisplayModes SupportedModes()
        {
            return new RDisplayModes(new List<RDisplayMode> { mode });
        }

        public void SwapBuffers()
        {
        }
    }

    public class DummyRenderControl : RenderControl
    {
        public override void Init()
        {
            if (!Glfw.Init()) return;
            Context = new DummyContext();
            Context.MakeCurrent();
        }

        public override void MakeCurrent()
        {
            Context.MakeCurrent();
        }

        public override void SwapBuffers()
        {
            Context.SwapBuffers();
        }

        public override void Dispose()
        {
            Glfw.Terminate();
        }
    }
}