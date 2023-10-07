using System;
using Reactor.Geometry;
using Reactor.Math;
using Reactor.Types;

namespace Reactor.UI
{
    public class UIRenderer : IDisposable
    {
        /// <summary>
        ///     Creates a default "full-screen" UI Renderer. Typically this is what you want for your game ui. However,
        ///     you can also render UI to a FrameBuffer and use that as a texture for 3D world interfaces.
        /// </summary>
        public UIRenderer(RViewport viewport)
        {
            Bounds = viewport.Bounds();
            FrameBuffer = new RFrameBuffer(Bounds.Width, Bounds.Height, false, RSurfaceFormat.Color, RDepthFormat.None);
        }

        /// <summary>
        /// The framebuffer texture that holds the UI.
        /// </summary>
        public RFrameBuffer FrameBuffer { get; set; }

        /// <summary>
        /// The bounds of the renderer. Typically this is the same bounds as the viewport or window. However, when rendering
        /// UI to a texture surface on a mesh, it's best to set this to a power of 2 size like 1024x1024.
        /// </summary>
        public Rectangle Bounds { get; set; }

        private RShader _shader;

        public void Dispose()
        {
            if (FrameBuffer != null)
                FrameBuffer.Dispose();
        }

        public void Begin()
        {
            FrameBuffer.Bind();
        }

        public void Render(ref UIElement element, ref RVertexBuffer vertexBuffer, ref RIndexBuffer indexBuffer)
        {
            element.Draw(ref _shader, ref vertexBuffer, ref indexBuffer);
        }

        public void End()
        {
            FrameBuffer.Unbind();
        }
    }
}