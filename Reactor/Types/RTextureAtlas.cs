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
using Reactor.Math;
using Reactor.Platform.OpenGL;

namespace Reactor.Types
{
    public class RTextureAtlas : RTexture2D
    {
        private const int FixedWidth = 64;

        public RTextureAtlas()
        {
            textureTarget = TextureTarget.Texture2D;
            pixelFormat = RPixelFormat.Rgba;
        }

        public void BuildAtlas(List<RTextureSprite> textures)
        {
            REngine.CheckGLError();
            textures.Sort(new RTextureSizeSorter());
            textures.Reverse();
            var largest = textures[0].Bounds;
            //int cellSize = System.Math.Max(largest.Width, largest.Height);
            //double sqr = System.Math.Sqrt((double)textures.Count);
            //int remainder = ((int)(sqr*100) % 100);
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
            var root = new AtlasNode();
            root.bounds = new Rectangle(0, 0, 512, 512);
            Create(512, 512, textures[0].GetPixelFormat(), RSurfaceFormat.Color);
            uint index = 0;
            var unclaimed = 0;

            REngine.CheckGLError();
            foreach (var sprite in textures)
            {
                try
                {
                    var node = root.Insert(sprite.Bounds);
                    if (node != null)
                    {
                        RLog.Info(node.ToString());
                        sprite.ScaledBounds = node.bounds;

                        //Pack(sprite, sprite.GetPixelFormat());
                    }
                    else
                    {
                        unclaimed++;
                    }
                }
                catch (Exception e)
                {
                    RLog.Error(e);
                }

                index++;
            }
        }

        private void Pack(RTextureSprite sprite, RPixelFormat format)
        {
            var data = sprite.GetData<byte>();
            SetData(data, format, (int)sprite.Offset.X, (int)sprite.Offset.Y, sprite.ScaledBounds.Width,
                sprite.ScaledBounds.Height, false);
        }
    }

    internal class AtlasNode
    {
        public Rectangle bounds;
        public bool filled;
        public AtlasNode left;
        public AtlasNode right;

        public AtlasNode()
        {
            left = null;
            right = null;
            bounds = Rectangle.Empty;
            filled = false;
        }

        public AtlasNode Insert(Rectangle sBounds)
        {
            if (left != null)
            {
                var newNode = left.Insert(sBounds);
                if (newNode != null)
                    return newNode;

                return right.Insert(sBounds);
            }

            if (filled) // occupied!
                return null;
            if (bounds.Width < sBounds.Width || bounds.Height < sBounds.Height) // bounds are too small for this image
                return null;
            if (bounds.Width == sBounds.Width && bounds.Height == sBounds.Height)
            {
                // fits just right!
                filled = true;
                return this;
            }

            //otherwise split and traverse further...
            left = new AtlasNode();
            right = new AtlasNode();

            // decide which way to split
            var dw = bounds.Width - sBounds.Width;
            var dh = bounds.Height - sBounds.Height;
            if (dw > dh)
            {
                left.bounds = new Rectangle(bounds.X, bounds.Y, sBounds.Width, bounds.Height);
                right.bounds = new Rectangle(bounds.X + sBounds.Width, bounds.Y, bounds.Width - sBounds.Width,
                    bounds.Height);
            }
            else
            {
                left.bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width, sBounds.Height);
                right.bounds = new Rectangle(bounds.X, bounds.Y + sBounds.Height, bounds.Width,
                    bounds.Height - sBounds.Height);
            }

            return left.Insert(sBounds);
        }
    }

    internal class RTextureSizeSorter : IComparer<RTextureSprite>
    {
        public int Compare(RTextureSprite left, RTextureSprite right)
        {
            if (left.Bounds.Height > right.Bounds.Height)
                return 1;
            return -1;
            return 0;
        }
    }
}