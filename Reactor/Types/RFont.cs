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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using Reactor.Geometry;
using Reactor.Math;
using Reactor.Platform.OpenGL;
using SharpFont;
using Matrix = Reactor.Math.Matrix;
using Rectangle = Reactor.Math.Rectangle;

namespace Reactor.Types
{
    public class RFont
    {
        public int Size;
        public string Name;
        public bool Kerning;
        public int LineHeight;
        public int SpaceWidth;
        public int Ascent;
        public int Descent;
        internal List<RFontGlyph> Glyphs;
        public RTexture2D Texture;
        internal RVertexData2D[] quadVerts = new RVertexData2D[4];
        internal static RFont Default = new RFont(RFontResources.SystemFont);

        public RFont()
        {
        }

        internal RFont(Face face)
        {
            Vector2 dpi = RScreen.GetDPI();
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                Generate(face, 16, (int)dpi.X);
            else
                Generate(face, 16, (int)dpi.Y);
        }
        internal void Save(ref BinaryWriter stream)
        {
            stream.Write(Size);
            stream.Write(Name);
            stream.Write(Kerning);
            stream.Write(LineHeight);
            stream.Write(SpaceWidth);
            stream.Write(Ascent);
            stream.Write(Descent);
            stream.Write(Glyphs.Count);
            foreach (var glyph in Glyphs)
            {
                glyph.Save(ref stream);
            }
            stream.Close();
        }
        internal void Load(ref BinaryReader stream)
        {
            Size = stream.ReadInt32();
            Name = stream.ReadString();
            Kerning = stream.ReadBoolean();
            LineHeight = stream.ReadInt32();
            SpaceWidth = stream.ReadInt32();
            Ascent = stream.ReadInt32();
            Descent = stream.ReadInt32();
            int glyph_count = stream.ReadInt32();
            Glyphs = new List<RFontGlyph>();
            for (var i = 0; i < glyph_count; i++)
            {
                var glyph = new RFontGlyph();
                glyph.Load(ref stream);
                Glyphs.Add(glyph);
            }
            stream.Close();


        }
        internal void Generate(string filename, int size, int dpi)
        {
            Face face = new Face(RFontResources.FreetypeLibrary, filename);
            Generate(face, size, dpi);
        }
        internal void Generate(Face face, int size, int dpi)
        {
            face.SetCharSize(0, new Fixed26Dot6(size), 0, (uint)dpi);
            Name = face.FamilyName;
            face.LoadChar((uint)32, (LoadFlags.Render | LoadFlags.Monochrome | LoadFlags.Pedantic), LoadTarget.Normal);
            SpaceWidth = face.Glyph.Metrics.HorizontalAdvance.ToInt32();
            LineHeight = face.Height >> 6;
            Kerning = face.HasKerning;
            Size = size;
            Ascent = face.Ascender >> 6;
            Descent = face.Descender >> 6;
            Glyphs = new List<RFontGlyph>();


            for (int i = 33; i < 126; i++)
            {


                uint charIndex = face.GetCharIndex((uint)i);
                face.LoadGlyph(charIndex, (LoadFlags.Render | LoadFlags.Color | LoadFlags.Pedantic | LoadFlags.CropBitmap), LoadTarget.Normal);
                if (face.Glyph.Bitmap.PixelMode == PixelMode.None)
                    continue;
                RFontGlyph glyph = new RFontGlyph();
                
                glyph.bitmap = face.Glyph.Bitmap.ToGdipBitmap(Color.White);
                glyph.Bounds = new Rectangle(0, 0, glyph.bitmap.Width, glyph.bitmap.Height);
                glyph.CharIndex = i;
                glyph.Offset = new Vector2(face.Glyph.Metrics.HorizontalBearingX.ToInt32(), face.Glyph.Metrics.HorizontalBearingY.ToInt32());
                glyph.Advance = face.Glyph.Advance.X.ToInt32();

                Glyphs.Add(glyph);
            }
            Glyphs.Sort(new FontGlyphSizeSorter());
            var missed = -1;
            var width = 16;
            Bitmap b = new Bitmap (1, 1);
            while (missed != 0)
            {
                missed = 0;
                AtlasNode root = new AtlasNode();
                root.bounds = new Rectangle(0, 0, width, width);
                b.Dispose ();
                b = new Bitmap(width, width);
                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.Transparent);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                for (var i = 0; i < Glyphs.Count; i++)
                {
                    RFontGlyph glyph = Glyphs[i];
                    AtlasNode result = root.Insert(glyph.Bounds);

                    if (result != null)
                    {
                        Rectangle bounds = result.bounds;
                        //g.DrawImageUnscaledAndClipped(glyph.bitmap, bounds);
                        g.DrawImage(glyph.bitmap, bounds);
                        glyph.Bounds = bounds;
                        glyph.UVBounds = new Vector4((float)bounds.X, (float)bounds.Y, (float)bounds.Width, (float)bounds.Height);
                        glyph.UVBounds /= (float)width;
                        Glyphs[i] = glyph;
                    }
                    else
                    {
                        missed += 1;
                        break;
                    }


                }
                width += 16;
            }
            Texture = new RTexture2D();
            Texture.LoadFromBitmap(b);
            Texture.SetTextureMagFilter(RTextureMagFilter.Linear);
            Texture.SetTextureMinFilter(RTextureMinFilter.LinearMipmapLinear);
            Texture.SetTextureWrapMode(RTextureWrapMode.ClampToBorder, RTextureWrapMode.ClampToBorder);
            REngine.CheckGLError();

        }
        internal void Render(ref RShader shader, ref RVertexBuffer vertexBuffer, ref RIndexBuffer indexBuffer, string text, Vector2 location, RColor color, Matrix matrix)
        {
            Vector2 pen = location;
            pen.Y += MeasureString(text).Height;
            float x = pen.X;
            List<RVertexData2D> quads = new List<RVertexData2D>();
            foreach (char c in text)
            {
                if(c == '\r')
                {
                    continue;
                }
                if(c == '\n')
                {
                    pen.X = x;
                    pen.Y += LineHeight;
                    continue;
                }
                if (c == ' ')
                {
                    pen.X += SpaceWidth;
                    continue;
                }
                if (c == '\t')
                {
                    pen.X += (SpaceWidth * 3);
                    continue;
                }
                RFontGlyph glyph = GetGlyphForChar(c);
                var dest = new Rectangle();
                dest.X = (int)(pen.X + glyph.Offset.X);
                dest.Y = (int)pen.Y - ((int)glyph.Offset.Y);
                dest.Width = glyph.Bounds.Width;
                dest.Height = glyph.Bounds.Height;

                vertexBuffer.SetData<RVertexData2D>(AddQuads(dest, glyph.UVBounds));
                vertexBuffer.Bind();
                vertexBuffer.BindVertexArray();
                indexBuffer.Bind();
                vertexBuffer.VertexDeclaration.Apply(shader, IntPtr.Zero);
                GL.DrawElements(BeginMode.Triangles, indexBuffer.IndexCount, DrawElementsType.UnsignedShort, IntPtr.Zero);
                REngine.CheckGLError();
                indexBuffer.Unbind();
                vertexBuffer.Unbind();
                vertexBuffer.UnbindVertexArray();
                pen.X += glyph.Advance;

            }

        }

