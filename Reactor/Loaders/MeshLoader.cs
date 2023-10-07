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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Xsl;
using Reactor.Core;
using Reactor.Core.Extensions;
using Reactor.Geometry;
using Reactor.Math;
using Reactor.Platform.glTF;
using Reactor.Platform.glTF.Schema;
using Reactor.Types;

namespace Reactor.Loaders
{
    internal static class Assert
    {
        public static void NotNull(object o)
        {
            if (o == null) throw new ArgumentNullException("o");
        }

        public static void Fail(string msg)
        {
            throw new Exception(msg);
        }
    }

    internal static class MeshLoader
    {
        private static Vector4[] ReadVec4s(ref Gltf gltf, ref MeshPrimitive primitive, string attr, string filename)
        {
            var positionAttr = gltf.Accessors[primitive.Attributes[attr]];
            if (positionAttr.Type != Accessor.TypeEnum.VEC4)
                throw new Exception("Error! " + attr + " is not of type VEC4 in " + filename);
            var positionAttrBuf = gltf.BufferViews[positionAttr.BufferView.Value];
            var buff = gltf.LoadBinaryBuffer(positionAttrBuf.Buffer, filename).AsMemory();
            var data = buff.Slice(positionAttrBuf.ByteOffset + positionAttr.ByteOffset, positionAttrBuf.ByteLength);
            var ret = new Vector4[positionAttr.Count];
            var stride = positionAttrBuf.ByteStride.HasValue ? positionAttrBuf.ByteStride.Value : 0;
            using (var reader = new BinaryReader(new MemoryStream(data.ToArray())))
            {
                for (var p = 0; p < positionAttr.Count; ++p)
                {
                    ret[p].X = reader.ReadSingle();
                    ret[p].Y = reader.ReadSingle();
                    ret[p].Z = reader.ReadSingle();
                    ret[p].W = reader.ReadSingle();
                }
            }

            return ret;
        }
        private static Vector4[] ReadVec4s(ref Gltf gltf, ref MeshPrimitive primitive, string attr, Stream file)
        {
            var positionAttr = gltf.Accessors[primitive.Attributes[attr]];
            if (positionAttr.Type != Accessor.TypeEnum.VEC4)
                throw new Exception("Error! " + attr + " is not of type VEC4 in " + gltf.ToString());
            var positionAttrBuf = gltf.BufferViews[positionAttr.BufferView.Value];
            var buff = gltf.LoadBinaryBuffer(
                positionAttrBuf.Buffer,
                (s)=> { return file; }
                ).AsMemory();
            var data = buff.Slice(positionAttrBuf.ByteOffset + positionAttr.ByteOffset, positionAttrBuf.ByteLength);
            var ret = new Vector4[positionAttr.Count];
            var stride = positionAttrBuf.ByteStride.HasValue ? positionAttrBuf.ByteStride.Value : 0;
            using (var reader = new BinaryReader(data))
            {
                for (var p = 0; p < positionAttr.Count; ++p)
                {
                    ret[p].X = reader.ReadSingle();
                    ret[p].Y = reader.ReadSingle();
                    ret[p].Z = reader.ReadSingle();
                    ret[p].W = reader.ReadSingle();
                }
            }

            return ret;
        }

