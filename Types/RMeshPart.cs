﻿//
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

namespace Reactor.Types
{
    internal class RMeshPart : RNode
    {
        internal RVertexBuffer VertexBuffer { get; set; }
        internal RIndexBuffer<int> IndexBuffer { get; set; }
        internal RShader Shader { get; set; }
        internal List<uint> Textures { get; set; }
        internal void Draw(PrimitiveType primitiveType)
        {
            var shortIndices = IndexBuffer.IndexElementSize == RIndexElementSize.SixteenBits;
            var indexElementType = shortIndices ? DrawElementsType.UnsignedShort : DrawElementsType.UnsignedInt;
            var indexElementSize = shortIndices ? 2 : 4;
            var indexOffsetInBytes = (IntPtr)(indexElementSize);
            var indexElementCount = IndexBuffer.GetElementCountArray(primitiveType, VertexBuffer.VertexCount / 3);
            VertexBuffer.VertexDeclaration.Apply(Shader, IntPtr.Zero);
            VertexBuffer.Bind();
            IndexBuffer.Bind();
            Shader.Bind();
            GL.DrawElements(primitiveType, indexElementCount, indexElementType, indexOffsetInBytes);
            Shader.Unbind();
            IndexBuffer.Unbind();
            VertexBuffer.Unbind();

        }
        internal void SetTexture(uint texture, RTextureLayer layer)
        {
            if(Textures == null)
                Textures = new List<uint>(16);

            Textures[(int)layer] = texture;
        }

        internal void BindTextures()
        {
            if(Textures != null)
            {
                foreach(uint t in Textures)
                {
                    if(t!=0)
                        RTextures.GetTexture(t).Bind();
                }
            }
        }
        internal void UnbindTextures()
        {
            if(Textures != null)
            {
                foreach(uint t in Textures)
                {
                    if(t!=0)
                        RTextures.GetTexture(t).Unbind();
                }
            }
        }
    }
}
