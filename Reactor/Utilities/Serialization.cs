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
using System.IO;
using System.Runtime.InteropServices;

namespace Reactor.Utilities
{
    public class Serialization
    {
        public static T ReadStruct<T>(Stream input)
        {
            var buffer = new byte[Marshal.SizeOf(typeof(T))];

            if (input.Read(buffer, 0, buffer.Length) != buffer.Length)
                throw new IOException("Premature end of input stream while reading data.");

            var gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            try
            {
                return (T)Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof(T));
            }

            finally
            {
                gcHandle.Free();
            }
        }

        /// <summary>Write a struct to a <see cref="Stream" />.</summary>
        /// <typeparam name="T">
        ///     The type of the struct to read.
        ///     The packing used must match that used to write the struct.
        /// </typeparam>
        /// <param name="output">The stream to which to write the struct.</param>
        /// <param name="data">The struct to write.</param>
        public static void WriteStruct<T>(Stream output, T data)
        {
            var buffer = new byte[Marshal.SizeOf(typeof(T))];
            var gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            try
            {
                Marshal.StructureToPtr(data, gcHandle.AddrOfPinnedObject(), false);
                output.Write(buffer, 0, buffer.Length);
            }

            finally
            {
                gcHandle.Free();
            }
        }

        public static byte[] WriteString(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string ReadString(byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}