        private static Vector3[] ReadVec3s(ref Gltf gltf, ref MeshPrimitive primitive, string attr, string filename)
        {
            var positionAttr = gltf.Accessors[primitive.Attributes[attr]];
            if (positionAttr.Type != Accessor.TypeEnum.VEC3)
                throw new Exception("Error! " + attr + " is not of type VEC3 in " + filename);
            var positionAttrBuf = gltf.BufferViews[positionAttr.BufferView.Value];
            var buff = gltf.LoadBinaryBuffer(positionAttrBuf.Buffer, filename).AsMemory();
            var data = buff.Slice(positionAttrBuf.ByteOffset + positionAttr.ByteOffset, positionAttrBuf.ByteLength);
            var ret = new Vector3[positionAttr.Count];
            var stride = positionAttrBuf.ByteStride.HasValue ? positionAttrBuf.ByteStride.Value : 0;
            using (var reader = new BinaryReader(new MemoryStream(data.ToArray())))
            {
                for (var p = 0; p < positionAttr.Count; ++p)
                {
                    ret[p].X = reader.ReadSingle();
                    ret[p].Y = reader.ReadSingle();
                    ret[p].Z = reader.ReadSingle();
                }
            }

            return ret;
        }
        private static Vector3[] ReadVec3s(ref Gltf gltf, ref MeshPrimitive primitive, string attr, Stream file)
        {
            var positionAttr = gltf.Accessors[primitive.Attributes[attr]];
            if (positionAttr.Type != Accessor.TypeEnum.VEC3)
                throw new Exception("Error! " + attr + " is not of type VEC3 in " + gltf.ToString());
            var positionAttrBuf = gltf.BufferViews[positionAttr.BufferView.Value];
            var buff = gltf.LoadBinaryBuffer(positionAttrBuf.Buffer, (s) => { return file;});
            var data = buff.Slice(positionAttrBuf.ByteOffset + positionAttr.ByteOffset, positionAttrBuf.ByteLength);
            var ret = new Vector3[positionAttr.Count];
            var stride = positionAttrBuf.ByteStride.HasValue ? positionAttrBuf.ByteStride.Value : 0;
            using (var reader = new BinaryReader(data))
            {
                for (var p = 0; p < positionAttr.Count; ++p)
                {
                    ret[p].X = reader.ReadSingle();
                    ret[p].Y = reader.ReadSingle();
                    ret[p].Z = reader.ReadSingle();
                }
            }

            return ret;
        }

        private static Vector2[] ReadVec2s(ref Gltf gltf, ref MeshPrimitive primitive, string attr, string filename)
        {
            var positionAttr = gltf.Accessors[primitive.Attributes[attr]];
            if (positionAttr.Type != Accessor.TypeEnum.VEC2)
                throw new Exception("Error! " + attr + " is not of type VEC2 in " + filename);
            var positionAttrBuf = gltf.BufferViews[positionAttr.BufferView.Value];
            var buff = gltf.LoadBinaryBuffer(positionAttrBuf.Buffer, filename);
            var data = buff.Slice(positionAttrBuf.ByteOffset + positionAttr.ByteOffset, positionAttrBuf.ByteLength);
            var ret = new Vector2[positionAttr.Count];
            var stride = positionAttrBuf.ByteStride.HasValue ? positionAttrBuf.ByteStride.Value : 0;
            using (var reader = new BinaryReader(data))
            {
                for (var p = 0; p < positionAttr.Count; ++p)
                {
                    ret[p].X = reader.ReadSingle();
                    ret[p].Y = reader.ReadSingle();
                    p += stride;
                }
            }

            return ret;
        }
        private static Vector2[] ReadVec2s(ref Gltf gltf, ref MeshPrimitive primitive, string attr, Stream file)
        {
            var positionAttr = gltf.Accessors[primitive.Attributes[attr]];
            if (positionAttr.Type != Accessor.TypeEnum.VEC2)
                throw new Exception("Error! " + attr + " is not of type VEC2 in " + gltf.ToString());
            var positionAttrBuf = gltf.BufferViews[positionAttr.BufferView.Value];
            var buff = gltf.LoadBinaryBuffer(positionAttrBuf.Buffer, (s)=> { return file; });
            var data = buff.Slice(positionAttrBuf.ByteOffset + positionAttr.ByteOffset, positionAttrBuf.ByteLength);
            var ret = new Vector2[positionAttr.Count];
            var stride = positionAttrBuf.ByteStride.HasValue ? positionAttrBuf.ByteStride.Value : 0;
            using (var reader = new BinaryReader(data))
            {
                for (var p = 0; p < positionAttr.Count; ++p)
                {
                    ret[p].X = reader.ReadSingle();
                    ret[p].Y = reader.ReadSingle();
                    p += stride;
                }
            }

            return ret;
        }

