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
