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
#if WINDOWS
using System.Windows.Forms;
#endif

namespace Reactor.Platform
{
    #if WINDOWS
    public class FormRenderControl : RenderControl
    {
        public Form Form { get; internal set; }
        private GLControl GLControl;
        public override void Init()
        {
            GLControl = new OpenTK.GLControl(GraphicsMode.Default, 4, 2, GraphicsContextFlags.ForwardCompatible);
            GLControl.Dock = DockStyle.Fill;
            GLControl.BackColor = System.Drawing.Color.Black;
            GLControl.MakeCurrent();
            GLControl.VSync = true;
            Form.Controls.Add(GLControl);
            Context = (GraphicsContext)GLControl.Context;
            WindowInfo = GLControl.WindowInfo;

            Threading.WindowInfo = WindowInfo;
        }

        public override void SwapBuffers()
        {
            Context.SwapBuffers();
        }

        public override void MakeCurrent()
        {
            Context.MakeCurrent(WindowInfo);
        }

        public override void Destroy()
        {
            Context.Dispose();
            WindowInfo.Dispose();
        }
    }
    #endif
}
