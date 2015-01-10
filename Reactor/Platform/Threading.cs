using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reactor.Platform
{
    internal class Threading
    {
        public const int kMaxWaitForUIThread = 750; // In milliseconds


        static int mainThreadId;
        public static IGraphicsContext BackgroundContext;
        public static IWindowInfo WindowInfo;

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
                BackgroundContext.MakeCurrent(WindowInfo);
                // Execute the action
                action();
                // Must flush the GL calls so the texture is ready for the main context to use
                GL.Flush();
                REngine.CheckGLError();
                // Must make the context not current on this thread or the next thread will get error 170 from the MakeCurrent call
                BackgroundContext.MakeCurrent(null);
            }
            
        }
    }
}
