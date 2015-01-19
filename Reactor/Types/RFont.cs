//
// RFont.cs
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
using System.Reflection;
using System.Collections.Generic;
using SharpFont;
using System.IO;
using OpenTK.Graphics.OpenGL;
using Reactor.Geometry;
using Reactor.Math;

namespace Reactor.Types
{
    public class RFont : RTexture
    {

        internal string FamilyName;
        internal Face font;
        public int width;
        public int height;
        //public int texture;
        int size;
        font_glyph[] glyphs = new font_glyph[128];
        RVertexBuffer buffer;
        public RFont(string fileName, int size)
        {
            RLog.Info("Creating font from :"+fileName);
            Load(fileName);

        }

        public RFont()
        {
            RLog.Info("Creating default system font.");
            font = RFontResources.SystemFont;
            size = 16;
            DoTextureUpload();
            REngine.CheckGLError();


            //BuildTextureMap(64);

        }

        public void Load(string filename)
        {
            font = new Face(RFontResources.FreetypeLibrary, filename);
            FamilyName = font.FamilyName;
            DoTextureUpload();
        }

        private void DoTextureUpload()
        {
            font.SetCharSize(0, size << 6, 0, 300);
            int w = 0, h = 0;
            for(uint i = 32; i < 128; i++) {
                font.LoadChar(i, LoadFlags.Render, LoadTarget.Light);
                GlyphSlot g = font.Glyph;
                if(g.Bitmap != null)
                {
                    w += g.Bitmap.Width;
                    h = System.Math.Max(h, g.Bitmap.Rows);
                }



            }
            width = w;
            height = h;
            int atlas_width = w;
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.GenTextures(1, out Id);
            REngine.CheckGLError();
            GL.BindTexture(TextureTarget.Texture2D, Id);
            REngine.CheckGLError();
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);
            REngine.CheckGLError();
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.R8, w, h, 0, PixelFormat.Red, PixelType.UnsignedByte, IntPtr.Zero);
            REngine.CheckGLError();
            int x = 0;
            for(int i = 32; i < 128; i++) {
                font.LoadChar((uint)i, LoadFlags.Render, LoadTarget.Light);
                GlyphSlot g = font.Glyph;
                g.RenderGlyph(RenderMode.Light);
                if(g != null)
                if(g.Bitmap.Buffer != IntPtr.Zero)
                {
                    GL.TexSubImage2D<byte>(TextureTarget.Texture2D, 0, x, 0, g.Bitmap.Width, g.Bitmap.Rows, PixelFormat.Red, PixelType.UnsignedByte, g.Bitmap.BufferData);
                //glTexSubImage2D(GL_TEXTURE_2D, 0, x, 0, g->bitmap.width, g->bitmap.rows, GL_ALPHA, GL_UNSIGNED_BYTE, g->bitmap.buffer);
                    REngine.CheckGLError();
                    glyphs[i].ax = g.Advance.X >> 6;
                    glyphs[i].ay = g.Advance.Y >> 6;

                    glyphs[i].bw = g.Bitmap.Width;
                    glyphs[i].bh = g.Bitmap.Rows;

                    glyphs[i].bl = g.BitmapLeft;
                    glyphs[i].bt = g.BitmapTop;

                    glyphs[i].tx = (float)x / w;
                


                    x += g.Bitmap.Width;
                }
            }

            GL.BindTexture(TextureTarget.Texture2D, 0);
            REngine.CheckGLError();

        }

        internal void RenderText(RShader shader, string text, float x, float y, float sx, float sy)
        {

            RVertexData2D[] coords = new RVertexData2D[6*text.Length];

            int n = 0;
            foreach(char c in text)
            {
                float x2 =  x + glyphs[c].bl * sx;
                float y2 = -y - glyphs[c].bt * sy;
                float w = glyphs[c].bw * sx;
                float h = glyphs[c].bh * sy;

                /* Advance the cursor to the start of the next character */
                x += glyphs[c].ax * sx;
                y += glyphs[c].ay * sy;

                /* Skip glyphs that have no pixels */
                if(w == 0 || h == 0)
                    continue;

                coords[n++] = new RVertexData2D(new Vector2(x2,-y2), new Vector2(glyphs[c].tx,0));
                coords[n++] = new RVertexData2D(new Vector2(x2 + w, -y2), new Vector2(glyphs[c].tx + glyphs[c].bw / width,0));
                coords[n++] = new RVertexData2D(new Vector2(x2,-y2 - h), new Vector2(glyphs[c].tx, glyphs[c].bh / height)); //remember: each glyph occupies a different amount of vertical space
                coords[n++] = new RVertexData2D(new Vector2(x2 + w, -y2), new Vector2(glyphs[c].tx + glyphs[c].bw / width,0));
                coords[n++] = new RVertexData2D(new Vector2(x2,-y2 - h), new Vector2(glyphs[c].tx,glyphs[c].bh / height));
                coords[n++] = new RVertexData2D(new Vector2(x2 + w, -y2 - h), new Vector2(glyphs[c].tx + glyphs[c].bw / width, glyphs[c].bh / height));

            }
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Id);
            REngine.CheckGLError();
            shader.Bind();
            shader.SetSamplerValue(RTextureLayer.DIFFUSE, this);
            buffer = new RVertexBuffer(coords[0].Declaration, coords.Length, RBufferUsage.WriteOnly);
            buffer.SetData<RVertexData2D>(coords);
            buffer.BindVertexArray();
            buffer.Bind();
            coords[0].Declaration.Apply(shader, IntPtr.Zero);
            GL.DrawArrays(PrimitiveType.Triangles, 0, buffer.VertexCount);
            REngine.CheckGLError();
            buffer.Unbind();
            buffer.UnbindVertexArray();


        }

    }
    internal static class RFontResources
    {
        internal static Library FreetypeLibrary = new Library();
        internal static Assembly Assembly = Assembly.GetAssembly(typeof(RFontResources));
        internal static Face GetResource(string resource){
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Assembly.GetManifestResourceStream(resource));
            byte[] buffer = new byte[reader.BaseStream.Length];
            reader.Read(buffer, 0, buffer.Length);
            reader.Close();

            Face face = FreetypeLibrary.NewMemoryFace(buffer, 0);
            return face;

        }

        internal static Face SystemFont = GetResource("Reactor.Fonts.coders_crux.ttf");

    }

    public struct font_glyph
    {
        public float ax; // advance.x
        public float ay; // advance.y

        public float bw; // bitmap.width;
        public float bh; // bitmap.rows;

        public float bl; // bitmap_left;
        public float bt; // bitmap_top;

        public float tx; // x offset of glyph in texture coordinates
    }


}

