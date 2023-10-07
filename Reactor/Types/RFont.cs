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
using System.IO;
using System.Reflection;
using Reactor.Fonts;
using Reactor.Geometry;
using Reactor.Loaders;
using Reactor.Math;
using Reactor.Platform.OpenGL;

namespace Reactor.Types
{
    public class RFont
    {
        internal static RFont Default = new RFont(RFontResources.SystemFont);
        private BitmapFont _face;
        private int _texWidth;
        internal RVertexData2D[] quadVerts = new RVertexData2D[4];
        public List<RTexture2D> Textures = new List<RTexture2D>();

        public RFont()
        {
        }

        internal RFont(BitmapFont face)
        {
            _face = face;
            Name = _face.FamilyName;
            FontSize = _face.FontSize;
            Scale = 1.0f;
            Generate();
            REngine.CheckGLError();
        }

        public string Name { get; internal set; }
        public int FontSize { get; internal set; }
        public float Scale { get; set; }

        ~RFont()
        {
            foreach (var t in Textures) t.Dispose();
        }

        internal void Load(ref BinaryReader stream)
        {
            _face = BitmapFontLoader.LoadBinary(stream.BaseStream);
        }

        internal void Generate()
        {
            foreach (var page in _face.Pages)
                if (RFontResources.IsInternalResource($"Reactor.Resources.{page.FileName}"))
                {
                    var texture = new RTexture2D();
                    texture.LoadFromData(RFontResources.GetBytes(
                        $"Reactor.Resources.{page.FileName}"
                    ), page.FileName, false);
                    Textures.Add(texture);
                    texture.SetTextureMagFilter(RTextureMagFilter.Linear);
                    texture.SetTextureMinFilter(RTextureMinFilter.Linear);
                    texture.SetTextureWrapMode(RTextureWrapMode.Clamp, RTextureWrapMode.Clamp);
                    REngine.CheckGLError();
                }
                else
                {
                    var texture = new RTexture2D();
                    texture.LoadFromDisk(page.FileName);
                    Textures.Add(texture);
                    texture.SetTextureMagFilter(RTextureMagFilter.Nearest);
                    texture.SetTextureMinFilter(RTextureMinFilter.Nearest);
                    texture.SetTextureWrapMode(RTextureWrapMode.Repeat, RTextureWrapMode.Repeat);
                    REngine.CheckGLError();
                }

            _texWidth = Textures[0].Bounds.Height;
        }

        private Vector4 GetUVs(char c)
        {
            var ch = _face[c];
            var x = _face[c].X;
            var y = _face[c].Y;
            var w = _face[c].Width;
            var h = _face[c].Height;
            return new Vector4(x, y, w, h) / _texWidth;
        }

        internal void Render(ref RShader shader, ref RVertexBuffer vertexBuffer, ref RIndexBuffer indexBuffer,
            string text, Vector2 location, RColor color, Matrix matrix)
        {
            var lh = _face.LineHeight;
            var pen = location;
            //pen.Y += lh; // OpenGL's origin is lower-left. TODO: Handle this edge case for when we aren't using OpenGL...
            var x = pen.X;
            var prev = ' ';

            foreach (var c in text)
            {
                var k = _face.GetKerning(prev, c);

                var dest = new Rectangle();
                dest.X = (int)pen.X + (int)(_face[c].XOffset * Scale + k * Scale);
                dest.Y = (int)pen.Y + (int)(_face[c].YOffset * Scale);
                dest.Width = (int)(_face[c].Width * Scale);
                dest.Height = (int)(_face[c].Height * Scale);

                if (c == '\r') continue;
                if (c == '\n') // new line so we drop down a line height; TODO: check to see if we are still within bounds of framebuffer texture we are drawing to.
                {
                    pen.X = x;
                    pen.Y += _face.LineHeight * Scale;
                    continue;
                }

                if (c == ' ')
                {
                    pen.X += (_face[c].XOffset + _face[c].XAdvance + k) * Scale;
                    continue;
                }

                if (c == '\t')
                {
                    pen.X += (_face[c].XOffset + _face[c].XAdvance) * Scale;
                    continue;
                }

                var t = Textures[_face[c].TexturePage];

                shader.SetSamplerValue(RTextureLayer.TEXTURE0, t);
                SetVerts(dest, GetUVs(c));
                vertexBuffer.SetData(quadVerts);
                vertexBuffer.Bind();
                vertexBuffer.BindVertexArray();
                indexBuffer.Bind();
                vertexBuffer.VertexDeclaration.Apply(shader, IntPtr.Zero);
                GL.DrawElements(BeginMode.Triangles, indexBuffer.IndexCount, DrawElementsType.UnsignedShort,
                    IntPtr.Zero);
                REngine.CheckGLError();
                indexBuffer.Unbind();
                vertexBuffer.Unbind();
                vertexBuffer.UnbindVertexArray();
                pen.X += _face[c].XAdvance * Scale;
            }
        }

        public Rectangle MeasureString(string text)
        {
            var size = _face.MeasureFont(text);
            return new Rectangle(0, 0, size.Width, size.Height);
        }


        private void SetVerts(Rectangle placement, Vector4 UVs)
        {
            quadVerts[0].Position = new Vector2(placement.X, placement.Y);
            quadVerts[0].TexCoord = new Vector2(UVs.X, UVs.Y);
            //quadVerts[0].TexCoord = new Vector2(                          0, 0      );
            quadVerts[1].Position = new Vector2(placement.X + placement.Width, placement.Y);
            quadVerts[1].TexCoord = new Vector2(UVs.X + UVs.Z, UVs.Y);
            quadVerts[2].Position = new Vector2(placement.X + placement.Width, placement.Y + placement.Height);
            quadVerts[2].TexCoord = new Vector2(UVs.X + UVs.Z, UVs.Y + UVs.W);
            quadVerts[3].Position = new Vector2(placement.X, placement.Y + placement.Height);
            quadVerts[3].TexCoord = new Vector2(UVs.X, UVs.Y + UVs.W);
        }
    }

    internal static class RFontResources
    {
        internal static Assembly Assembly = Assembly.GetAssembly(typeof(RFontResources));

        internal static BitmapFont SystemFont = GetResource("Reactor.Resources.coders_crux.fnt");

        internal static BitmapFont GetResource(string resource)
        {
            var stream = Assembly.GetManifestResourceStream(resource);
            var face = BitmapFontLoader.LoadBinary(stream);
            return face;
        }

        internal static bool IsInternalResource(string resource)
        {
            var stream = Assembly.GetManifestResourceStream(resource);
            if (stream == null)
                return false;
            if (stream.Length > 0) return true;

            return false;
        }

        internal static byte[] GetBytes(string resource)
        {
            var stream = Assembly.GetManifestResourceStream(resource);
            byte[] bytes;
            if (stream != null)
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    bytes = binaryReader.ReadBytes((int)stream.Length);
                }

                return bytes;
            }

            return null;
        }
    }
}