        public Rectangle MeasureString(string text)
        {
            Rectangle r = new Rectangle();
            foreach (char c in text)
            {
                if (c == ' ')
                {
                    r.Width += SpaceWidth;
                    continue;
                }
                if (c == '\t')
                {
                    r.Width += (SpaceWidth * 3);
                    continue;
                }
                RFontGlyph glyph = GetGlyphForChar(c);
                r.Height = System.Math.Max(r.Height, glyph.Bounds.Height);
                r.Width += (int)glyph.Offset.X;
            }
            return r;
        }

        RFontGlyph GetGlyphForChar(char ch)
        {
            foreach (var g in Glyphs)
            {
                if (g.CharIndex == (int)ch)
                    return g;
            }
            throw new InvalidOperationException(String.Format("Character {0} not found in character map", ch));
        }
        RVertexData2D[] AddQuads(Rectangle placement, Vector4 UVs)
        {
            quadVerts[0].Position = new Vector2(placement.X, placement.Y);
            quadVerts[0].TexCoord = new Vector2(UVs.X, UVs.Y);
            quadVerts[1].Position = new Vector2(placement.X + placement.Width, placement.Y);
            quadVerts[1].TexCoord = new Vector2(UVs.X+UVs.Z, UVs.Y);
            quadVerts[2].Position = new Vector2(placement.X + placement.Width, placement.Y + placement.Height);
            quadVerts[2].TexCoord = new Vector2(UVs.X+UVs.Z, UVs.Y+UVs.W);
            quadVerts[3].Position = new Vector2(placement.X, placement.Y + placement.Height);
            quadVerts[3].TexCoord = new Vector2(UVs.X, UVs.Y+UVs.W);
            //vertexBuffer.SetData<RVertexData2D>(quadVerts);
            return quadVerts;
        }
    }
    internal static class RFontResources
    {
        internal static Library FreetypeLibrary = new Library();
        internal static Assembly Assembly = Assembly.GetAssembly(typeof(RFontResources));
        internal static Face GetResource(string resource)
        {
            var names = Assembly.GetManifestResourceNames();
            Stream stream = Assembly.GetManifestResourceStream(resource);
            BinaryReader reader = new BinaryReader(stream);
            byte[] buffer = new byte[reader.BaseStream.Length];
            reader.Read(buffer, 0, buffer.Length);
            reader.Close();

            Face face = FreetypeLibrary.NewMemoryFace(buffer, 0);
            return face;

        }

        internal static Face SystemFont = GetResource("Reactor.Resources.coders_crux.ttf");

    }

    internal class FontGlyphSizeSorter : IComparer<RFontGlyph>
    {
        public int Compare(RFontGlyph left, RFontGlyph right)
        {
            if (left.Bounds.Height > right.Bounds.Height)
                return -1;  // this is reversed on purpose, we want largest to smallest!
            else
                return 1;
            return 0;

        }
    }


}

