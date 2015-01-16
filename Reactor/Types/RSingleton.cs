using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Reactor.Types
{
    public abstract class RSingleton<T> where T : class, new()
    {
        private static readonly T _instance = new T();

        public static T Instance { 
            get { 
                return _instance;
            }
        }

        protected static T Create()
        {
            return (T)Activator.CreateInstance(typeof(T), true);
        }

    }
}
