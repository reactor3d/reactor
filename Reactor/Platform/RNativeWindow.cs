using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Reactor.Math;
using Reactor.Platform.GLFW;
using Reactor.Platform.OpenGL;
using Reactor.Types;

namespace Reactor.Platform
{
    public class RNativeWindow : NativeWindow, IGraphicsContext
    {
        public enum DWM_USE_IMMERSIVE_DARK_MODE
        {
            FALSE = 0,
            TRUE = 1
        }

        // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
        // what value of the enum to set.
        // Copied from dwmapi.h
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        public RNativeWindow()
        {
        }

        public RNativeWindow(int width, int height, string title) : base(width, height, title)
        {
            var m = VideoMode;
            Mode = new RDisplayMode(m.Width, m.Height, m.RefreshRate);
            if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 10)
            {
                var hWnd = Glfw.GetWin32Handle(Window);
                var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
                var preference = (int)DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
                DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(int));
                attribute = DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE;
                var dark = 1;
                DwmSetWindowAttribute(hWnd, attribute, ref dark, sizeof(int));
                uint color = GetSysColor(8);
                var c = RColor.FromRGB(color).ToVector4();
                GL.ClearColor(0.125f, 0.135f, 0.145f, 1.0f);
            }
        }

        public RNativeWindow(RDisplayMode displayMode, string title, bool fullscreen) : base(displayMode.Width,
            displayMode.Height, title)
        {
            Mode = displayMode;

            ClientBounds = new System.Drawing.Rectangle(0, 0, displayMode.Width, displayMode.Height);
            if (fullscreen)
                Fullscreen(Glfw.PrimaryMonitor);
            else
                CenterOnScreen();
        }

        public RDisplayMode Mode { get; private set; }

        public void SetMode(RDisplayMode mode)
        {
            SetMode(mode, false);
        }

        public RDisplayModes SupportedModes()
        {
            var videoModes = Glfw.GetVideoModes(Glfw.PrimaryMonitor);
            var ret = new List<RDisplayMode>();
            for (var i = 0; i < videoModes.Length; i++)
                ret[i] = new RDisplayMode(videoModes[i].Width, videoModes[i].Height, videoModes[i].RefreshRate);
            return new RDisplayModes(ret);
        }

        public RDisplayMode CurrentMode()
        {
            return Mode;
        }

        ~RNativeWindow()
        {
            base.ReleaseHandle();
        }

        public void SetMode(RDisplayMode displayMode, bool fullscreen)
        {
            Mode = displayMode;
            ClientBounds = new System.Drawing.Rectangle(0, 0, displayMode.Width, displayMode.Height);
            if (fullscreen)
                SetMonitor(Glfw.PrimaryMonitor, 0, 0, Mode.Width, Mode.Height, Mode.RefreshRate);
        }

        public void Exit()
        {
            Close();
        }

        public Rectangle CurrentBackBufferSize()
        {
            Glfw.GetFramebufferSize(Window, out var width, out var height);
            return new Rectangle(Position.X, Position.Y, width, height);
        }

        // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void DwmSetWindowAttribute(IntPtr hwnd,
            DWMWINDOWATTRIBUTE attribute,
            ref int pvAttribute,
            uint cbAttribute);
        
        [DllImport("user32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern uint GetSysColor(int nIndex);
    }
}