        private static int[] ReadIndices(ref Gltf gltf, ref MeshPrimitive primitive, string filename)
        {
            var attr = gltf.Accessors[primitive.Indices.Value];
            var attrBuf = gltf.BufferViews[attr.BufferView.Value];
            var buff = gltf.LoadBinaryBuffer(attrBuf.Buffer, filename).AsMemory();
            var data = buff.Slice(attrBuf.ByteOffset + attr.ByteOffset, attrBuf.ByteLength);
            var ret = new int[attr.Count];
            using (var reader = new BinaryReader(data))
            {
                for (var p = 0; p < attr.Count; ++p)
                    switch (attr.ComponentType)
                    {
                        case Accessor.ComponentTypeEnum.BYTE:
                            ret[p] = reader.ReadByte();
                            break;
                        case Accessor.ComponentTypeEnum.SHORT:
                            ret[p] = reader.ReadInt16();
                            break;
                        case Accessor.ComponentTypeEnum.UNSIGNED_BYTE:
                            ret[p] = reader.ReadByte();
                            break;
                        case Accessor.ComponentTypeEnum.UNSIGNED_SHORT:
                            ret[p] = reader.ReadUInt16();
                            break;
                        case Accessor.ComponentTypeEnum.UNSIGNED_INT:
                            ret[p] = (int)reader.ReadUInt32();
                            break;
                        default:
                            ret[p] = reader.ReadInt32();
                            break;
                    }
            }

            return ret;
        }
        private static int[] ReadIndices(ref Gltf gltf, ref MeshPrimitive primitive, Stream file)
        {
            var attr = gltf.Accessors[primitive.Indices.Value];
            var attrBuf = gltf.BufferViews[attr.BufferView.Value];
            var buff = gltf.LoadBinaryBuffer(attrBuf.Buffer, (s)=> { return file; });
            var data = buff.Slice(attrBuf.ByteOffset + attr.ByteOffset, attrBuf.ByteLength);
            var ret = new int[attr.Count];
            using (var reader = new BinaryReader(data))
            {
                for (var p = 0; p < attr.Count; ++p)
                    switch (attr.ComponentType)
                    {
                        case Accessor.ComponentTypeEnum.BYTE:
                            ret[p] = reader.ReadByte();
                            break;
                        case Accessor.ComponentTypeEnum.SHORT:
                            ret[p] = reader.ReadInt16();
                            break;
                        case Accessor.ComponentTypeEnum.UNSIGNED_BYTE:
                            ret[p] = reader.ReadByte();
                            break;
                        case Accessor.ComponentTypeEnum.UNSIGNED_SHORT:
                            ret[p] = reader.ReadUInt16();
                            break;
                        case Accessor.ComponentTypeEnum.UNSIGNED_INT:
                            ret[p] = (int)reader.ReadUInt32();
                            break;
                        default:
                            ret[p] = reader.ReadInt32();
                            break;
                    }
            }

            return ret;
        }

        private static Stream TextureResolver(string filename)
        {
            if (!RTextures.Textures.ContainsKey(filename))
            {
                var s = REngine.Instance.FileSystem.GetPackageContent(filename);
                if (s == null || s.Length == 0)
                {
                    return RFileSystem.Instance.GetFile(filename);
                }

                return s;
            }

            return RFileSystem.Instance.GetFile(filename);
        }


        private static RTexture ReadTexture(ref Gltf gltf, int index)
        {
            var s = gltf.OpenImageFile(index, TextureResolver);
            if (s != null)
            {
                var tex = gltf.Textures[index];
                var reader = new BinaryReader(s);
                var data = new List<byte>();
                var buf = new byte[1];
                while (reader.Read(buf, 0, buf.Length) != 0) data.Add(buf[0]);
                return RTextures.Instance.CreateTexture<RTexture2D>(data.ToArray(), tex.Name, false);
            }

            return null;
        }

        private static RMaterial ReadMaterial(ref Gltf gltf, ref Material mat)
        {
            var material = RMaterials.Instance.CreateMaterial(mat.Name);
            var pbr = mat.PbrMetallicRoughness;
            material.Color = RColor.FromArray(pbr.BaseColorFactor);
            var basetexture = ReadTexture(ref gltf, pbr.BaseColorTexture.Index);
            material.SetTexture(RTextureLayer.TEXTURE0, basetexture);
            var normaltexture = ReadTexture(ref gltf, mat.NormalTexture.Index);
            material.SetTexture(RTextureLayer.TEXTURE1, normaltexture);
            var metallictexture = ReadTexture(ref gltf, pbr.MetallicRoughnessTexture.Index);
            material.SetTexture(RTextureLayer.TEXTURE2, metallictexture);
            var aotexture = ReadTexture(ref gltf, mat.OcclusionTexture.Index);
            material.SetTexture(RTextureLayer.TEXTURE3, aotexture);
            material.EmissiveColor = RColor.FromArray(mat.EmissiveFactor);
            var emissivetexture = ReadTexture(ref gltf, mat.EmissiveTexture.Index);
            material.SetTexture(RTextureLayer.TEXTURE4, emissivetexture);
            material.Roughness = pbr.RoughnessFactor;
            material.Metallic = pbr.MetallicFactor;
            return material;
        }

