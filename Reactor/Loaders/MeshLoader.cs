//
// MeshLoader.cs
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
using Reactor.Types;
using Assimp;
using Reactor.Geometry;
using Reactor.Math;
using System.Collections.Generic;
using System.Diagnostics;

namespace Reactor.Loaders
{
    static class MeshLoader
    {
        public static void Load(this RMesh rmesh, string filename)
        {
            AssimpContext context = new AssimpContext();
            Scene scene = context.ImportFile(filename);

            if(scene.HasMeshes)
            {
                foreach(Mesh mesh in scene.Meshes)
                {
                    if(!mesh.HasVertices){
                        continue;
                    }
                    RMeshPart rmeshpart = RMeshPart.Create<RMeshPart>();
                    List<Vector3> verticies = new List<Vector3>();
                    List<Vector3> normals = new List<Vector3>();
                    List<Vector3> bitangents = new List<Vector3>();
                    List<Vector3> tangents = new List<Vector3>();
                    List<Vector2> texCoords = new List<Vector2>();

                    RVertexBuffer vbuffer = new RVertexBuffer(typeof(RVertexData), mesh.VertexCount, RBufferUsage.WriteOnly);

                    int[] indices = mesh.GetIndices();
                    RIndexBuffer<int> ibuffer = new RIndexBuffer<int>(indices.Length, RBufferUsage.WriteOnly);
                    ibuffer.SetData<int>(indices);
                    List<int> indicesList = new List<int>(indices);

                    foreach(Vector3D v in mesh.Vertices)
                    {
                        verticies.Add(v);
                    }
                    foreach(Vector3D n in mesh.Normals)
                    {
                        normals.Add(n);
                    }
                    if (mesh.HasTextureCoords(0))
                    {
                        foreach(Vector3D tex in mesh.TextureCoordinateChannels[0])
                        {
                            texCoords.Add(new Vector2(tex.X, tex.Y));
                        }
                    }
                    if( mesh.HasTangentBasis)
                    {
                        foreach(Vector3D b in mesh.BiTangents)
                        {
                            bitangents.Add(b);
                        }
                        foreach(Vector3D t in mesh.Tangents)
                        {
                            tangents.Add(t);
                        }
                    } else {
                        Vector3[] t;
                        Vector3[] b;
                        CalculateTangents(verticies, indicesList, normals, texCoords, out t, out b);
                        tangents.AddRange(t);
                        bitangents.AddRange(b);
                    }

                        RVertexData[] data = new RVertexData[mesh.VertexCount];
                        for(int i=0; i<mesh.VertexCount; i++)
                        {
                            data[i] = new RVertexData(
                                verticies[i],
                                normals[i],
                                bitangents[i],
                                tangents[i],
                                texCoords[i]
                            );
                        }
                        vbuffer.SetData<RVertexData>(data);
                        

                    rmeshpart.VertexBuffer = vbuffer;
                    rmeshpart.IndexBuffer = ibuffer;
                    rmesh.Parts.Add(rmeshpart);



                }
                //return rmesh;
            }
            //return null;
        }
        internal static void CalculateTangents(IList<Vector3> positions,
            IList<int> indices,
            IList<Vector3> normals,
            IList<Vector2> textureCoords,
            out Vector3[] tangents,
            out Vector3[] bitangents)
        {
            // Lengyel, Eric. “Computing Tangent Space Basis Vectors for an Arbitrary Mesh”. 
            // Terathon Software 3D Graphics Library, 2001.
            // http://www.terathon.com/code/tangent.html

            // Hegde, Siddharth. "Messing with Tangent Space". Gamasutra, 2007. 
            // http://www.gamasutra.com/view/feature/129939/messing_with_tangent_space.php

            var numVerts = positions.Count;
            var numIndices = indices.Count;

            var tan1 = new Vector3[numVerts];
            var tan2 = new Vector3[numVerts];

            for (var index = 0; index < numIndices; index += 3)
            {
                var i1 = indices[index + 0];
                var i2 = indices[index + 1];
                var i3 = indices[index + 2];

                var w1 = textureCoords[i1];
                var w2 = textureCoords[i2];
                var w3 = textureCoords[i3];

                var s1 = w2.X - w1.X;
                var s2 = w3.X - w1.X;
                var t1 = w2.Y - w1.Y;
                var t2 = w3.Y - w1.Y;

                var denom = s1 * t2 - s2 * t1;
                if (System.Math.Abs(denom) < float.Epsilon)
                {
                    // The triangle UVs are zero sized one dimension.
                    //
                    // So we cannot calculate the vertex tangents for this
                    // one trangle, but maybe it can with other trangles.
                    continue;
                }

                var r = 1.0f / denom;
                Debug.Assert(IsFinite(r), "Bad r!");

                var v1 = positions[i1];
                var v2 = positions[i2];
                var v3 = positions[i3];

                var x1 = v2.X - v1.X;
                var x2 = v3.X - v1.X;
                var y1 = v2.Y - v1.Y;
                var y2 = v3.Y - v1.Y;
                var z1 = v2.Z - v1.Z;
                var z2 = v3.Z - v1.Z;

                var sdir = new Vector3()
                    {
                        X = (t2 * x1 - t1 * x2) * r,
                        Y = (t2 * y1 - t1 * y2) * r,
                        Z = (t2 * z1 - t1 * z2) * r,
                    };

                var tdir = new Vector3()
                    {
                        X = (s1 * x2 - s2 * x1) * r,
                        Y = (s1 * y2 - s2 * y1) * r,
                        Z = (s1 * z2 - s2 * z1) * r,
                    };

                tan1[i1] += sdir;
                Debug.Assert(tan1[i1].IsFinite(), "Bad tan1[i1]!");
                tan1[i2] += sdir;
                Debug.Assert(tan1[i2].IsFinite(), "Bad tan1[i2]!");
                tan1[i3] += sdir;
                Debug.Assert(tan1[i3].IsFinite(), "Bad tan1[i3]!");

                tan2[i1] += tdir;
                Debug.Assert(tan2[i1].IsFinite(), "Bad tan2[i1]!");
                tan2[i2] += tdir;
                Debug.Assert(tan2[i2].IsFinite(), "Bad tan2[i2]!");
                tan2[i3] += tdir;
                Debug.Assert(tan2[i3].IsFinite(), "Bad tan2[i3]!");
            }

            tangents = new Vector3[numVerts];
            bitangents = new Vector3[numVerts];

            // At this point we have all the vectors accumulated, but we need to average
            // them all out. So we loop through all the final verts and do a Gram-Schmidt
            // orthonormalize, then make sure they're all unit length.
            for (var i = 0; i < numVerts; i++)
            {
                var n = normals[i];
                Debug.Assert(n.IsFinite(), "Bad normal!");
                Debug.Assert(n.Length() >= 0.9999f, "Bad normal!");

                var t = tan1[i];
                if (t.LengthSquared() < float.Epsilon)
                {
                    // TODO: Ideally we could spit out a warning to the
                    // content logging here!

                    // We couldn't find a good tanget for this vertex.
                    //
                    // Rather than set them to zero which could produce
                    // errors in other parts of the pipeline, we just take        
                    // a guess at something that may look ok.

                    t = Vector3.Cross(n, Vector3.UnitX);
                    if (t.LengthSquared() < float.Epsilon)
                        t = Vector3.Cross(n, Vector3.UnitY);

                    tangents[i] = Vector3.Normalize(t);
                    bitangents[i] = Vector3.Cross(n, tangents[i]);
                    continue;
                }

                // Gram-Schmidt orthogonalize
                // TODO: This can be zero can cause NaNs on 
                // normalize... how do we fix this?
                var tangent = t - n * Vector3.Dot(n, t);
                tangent = Vector3.Normalize(tangent);
                Debug.Assert(tangent.IsFinite(), "Bad tangent!");
                tangents[i] = tangent;

                // Calculate handedness
                var w = (Vector3.Dot(Vector3.Cross(n, t), tan2[i]) < 0.0F) ? -1.0F : 1.0F;
                Debug.Assert(IsFinite(w), "Bad handedness!");

                // Calculate the bitangent
                var bitangent = Vector3.Cross(n, tangent) * w;
                Debug.Assert(bitangent.IsFinite(), "Bad bitangent!");
                bitangents[i] = bitangent;
            }
        }
        static bool IsFinite(float v)
        {
            return !float.IsInfinity(v) && !float.IsNaN(v);
        }

        static bool IsFinite(this Vector3 v)
        {
            return IsFinite(v.X) && IsFinite(v.Y) && IsFinite(v.Z);
        }
    }
}

