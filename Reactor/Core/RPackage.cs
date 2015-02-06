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
        public RPackage(string filename, string password = null)
        {
            file = new ZipFile(filename);
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
            Task.Run(() =>
            {
                file.Save(filename);
            });
        }

        public bool ContainsEntry(string name)
        {
            if (file[name] != null)
                return true;
            else
                return false;
        }

    }
}
