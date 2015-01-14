using Reactor.Geometry;
using Reactor.Math;
//
// RMeshBuilder.cs
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
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace Reactor.Types
{
    public class RMeshBuilder : RRenderNode
    {
        #region Members
        internal RMaterial _material;

        internal RVertexBuffer _buffer;
        internal RIndexBuffer _index;
        
        internal int vertCount = 0;
        internal uint texture = 0;
        #endregion

        internal RShader Shader { get; set; }

        #region Methods
        public RMeshBuilder()
        {
            this.Scale = Vector3.One;
            this.Rotation = Quaternion.Identity;
            this.Position = Vector3.Zero;
            Shader = new RShader();
            Shader.Load(RShaderResources.BasicEffectVert, RShaderResources.BasicEffectFrag, null);
        }
        
        public void SetTexture(int layer, RTexture texture)
        {
            if (_material != null)
                _material.SetTexture(layer, texture);
            else
            {
                this.texture = texture.Id;
            }

        }
        public void CreateBox(Vector3 Center, Vector3 Size, bool FlipNormals)
        {

            RVertexData[] vertices = new RVertexData[36];


            // Calculate the position of the vertices on the top face.
            Vector3 topLeftFront = Position + new Vector3(-1.0f, 1.0f, -1.0f) * Size;
            Vector3 topLeftBack = Position + new Vector3(-1.0f, 1.0f, 1.0f) * Size;
            Vector3 topRightFront = Position + new Vector3(1.0f, 1.0f, -1.0f) * Size;
            Vector3 topRightBack = Position + new Vector3(1.0f, 1.0f, 1.0f) * Size;

            // Calculate the position of the vertices on the bottom face.
            Vector3 btmLeftFront = Position + new Vector3(-1.0f, -1.0f, -1.0f) * Size;
            Vector3 btmLeftBack = Position + new Vector3(-1.0f, -1.0f, 1.0f) * Size;
            Vector3 btmRightFront = Position + new Vector3(1.0f, -1.0f, -1.0f) * Size;
            Vector3 btmRightBack = Position + new Vector3(1.0f, -1.0f, 1.0f) * Size;

            // Normal vectors for each face (needed for lighting / display)
            Vector3 normalFront = new Vector3(0.0f, 0.0f, -1.0f) * Size;
            Vector3 normalBack = new Vector3(0.0f, 0.0f, 1.0f) * Size;
            Vector3 normalTop = new Vector3(0.0f, 1.0f, 0.0f) * Size;
            Vector3 normalBottom = new Vector3(0.0f, -1.0f, 0.0f) * Size;
            Vector3 normalLeft = new Vector3(1.0f, 0.0f, 0.0f) * Size;
            Vector3 normalRight = new Vector3(-1.0f, 0.0f, 0.0f) * Size;

            // UV texture coordinates
            Vector2 textureTopLeft = new Vector2(1.0f * Size.X, 0.0f * Size.Y);
            Vector2 textureTopRight = new Vector2(0.0f * Size.X, 0.0f * Size.Y);
            Vector2 textureBottomLeft = new Vector2(1.0f * Size.X, 1.0f * Size.Y);
            Vector2 textureBottomRight = new Vector2(0.0f * Size.X, 1.0f * Size.Y);

            // Add the vertices for the FRONT face.
            vertices[0] = new RVertexData(topLeftFront, normalFront, textureTopLeft);
            vertices[1] = new RVertexData(btmLeftFront, normalFront, textureBottomLeft);
            vertices[2] = new RVertexData(topRightFront, normalFront, textureTopRight);
            vertices[3] = new RVertexData(btmLeftFront, normalFront, textureBottomLeft);
            vertices[4] = new RVertexData(btmRightFront, normalFront, textureBottomRight);
            vertices[5] = new RVertexData(topRightFront, normalFront, textureTopRight);

            // Add the vertices for the BACK face.
            vertices[6] = new RVertexData(topLeftBack, normalBack, textureTopRight);
            vertices[7] = new RVertexData(topRightBack, normalBack, textureTopLeft);
            vertices[8] = new RVertexData(btmLeftBack, normalBack, textureBottomRight);
            vertices[9] = new RVertexData(btmLeftBack, normalBack, textureBottomRight);
            vertices[10] = new RVertexData(topRightBack, normalBack, textureTopLeft);
            vertices[11] = new RVertexData(btmRightBack, normalBack, textureBottomLeft);

            // Add the vertices for the TOP face.
            vertices[12] = new RVertexData(topLeftFront, normalTop, textureBottomLeft);
            vertices[13] = new RVertexData(topRightBack, normalTop, textureTopRight);
            vertices[14] = new RVertexData(topLeftBack, normalTop, textureTopLeft);
            vertices[15] = new RVertexData(topLeftFront, normalTop, textureBottomLeft);
            vertices[16] = new RVertexData(topRightFront, normalTop, textureBottomRight);
            vertices[17] = new RVertexData(topRightBack, normalTop, textureTopRight);

            // Add the vertices for the BOTTOM face. 
            vertices[18] = new RVertexData(btmLeftFront, normalBottom, textureTopLeft);
            vertices[19] = new RVertexData(btmLeftBack, normalBottom, textureBottomLeft);
            vertices[20] = new RVertexData(btmRightBack, normalBottom, textureBottomRight);
            vertices[21] = new RVertexData(btmLeftFront, normalBottom, textureTopLeft);
            vertices[22] = new RVertexData(btmRightBack, normalBottom, textureBottomRight);
            vertices[23] = new RVertexData(btmRightFront, normalBottom, textureTopRight);

            // Add the vertices for the LEFT face.
            vertices[24] = new RVertexData(topLeftFront, normalLeft, textureTopRight);
            vertices[25] = new RVertexData(btmLeftBack, normalLeft, textureBottomLeft);
            vertices[26] = new RVertexData(btmLeftFront, normalLeft, textureBottomRight);
            vertices[27] = new RVertexData(topLeftBack, normalLeft, textureTopLeft);
            vertices[28] = new RVertexData(btmLeftBack, normalLeft, textureBottomLeft);
            vertices[29] = new RVertexData(topLeftFront, normalLeft, textureTopRight);

            // Add the vertices for the RIGHT face. 
            vertices[30] = new RVertexData(topRightFront, normalRight, textureTopLeft);
            vertices[31] = new RVertexData(btmRightFront, normalRight, textureBottomLeft);
            vertices[32] = new RVertexData(btmRightBack, normalRight, textureBottomRight);
            vertices[33] = new RVertexData(topRightBack, normalRight, textureTopRight);
            vertices[34] = new RVertexData(topRightFront, normalRight, textureTopLeft);
            vertices[35] = new RVertexData(btmRightBack, normalRight, textureBottomRight);

            if (FlipNormals)
            {
                for (int i = 0; i < 36; i++)
                {
                    vertices[i].Normal *= -1.0f;
                }
            }
            VertexBuffer = new RVertexBuffer(typeof(RVertexData), vertices.Length,
               RBufferUsage.WriteOnly);

            VertexBuffer.SetData<RVertexData>(vertices);
            vertices = null;
            vertCount = 36;

        }

        public void CreateSphere(Vector3 Center, float Radius, int Tessellation)
        {
            int Stacks = Tessellation;
            int Slices = Tessellation * 2;

            List<RVertexData> vertices = new List<RVertexData>();



            float dphi = Reactor.Math.MathHelper.Pi / Stacks;
            float dtheta = Reactor.Math.MathHelper.TwoPi / Slices;

            int index = 0;
            vertices.Add(new RVertexData(new Vector3(0, -1f, 0) * Radius, new Vector3(0, -1f, 0), Vector2.Zero));
            for (int i = 0; i < Stacks - 1; i++)
            {
                float latitude = ((i + 1) * Reactor.Math.MathHelper.Pi / Stacks) - Reactor.Math.MathHelper.PiOver2;

                float dy = (float)System.Math.Sin(latitude);
                float dxz = (float)System.Math.Cos(latitude);

                // Create a single ring of vertices at this latitude.
                for (int j = 0; j < Slices; j++)
                {
                    float longitude = j * Reactor.Math.MathHelper.TwoPi / Slices;

                    float dx = (float)System.Math.Cos(longitude) * dxz;
                    float dz = (float)System.Math.Sin(longitude) * dxz;


                    Vector3 normal = new Vector3(dx, dy, dz);
                    Vector3 position = normal * Radius;
                    Vector2 tex = new Vector2(1.0f - ((float)j / (float)Slices - 1), (1.0f - ((float)(i) / (float)(Stacks - 1))));
                    //Vector3 tangent = Vector3.Cross(position, Vector3.UnitX);
                    //Vector3 binormal = Vector3.Cross(position, tangent);
                    vertices.Add(new RVertexData(position, Vector3.Normalize(normal), tex));
                }
            }
            vertices.Add(new RVertexData(new Vector3(0, 1f, 0) * Radius, new Vector3(0, 1f, 0), Vector2.Zero));
            vertCount = vertices.Count;
            /*for (int x = 0; x < Stacks-1; x++)
                for (int y = 0; y < Slices-1; y++)
                {
                    //Vector3 normal = Vector3.Normalize(vertices[y * Stacks + x].position);
                    //Normal.Normalize();
                    //vertices[y * Stacks + x].texture = new Vector2(((float)x) / (float)Slices, ((float)y) / (float)Stacks);
                    // Tangent Data.
                    RVERTEXFORMAT v = vertices[y * Stacks + x];
                    if (x != 0 && x < Slices - 1)
                        v.tangent = vertices[y * Stacks + x - 1].position - vertices[y * Stacks + x + 1].position;
                    else
                        if (x == 0)
                            v.tangent = vertices[y * Stacks + x].position - vertices[y * Stacks + x+1].position;
                        else
                            v.tangent = vertices[y * Stacks + x - 1].position - vertices[y * Stacks + x].position;

                    // Bi Normal Data.
                    if (y != 0 && y < Stacks - 1)
                        v.binormal = vertices[(y - 1) * Stacks + x].position - vertices[(y + 1) * Stacks + x].position;
                    else
                        if (y == 0)
                            v.binormal = vertices[y * Stacks + x].position - vertices[(y + 1) * Stacks + x].position;
                        else
                            v.binormal = vertices[(y - 1) * Stacks + x].position - vertices[y * Stacks + x].position;

                    //vertices[y * Stacks + x].normal = normal;
                    //vertices[y * Stacks + x].normal.Normalize();
                    vertices[y * Stacks + x] = v;
                    
                }*/
            List<ushort> indices = new List<ushort>();
            for (int i = 0; i < Slices; i++)
            {
                indices.Add(0);
                indices.Add((ushort)(1 + (i + 1) % Slices));
                indices.Add((ushort)(1 + i));
            }

            // Fill the sphere body with triangles joining each pair of latitude rings.
            for (int i = 0; i < Stacks-2; i++)
            {
                for (int j = 0; j < Slices; j++)
                {
                    int nextI = i + 1;
                    int nextJ = (j + 1) % Slices;

                    indices.Add((ushort)(1 + i * Slices + j));
                    indices.Add((ushort)(1 + i * Slices + nextJ));
                    indices.Add((ushort)(1 + nextI * Slices + j));

                    indices.Add((ushort)(1 + i * Slices + nextJ));
                    indices.Add((ushort)(1 + nextI * Slices + nextJ));
                    indices.Add((ushort)(1 + nextI * Slices + j));
                }
            }

            // Create a fan connecting the top vertex to the top latitude ring.
            for (int i = 0; i < Slices; i++)
            {
                indices.Add((ushort)(vertices.Count - 1));
                indices.Add((ushort)(vertices.Count - 2 - (i + 1) % Slices));
                indices.Add((ushort)(vertices.Count - 2 - i));
            }

            VertexBuffer = new RVertexBuffer(typeof(RVertexData), vertices.Count,
                RBufferUsage.None);

            VertexBuffer.SetData<RVertexData>(vertices.ToArray());
            //vertCount = vertices.Length;
            vertices = null;

            _index = new RIndexBuffer(typeof(ushort), indices.Count, RBufferUsage.None, false);

            _index.SetData(indices.ToArray());
            indices = null;

            this.Position = Center;

        }

        public override void Render()
        {
            GL.Enable(EnableCap.DepthTest);
            REngine.CheckGLError();
            GL.DepthMask(true);
            REngine.CheckGLError();
            GL.DepthFunc(DepthFunction.Less);
            REngine.CheckGLError();

            GL.FrontFace(FrontFaceDirection.Cw);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            REngine.CheckGLError();
            GL.Disable(EnableCap.CullFace);
            /*
            GL.Enable(EnableCap.CullFace);
            REngine.CheckGLError();
            GL.FrontFace(FrontFaceDirection.Ccw);
            REngine.CheckGLError();
            GL.CullFace(CullFaceMode.FrontAndBack);
            REngine.CheckGLError();
            */

            VertexBuffer.BindVertexArray();
            VertexBuffer.Bind();

            Shader.Bind();
            VertexBuffer.VertexDeclaration.Apply(Shader, IntPtr.Zero);


            Shader.SetUniformValue("world", Matrix.Identity);
            Shader.SetUniformValue("view", REngine.camera.View);
            Shader.SetUniformValue("projection", REngine.camera.Projection);

            if(_index != null)
            {

                var shortIndices = _index.IndexElementSize == RIndexElementSize.SixteenBits;
                var indexElementType = shortIndices ? DrawElementsType.UnsignedShort : DrawElementsType.UnsignedInt;
                var indexElementSize = shortIndices ? 2 : 4;
                var indexOffsetInBytes = (IntPtr)(indexElementSize);
                var indexElementCount = _index.GetElementCountArray(PrimitiveType.Triangles, vertCount*2);
                _index.Bind();
                REngine.CheckGLError();
                GL.DrawElements(PrimitiveType.TriangleStrip, _index.IndexCount, indexElementType, indexOffsetInBytes);
                REngine.CheckGLError();
                _index.Unbind();
                REngine.CheckGLError();
            }
            else 
            {

                GL.DrawArrays(PrimitiveType.Triangles, 0, VertexBuffer.VertexCount);
                REngine.CheckGLError();
            }

            Shader.Unbind();

            VertexBuffer.Unbind();
            VertexBuffer.UnbindVertexArray();

        }
        
        public void Dispose()
        {
            _buffer.Dispose();
            _index.Dispose();

        }


        #endregion
    }
}

