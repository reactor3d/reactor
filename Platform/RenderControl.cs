using OpenTK.Graphics;
using OpenTK.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Platform
{
    public abstract class RenderControl
    {
        public GraphicsContext Context { get; internal set; }

        public IWindowInfo WindowInfo { get; internal set; }
        public abstract void Init();

        public abstract void Destroy();

        public event EventHandler OnRender;

        public abstract void SwapBuffers();

        public abstract void MakeCurrent();

        public bool IsFullscreen { get; set; }
    }
}