        private static RMeshPart LoadMeshPart(ref Gltf gltf, ref MeshPrimitive primitive, String filename)
        {
            var attributes = primitive.Attributes;
            var positions = new List<Vector3>();
            var normals = new List<Vector3>();
            var tangents = new List<Vector4>();
            var uvs = new List<Vector2>();
            var indices = new List<int>();

            if (attributes.ContainsKey("POSITION"))
                positions.AddRange(ReadVec3s(ref gltf, ref primitive, "POSITION", filename));
            if (attributes.ContainsKey("NORMAL"))
                normals.AddRange(ReadVec3s(ref gltf, ref primitive, "NORMAL", filename));
            if (attributes.ContainsKey("TANGENT"))
                tangents.AddRange(ReadVec4s(ref gltf, ref primitive, "TANGENT", filename));
            if (attributes.ContainsKey("TEXCOORD"))
                uvs.AddRange(ReadVec2s(ref gltf, ref primitive, "TEXCOORD", filename));
            if (attributes.ContainsKey("TEXCOORD_0"))
                uvs.AddRange(ReadVec2s(ref gltf, ref primitive, "TEXCOORD_0", filename));
            indices.AddRange(ReadIndices(ref gltf, ref primitive, filename));

            var hasNormals = false;
            var hasTangents = false;
            var hasUvs = false;
            if (normals.Count == positions.Count)
                hasNormals = true;
            if (tangents.Count == positions.Count)
                hasTangents = true;
            if (uvs.Count == positions.Count)
                hasUvs = true;

            var vertexData = new RVertexData[positions.Count];
            for (var i = 0; i < positions.Count; ++i)
            {
                vertexData[i].Position = positions[i];
                if (hasNormals)
                    vertexData[i].Normal = normals[i];
                if (hasTangents)
                {
                    // generate physically accurate tangent and bitangent factors for TBN matrix.
                    var t = new Vector3(tangents[i].X, tangents[i].Y, tangents[i].Z);
                    var n = vertexData[i].Normal;
                    var b = Vector3.Cross(n, t) * tangents[i].W;

                    vertexData[i].Tangent = t;
                    vertexData[i].Bitangent = b;
                }
            }

            if (!hasTangents)
            {
                // model didn't include tangent space data. Let's pre-compute this data to satisfy our RVertexData declarations.
                var t = new Vector3[positions.Count];
                var b = new Vector3[positions.Count];
                CalculateTangents(positions, indices, normals, uvs, out t, out b);
                for (var i = 0; i < positions.Count; ++i)
                {
                    vertexData[i].Tangent = t[i];
                    vertexData[i].Bitangent = b[i];
                }
            }

            var vertexBuffer = new RVertexBuffer(typeof(RVertexData), vertexData.Length, RBufferUsage.None);
            vertexBuffer.SetData(vertexData);
            var indexBuffer = new RIndexBuffer(typeof(int), indices.Count, RBufferUsage.None);
            indexBuffer.SetData(indices.ToArray());
            var part = new RMeshPart();
            part.VertexBuffer = vertexBuffer;
            part.IndexBuffer = indexBuffer;
            part.BoundingBox = BoundingBox.CreateFromPoints(positions);

            return part;
        }
        private static RMeshPart LoadMeshPart(ref Gltf gltf, ref MeshPrimitive primitive, Stream file)
        {
            var attributes = primitive.Attributes;
            var positions = new List<Vector3>();
            var normals = new List<Vector3>();
            var tangents = new List<Vector4>();
            var uvs = new List<Vector2>();
            var indices = new List<int>();

            if (attributes.ContainsKey("POSITION"))
                positions.AddRange(ReadVec3s(ref gltf, ref primitive, "POSITION", file));
            if (attributes.ContainsKey("NORMAL"))
                normals.AddRange(ReadVec3s(ref gltf, ref primitive, "NORMAL", file));
            if (attributes.ContainsKey("TANGENT"))
                tangents.AddRange(ReadVec4s(ref gltf, ref primitive, "TANGENT", file));
            if (attributes.ContainsKey("TEXCOORD"))
                uvs.AddRange(ReadVec2s(ref gltf, ref primitive, "TEXCOORD", file));
            if (attributes.ContainsKey("TEXCOORD_0"))
                uvs.AddRange(ReadVec2s(ref gltf, ref primitive, "TEXCOORD_0", file));
            indices.AddRange(ReadIndices(ref gltf, ref primitive, file));

            var hasNormals = false;
            var hasTangents = false;
            var hasUvs = false;
            if (normals.Count == positions.Count)
                hasNormals = true;
            if (tangents.Count == positions.Count)
                hasTangents = true;
            if (uvs.Count == positions.Count)
                hasUvs = true;

            var vertexData = new RVertexData[positions.Count];
            for (var i = 0; i < positions.Count; ++i)
            {
                vertexData[i].Position = positions[i];
                if (hasNormals)
                    vertexData[i].Normal = normals[i];
                if (hasTangents)
                {
                    // generate physically accurate tangent and bitangent factors for TBN matrix.
                    var t = new Vector3(tangents[i].X, tangents[i].Y, tangents[i].Z);
                    var n = vertexData[i].Normal;
                    var b = Vector3.Cross(n, t) * tangents[i].W;

                    vertexData[i].Tangent = t;
                    vertexData[i].Bitangent = b;
                }
            }

            if (!hasTangents)
            {
                // model didn't include tangent space data. Let's pre-compute this data to satisfy our RVertexData declarations.
                var t = new Vector3[positions.Count];
                var b = new Vector3[positions.Count];
                CalculateTangents(positions, indices, normals, uvs, out t, out b);
                for (var i = 0; i < positions.Count; ++i)
                {
                    vertexData[i].Tangent = t[i];
                    vertexData[i].Bitangent = b[i];
                }
            }

            var vertexBuffer = new RVertexBuffer(typeof(RVertexData), vertexData.Length, RBufferUsage.None);
            vertexBuffer.SetData(vertexData);
            var indexBuffer = new RIndexBuffer(typeof(int), indices.Count, RBufferUsage.None);
            indexBuffer.SetData(indices.ToArray());
            var part = new RMeshPart();
            part.VertexBuffer = vertexBuffer;
            part.IndexBuffer = indexBuffer;
            part.BoundingBox = BoundingBox.CreateFromPoints(positions);

            return part;
        }

