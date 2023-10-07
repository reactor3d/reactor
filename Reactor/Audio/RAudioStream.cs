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

using System;
using System.IO;
using NVorbis;
using Reactor.Audio.OpenAL;

namespace Reactor.Audio
{
    public class RAudioStream
    {
        private const int DefaultBufferCount = 3;
        internal readonly int[] alBufferIds;

        private readonly int alFilterId;

        internal readonly int alSourceId;
        internal readonly object prepareMutex = new object();

        internal readonly object stopMutex = new object();
        private readonly Stream underlyingStream;

        internal EventHandler Finished;

        private float lowPassHfGain;

        private float volume;

        public RAudioStream(string filename, string name, int bufferCount = DefaultBufferCount) : this(
            File.OpenRead(filename), name, bufferCount)
        {
        }

        public RAudioStream(Stream stream, string name, int bufferCount = DefaultBufferCount)
        {
            Name = name;

            BufferCount = bufferCount;

            alBufferIds = AL.GenBuffers(bufferCount);
            alSourceId = AL.GenSource();

            Volume = 1;

            alFilterId = ALC.EFX.GenFilter();
            ALC.EFX.Filter(alFilterId, FilterInteger.FilterType, (int)FilterType.Lowpass);
            ALC.EFX.Filter(alFilterId, FilterFloat.LowpassGain, 1);
            LowPassHFGain = 1;

            underlyingStream = stream;
        }

        internal VorbisReader Reader { get; private set; }
        public bool Ready { get; private set; }
        internal bool Preparing { get; private set; }

        public int BufferCount { get; private set; }

        public string Name { get; set; }

        public float LowPassHFGain
        {
            get => lowPassHfGain;
            set
            {
                ALC.EFX.Filter(alFilterId, FilterFloat.LowpassGainHF, lowPassHfGain = value);
                ALC.EFX.Source(alSourceId, EFXSourceInteger.DirectFilter, alFilterId);
                ALHelper.Check();
            }
        }

        public float Volume
        {
            get => volume;
            set
            {
                AL.Source(alSourceId, ALSourcef.Gain, volume = value);
                ALHelper.Check();
            }
        }

        public bool IsLooped { get; set; }

        public void Prepare()
        {
            if (Preparing) return;
            var state = ALHelper.GetSourceState(alSourceId);

            lock (stopMutex)
            {
                switch (state)
                {
                    case ALSourceState.Playing:
                    case ALSourceState.Paused:
                        return;

                    case ALSourceState.Stopped:
                        lock (prepareMutex)
                        {
                            Reader.DecodedTime = TimeSpan.Zero;
                            Ready = false;
                            Empty();
                        }

                        break;
                }

                if (!Ready)
                    lock (prepareMutex)
                    {
                        Preparing = true;
                        RLog.Info("Preparing audio stream: " + Name);
                        Open(true);
                        RLog.Info("Finished preparing audio stream: " + Name);
                    }
            }
        }

        public void Play()
        {
            var state = ALHelper.GetSourceState(alSourceId);

            switch (state)
            {
                case ALSourceState.Playing: return;
                case ALSourceState.Paused:
                    Resume();
                    return;
            }

            Prepare();


            AL.SourcePlay(alSourceId);
            ALHelper.Check();

            Preparing = false;

            RAudioStreamer.Instance.AddStream(this);
        }

        public void Pause()
        {
            if (ALHelper.GetSourceState(alSourceId) != ALSourceState.Playing)
                return;

            RAudioStreamer.Instance.RemoveStream(this);

            AL.SourcePause(alSourceId);
            ALHelper.Check();
        }

        public void Resume()
        {
            if (ALHelper.GetSourceState(alSourceId) != ALSourceState.Paused)
                return;

            RAudioStreamer.Instance.AddStream(this);

            AL.SourcePlay(alSourceId);
            ALHelper.Check();
        }

        public void Stop()
        {
            var state = ALHelper.GetSourceState(alSourceId);
            if (state == ALSourceState.Playing || state == ALSourceState.Paused) StopPlayback();

            lock (stopMutex)
            {
                RAudioStreamer.Instance.RemoveStream(this);
            }
        }

        public void Dispose()
        {
            var state = ALHelper.GetSourceState(alSourceId);
            if (state == ALSourceState.Playing || state == ALSourceState.Paused)
                StopPlayback();

            lock (prepareMutex)
            {
                RAudioStreamer.Instance.RemoveStream(this);

                if (state != ALSourceState.Initial)
                    Empty();

                Close();

                underlyingStream.Dispose();
            }

            AL.DeleteSource(alSourceId);
            AL.DeleteBuffers(alBufferIds);

            ALC.EFX.DeleteFilter(alFilterId);

            ALHelper.Check();
        }

        private void StopPlayback()
        {
            AL.SourceStop(alSourceId);
            ALHelper.Check();
        }

        internal void NotifyFinished()
        {
            var callback = Finished;
            if (callback != null)
            {
                callback(this, EventArgs.Empty);
                Finished = null; // This is not typical...  Usually we count on whatever code added the event handler to also remove it
            }
        }

        private void Empty()
        {
            int queued;
            AL.GetSource(alSourceId, ALGetSourcei.BuffersQueued, out queued);
            if (queued > 0)
                try
                {
                    AL.SourceUnqueueBuffers(alSourceId, queued);
                    ALHelper.Check();
                }
                catch (InvalidOperationException)
                {
                    // This is a bug in the OpenAL implementation
                    // Salvage what we can
                    int processed;
                    AL.GetSource(alSourceId, ALGetSourcei.BuffersProcessed, out processed);
                    var salvaged = new int[processed];
                    if (processed > 0)
                    {
                        AL.SourceUnqueueBuffers(alSourceId, processed, salvaged);
                        ALHelper.Check();
                    }

                    // Try turning it off again?
                    AL.SourceStop(alSourceId);
                    ALHelper.Check();

                    Empty();
                }
        }

        internal void Open(bool precache = false)
        {
            underlyingStream.Seek(0, SeekOrigin.Begin);
            Reader = new VorbisReader(underlyingStream, false);

            if (precache)
            {
                // Fill first buffer synchronously
                RAudioStreamer.Instance.FillBuffer(this, alBufferIds[0]);
                AL.SourceQueueBuffer(alSourceId, alBufferIds[0]);
                ALHelper.Check();

                // Schedule the others asynchronously
                RAudioStreamer.Instance.AddStream(this);
            }

            Ready = true;
        }

        internal void Close()
        {
            if (Reader != null)
            {
                Reader.Dispose();
                Reader = null;
            }

            Ready = false;
        }
    }
}