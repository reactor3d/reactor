using Reactor.Math;
using System;
using System.Collections.Generic;
using Drawing = System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace RFont_Generator
{
    public struct FontGlyph
    {
        public int CharIndex;
        public Rectangle Bounds;
        public Rectangle AtlasBounds;
        public Vector2 Offset;
        public int Advance;
        public Drawing.Bitmap bitmap;
        internal void Save(ref BinaryWriter stream)
        {
            stream.Write(CharIndex);
            stream.Write(Bounds.X);
            stream.Write(Bounds.Y);
            stream.Write(Bounds.Width);
            stream.Write(Bounds.Height);
            
            stream.Write(AtlasBounds.X);
            stream.Write(AtlasBounds.Y);
            stream.Write(AtlasBounds.Width);
            stream.Write(AtlasBounds.Height);

            stream.Write(Offset.X);
            stream.Write(Offset.Y);

            stream.Write(Advance);
        }
    }
}
