//
// Serialization.cs
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
using System.IO;
using System.Runtime.InteropServices;


namespace Reactor.Utilities
{
    public class Serialization
    {

        public static T ReadStruct<T>(ref Stream input) where T: struct
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];

            if (input.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new IOException("Premature end of input stream while reading a struct.");
            }

            GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            try
            {
                return (T)Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof(T));
            }

            finally
            {
                gcHandle.Free();
            }
        }

        /// <summary>Write a struct to a <see cref="Stream"/>.</summary>
        /// <typeparam name="T">The type of the struct to read.
        /// The packing used must match that used to write the struct.</typeparam>
        /// <param name="output">The stream to which to write the struct.</param>
        /// <param name="data">The struct to write.</param>

        public static void WriteStruct<T>(ref Stream output, T data) where T: struct
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
            GCHandle gcHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

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

    }
}

