
using System.IO;

namespace Reactor.Core.Extensions
{
    public static class BinaryExtensions
    {
        public static MemoryStream AsMemory(this Stream stream)
        {
            return new MemoryStream(stream.AsBytes());
        }

        public static byte[] AsBytes(this Stream stream)
        {
            using (var reader = new BinaryReader(stream))
            {
                return reader.ReadBytes((int)stream.Length);
            }
        }

        public static Stream ToStream(this byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public static byte[] ToArray(this Stream stream)
        {
            return stream.AsBytes();
        }

        public static Stream Slice(this Stream stream, int offset, int length)
        {
            return stream.AsMemory().Slice(offset, length);
        }
    }
    
}