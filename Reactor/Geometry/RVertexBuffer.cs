// Author:
//       Gabriel Reiser <gabe@reisergames.com>
//
// Copyright (c) 2007-2020 Reiser Games, LLC.
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
using System.Runtime.InteropServices;
using Reactor.Platform;
using Reactor.Platform.OpenGL;
using Reactor.Utilities;

namespace Reactor.Geometry
{
    public class RVertexBuffer : IDisposable
    {
        internal bool _isDynamic;
        internal bool IsDisposed;

        public RVertexBuffer(RVertexDeclaration vertexDeclaration, int vertexCount, RBufferUsage bufferUsage,
            bool dynamic)
        {
            if (vertexDeclaration == null)
                throw new ArgumentNullException("vertexDeclaration", "vertexDeclaration not set! was null.");
            VertexDeclaration = vertexDeclaration;
            VertexCount = vertexCount;
            BufferUsage = bufferUsage;

            _isDynamic = dynamic;

            Threading.BlockOnUIThread(GenerateIfRequired);
        }

        public RVertexBuffer(Type type, int vertexCount, RBufferUsage bufferUsage, bool dynamic) :
            this(RVertexDeclaration.FromType(type), vertexCount, bufferUsage, dynamic)
        {
        }

        public RVertexBuffer(RVertexDeclaration vertexDeclaration, int vertexCount, RBufferUsage bufferUsage) :
            this(vertexDeclaration, vertexCount, bufferUsage, false)
        {
        }

        public RVertexBuffer(Type type, int vertexCount, RBufferUsage bufferUsage) :
            this(RVertexDeclaration.FromType(type), vertexCount, bufferUsage, false)
        {
        }

        public int VertexCount { get; }
        public RVertexDeclaration VertexDeclaration { get; }
        public RBufferUsage BufferUsage { get; }

        public uint VBO { get; private set; }

        public uint VAO { get; private set; }

        public void Dispose()
        {
            if (!IsDisposed)
                Threading.BlockOnUIThread(() =>
                {
                    Allocator.UInt32_1[0] = VBO;
                    GL.DeleteBuffers(1, Allocator.UInt32_1);
                    REngine.CheckGLError();
                });
            IsDisposed = true;
        }

        /// <summary>
        ///     The GraphicsDevice is resetting, so GPU resources must be recreated.
        /// </summary>
        protected internal void GraphicsDeviceResetting()
        {
            VBO = 0;
        }

        public void GetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride)
            where T : struct
        {
            if (data == null)
                throw new ArgumentNullException("data", "This method does not accept null for this parameter.");
            if (data.Length < startIndex + elementCount)
                throw new ArgumentOutOfRangeException("elementCount",
                    "This parameter must be a valid index within the array.");
            if (BufferUsage == RBufferUsage.WriteOnly)
                throw new NotSupportedException(
                    "Calling GetData on a resource that was created with BufferUsage.WriteOnly is not supported.");
            if (elementCount * vertexStride > VertexCount * VertexDeclaration.VertexStride)
                throw new InvalidOperationException(
                    "The array is not the correct size for the amount of data requested.");

            if (Threading.IsOnUIThread())
                GetBufferData(offsetInBytes, data, startIndex, elementCount, vertexStride);
            else
                Threading.BlockOnUIThread(() =>
                    GetBufferData(offsetInBytes, data, startIndex, elementCount, vertexStride));
        }

