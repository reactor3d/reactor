// Author:
//       Gabriel Reiser <gabe@reisergames.com>
//
// Copyright (c) 2010-2016 Reiser Games, LLC.
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
    internal class RMeshPart : RNode, IDisposable
    {
        internal RVertexBuffer VertexBuffer { get; set; }
        internal RIndexBuffer IndexBuffer { get; set; }
        public BoundingSphere BoundingSphere { get; set; }
        public BoundingBox BoundingBox { get; set; }

        public RMaterial Material { get; set; }
        RMeshPart()
        {
            Material = RMaterial.defaultMaterial;
        }

        internal void Draw(PrimitiveType primitiveType, Matrix world)
        {
            Threading.EnsureUIThread();
            Material.Shader.Bind();
            Material.Apply();

            VertexBuffer.BindVertexArray();
            VertexBuffer.Bind();
            IndexBuffer.Bind();
            VertexBuffer.VertexDeclaration.Apply(Material.Shader, IntPtr.Zero);

            REngine.CheckGLError();
            Material.Shader.SetUniformBySemantic(RShaderSemanticDefinition.WORLD, world);
            Material.Shader.SetUniformBySemantic(RShaderSemanticDefinition.MODEL, world);
            Material.Shader.SetUniformBySemantic(RShaderSemanticDefinition.FAR_PLANE, REngine.camera.Far);
            Material.Shader.SetUniformBySemantic(RShaderSemanticDefinition.NEAR_PLANE, REngine.camera.Near);
            REngine.CheckGLError();
            Material.Shader.BindSemantics();
            REngine.CheckGLError();

            GL.DrawElements(primitiveType, IndexBuffer.IndexCount, DrawElementsType.UnsignedInt , IntPtr.Zero);

            Material.Shader.Unbind();
            IndexBuffer.Unbind();
            VertexBuffer.Unbind();
            VertexBuffer.UnbindVertexArray();

        }


        public void Dispose()
        {
            Material.Dispose();
            VertexBuffer.Dispose();
            IndexBuffer.Dispose();
        }
    }
}

