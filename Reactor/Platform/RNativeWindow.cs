using System.Collections.Generic;
using Reactor.Platform.GLFW;
using Reactor.Types;

namespace Reactor.Platform
{
    public class RNativeWindow : NativeWindow, IGraphicsContext
    {
        public RDisplayMode Mode
        {
            get; private set;
        }

        public RNativeWindow() : base()
        {
        }
        public RNativeWindow(int width, int height, string title) : base(width, height, title)
        {
            var m = this.VideoMode;
            Mode = new RDisplayMode(m.Width, m.Height, m.RefreshRate);
        }
        public RNativeWindow(RDisplayMode displayMode, string title, bool fullscreen) : base(displayMode.Width, displayMode.Height, title)
        {
            Mode = displayMode;

            base.ClientBounds = new System.Drawing.Rectangle(0, 0, displayMode.Width, displayMode.Height);
            if (fullscreen)
            {
                Fullscreen(Glfw.PrimaryMonitor);
            }
            else
            {
                CenterOnScreen();
            }
        }
        ~RNativeWindow()
        {

        }
        public void SetMode(RDisplayMode displayMode, bool fullscreen)
        {
            Mode = displayMode;
            ClientBounds = new System.Drawing.Rectangle(0, 0, displayMode.Width, displayMode.Height);
            if (fullscreen)
                SetMonitor(Glfw.PrimaryMonitor, 0, 0, Mode.Width, Mode.Height, Mode.RefreshRate);
        }
        public void SetMode(RDisplayMode mode)
        {
            SetMode(mode, false);
        }
        public void Exit()
        {
            Close();
        }

        public RDisplayModes SupportedModes()
        {
            var videoModes = Glfw.GetVideoModes(Glfw.PrimaryMonitor);
            var ret = new List<RDisplayMode>();
            for (var i = 0; i < videoModes.Length; i++)
            {
                ret[i] = new RDisplayMode(videoModes[i].Width, videoModes[i].Height, videoModes[i].RefreshRate);
            }
            return new RDisplayModes(ret);
        }

        public RDisplayMode CurrentMode()
        {
            return Mode;
        }
    }
}