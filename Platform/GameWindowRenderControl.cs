using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Platform
{
    public class GameWindowRenderControl : RenderControl
    {
        public GameWindow GameWindow { get; internal set; }
        public override void Init()
        {
        }

        public override void Destroy()
        {
            GameWindow.Exit();
        }

        public override void MakeCurrent()
        {
            GameWindow.MakeCurrent();
        }

        public override void SwapBuffers()
        {
            GameWindow.SwapBuffers();
        }
    }
}
