using Reactor.Math;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace RFont_Generator
{
    public struct Font
    {
        public int Size;
        public string Name;
        public bool Kerning;
        public int LineHeight;
        public int SpaceWidth;
        public int Ascent;
        public int Descent;
        public List<FontGlyph> Glyphs;
        public Bitmap Bitmap;

        public void Save(ref BinaryWriter stream)
        {
            stream.Write(Size);
            stream.Write(Name);
            stream.Write(Kerning);
            stream.Write(LineHeight);
            stream.Write(SpaceWidth);
            stream.Write(Ascent);
            stream.Write(Descent);
            stream.Write(Glyphs.Count);
            foreach(var glyph in Glyphs)
            {
                glyph.Save(ref stream);
            }
            stream.Close();
        }
        public void Load(ref BinaryReader stream)
        {
            Size = stream.ReadInt32();
            Name = stream.ReadString();
            Kerning = stream.ReadBoolean();
            LineHeight = stream.ReadInt32();
            SpaceWidth = stream.ReadInt32();
            Ascent = stream.ReadInt32();
            Descent = stream.ReadInt32();
            int glyph_count = stream.ReadInt32();
            Glyphs = new List<FontGlyph>();
            for(var i = 0; i < glyph_count; i++)
            {
                var glyph = new FontGlyph();
                glyph.Load(ref stream);
                Glyphs.Add(glyph);
            }
            stream.Close();

        }

        public void Render(string text, IntPtr handle)
        {
            Graphics g = Graphics.FromHwnd(handle);
            g.Clear(Color.Black);
            Vector2 pen = new Vector2();
            pen.Y += MeasureString(text).Height;
            foreach(char c in text)
            {
                if(c == ' ')
                {
                    pen.X += SpaceWidth;
                    continue;
                }
                if(c == '\t')
                {
                    pen.X += (SpaceWidth * 3);
                    continue;
                }
                FontGlyph glyph = GetGlyphForChar(c);
                System.Drawing.Rectangle dest = new System.Drawing.Rectangle();
                dest.X = (int)(pen.X + glyph.Offset.X);
                dest.Y = (int)pen.Y - ((int)glyph.Offset.Y);
                dest.Width = glyph.Bounds.Width;
                dest.Height = glyph.Bounds.Height;
                g.DrawImage(Bitmap, dest, glyph.Bounds, GraphicsUnit.Pixel);
                pen.X += glyph.Advance;

            }
        }

        public Reactor.Math.Rectangle MeasureString(string text)
        {
            Reactor.Math.Rectangle r = new Reactor.Math.Rectangle();
            foreach(char c in text)
            {
                if(c == ' ')
                {
                    r.Width += SpaceWidth;
                    continue;
                }
                if(c == '\t')
                {
                    r.Width += (SpaceWidth * 3);
                    continue;
                }
                FontGlyph glyph = GetGlyphForChar(c);
                r.Height = Math.Max(r.Height, glyph.Bounds.Height);
                r.Width += (int)glyph.Offset.X;
            }
            return r;
        }

        FontGlyph GetGlyphForChar(char ch)
        {
            foreach(var g in Glyphs)
            {
                if (g.CharIndex == (int)ch)
                    return g;
            }
            throw new InvalidOperationException(String.Format("Character {0} not found in character map", ch));
        }

    }
}
