using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Reactor.Types
{
    public abstract class RSingleton<T> where T : class
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() => Create(), true);

        public static T Instance { 
            get { 
                try { return _instance.Value; } catch(Exception e){RLog.Error(e); return null;}
            }
        }

        private static T Create()
        {
            return (T)Activator.CreateInstance(typeof(T), true);
        }

    }
}
