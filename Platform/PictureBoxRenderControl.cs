using OpenTK;
using OpenTK.Graphics;
using OpenTK.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reactor.Platform
{
    public class PictureBoxRenderControl : RenderControl
    {
        public PictureBox PictureBox { get; internal set; }
        
        public override void Init()
        {
            IntPtr hWnd = PictureBox.Handle;
            WindowInfo = OpenTK.Platform.Utilities.CreateWindowsWindowInfo(hWnd);
            Context = new GraphicsContext(GraphicsMode.Default, WindowInfo);
            Context.MakeCurrent(WindowInfo);

            //Load OpenGL function entry points into OpenTK.
            Context.LoadAll();
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
