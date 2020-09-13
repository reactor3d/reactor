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
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Reactor.Platform;
using Reactor.Graphics.OpenGL;

namespace Reactor.Geometry
{
    public class RIndexBuffer : IDisposable
    {
        bool _isDynamic;
        internal uint ibo;

        public RBufferUsage BufferUsage { get; private set; }
        public int IndexCount { get; private set; }
        public RIndexElementSize IndexElementSize { get; private set; }

        public RIndexBuffer(Type type, int indexCount, RBufferUsage usage, bool dynamic)
            : this(SizeForType(type), indexCount, usage, dynamic)
        {
        }

        public RIndexBuffer(RIndexElementSize indexElementSize, int indexCount, RBufferUsage usage, bool dynamic)
        {
            this.IndexElementSize = indexElementSize;   
            this.IndexCount = indexCount;
            this.BufferUsage = usage;

            _isDynamic = dynamic;

            Threading.BlockOnUIThread(GenerateIfRequired);
        }

        public RIndexBuffer(RIndexElementSize indexElementSize, int indexCount, RBufferUsage bufferUsage) :
        this(indexElementSize, indexCount, bufferUsage, false)
        {
        }

        public RIndexBuffer(Type type, int indexCount, RBufferUsage usage) :
        this(SizeForType(type), indexCount, usage, false)
        {
        }

        /// <summary>
        /// Gets the relevant IndexElementSize enum value for the given type.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="type">The type to use for the index buffer</param>
        /// <returns>The IndexElementSize enum value that matches the type</returns>
        static RIndexElementSize SizeForType(Type type)
        {
            switch (Marshal.SizeOf(type))
            {
                case 2:
                    return RIndexElementSize.SixteenBits;
                case 4:
                    return RIndexElementSize.ThirtyTwoBits;
                default:
                    throw new ArgumentOutOfRangeException("Index buffers can only be created for types that are sixteen or thirty two bits in length");
            }
        }

        /// <summary>
        /// The GraphicsDevice is resetting, so GPU resources must be recreated.
        /// </summary>
        internal protected void GraphicsDeviceResetting()
        {
            ibo = 0;
        }

        public void GetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount) where T : struct
        {
            if (data == null)
                throw new ArgumentNullException("data is null");
            if (data.Length < (startIndex + elementCount))
                throw new InvalidOperationException("The array specified in the data parameter is not the correct size for the amount of data requested.");
            if (BufferUsage == RBufferUsage.WriteOnly)
                throw new NotSupportedException("This IndexBuffer was created with a usage type of BufferUsage.WriteOnly. Calling GetData on a resource that was created with BufferUsage.WriteOnly is not supported.");

            #if GLES
            // Buffers are write-only on OpenGL ES 1.1 and 2.0.  See the GL_OES_mapbuffer extension for more information.
            // http://www.khronos.org/registry/gles/extensions/OES/OES_mapbuffer.txt
            throw new NotSupportedException("Index buffers are write-only on OpenGL ES platforms");
            #endif
            #if !GLES
            if (Threading.IsOnUIThread())
            {
                GetBufferData(offsetInBytes, data, startIndex, elementCount);
            }
            else
            {
                Threading.BlockOnUIThread(() => GetBufferData(offsetInBytes, data, startIndex, elementCount));
            }
            #endif
        }

