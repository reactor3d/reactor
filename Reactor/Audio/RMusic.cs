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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Audio
{
    public class RMusic
    {
        RAudioStream stream;

        public bool IsPlaying
        {
            get;
            private set;
        }

        public float Volume
        {
            get { return stream.Volume; }
            set { stream.Volume = value; }
        }

        public float LowPassGain
        {
            get { return stream.LowPassHFGain; }
            set { stream.LowPassHFGain = value; }
        }

        public bool IsLooped
        {
            get { return stream.IsLooped; }
            set { stream.IsLooped = value; }
        }
        public event EventHandler Finished;
        public RMusic(string filename, string name) : this(File.OpenRead(filename), name){}

        public RMusic(FileStream fileStream, string name)
        {
            stream = new RAudioStream(fileStream, name);
            stream.Open();
            stream.Finished = new EventHandler((o, e) =>
            {
                if (Finished != null)
                    Finished.Invoke(o, e);
            });
        }

        public void Play()
        {
            IsPlaying = true;
            stream.Prepare();
            
            stream.Play();
        }

        public void Pause()
        {
            IsPlaying = false;
            stream.Pause();
        }

        public void Stop()
        {
            
            IsPlaying = false;
            stream.Stop();
            Notify();
        }

        public void Resume()
        {
            IsPlaying = true;
            stream.Resume();
        }

        void Notify()
        {
            if (Finished != null)
                Finished.Invoke(this, null);
        }
    }
}
