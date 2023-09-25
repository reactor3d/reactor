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

//
// The Open Toolkit Library License
//
// Copyright (c) 2006 - 2008 the Open Toolkit library, except where noted.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//

using Reactor.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reactor.Audio
{
    public class RAudioStreamer
    {
        const float DefaultUpdateRate = 10;
        const int DefaultBufferSize = 44100;

        static readonly object singletonMutex = new object();

        readonly object iterationMutex = new object();
        readonly object readMutex = new object();

        readonly float[] readSampleBuffer;
        readonly short[] castBuffer;

        readonly HashSet<RAudioStream> streams = new HashSet<RAudioStream>();
        readonly List<RAudioStream> threadLocalStreams = new List<RAudioStream>();

        Thread underlyingThread;
        volatile bool cancelled;

        public float UpdateRate { get; private set; }
        public int BufferSize { get; private set; }

        static RAudioStreamer instance;
        public static RAudioStreamer Instance
        {
            get
            {
                lock (singletonMutex)
                {
                    if (instance == null)
                        throw new InvalidOperationException("No instance running");
                    return instance;
                }
            }
            private set { lock (singletonMutex) instance = value; }
        }

        /// <summary>
        /// Constructs an RAudioStreamer that plays ogg files in the background, useful for background music
        /// </summary>
        /// <param name="bufferSize">Buffer size</param>
        /// <param name="updateRate">Number of times per second to update</param>
        /// <param name="internalThread">True to use an internal thread, false to use your own thread, in which case use must call EnsureBuffersFilled periodically</param>
        public RAudioStreamer(int bufferSize = DefaultBufferSize, float updateRate = DefaultUpdateRate, bool internalThread = true)
        {
            lock (singletonMutex)
            {
                if (instance != null)
                    throw new InvalidOperationException("Already running");

                Instance = this;
                if (internalThread)
                {
                    underlyingThread = new Thread(EnsureBuffersFilled) { Priority = ThreadPriority.Lowest };
                    underlyingThread.Start();
                }
                else
                {
                    // no need for this, user is in charge
                    updateRate = 0;
                }
            }

            UpdateRate = updateRate;
            BufferSize = bufferSize;

            readSampleBuffer = new float[bufferSize];
            castBuffer = new short[bufferSize];

            
        }

        public void Dispose()
        {
            lock (singletonMutex)
            {
                Debug.Assert(Instance == this, "Two instances running, somehow...?");

                cancelled = true;
                lock (iterationMutex)
                    streams.Clear();

                Instance = null;
                underlyingThread = null;
            }
        }

        internal bool AddStream(RAudioStream stream)
        {
            lock (iterationMutex)
                return streams.Add(stream);
        }
        internal bool RemoveStream(RAudioStream stream)
        {
            lock (iterationMutex) 
                return streams.Remove(stream);
        }

        public bool FillBuffer(RAudioStream stream, int bufferId)
        {
            int readSamples;
            lock (readMutex)
            {   
                readSamples = stream.Reader.ReadSamples(readSampleBuffer, 0, BufferSize);
                CastBuffer(readSampleBuffer, castBuffer, readSamples);
            }
            AL.BufferData(bufferId, stream.Reader.Channels == 1 ? ALFormat.Mono16 : ALFormat.Stereo16, castBuffer,
                          stream.Reader.SampleRate);
            ALHelper.Check();

            

            return readSamples != BufferSize;
        }
        public static void CastBuffer(float[] inBuffer, short[] outBuffer, int length)
        {
            for (int i = 0; i < length; i++)
            {
                var temp = (int)(32767f * inBuffer[i]);
                if (temp > short.MaxValue) temp = short.MaxValue;
                else if (temp < short.MinValue) temp = short.MinValue;
                outBuffer[i] = (short)temp;
            }
        }

        public void EnsureBuffersFilled()
        {
            do
            {
                threadLocalStreams.Clear();
                lock (iterationMutex) threadLocalStreams.AddRange(streams);

                foreach (var stream in threadLocalStreams)
                {
                    lock (stream.prepareMutex)
                    {
                        lock (iterationMutex)
                            if (!streams.Contains(stream))
                                continue;

                        bool finished = false;

                        int queued;
                        AL.GetSource(stream.alSourceId, ALGetSourcei.BuffersQueued, out queued);
                        ALHelper.Check();
                        int processed;
                        AL.GetSource(stream.alSourceId, ALGetSourcei.BuffersProcessed, out processed);
                        ALHelper.Check();

                        if (processed == 0 && queued == stream.BufferCount) continue;

                        int[] tempBuffers;
                        if (processed > 0)
                            tempBuffers = AL.SourceUnqueueBuffers(stream.alSourceId, processed);
                        else
                            tempBuffers = stream.alBufferIds.Skip(queued).ToArray();

                        int bufIdx = 0;
                        for (; bufIdx < tempBuffers.Length; bufIdx++)
                        {
                            finished |= FillBuffer(stream, tempBuffers[bufIdx]);

                            if (finished)
                            {
                                if (stream.IsLooped)
                                {
                                    stream.Reader.DecodedTime = TimeSpan.Zero;
                                    if (bufIdx == 0)
                                    {
                                        // we didn't have any buffers left over, so let's start from the beginning on the next cycle...
                                        continue;
                                    }
                                }
                                else
                                {
                                    lock (stream.stopMutex)
                                    {
                                        stream.NotifyFinished();
                                    }
                                    streams.Remove(stream);
                                    break;
                                }
                            }
                        }

                        AL.SourceQueueBuffers(stream.alSourceId, bufIdx, tempBuffers);
                        ALHelper.Check();

                        if (finished && !stream.IsLooped)
                            continue;
                    }

                    lock (stream.stopMutex)
                    {
                        if (stream.Preparing) continue;

                        lock (iterationMutex)
                            if (!streams.Contains(stream))
                                continue;

                        var state = ALHelper.GetSourceState(stream.alSourceId);
                        if (state == ALSourceState.Stopped)
                        {
                            
                            AL.SourcePlay(stream.alSourceId);
                            ALHelper.Check();
                        }
                    }
                }

                if (UpdateRate > 0)
                {
                    Thread.Sleep((int)(1000 / UpdateRate));
                }
            }
            while (underlyingThread != null && !cancelled);
        }
    }
    
}
