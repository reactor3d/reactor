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
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Reactor.Core
{
    public class RPackage
    {
        ZipArchive file;
        public RPackage()
        {
            file = new ZipArchive(Stream.Null);
        }

        public RPackage(string filename)
        {
            file = ZipFile.Open(filename, ZipArchiveMode.Update);
        }

        public async Task<MemoryStream> GetEntry(string name)
        {
            if(ContainsEntry(name))
            {
                ZipArchiveEntry entry = file.GetEntry(name);
                MemoryStream m = new MemoryStream();
                using (var s = entry.Open())
                {
                    await s.CopyToAsync(m);
                    return m;
                }
            }
            return null;
        }

        public async void AddEntry(string name, Stream data)
        {
            var entry = file.CreateEntry(name);
            using (var s = entry.Open())
            {
                await data.CopyToAsync(s);
            }
        }

        public async Task<bool> AddDirectory(string directory)
        {
            return false;
        }

        public async Task<bool> RemoveEntry(string name)
        {
            var entry =  GetEntry(name);
            if (entry != null)
            {
                entry.Dispose();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContainsEntry(string name)
        {
            foreach(var entry in file.Entries)
            {
                if (entry.FullName == name)
                {
                    return true;
                }
            }

            return false;
        }

        public List<string> GetEntries()
        {
            var names = new List<string>();
            foreach (var entry in file.Entries)
            {
                names.Add(entry.FullName);
            }
            return names;
        }

    }
}
