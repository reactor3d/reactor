/****************************************************************************
 * NVorbis                                                                  *
 * Copyright (C) 2014, Andrew Ward <afward@gmail.com>                       *
 *                                                                          *
 * See COPYING for license terms (Ms-PL).                                   *
 *                                                                          *
 ***************************************************************************/

using System;
using System.IO;
using System.Threading;

namespace NVorbis
{
    /// <summary>
    ///     A thread-safe, read-only, buffering stream wrapper.
    /// </summary>
    internal class BufferedReadStream : Stream
    {
        private const int DEFAULT_INITIAL_SIZE = 32768; // 32KB  (1/2 full page)
        private const int DEFAULT_MAX_SIZE = 262144; // 256KB (4 full pages)

        private readonly Stream _baseStream;
        private StreamReadBuffer _buffer;
        private readonly object _localLock = new object();
        private int _lockCount;
        private Thread _owningThread;
        private long _readPosition;

        public BufferedReadStream(Stream baseStream)
            : this(baseStream, DEFAULT_INITIAL_SIZE, DEFAULT_MAX_SIZE, false)
        {
        }

        public BufferedReadStream(Stream baseStream, bool minimalRead)
            : this(baseStream, DEFAULT_INITIAL_SIZE, DEFAULT_MAX_SIZE, minimalRead)
        {
        }

        public BufferedReadStream(Stream baseStream, int initialSize, int maxSize)
            : this(baseStream, initialSize, maxSize, false)
        {
        }

        public BufferedReadStream(Stream baseStream, int initialSize, int maxBufferSize, bool minimalRead)
        {
            if (baseStream == null) throw new ArgumentNullException("baseStream");
            if (!baseStream.CanRead) throw new ArgumentException("baseStream");

            if (maxBufferSize < 1) maxBufferSize = 1;
            if (initialSize < 1) initialSize = 1;
            if (initialSize > maxBufferSize) initialSize = maxBufferSize;

            _baseStream = baseStream;
            _buffer = new StreamReadBuffer(baseStream, initialSize, maxBufferSize, minimalRead);
            _buffer.MaxSize = maxBufferSize;
            _buffer.MinimalRead = minimalRead;
        }

        public bool CloseBaseStream { get; set; }

        public bool MinimalRead
        {
            get => _buffer.MinimalRead;
            set => _buffer.MinimalRead = value;
        }

        public int MaxBufferSize
        {
            get => _buffer.MaxSize;
            set
            {
                CheckLock();
                _buffer.MaxSize = value;
            }
        }

        public long BufferBaseOffset => _buffer.BaseOffset;

        public int BufferBytesFilled => _buffer.BytesFilled;

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => false;

        public override long Length => _baseStream.Length;

        public override long Position
        {
            get => _readPosition;
            set => Seek(value, SeekOrigin.Begin);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (_buffer != null)
                {
                    _buffer.Dispose();
                    _buffer = null;
                }

                if (CloseBaseStream) _baseStream.Close();
            }
        }

        // route all the container locking through here so we can track whether the caller actually took the lock...
        public void TakeLock()
        {
            Monitor.Enter(_localLock);
            if (++_lockCount == 1) _owningThread = Thread.CurrentThread;
        }

        private void CheckLock()
        {
            if (_owningThread != Thread.CurrentThread) throw new SynchronizationLockException();
        }

        public void ReleaseLock()
        {
            CheckLock();
            if (--_lockCount == 0) _owningThread = null;
            Monitor.Exit(_localLock);
        }

        public void Discard(int bytes)
        {
            CheckLock();
            _buffer.DiscardThrough(_buffer.BaseOffset + bytes);
        }

        public void DiscardThrough(long offset)
        {
            CheckLock();
            _buffer.DiscardThrough(offset);
        }

        public override void Flush()
        {
            // no-op
        }

        public override int ReadByte()
        {
            CheckLock();
            var val = _buffer.ReadByte(Position);
            if (val > -1) Seek(1, SeekOrigin.Current);
            return val;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            CheckLock();
            var cnt = _buffer.Read(Position, buffer, offset, count);
            Seek(cnt, SeekOrigin.Current);
            return cnt;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            CheckLock();
            switch (origin)
            {
                case SeekOrigin.Begin:
                    // no-op
                    break;
                case SeekOrigin.Current:
                    offset += Position;
                    break;
                case SeekOrigin.End:
                    offset += _baseStream.Length;
                    break;
            }

            if (!_baseStream.CanSeek)
            {
                if (offset < _buffer.BaseOffset)
                    throw new InvalidOperationException("Cannot seek to before the start of the buffer!");
                if (offset >= _buffer.BufferEndOffset)
                    throw new InvalidOperationException(
                        "Cannot seek to beyond the end of the buffer!  Discard some bytes.");
            }

            return _readPosition = offset;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}