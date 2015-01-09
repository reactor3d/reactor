using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Reactor.Platform;
using OpenTK.Graphics.OpenGL;

namespace Reactor.Geometry
{
    public class RIndexBuffer<T> : IDisposable where T : struct
    {
        bool _isDynamic;
        internal uint ibo;

        public RBufferUsage BufferUsage { get; private set; }
        public int IndexCount { get; private set; }
        public RIndexElementSize IndexElementSize { get; private set; }

        protected RIndexBuffer(int indexCount, RBufferUsage usage, bool dynamic)
            : this(SizeForType(typeof(T)), indexCount, usage, dynamic)
        {
        }

        protected RIndexBuffer(RIndexElementSize indexElementSize, int indexCount, RBufferUsage usage, bool dynamic)
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

        public RIndexBuffer(int indexCount, RBufferUsage usage) :
        this(SizeForType(typeof(T)), indexCount, usage, false)
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
            SetDataInternal<T>(offsetInBytes, data, startIndex, elementCount, RVertexDataOptions.None);
        }

        public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            SetDataInternal<T>(0, data, startIndex, elementCount, RVertexDataOptions.None);
        }

        public void SetData<T>(T[] data) where T : struct
        {
            SetDataInternal<T>(0, data, 0, data.Length, RVertexDataOptions.None);
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
                Buffer.BlockCopy(buffer, 0, data, startIndex * elementSizeInByte, elementCount * elementSizeInByte);
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

            GL.BufferSubData(BufferTarget.ElementArrayBuffer, (IntPtr)offsetInBytes, (IntPtr)sizeInBytes, dataPtr);
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
