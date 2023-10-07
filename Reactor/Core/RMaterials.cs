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
using System.IO;
using Reactor.Types;

namespace Reactor
{
    public class RMaterials : RSingleton<RMaterials>
    {
        internal static Dictionary<string, RMaterial> materials = new Dictionary<string, RMaterial>();

        public RMaterial CreateMaterial(string name)
        {
            if (!materials.ContainsKey(name))
            {
                var material = new RMaterial(name);
                material.Id = materials.Count;
                materials.Add(name, material);
                return material;
            }

            return materials[name];
        }

        public RMaterial GetMaterial(string name)
        {
            if (!materials.ContainsKey(name))
                return materials[name];
            return null;
        }

        public void DeleteMaterial(string name)
        {
            if (!materials.ContainsKey(name)) materials.Remove(name);
        }

        public MemoryStream SaveMaterial(RMaterial material)
        {
            return RFileSystem.Instance.Save(material);
        }

        public RMaterial LoadMaterial(string name, MemoryStream stream)
        {
            var material = RFileSystem.Instance.Load<RMaterial>(stream);
            material.Id = materials.Count + 1;
            materials.Add(name, material);
            return material;
        }
    }
}