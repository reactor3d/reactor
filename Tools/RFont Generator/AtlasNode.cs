using Reactor.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RFont_Generator
{
    class AtlasNode
    {
        public AtlasNode left;
        public AtlasNode right;
        public Rectangle bounds;
        public bool filled;

        public AtlasNode()
        {
            left = null;
            right = null;
            this.bounds = Rectangle.Empty;
            filled = false;
        }
        public AtlasNode Insert(Rectangle sBounds)
        {
            if (this.left != null)
            {
                AtlasNode newNode = this.left.Insert(sBounds);
                if (newNode != null)
                    return newNode;

                return this.right.Insert(sBounds);
            }
            else
            {
                if (this.filled) // occupied!
                    return null;
                if (this.bounds.Width < sBounds.Width || this.bounds.Height < sBounds.Height)  // bounds are too small for this image
                    return null;
                if (this.bounds.Width == sBounds.Width && this.bounds.Height == sBounds.Height)
                {
                    // fits just right!
                    filled = true;
                    return this;
                }

                //otherwise split and traverse further...
                this.left = new AtlasNode();
                this.right = new AtlasNode();

                // decide which way to split
                int dw = this.bounds.Width - sBounds.Width;
                int dh = this.bounds.Height - sBounds.Height;
                if (dw > dh)
                {
                    this.left.bounds = new Rectangle(this.bounds.X, this.bounds.Y, sBounds.Width, this.bounds.Height);
                    this.right.bounds = new Rectangle(this.bounds.X + sBounds.Width, this.bounds.Y, this.bounds.Width - sBounds.Width, this.bounds.Height);
                }
                else
                {
                    this.left.bounds = new Rectangle(this.bounds.X, this.bounds.Y, this.bounds.Width, sBounds.Height);
                    this.right.bounds = new Rectangle(this.bounds.X, this.bounds.Y + sBounds.Height, this.bounds.Width, this.bounds.Height - sBounds.Height);
                }

                return this.left.Insert(sBounds);

            }
        }
    }
    internal class FontGlyphSizeSorter : IComparer<FontGlyph>
    {
        public int Compare(FontGlyph left, FontGlyph right)
        {
            if (left.Bounds.Height > right.Bounds.Height)
                return 1;
            else
                return -1;
            return 0;

        }


    }
}