        public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            var elementSizeInByte = Marshal.SizeOf(typeof(T));
            GetData(0, data, startIndex, elementCount, elementSizeInByte);
        }

        public void GetData<T>(T[] data) where T : struct
        {
            var elementSizeInByte = Marshal.SizeOf(typeof(T));
            GetData(0, data, 0, data.Length, elementSizeInByte);
        }

        public void SetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride)
            where T : struct
        {
            SetDataInternal(offsetInBytes, data, startIndex, elementCount, VertexDeclaration.VertexStride,
                RVertexDataOptions.None);
        }

        public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            SetDataInternal(0, data, startIndex, elementCount, VertexDeclaration.VertexStride, RVertexDataOptions.None);
        }

        public void SetData<T>(T[] data) where T : struct
        {
            SetDataInternal(0, data, 0, data.Length, VertexDeclaration.VertexStride, RVertexDataOptions.None);
        }

        protected void SetDataInternal<T>(int offsetInBytes, T[] data, int startIndex, int elementCount,
            int vertexStride, RVertexDataOptions options) where T : struct
        {
            if (data == null)
                throw new ArgumentNullException("data is null");
            if (data.Length < startIndex + elementCount)
                throw new InvalidOperationException(
                    "The array specified in the data parameter is not the correct size for the amount of data requested.");

            var bufferSize = VertexCount * VertexDeclaration.VertexStride;

            if (vertexStride > bufferSize || vertexStride < VertexDeclaration.VertexStride)
                throw new ArgumentOutOfRangeException(
                    "One of the following conditions is true:\nThe vertex stride is larger than the vertex buffer.\nThe vertex stride is too small for the type of data requested.");

            var elementSizeInBytes = Marshal.SizeOf(typeof(T));

            if (Threading.IsOnUIThread())
                SetBufferData(bufferSize, elementSizeInBytes, offsetInBytes, data, startIndex, elementCount,
                    vertexStride, options);
            else
                Threading.BlockOnUIThread(() => SetBufferData(bufferSize, elementSizeInBytes, offsetInBytes, data,
                    startIndex, elementCount, vertexStride, options));
        }

        /// <summary>
        ///     If the VBO does not exist, create it.
        /// </summary>
        private void GenerateIfRequired()
        {
            if (VBO == 0)
            {
                GL.GenVertexArrays(1, Allocator.UInt32_1);
                VAO = Allocator.UInt32_1[0];
                REngine.CheckGLError();
                GL.BindVertexArray(VAO);
                REngine.CheckGLError();

                GL.GenBuffers(1, Allocator.UInt32_1);
                VBO = Allocator.UInt32_1[0];
                REngine.CheckGLError();
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
                REngine.CheckGLError();
                GL.BufferData(BufferTarget.ArrayBuffer,
                    new IntPtr(VertexDeclaration.VertexStride * VertexCount), IntPtr.Zero,
                    _isDynamic ? BufferUsageHint.StreamDraw : BufferUsageHint.StaticDraw);
                REngine.CheckGLError();
            }
        }


        private void GetBufferData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride)
            where T : struct
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            REngine.CheckGLError();
            var elementSizeInByte = Marshal.SizeOf(typeof(T));
            var ptr = GL.MapBuffer(BufferTarget.ArrayBuffer, BufferAccess.ReadOnly);
            REngine.CheckGLError();
            // Pointer to the start of data to read in the index buffer
            ptr = new IntPtr(ptr.ToInt64() + offsetInBytes);
            if (typeof(T) == typeof(byte))
            {
                var buffer = data as byte[];
                // If data is already a byte[] we can skip the temporary buffer
                // Copy from the vertex buffer to the destination array
                Marshal.Copy(ptr, buffer, 0, buffer.Length);
            }
            else
            {
                // Temporary buffer to store the copied section of data
                var buffer = new byte[elementCount * vertexStride - offsetInBytes];
                // Copy from the vertex buffer to the temporary buffer
                Marshal.Copy(ptr, buffer, 0, buffer.Length);

                var dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
                var dataPtr = (IntPtr)(dataHandle.AddrOfPinnedObject().ToInt64() + startIndex * elementSizeInByte);

                // Copy from the temporary buffer to the destination array

                var dataSize = Marshal.SizeOf(typeof(T));
                if (dataSize == vertexStride)
                    Marshal.Copy(buffer, 0, dataPtr, buffer.Length);
                else
                    // If the user is asking for a specific element within the vertex buffer, copy them one by one...
                    for (var i = 0; i < elementCount; i++)
                    {
                        Marshal.Copy(buffer, i * vertexStride, dataPtr, dataSize);
                        dataPtr = (IntPtr)(dataPtr.ToInt64() + dataSize);
                    }

                dataHandle.Free();

                //Buffer.BlockCopy(buffer, 0, data, startIndex * elementSizeInByte, elementCount * elementSizeInByte);
            }

            GL.UnmapBuffer(BufferTarget.ArrayBuffer);
            REngine.CheckGLError();
        }


        private void SetBufferData<T>(int bufferSize, int elementSizeInBytes, int offsetInBytes, T[] data,
            int startIndex, int elementCount, int vertexStride, RVertexDataOptions options) where T : struct
        {
            GenerateIfRequired();

            var sizeInBytes = elementSizeInBytes * elementCount;
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            REngine.CheckGLError();

            if (options == RVertexDataOptions.Discard)
            {
                // By assigning NULL data to the buffer this gives a hint
                // to the device to discard the previous content.
                GL.BufferData(BufferTarget.ArrayBuffer,
                    (IntPtr)bufferSize,
                    IntPtr.Zero,
                    _isDynamic ? BufferUsageHint.StreamDraw : BufferUsageHint.StaticDraw);
                REngine.CheckGLError();
            }

            var dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var dataPtr = (IntPtr)(dataHandle.AddrOfPinnedObject().ToInt64() + startIndex * elementSizeInBytes);

            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)offsetInBytes, (IntPtr)sizeInBytes, dataPtr);
            REngine.CheckGLError();

            dataHandle.Free();
        }

        public void Bind()
        {
            if (Threading.IsOnUIThread())
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
                REngine.CheckGLError();
            }
            else
            {
                Threading.BlockOnUIThread(() => { Bind(); });
            }
        }

        public void Unbind()
        {
            if (Threading.IsOnUIThread())
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                REngine.CheckGLError();
            }
            else
            {
                Threading.BlockOnUIThread(() => { Unbind(); });
            }
        }

        public void BindVertexArray()
        {
            if (!Threading.IsOnUIThread())
            {
                Threading.BlockOnUIThread(() => { BindVertexArray(); });
            }
            else
            {
                GenerateIfRequired();
                REngine.CheckGLError();
                GL.BindVertexArray(VAO);
                REngine.CheckGLError();
            }
        }

        public void UnbindVertexArray()
        {
            if (!Threading.IsOnUIThread())
            {
                Threading.BlockOnUIThread(() => { UnbindVertexArray(); });
            }
            else
            {
                GL.BindVertexArray(0);
                REngine.CheckGLError();
            }
        }

        public void Clear()
        {
        }
    }
}