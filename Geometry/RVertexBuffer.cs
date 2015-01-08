using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using Reactor.Platform;

namespace Reactor.Geometry
{
    public class RVertexBuffer : IDisposable
    {
        private int vbo;

        internal bool _isDynamic;
        internal bool IsDisposed;

        public int VertexCount { get; private set; }
        public RVertexDeclaration VertexDeclaration { get; private set; }
        public RBufferUsage BufferUsage { get; private set; }

        protected RVertexBuffer(RVertexDeclaration vertexDeclaration, int vertexCount, RBufferUsage bufferUsage, bool dynamic)
        {
            this.VertexDeclaration = vertexDeclaration;
            this.VertexCount = vertexCount;
            this.BufferUsage = bufferUsage;

            _isDynamic = dynamic;

            Threading.BlockOnUIThread(GenerateIfRequired);
        }

        public RVertexBuffer(RVertexDeclaration vertexDeclaration, int vertexCount, RBufferUsage bufferUsage) :
        this(vertexDeclaration, vertexCount, bufferUsage, false)
        {
        }

        public RVertexBuffer(Type type, int vertexCount, RBufferUsage bufferUsage) :
        this(RVertexDeclaration.FromType(type), vertexCount, bufferUsage, false)
        {
        }

        /// <summary>
        /// The GraphicsDevice is resetting, so GPU resources must be recreated.
        /// </summary>
        internal protected void GraphicsDeviceResetting()
        {
            vbo = 0;
        }

        public void GetData<T> (int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride) where T : struct
        {
            if (data == null)
                throw new ArgumentNullException("data", "This method does not accept null for this parameter.");
            if (data.Length < (startIndex + elementCount))
                throw new ArgumentOutOfRangeException("elementCount", "This parameter must be a valid index within the array.");
            if (BufferUsage == RBufferUsage.WriteOnly)
                throw new NotSupportedException("Calling GetData on a resource that was created with BufferUsage.WriteOnly is not supported.");
            if ((elementCount * vertexStride) > (VertexCount * VertexDeclaration.VertexStride))
                throw new InvalidOperationException("The array is not the correct size for the amount of data requested.");

            if (Threading.IsOnUIThread())
            {
                GetBufferData(offsetInBytes, data, startIndex, elementCount, vertexStride);
            }
            else
            {
                Threading.BlockOnUIThread (() => GetBufferData(offsetInBytes, data, startIndex, elementCount, vertexStride));
            }
        }

        public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            var elementSizeInByte = Marshal.SizeOf(typeof(T));
            this.GetData<T>(0, data, startIndex, elementCount, elementSizeInByte);
        }

        public void GetData<T>(T[] data) where T : struct
        {
            var elementSizeInByte = Marshal.SizeOf(typeof(T));
            this.GetData<T>(0, data, 0, data.Length, elementSizeInByte);
        }

