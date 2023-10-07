using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Reactor.Core;
using Reactor.Types;
using Reactor.Utilities;

namespace Reactor.Loaders
{
    public class RedFileLoader
    {
        

        internal static RedFile OpenRedFile(string filename)
        {
            Stream file = File.Open(filename, FileMode.Open);
            var gzip = new GZipStream(file, CompressionLevel.Optimal);
            var red = Serialization.ReadStruct<RedFile>(gzip);
            file.Close();
            return red;
        }
        internal static void SaveRedFile(RedFile redfile, string filename)
        {
            Stream file = File.Open(filename, FileMode.Create);
            var gzip = new GZipStream(file, CompressionLevel.Optimal);
            Serialization.WriteStruct(gzip, redfile);
            file.Close();
        }
        
        public static T LoadTexture<T>(string filename) where T : RTexture
        {
            var stream = RFileSystem.Instance.GetFile(filename);
            var gzip = new GZipStream(stream, CompressionLevel.Optimal);
            BinaryFormatter formatter = new BinaryFormatter();
            var red = (RedFile)formatter.Deserialize(gzip);
            switch ((RedFileType)red.filetype)
            {
                case RedFileType.GIF:
                case RedFileType.PNG:
                case RedFileType.BMP:
                    var t = new RTexture();
                    t.LoadFromData(red.data, Encoding.UTF8.GetString(red.filename), false);
                    return t as T;
                case RedFileType.DDS:
                    var t2 = new RTexture();
                    t2.LoadFromData(red.data, Encoding.UTF8.GetString(red.filename), true);
                    return t2 as T;
            }

            return null;
        }
        public static T LoadMesh<T>(string name, string filename) where T : RMesh
        {
            var stream = RFileSystem.Instance.GetFile(filename);
            var gzip = new GZipStream(stream, CompressionLevel.Optimal);
            BinaryFormatter formatter = new BinaryFormatter();
            var red = (RedFile)formatter.Deserialize(gzip);
            switch ((RedFileType)red.filetype)
            {
                case RedFileType.GLTF:
                    var t = RScene.Instance.CreateMesh(name);
                    t.LoadSource(new MemoryStream(red.data));
                    return t as T;
            }

            return null;
        }
    }
}