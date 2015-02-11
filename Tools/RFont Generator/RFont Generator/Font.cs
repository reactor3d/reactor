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
        public bool Bold;
        public bool Italic;
        public int LineHeight;
        public int SpaceWidth;
        public List<FontGlyph> Glyphs;
        public Bitmap Bitmap;

        public void Save(ref BinaryWriter stream)
        {
            stream.Write(Size);
            stream.Write(Name);
            stream.Write(Kerning);
            stream.Write(Bold);
            stream.Write(Italic);
            stream.Write(LineHeight);
            stream.Write(SpaceWidth);
            foreach(var glyph in Glyphs)
            {
                glyph.Save(ref stream);
            }
            stream.Close();
        }
    }
}
