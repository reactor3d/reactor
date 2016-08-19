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
using Ionic.Zip;
using Ionic.Crc;
using System.Threading.Tasks;
namespace Reactor.Core
{
    public class RPackage
    {
        ZipFile file;
        public RPackage()
        {
            file = new ZipFile();
            file.Password = null;
        }

        public RPackage(string filename, string password = null)
        {
            file = ZipFile.Read(filename);
            file.Password = password;
        }

        public async Task<MemoryStream> GetEntry(string name)
        {
            if(file.ContainsEntry(name))
            {
                ZipEntry entry = file[name];

                CrcCalculatorStream s = entry.OpenReader();
                byte[] bytes = new byte[s.Length];
                int i = await s.ReadAsync(bytes, 0, bytes.Length);
                MemoryStream  m = new MemoryStream(bytes);
                return m;
            }
            return null;
        }

        public async void AddEntry(string name, Stream data)
        {
            file.AddEntry(name, data);
        }

        public async void AddDirectory(string directory)
        {
            file.AddDirectory(directory);
        }

        public async Task<bool> RemoveEntry(string name)
        {
            return await Task<bool>.Run(() => {
                if (file.EntryFileNames.Contains(name))
                {
                    file.RemoveEntry(name);
                    return true;
                }
                else { return false; }
            });
        }

        public void Save(string filename)
        {
            file.ParallelDeflateThreshold = -1; 
            file.Save(filename);
        }

        public bool ContainsEntry(string name)
        {
            if (file[name] != null)
                return true;
            else
                return false;
        }

        public List<string> GetEntries()
        {
            return new List<string>(file.EntryFileNames);
        }

    }
}
