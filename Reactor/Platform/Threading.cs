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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Reactor.Platform.GLFW;
using Reactor.Platform.OpenGL;
using Reactor.Types;

namespace Reactor.Platform
{
    
    public class RThreadPool : RSingleton<RThreadPool>
    {
        
        public RThreadPool()
        {
        }
        public int ThreadCount
        {
            get
            {
                int workers, p;
                ThreadPool.GetAvailableThreads(out workers, out p);
                return workers;
            }
        }

        public async void Queue(Action action, object state)
        {
            if(state != null)
            {
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    action.DynamicInvoke(o);
                }, state);
            }
            else
            {
                ThreadPool.QueueUserWorkItem((_) =>
                {
                    action.Invoke();
                });
            }
        }
        public async void Queue(Action action, Action callback)
        {
            ThreadPool.QueueUserWorkItem((_) =>
            {
                action.BeginInvoke((r) =>
                {
                    action.EndInvoke(r);
                    if (r.IsCompleted)
                    {
                        callback();
                    }
                }, null);
                
            });
        }
        public async void Queue(Task task)
        {
            ThreadPool.QueueUserWorkItem((_) => 
            {
                task.Start(TaskScheduler.Default);

            });
        }
    }
    internal class Threading
    {
        public const int kMaxWaitForUIThread = 750; // In milliseconds


        static int mainThreadId;
        public static IGraphicsContext BackgroundContext;

        static Threading()
        {
            mainThreadId = Thread.CurrentThread.ManagedThreadId;
        }


        /// <summary>
        /// Checks if the code is currently running on the UI thread.
        /// </summary>
        /// <returns>true if the code is currently running on the UI thread.</returns>
        public static bool IsOnUIThread()
        {
            return mainThreadId == Thread.CurrentThread.ManagedThreadId;
        }

        /// <summary>
        /// Throws an exception if the code is not currently running on the UI thread.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the code is not currently running on the UI thread.</exception>
        public static void EnsureUIThread()
        {
            if (!IsOnUIThread())
                throw new InvalidOperationException("Operation not called on UI thread.");
        }

        /// <summary>
        /// Runs the given action on the UI thread and blocks the current thread while the action is running.
        /// If the current thread is the UI thread, the action will run immediately.
        /// </summary>
        /// <param name="action">The action to be run on the UI thread</param>
        internal static void BlockOnUIThread(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            // If we are already on the UI thread, just call the action and be done with it
            if (IsOnUIThread())
            {
                action();
                return;
            }

            lock (BackgroundContext)
            {
                // Make the context current on this thread
                BackgroundContext.MakeCurrent();
                // Execute the action
                action();
                // Must flush the GL calls so the texture is ready for the main context to use
                GL.Flush();
                REngine.CheckGLError();
                // Must make the context not current on this thread or the next thread will get error 170 from the MakeCurrent call
                BackgroundContext.MakeNoneCurrent();
            }
        }

        public void RunOnThreadPool(Action action)
        {
            if(!IsOnUIThread())
            {
                action();
            }
            else
            {
                new Thread(() => { action(); }).Start();
            }
        }
    }
}
