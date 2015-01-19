//
// RTextureAtlas.cs
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
using Reactor.Math;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace Reactor.Types
{
    public class RTextureAtlas : RTexture
    {
        const int FixedWidth = 64;
        public RTextureAtlas()
        {
            textureTarget = TextureTarget.Texture2D;
            pixelFormat = RPixelFormat.Rgba;
        }

        public void BuildAtlas(List<RTextureSprite> textures)
        {
            textures.Sort(new RTextureSizeSorter());
            textures.Reverse();
            Rectangle largest = textures[0].Bounds;
            int cellSize = System.Math.Max(largest.Width, largest.Height);
            double sqr = System.Math.Sqrt((double)textures.Count);
            int remainder = ((int)(sqr*100) % 100);
            /*Rectangle bounds = new Rectangle();
            foreach(RTextureSprite sprite in textures)
            {
                bounds = Rectangle.Union(bounds, sprite.Bounds);
            }
            RTextureSprite previous = null;
            foreach(RTextureSprite sprite in textures)
            {
                if(previous == null){
                    sprite.Offset.X = 0;
                    previous = sprite;
                }
                else {
                    sprite.Offset.X = previous.Offset.X + previous.Bounds.Width;
                    sprite.Bounds.Offset(sprite.Offset);
                    previous = sprite;
                }

                bounds = Rectangle.Union(bounds, previous.Bounds);
            }
            while(!this.isPowerOfTwo((uint)bounds.Height))
                bounds.Height += 1;
            while(!this.isPowerOfTwo((uint)bounds.Width))
                bounds.Width += 1;
                */
            AtlasNode root = new AtlasNode();
            root.bounds = new Rectangle(0, 0, 512, 512);
            uint index = 0;
            int unclaimed = 0;
            foreach(RTextureSprite sprite in textures)
            {
                try{
                    AtlasNode node = root.Insert(sprite.Bounds);
                    if(node != null)
                    {
                        RLog.Info(node.ToString());
                        sprite.ScaledBounds = node.bounds;

                        Pack(sprite);
                    } else {
                        unclaimed++;
                    }

                }
                catch(Exception e)
                {
                    RLog.Error(e);
                }
                index++;

            }

        }

        private void Pack(RTextureSprite sprite)
        {

        }
    }
    internal class AtlasNode
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
            if(this.left != null)
            {
                AtlasNode newNode = this.left.Insert(sBounds);
                if(newNode != null)
                    return newNode;

                return this.right.Insert(sBounds);
            }
            else
            {
                if(this.filled) // occupied!
                    return null;
                if(this.bounds.Width < sBounds.Width || this.bounds.Height < sBounds.Height)  // bounds are too small for this image
                    return null;
                if(this.bounds.Width == sBounds.Width && this.bounds.Height == sBounds.Height)
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
                    if (dw > dh) {
                        this.left.bounds = new Rectangle(this.bounds.X, this.bounds.Y, sBounds.Width, this.bounds.Height);
                        this.right.bounds = new Rectangle(this.bounds.X+sBounds.Width, this.bounds.Y, this.bounds.Width - sBounds.Width, this.bounds.Height);
                    }
                    else {
                        this.left.bounds = new Rectangle(this.bounds.X, this.bounds.Y, this.bounds.Width, sBounds.Height);
                        this.right.bounds = new Rectangle(this.bounds.X, this.bounds.Y+sBounds.Height, this.bounds.Width, this.bounds.Height - sBounds.Height);
                    }

                    return this.left.Insert(sBounds);

            }
        }
    }
    internal class RTextureSizeSorter : IComparer<RTextureSprite>
    {
        public int Compare(RTextureSprite left, RTextureSprite right)
        {
            if(left.Bounds.Height > right.Bounds.Height)
                return 1;
            else
                return -1;
            return 0;
            
        }


    }
}

