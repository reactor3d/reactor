using System;
using System.Collections.Generic;
using Newtonsoft.Json.Utilities.LinqBridge;
using Reactor.Geometry;
using Reactor.Math;
using Reactor.Platform.OpenGL;
using Reactor.Types;

namespace Reactor.UI
{
    public class UIElement : IDisposable
    {
        private static RVertexData2D[] quadVerts = new RVertexData2D[4];
        public bool IsDirty { get; set; }

        public Rectangle Bounds { get; set; } = new Rectangle();
        public Vector4 Padding { get; set; } = Vector4.Zero;
        public RColor ForegroundColor { get; set; } = RColor.Black;
        public RColor BackgroundColor { get; set; } = RColor.White;
        public int BorderSize { get; set; } = 1;
        public RColor BorderColor { get; set; } = RColor.Azure;

        private List<UIElement> Children { get; } = new List<UIElement>();
        
        public int ZLevel { get; set; }
        public void Dispose()
        {
        }

        void sortChildren()
        {
            // sort on zlevel starting at 0 and working upwards. Children[0] should have zlevel 0.
            Children.Sort((left, right) =>
            {
                if (left.ZLevel > right.ZLevel)
                {
                    return 1;
                }

                if (left.ZLevel == right.ZLevel)
                {
                    throw new Exception("UIElements have seemed to occupy the same z-level. This is a bug");
                }
                return -1;
            });
            for (var i = 0; i < Children.Count; i++)
            {
                Children[i].ZLevel = i;
            }
        }

        public void AddControl(UIElement element)
        {
            element.ZLevel = Children.Count;
            Children.Add(element);
        }

        public void BringToFront(UIElement element)
        {
            foreach (var c in Children)
            {
                if (c != element)
                    c.ZLevel += 1;
            }

            element.ZLevel = 0;
            sortChildren();
            
        }

        public void SendToBack(UIElement element)
        {

            foreach (var c in Children)
            {
                if (c != element && c.ZLevel > element.ZLevel)
                    c.ZLevel -= 1;
            }

            element.ZLevel = Children.Count;
            sortChildren();
        }
        public virtual void Draw(ref RShader shader, ref RVertexBuffer vertexBuffer, ref RIndexBuffer indexBuffer)
        {
            shader.SetUniformValue("base_color", BackgroundColor);
            shader.SetUniformValue("border_color", BorderColor);
            shader.SetUniformValue("border_size", BorderSize);
            setVerts(Bounds, new Vector4(0, 0, 1, 1));
            vertexBuffer.SetData(quadVerts);
            vertexBuffer.VertexDeclaration.Apply(shader, IntPtr.Zero);
            GL.DrawElements(BeginMode.Triangles, indexBuffer.IndexCount, DrawElementsType.UnsignedShort,
                IntPtr.Zero);
            REngine.CheckGLError();

            foreach (var child in Children)
            {
                child.Draw(ref shader, ref vertexBuffer, ref indexBuffer);
            }
            IsDirty = false;
        }
        
        private void setVerts(Rectangle placement, Vector4 UVs)
        {
            quadVerts[0].Position = new Vector2(placement.X, placement.Y);
            quadVerts[0].TexCoord = new Vector2(UVs.X, UVs.Y);
            quadVerts[1].Position = new Vector2(placement.X + placement.Width, placement.Y);
            quadVerts[1].TexCoord = new Vector2(UVs.X + UVs.Z, UVs.Y);
            quadVerts[2].Position = new Vector2(placement.X + placement.Width, placement.Y + placement.Height);
            quadVerts[2].TexCoord = new Vector2(UVs.X + UVs.Z, UVs.Y + UVs.W);
            quadVerts[3].Position = new Vector2(placement.X, placement.Y + placement.Height);
            quadVerts[3].TexCoord = new Vector2(UVs.X, UVs.Y + UVs.W);
        }
    }
}