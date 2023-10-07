using System;
using System.IO;
using System.Runtime.InteropServices;
using Reactor.Loaders;
using Reactor.Utilities;

namespace Reactor
{
    public enum RedFileType : int
    {
        UNKNOWN = 0,
        PNG,
        TGA,
        BMP,
        GIF,
        DDS,
        HDRI,
        RGBA,
        BVH,
        ANIM,
        GLTF,
        ACTOR,
        PARTICLESYSTEM,
        MATERIAL,
        TEXTURE,
        JSON,
        PACKAGE // RPK's within RPK's within RPK's
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct RedFile
    {
        public byte[] version;
        public byte[] filename;
        public int filetype;
        public ulong length;
        public byte[] data;

        public RedFile(RedFileType type)
        {
            this.version = new byte[]{1, 0};
            this.filename = new byte[]{};
            this.length = 0;
            this.data = new byte[]{};
            this.filetype = (int)type;
        }

        public void Save(string filename)
        {
            RedFileLoader.SaveRedFile(this, filename);
        }
    }


}