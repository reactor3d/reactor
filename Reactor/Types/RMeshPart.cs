//
// RMeshPart.cs
//
// Author:
//       Gabriel Reiser <gabriel@reisergames.com>
//
// Copyright (c) 2015 2014
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using Reactor.Geometry;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using Reactor.Math;
using Reactor.Platform;
using OpenTK;

namespace Reactor.Types
{
    internal class RMeshPart : RNode
    {
        internal RVertexBuffer VertexBuffer { get; set; }
        internal RIndexBuffer IndexBuffer { get; set; }
        public BoundingSphere BoundingSphere { get; set; }
        public BoundingBox BoundingBox { get; set; }
        RMeshPart()
        {
            
        }
        internal void Draw(RMaterial material, PrimitiveType primitiveType, Matrix world)
        {
            Threading.EnsureUIThread();

            var shortIndices = IndexBuffer.IndexElementSize == RIndexElementSize.SixteenBits;
            var indexElementType = shortIndices ? DrawElementsType.UnsignedShort : DrawElementsType.UnsignedInt;
            var indexElementSize = shortIndices ? 2 : 4;
            var indexOffsetInBytes = (IntPtr)(indexElementSize);
            var indexElementCount = IndexBuffer.GetElementCountArray(primitiveType, VertexBuffer.VertexCount / 3);
            var vertexOffset = (IntPtr)(VertexBuffer.VertexDeclaration.VertexStride * 0);
            VertexBuffer.BindVertexArray();
            VertexBuffer.Bind();
            IndexBuffer.Bind();
            VertexBuffer.VertexDeclaration.Apply(material.Shader, IntPtr.Zero);


            material.Shader.SetUniformValue("world", world);
            material.Shader.SetUniformValue("view", REngine.camera.View);
            material.Shader.SetUniformValue("projection", REngine.camera.Projection);

            GL.DrawElements(primitiveType, IndexBuffer.IndexCount, indexElementType, IntPtr.Zero);

            IndexBuffer.Unbind();
            VertexBuffer.Unbind();
            VertexBuffer.UnbindVertexArray();

        }
        
    }
}

