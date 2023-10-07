using System;
using Reactor.Types;
using Reactor.Geometry;

namespace Reactor.Platform
{
    public class Platform : RSingleton<Platform>, IDisposable
    {

        public RenderControl RenderControl { get; internal set; }

        public void Dispose()
        {
            if(RenderControl != null)
                RenderControl.Dispose();
        }

        public RenderControl CreateRenderControl()
        {
            return new GameWindowRenderControl();
        }

        public RVertexBuffer CreateVertexBuffer()
        {
            return null;
        }
        public RIndexBuffer CreateIndexBuffer()
        {
            return null;
        }

        public RMaterial CreateMaterial()
        {
            return null;
        }

        public RShader CreateShader()
        {
            return null;
        }

        public RTexture CreateTexture()
        {
            return null;
        }


        public void Draw(ref RVertexBuffer vertexBuffer, ref RIndexBuffer indexBuffer)
        {

        }

        public void DrawIndexed(ref RVertexBuffer vertexBuffer, ref RIndexBuffer indexBuffer, int numElements)
        {

        }

        public void DrawInstanced(ref RVertexBuffer vertexBuffer, ref RIndexBuffer indexBuffer)
        {

        }
    }
}
