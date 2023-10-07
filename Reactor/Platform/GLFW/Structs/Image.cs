using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Reactor.Platform.GLFW
{
    /// <summary>
    ///     Describes a basic image structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Image
    {
        /// <summary>
        ///     The height, in pixels, of this image.
        /// </summary>
        public readonly int Width;

        /// <summary>
        ///     The width, in pixels, of this image.
        /// </summary>
        public readonly int Height;

        /// <summary>
        ///     Pointer to the RGBA pixel data of this image, arranged left-to-right, top-to-bottom.
        /// </summary>
        public readonly IntPtr Pixels;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Image" /> struct.
        /// </summary>
        /// <param name="width">The height, in pixels, of this image.</param>
        /// <param name="height">The width, in pixels, of this image..</param>
        /// <param name="pixels">Pointer to the RGBA pixel data of this image, arranged left-to-right, top-to-bottom.</param>
        public Image(int width, int height, IntPtr pixels)
        {
            Width = width;
            Height = height;
            Pixels = pixels;
        }

        public Image(Bitmap bitmap)
        {
            Width = bitmap.Width;
            Height = bitmap.Height;
            var d = new byte[Width * Height * 4];
            var offset = 0;
            for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
            {
                d[offset] = bitmap.GetPixel(x, y).R;
                d[offset + 1] = bitmap.GetPixel(x, y).G;
                d[offset + 2] = bitmap.GetPixel(x, y).B;
                d[offset + 3] = bitmap.GetPixel(x, y).A;
                offset += 4;
            }

            unsafe
            {
                fixed (byte* ptr = &d[0])
                {
                    Pixels = new IntPtr(ptr);
                }
            }
        }
    }
}