        public static RMaterial[] ReadMaterials(ref Gltf gltf)
        {
            var materials = new List<RMaterial>();
            for (var i = 0; i < gltf.Materials.Length; ++i)
            {
                var mat = gltf.Materials[i];
                var m = ReadMaterial(ref gltf, ref mat);
                materials.Add(m);
            }

            return materials.ToArray();
        }

        public static void LoadSource(this RMesh rmesh, Stream stream)
        {
            var gltf = Interface.LoadModel(stream);
            if (gltf.Materials.Length == 0) RLog.Info("WARNING!! " + gltf.ToString() + " has no materials defined.");
            var materials = ReadMaterials(ref gltf);
            if (gltf.Nodes.Length > 1)
            {
                RLog.Info("WARNING!! gltf has more than 1 root node.");
                var node = gltf.Nodes[0];
                if (!node.Mesh.HasValue)
                {
                    RLog.Info("WARNING!! gltf root node doesn't have mesh data.");
                    return;
                }

                var meshId = node.Mesh.Value;
                var mesh = gltf.Meshes[meshId];
                rmesh.Name = mesh.Name;
                var meshPartsLength = mesh.Primitives.Length;
                rmesh.Parts = new List<RMeshPart>(meshPartsLength);
                for (var i = 0; i < meshPartsLength; i++)
                {
                    var primitive = mesh.Primitives[i];
                    var part = LoadMeshPart(ref gltf, ref primitive, stream);
                    if (primitive.Material.HasValue) part.Material = materials[primitive.Material.Value];
                    rmesh.Parts.Add(part);
                }

                var matrixf = node.Matrix;
                rmesh.Matrix = Matrix.FromColumnMajor(matrixf);
            }
        }
        public static void LoadSource(this RMesh rmesh, string filename)
        {
            var gltf = Interface.LoadModel(filename);
            if (gltf.Materials.Length == 0) RLog.Info("WARNING!! " + filename + " has no materials defined.");
            var materials = ReadMaterials(ref gltf);
            if (gltf.Nodes.Length > 1)
            {
                RLog.Info("WARNING!! " + filename + " has more than 1 root node.");
                var node = gltf.Nodes[0];
                if (!node.Mesh.HasValue)
                {
                    RLog.Info("WARNING!! " + filename + " root node doesn't have mesh data.");
                    return;
                }

                var meshId = node.Mesh.Value;
                var mesh = gltf.Meshes[meshId];
                rmesh.Name = mesh.Name;
                var meshPartsLength = mesh.Primitives.Length;
                rmesh.Parts = new List<RMeshPart>(meshPartsLength);
                for (var i = 0; i < meshPartsLength; i++)
                {
                    var primitive = mesh.Primitives[i];
                    var part = LoadMeshPart(ref gltf, ref primitive, filename);
                    if (primitive.Material.HasValue) part.Material = materials[primitive.Material.Value];
                    rmesh.Parts.Add(part);
                }

                var matrixf = node.Matrix;
                rmesh.Matrix = Matrix.FromColumnMajor(matrixf);
            }
            
        }
        
