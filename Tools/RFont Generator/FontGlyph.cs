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
        public Vector4 UVBounds;
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
            
            stream.Write(UVBounds.X);
            stream.Write(UVBounds.Y);
            stream.Write(UVBounds.Z);
            stream.Write(UVBounds.W);

            stream.Write(Offset.X);
            stream.Write(Offset.Y);

            stream.Write(Advance);
        }

        internal void Load(ref BinaryReader stream)
        {
            CharIndex = stream.ReadInt32();
            Bounds = new Rectangle();
            Bounds.X = stream.ReadInt32();
            Bounds.Y = stream.ReadInt32();
            Bounds.Width = stream.ReadInt32();
            Bounds.Height = stream.ReadInt32();

            UVBounds = new Vector4();
            UVBounds.X = stream.ReadSingle();
            UVBounds.Y = stream.ReadSingle();
            UVBounds.Z = stream.ReadSingle();
            UVBounds.W = stream.ReadSingle();

            Offset = new Vector2();
            Offset.X = stream.ReadSingle();
            Offset.Y = stream.ReadSingle();

            Advance = stream.ReadInt32();
        }
    }
}
