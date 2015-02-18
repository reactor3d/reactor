//
// RFileSystem.cs
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
using Reactor.Types;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.IO.Compression;
using Reactor.Core;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Reactor.Utilities;

namespace Reactor
{
    public class RFileSystem : RSingleton<RFileSystem>
    {
        private List<RPackage> packages = new List<RPackage>();

        public string GetFilePath(string relativeFilename)
        {
            return Path.GetFullPath(AssemblyDirectory+relativeFilename);
        }

        public Stream GetFile(string relativeFilename)
        {
            string absolutePath = GetFilePath(relativeFilename);
            if(File.Exists(absolutePath))
            {
                return File.Open(absolutePath, FileMode.Open);
            }
            else
            {
                //Search the packages for the file...
                foreach(var package in packages)
                {
                    if(package.ContainsEntry(relativeFilename))
                    {
                        var result = package.GetEntry(relativeFilename).Result;
                        return result;
                    }
                }
                return null;
            }
        }
        public string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public bool LoadPackage(string relativeFilename, string password = null)
        {
            string fullFilename = GetFilePath(relativeFilename);
            if(File.Exists(fullFilename))
            {
                RPackage package = new RPackage(fullFilename, password);
                packages.Add(package);
                return true;
            }
            return false;
        }

        public MemoryStream GetPackageContent(string name)
        {
            foreach(var package in packages)
            {
                if (package.ContainsEntry(name))
                    return package.GetEntry(name).Result;
            }
            RLog.Error(String.Format("Entry {0} not found in any loaded packages"));
            return null;
        }
        internal System.IO.MemoryStream Save<T>(T data)
        {
            string json = JsonConvert.SerializeObject(data);
            return Task.Run<System.IO.MemoryStream>(() =>
            {
                System.IO.MemoryStream stream = new System.IO.MemoryStream();

                byte[] d = Serialization.WriteString(json);
                stream.WriteAsync(d, 0, d.Length);
                return stream;
            }).Result;
        }
        internal T Load<T>(System.IO.MemoryStream stream)
        {
            return Task<T>.Factory.StartNew(() =>
            {
                return JsonConvert.DeserializeObject<T>(Serialization.ReadString(stream.ToArray()));
            }).Result;
        }
        
    }
}

