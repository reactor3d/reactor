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
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Reactor.Core;
using Reactor.Types;
using Reactor.Utilities;

namespace Reactor
{
    public class RFileSystem : RSingleton<RFileSystem>
    {
        private readonly List<RPackage> packages = new List<RPackage>();

        public string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public string GetFilePath(string relativeFilename)
        {
            return Path.GetFullPath(AssemblyDirectory + relativeFilename);
        }

        public Stream GetFile(string relativeFilename)
        {
            var absolutePath = GetFilePath(relativeFilename);
            if (File.Exists(absolutePath))
            {
                return File.Open(absolutePath, FileMode.Open);
            }

            //Search the packages for the file...
            foreach (var package in packages)
                if (package.ContainsEntry(relativeFilename))
                {
                    var result = package.GetEntry(relativeFilename).Result;
                    return result;
                }

            return null;
        }
        
        public byte[] GetFileBytes(string relativeFilename)
        {
            var absolutePath = GetFilePath(relativeFilename);
            if (File.Exists(absolutePath))
                using (var s = File.Open(absolutePath, FileMode.Open))
                    using (var reader = new BinaryReader(s))
                        return reader.ReadBytes((int)s.Length);
            return null;
        }

        public bool LoadPackage(string relativeFilename)
        {
            var fullFilename = GetFilePath(relativeFilename);
            if (File.Exists(fullFilename))
            {
                var package = new RPackage(fullFilename);
                packages.Add(package);
                return true;
            }

            return false;
        }

        public MemoryStream GetPackageContent(string name)
        {
            foreach (var package in packages)
                if (package.ContainsEntry(name))
                    return package.GetEntry(name).Result;
            RLog.Error(string.Format("Entry {0} not found in any loaded packages"));
            return null;
        }

        internal MemoryStream Save<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data);
            return Task.Run(() =>
            {
                var stream = new MemoryStream();

                var d = Serialization.WriteString(json);
                stream.WriteAsync(d, 0, d.Length);
                return stream;
            }).Result;
        }

        internal T Load<T>(MemoryStream stream)
        {
            return Task<T>.Factory.StartNew(() =>
            {
                return JsonConvert.DeserializeObject<T>(Serialization.ReadString(stream.ToArray()));
            }).Result;
        }
    }
}