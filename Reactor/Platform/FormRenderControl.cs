using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reactor.Platform
{
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
}
