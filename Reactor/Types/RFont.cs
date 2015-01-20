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
using System.Drawing;
using System.Text;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using SharpFont;
using System.IO;
using Reactor.Math;


namespace Reactor.Types
{
    public class RFont
    {
        RTextureAtlas atlas;
        internal string FamilyName;
        internal Face font;
        List<RTextureSprite> glyphs;

        public int LineHeight
        {
            get; set;

        }
        public int SpaceWidth
        {
            get; set;
        }
        public int Size
        {
            get; set;
        }
        public int DPI
        {
            get; set;
        }
        public RTextureAtlas Atlas
        {
            get { return atlas; } set { atlas = value; }
        }
        public RFont(string fileName)
        {
        }

        public RFont()
        {
            RLog.Info("Creating default system font.");
            font = RFontResources.SystemFont;
            LineHeight = font.Height >> 6;
            DPI=72;
            BuildTextureMap(36);

        }

        public void Load(string filename, int size)
        {
            font = new Face(RFontResources.FreetypeLibrary, filename);
            LineHeight = font.Height >> 6;
            FamilyName = font.FamilyName;
            BuildTextureMap(size);
        }

        public void BuildTextureMap(int Size)
        {
            if(this.Size == Size)
                return;
            else
                this.Size = Size;
            if(glyphs != null)
            {
                foreach(RTextureGlyph g in glyphs)
                {
                    g.Dispose();
                }
            }
            //SpaceWidth = Size;
            font.SetCharSize(0, Size*64,(uint) DPI,(uint) DPI);

            string table = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-=!@#$%^&*()_+~`\\|]}[{'\";:/?.>,<";
            glyphs = new List<RTextureSprite>();
            font.LoadChar((uint)32, (LoadFlags.Render|LoadFlags.Color|LoadFlags.Pedantic), LoadTarget.Normal);
            SpaceWidth = font.Glyph.Metrics.HorizontalAdvance >> 6;
            for(int i=33; i < 126; i++)
            {

                font.LoadChar((uint)i, (LoadFlags.Render|LoadFlags.Color|LoadFlags.Pedantic), LoadTarget.Normal);
                SpaceWidth = font.Glyph.Metrics.HorizontalAdvance >> 6;
                //font.Glyph.RenderGlyph(RenderMode.Normal);

                glyphs.Add(new RTextureGlyph(font.Glyph, (char)i));
            }
            atlas = new RTextureAtlas();

            atlas.BuildAtlas(glyphs);

        }
        internal Vector2 Kerning(char a, char b)
        {
            if(font.HasKerning)
            {
                uint first = font.GetCharIndex((uint)a);
                uint last = font.GetCharIndex((uint)b);
                FTVector k = font.GetKerning((uint)first, (uint)last, KerningMode.Default);
                return new Vector2(k.X >> 6, k.Y >> 6);
            }
            return Vector2.Zero;
        }
        internal RTextureGlyph GetGlyph(char c)
        {
            foreach(RTextureGlyph glyph in glyphs)
            {
                if(glyph.keyCode == c)
                    return glyph;
            }
            return null;
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


}