        public void SetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride) where T : struct
        {
            SetDataInternal<T>(offsetInBytes, data, startIndex, elementCount, VertexDeclaration.VertexStride, RVertexDataOptions.None);
        }

        public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            SetDataInternal<T>(0, data, startIndex, elementCount, VertexDeclaration.VertexStride, RVertexDataOptions.None);
        }

        public void SetData<T>(T[] data) where T : struct
        {
            SetDataInternal<T>(0, data, 0, data.Length, VertexDeclaration.VertexStride, RVertexDataOptions.None);
        }

        protected void SetDataInternal<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride, RVertexDataOptions options) where T : struct
        {
            if (data == null)
                throw new ArgumentNullException("data is null");
            if (data.Length < (startIndex + elementCount))
                throw new InvalidOperationException("The array specified in the data parameter is not the correct size for the amount of data requested.");

            var bufferSize = VertexCount * VertexDeclaration.VertexStride;

            if ((vertexStride > bufferSize) || (vertexStride < VertexDeclaration.VertexStride))
                throw new ArgumentOutOfRangeException("One of the following conditions is true:\nThe vertex stride is larger than the vertex buffer.\nThe vertex stride is too small for the type of data requested.");

            var elementSizeInBytes = Marshal.SizeOf(typeof(T));

            if (Threading.IsOnUIThread())
            {
                SetBufferData(bufferSize, elementSizeInBytes, offsetInBytes, data, startIndex, elementCount, vertexStride, options);
            }
            else
            {
                Threading.BlockOnUIThread(() => SetBufferData(bufferSize, elementSizeInBytes, offsetInBytes, data, startIndex, elementCount, vertexStride, options));
            }
        }

        /// <summary>
        /// If the VBO does not exist, create it.
        /// </summary>
        void GenerateIfRequired()
        {
            if (vbo == 0)
            {
                //GLExt.Oes.GenVertexArrays(1, out this.vao);
                //GLExt.Oes.BindVertexArray(this.vao);

                GL.GenBuffers(1, out this.vbo);

                REngine.CheckGLError();
                GL.BindBuffer(BufferTarget.ArrayBuffer, this.vbo);
                REngine.CheckGLError();
                GL.BufferData(BufferTarget.ArrayBuffer,
                    new IntPtr(VertexDeclaration.VertexStride * VertexCount), IntPtr.Zero,
                    _isDynamic ? BufferUsageHint.StreamDraw : BufferUsageHint.StaticDraw);
                REngine.CheckGLError();
            }
        }



        private void GetBufferData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride) where T : struct
        {
            GL.BindBuffer (BufferTarget.ArrayBuffer, vbo);
            REngine.CheckGLError();
            var elementSizeInByte = Marshal.SizeOf(typeof(T));
            IntPtr ptr = GL.MapBuffer (BufferTarget.ArrayBuffer, BufferAccess.ReadOnly);
            REngine.CheckGLError();
            // Pointer to the start of data to read in the index buffer
            ptr = new IntPtr (ptr.ToInt64 () + offsetInBytes);
            if (typeof(T) == typeof(byte)) {
                byte[] buffer = data as byte[];
                // If data is already a byte[] we can skip the temporary buffer
                // Copy from the vertex buffer to the destination array
                Marshal.Copy (ptr, buffer, 0, buffer.Length);
            } else {
                // Temporary buffer to store the copied section of data
                byte[] buffer = new byte[elementCount * vertexStride - offsetInBytes];
                // Copy from the vertex buffer to the temporary buffer
                Marshal.Copy(ptr, buffer, 0, buffer.Length);

                var dataHandle = GCHandle.Alloc (data, GCHandleType.Pinned);
                var dataPtr = (IntPtr)(dataHandle.AddrOfPinnedObject ().ToInt64 () + startIndex * elementSizeInByte);

                // Copy from the temporary buffer to the destination array

                int dataSize = Marshal.SizeOf(typeof(T));
                if (dataSize == vertexStride)
                    Marshal.Copy(buffer, 0, dataPtr, buffer.Length);
                else
                {
                    // If the user is asking for a specific element within the vertex buffer, copy them one by one...
                    for (int i = 0; i < elementCount; i++)
                    {
                        Marshal.Copy(buffer, i * vertexStride, dataPtr, dataSize);
                        dataPtr = (IntPtr)(dataPtr.ToInt64() + dataSize);
                    }
                }

                dataHandle.Free ();

                //Buffer.BlockCopy(buffer, 0, data, startIndex * elementSizeInByte, elementCount * elementSizeInByte);
            }
            GL.UnmapBuffer(BufferTarget.ArrayBuffer);
        }
            

        private void SetBufferData<T>(int bufferSize, int elementSizeInBytes, int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride, RVertexDataOptions options) where T : struct
        {
            GenerateIfRequired();

            var sizeInBytes = elementSizeInBytes * elementCount;
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            REngine.CheckGLError();

            if (options == RVertexDataOptions.Discard)
            {
                // By assigning NULL data to the buffer this gives a hint
                // to the device to discard the previous content.
                GL.BufferData(  BufferTarget.ArrayBuffer,
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

        public void Dispose()
        {
            if (!IsDisposed)
            {
                Threading.BlockOnUIThread(() =>
                    {
                        GL.DeleteBuffers(1, ref vbo);
                        REngine.CheckGLError();
                    });
            }
            IsDisposed = true;

        }
    }
}
