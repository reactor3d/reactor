// Author:
//       Gabriel Reiser <gabe@reisergames.com>
//
// Copyright (c) 2007-2020 Reiser Games, LLC.
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

using Reactor.Math;

namespace Reactor.Geometry
{
    public struct RVertexData : IVertexType
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector3 Bitangent;
        public Vector3 Tangent;
        public Vector2 TexCoord;

        private static readonly RVertexDeclaration VertexDeclaration;

        public RVertexDeclaration Declaration => VertexDeclaration;

        public RVertexData(Vector3 Position, Vector3 Normal, Vector3 Bitangent, Vector3 Tangent, Vector2 TexCoord)
        {
            this.Position = Position;
            this.Normal = Normal;
            this.TexCoord = TexCoord;
            this.Bitangent = Bitangent;
            this.Tangent = Tangent;
        }

        static RVertexData()
        {
            RVertexElement[] elements =
            {
                new RVertexElement(0, RVertexElementFormat.Vector3, RVertexElementUsage.Position),
                new RVertexElement(sizeof(float) * 3 * 1, RVertexElementFormat.Vector3, RVertexElementUsage.Normal),
                new RVertexElement(sizeof(float) * 3 * 2, RVertexElementFormat.Vector3, RVertexElementUsage.Bitangent),
                new RVertexElement(sizeof(float) * 3 * 3, RVertexElementFormat.Vector3, RVertexElementUsage.Tangent),
                new RVertexElement(sizeof(float) * 3 * 4, RVertexElementFormat.Vector2,
                    RVertexElementUsage.TextureCoordinate)
            };
            var declaration = new RVertexDeclaration(elements);
            VertexDeclaration = declaration;
        }
    }

    public struct RVertexData2D : IVertexType
    {
        public Vector2 Position;
        public Vector2 TexCoord;


        private static readonly RVertexDeclaration VertexDeclaration;

        public RVertexDeclaration Declaration => VertexDeclaration;

        public RVertexData2D(Vector2 Position, Vector2 TexCoord)
        {
            this.Position = Position;
            this.TexCoord = TexCoord;
        }


        static RVertexData2D()
        {
            RVertexElement[] elements =
            {
                new RVertexElement(0, RVertexElementFormat.Vector2, RVertexElementUsage.Position),
                new RVertexElement(8, RVertexElementFormat.Vector2, RVertexElementUsage.TextureCoordinate)
            };
            var declaration = new RVertexDeclaration(elements);
            VertexDeclaration = declaration;
        }
    }
}