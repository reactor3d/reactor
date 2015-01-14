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
            #if MACOSX
            WindowInfo = OpenTK.Platofrm.Utilities.CreateMacOSWindowInfo(hWnd);
            #else
            WindowInfo = OpenTK.Platform.Utilities.CreateWindowsWindowInfo(hWnd);
            #endif
            Context = new GraphicsContext(GraphicsMode.Default, WindowInfo, 3, 2, GraphicsContextFlags.ForwardCompatible);
            Context.MakeCurrent(WindowInfo);

            //Load OpenGL function entry points into OpenTK.
            Context.LoadAll();
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