        public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            this.GetData<T>(0, data, startIndex, elementCount);
        }

        public void GetData<T>(T[] data) where T : struct
        {
            this.GetData<T>(0, data, 0, data.Length);
        }

        public void SetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount) where T : struct
        {
            SetDataInternal<T>(offsetInBytes, data, startIndex, elementCount, RVertexDataOptions.Discard);
        }

        public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            SetDataInternal<T>(0, data, startIndex, elementCount, RVertexDataOptions.Discard);
        }

        public void SetData<T>(T[] data) where T : struct
        {
            SetDataInternal<T>(0, data, 0, data.Length, RVertexDataOptions.Discard);
        }

        protected void SetDataInternal<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, RVertexDataOptions options) where T : struct
        {
            if (data == null)
                throw new ArgumentNullException("data", "data is null");
            if (data.Length < (startIndex + elementCount))
                throw new InvalidOperationException("The array specified in the data parameter is not the correct size for the amount of data requested.");

            if (Threading.IsOnUIThread())
            {
                BufferData(offsetInBytes, data, startIndex, elementCount, options);
            }
            else
            {
                Threading.BlockOnUIThread(() => BufferData(offsetInBytes, data, startIndex, elementCount, options));
            }
        }
        #if !GLES
        private void GetBufferData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount) where T : struct
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, ibo);
            REngine.CheckGLError();
            var elementSizeInByte = Marshal.SizeOf(typeof(T));
            IntPtr ptr = GL.MapBuffer(BufferTarget.ArrayBuffer, BufferAccess.ReadOnly);
            // Pointer to the start of data to read in the index buffer
            ptr = new IntPtr(ptr.ToInt64() + offsetInBytes);
            if (typeof(T) == typeof(byte))
            {
                byte[] buffer = data as byte[];
                // If data is already a byte[] we can skip the temporary buffer
                // Copy from the index buffer to the destination array
                Marshal.Copy(ptr, buffer, 0, buffer.Length);
            }
            else
            {
                // Temporary buffer to store the copied section of data
                byte[] buffer = new byte[elementCount * elementSizeInByte];
                // Copy from the index buffer to the temporary buffer
                Marshal.Copy(ptr, buffer, 0, buffer.Length);
                // Copy from the temporary buffer to the destination array
                System.Buffer.BlockCopy(buffer, 0, data, startIndex * elementSizeInByte, elementCount * elementSizeInByte);
            }
            GL.UnmapBuffer(BufferTarget.ArrayBuffer);
            REngine.CheckGLError();
        }
        #endif
        private void BufferData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, RVertexDataOptions options) where T : struct
        {
            GenerateIfRequired();

            var elementSizeInByte = Marshal.SizeOf(typeof(T));
            var sizeInBytes = elementSizeInByte * elementCount;
            var dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            var dataPtr = (IntPtr)(dataHandle.AddrOfPinnedObject().ToInt64() + startIndex * elementSizeInByte);
            var bufferSize = IndexCount * (IndexElementSize == RIndexElementSize.SixteenBits ? 2 : 4);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);
            REngine.CheckGLError();

            if (options == RVertexDataOptions.Discard)
            {
                // By assigning NULL data to the buffer this gives a hint
                // to the device to discard the previous content.
                GL.BufferData(  BufferTarget.ElementArrayBuffer,
                    (IntPtr)bufferSize,
                    IntPtr.Zero,
                    _isDynamic ? BufferUsageHint.StreamDraw : BufferUsageHint.StaticDraw);
                REngine.CheckGLError();
            }

            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)bufferSize, dataPtr, _isDynamic ? BufferUsageHint.StreamDraw : BufferUsageHint.StaticDraw);
            REngine.CheckGLError();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            REngine.CheckGLError();
            dataHandle.Free();
        }
        /// <summary>
        /// If the IBO does not exist, create it.
        /// </summary>
        void GenerateIfRequired()
        {
            if (ibo == 0)
            {
                var sizeInBytes = IndexCount * (this.IndexElementSize == RIndexElementSize.SixteenBits ? 2 : 4);

                GL.GenBuffers(1, out ibo);

                REngine.CheckGLError();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);
                REngine.CheckGLError();
                GL.BufferData(BufferTarget.ElementArrayBuffer,
                    (IntPtr)sizeInBytes, IntPtr.Zero, _isDynamic ? BufferUsageHint.StreamDraw : BufferUsageHint.StaticDraw);
                REngine.CheckGLError();
            }
        }
        internal int GetElementCountArray(PrimitiveType primitiveType, int primitiveCount)
        {
            //TODO: Overview the calculation
            switch (primitiveType)
            {
                case PrimitiveType.Points:
                    return primitiveCount;
                case PrimitiveType.Lines:
                    return primitiveCount * 2;
                case PrimitiveType.LineStrip:
                    return primitiveCount + 1;
                case PrimitiveType.Triangles:
                    return primitiveCount * 3;
                case PrimitiveType.TriangleStrip:
                    return 3 + (primitiveCount - 1); // ???
                case PrimitiveType.Polygon:
                    return primitiveCount * 4;
            }

            throw new NotSupportedException();
        }

        internal void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);
        }
        internal void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
        #region IDisposable implementation

        public void Dispose()
        {
            Threading.BlockOnUIThread(() =>
                {
                    GL.DeleteBuffers(1, ref ibo);
                    REngine.CheckGLError();
                });
        }

        #endregion
    }
}
