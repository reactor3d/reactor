using Reactor.Math;
using SharpFont;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RFont_Generator
{
    public class FontGenerator
    {
        public static Library FreeTypeLibrary = new Library();
        public Font Build(string filename, int size, int dpi)
        {
            Face face = new Face(FreeTypeLibrary, filename);
            face.SetCharSize(0, new Fixed26Dot6(size), 0, (uint)dpi);
            Font font = new Font();
            font.Name = face.FamilyName;
            face.LoadChar((uint)32, (LoadFlags.Render | LoadFlags.Monochrome | LoadFlags.Pedantic), LoadTarget.Normal);
            font.SpaceWidth = face.Glyph.Metrics.HorizontalAdvance.ToInt32();
            font.LineHeight = face.Height;
            font.Kerning = face.HasKerning;
            font.Size = size;
            font.Ascent = face.Ascender >> 6;
            font.Descent = face.Descender >> 6;
            font.Glyphs = new List<FontGlyph>();

            
            for(int i = 33; i<126; i++)
            {


                uint charIndex = face.GetCharIndex((uint)i);
                face.LoadGlyph(charIndex, (LoadFlags.Render | LoadFlags.Color | LoadFlags.Pedantic ), LoadTarget.Normal);
                if (face.Glyph.Bitmap.Width == 0)
                    continue;
                FontGlyph glyph = new FontGlyph();
                glyph.bitmap = face.Glyph.Bitmap.ToGdipBitmap(Color.White);
                glyph.Bounds = new Reactor.Math.Rectangle(0, 0, glyph.bitmap.Width, glyph.bitmap.Height);
                glyph.CharIndex = i;
                glyph.Offset = new Vector2(face.Glyph.Metrics.HorizontalBearingX.ToInt32(), face.Glyph.Metrics.HorizontalBearingY.ToInt32());
                glyph.Advance = face.Glyph.Advance.X.ToInt32();
                
                font.Glyphs.Add(glyph);
            }
            font.Glyphs.Sort(new FontGlyphSizeSorter());
            font.Glyphs.Reverse();
            var missed = -1;
            var width = 16;
            Bitmap b = new Bitmap(1, 1);
            while(missed!=0)
            {
                Application.DoEvents();
                missed = 0;
                AtlasNode root = new AtlasNode();
                root.bounds = new Reactor.Math.Rectangle(0, 0, width, width);
                b.Dispose();
                b = new Bitmap(width, width);
                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.Transparent);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                for (var i = 0; i < font.Glyphs.Count; i++)
                {
                    FontGlyph glyph = font.Glyphs[i];
                    AtlasNode result = root.Insert(glyph.Bounds);

                    if (result != null)
                    {
                        Reactor.Math.Rectangle bounds = result.bounds;
                        //g.DrawImageUnscaledAndClipped(glyph.bitmap, bounds);
                        g.DrawImage(glyph.bitmap, bounds);
                        glyph.Bounds = bounds;
                        glyph.UVBounds = new Vector4((float)bounds.X / (float)width, (float)bounds.Y / (float)width, (float)bounds.Width / (float)width, (float)bounds.Height / (float)width);
                        font.Glyphs[i] = glyph;
                    }
                    else
                    {
                        missed += 1;
                        break;
                    }
                    
                    
                }
                width += 1;
            }

            if (missed > 0)
                MessageBox.Show("Oops, looks like there wasn't enough room!\r\nMissed: " + missed, "Missed Glyphs", MessageBoxButtons.OK);

            font.Bitmap = b;
            return font;
        }
    }
}