        private static Gltf LoadSchema(string filename)
        {
            if (!filename.EndsWith("gltf") && !filename.EndsWith("glb")) return null;

            try
            {
                var deserializedFile = Interface.LoadModel(filename);
                Assert.NotNull(deserializedFile);

                // read all buffers
                for (var i = 0; i < deserializedFile.Buffers?.Length; ++i)
                {
                    var expectedLength = deserializedFile.Buffers[i].ByteLength;

                    var bufferBytes = deserializedFile.LoadBinaryBuffer(i, filename);
                    Assert.NotNull(bufferBytes);
                }

                // open all images
                for (var i = 0; i < deserializedFile.Images?.Length; ++i)
                    using (var s = deserializedFile.OpenImageFile(i, filename))
                    {
                        Assert.NotNull(s);

                        using (var rb = new BinaryReader(s))
                        {
                            var header = rb.ReadUInt32();

                            if (header == 0x474e5089) continue; // PNG
                            if ((header & 0xffff) == 0xd8ff) continue; // JPEG

                            Assert.Fail($"Invalid image in Image index {i}");
                        }
                    }

                return deserializedFile;
            }
            catch (Exception e)
            {
                throw new Exception(filename, e);
            }
        }

        internal static void CalculateTangents(IList<Vector3> positions,
            IList<int> indices,
            IList<Vector3> normals,
            IList<Vector2> textureCoords,
            out Vector3[] tangents,
            out Vector3[] bitangents)
        {
            // Lengyel, Eric. "Computing Tangent Space Basis Vectors for an Arbitrary Mesh".
            // "Foundations of Game Engine Development, Vol 2. - 7.5 Tangent Space"
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
                    // The triangle UVs are zero sized one dimension.
                    //
                    // So we cannot calculate the vertex tangents for this
                    // one trangle, but maybe it can with other trangles.
                    continue;

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

                var sdir = new Vector3
                {
                    X = (t2 * x1 - t1 * x2) * r,
                    Y = (t2 * y1 - t1 * y2) * r,
                    Z = (t2 * z1 - t1 * z2) * r
                };

                var tdir = new Vector3
                {
                    X = (s1 * x2 - s2 * x1) * r,
                    Y = (s1 * y2 - s2 * y1) * r,
                    Z = (s1 * z2 - s2 * z1) * r
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
                var w = Vector3.Dot(Vector3.Cross(n, t), tan2[i]) < 0.0F ? -1.0F : 1.0F;
                Debug.Assert(IsFinite(w), "Bad handedness!");

                // Calculate the bitangent
                var bitangent = Vector3.Cross(n, tangent) * w;
                Debug.Assert(bitangent.IsFinite(), "Bad bitangent!");
                bitangents[i] = bitangent;
            }
        }

        private static bool IsFinite(float v)
        {
            return !float.IsInfinity(v) && !float.IsNaN(v);
        }

        private static bool IsFinite(this Vector3 v)
        {
            return IsFinite(v.X) && IsFinite(v.Y) && IsFinite(v.Z);
        }
    }
}