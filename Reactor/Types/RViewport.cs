using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Reactor
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RViewport
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public RViewport(int x, int y)
        {
            X = x;
            Y = y;
            Width = 800;
            Height = 600;
        }

        public RViewport(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        internal void Bind()
        {
            GL.Viewport(X, Y, Width, Height);
        }

    }
}
