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

using System.Collections.Generic;
using Reactor.Types;

namespace Reactor
{
    public class RTextures : RSingleton<RTextures>
    {
        internal static Dictionary<string, RTexture> Textures = new Dictionary<string, RTexture>();

        public RTexture CreateTexture<T>(byte[] data, string name, bool isCompressed) where T : RTexture, new()
        {
            if (Textures.ContainsKey(name))
                return Textures[name];
            RTexture texture = new T();
            texture.Name = name;
            texture.LoadFromData(data, name, isCompressed);
            Textures.Add(name, texture);
            return texture;
        }

        public RTexture CreateTexture<T>(string name, string filename) where T : RTexture, new()
        {
            if (Textures.ContainsKey(name))
                return Textures[name];
            RTexture texture = new T();
            texture.Name = name;
            texture.LoadFromDisk(filename);
            Textures.Add(name, texture);
            return texture;
        }

        public RTexture GetTexture(string name)
        {
            if (Textures.ContainsKey(name))
                return Textures[name];
            return null;
        }

        public bool RemoveTexture(string name)
        {
            if (Textures.ContainsKey(name))
            {
                Textures[name].Dispose();
                Textures.Remove(name);
                return true;
            }

            return false;
        }


        internal static RTexture GetTexture(uint texture)
        {
            foreach (var t in Textures.Values)
                if (t.Id == texture)
                    return t;
            return null;
        }
    }
}