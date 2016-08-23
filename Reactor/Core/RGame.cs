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
using OpenTK;
using Reactor.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reactor
{
    /// <summary>
    /// Provides a facility for creating a main Game complete with a <see cref="RGameWindow"/>.
    /// This class must be inherited in your game as the main component.
    /// </summary>
    public abstract class RGame : IDisposable
    {
        public RGameTime GameTime { get { return gameTime; } }
        private RGameTime gameTime;
        private DateTime startTime;
        private DateTime lastTime;
        /// <summary>
        /// The internal game window.
        /// </summary>
        /// <value>The game window.</value>
        public RGameWindow GameWindow { get { return gameWindow; } }
        private RGameWindow gameWindow;

        /// <summary>
        /// Main Reactor Engine reference.  Use this within your <see cref="RGame"/> class. 
        /// </summary>
        /// <value>The engine.</value>
        public REngine Engine { get { return REngine.Instance; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGame"/> class.
        /// </summary>
        public RGame()
        {
            gameTime = new RGameTime();
            startTime = DateTime.UtcNow;
            REngine.RGame = this;
            gameWindow = new RGameWindow(800, 600);
            REngine.Instance.SetViewport(new RViewport(0, 0, 800, 600));
            gameWindow.RenderFrame += GameWindow_RenderFrame;
            gameWindow.UpdateFrame += GameWindow_UpdateFrame;
            gameWindow.Resize += GameWindow_Resize;
            gameWindow.Closed += gameWindow_Closed;
            gameWindow.VSync = VSyncMode.On;
        }

        void gameWindow_Closed(object sender, EventArgs e)
        {
            gameWindow.Exit();
        }

        /// <summary>
        /// Override for Initialization code.  Override this and setup the engine in this block.
        /// </summary>
        /// <example>Example of how to initialize the engine with a window of 800x600.
        /// <code>
        /// public override void Init()
        /// {
        ///    Engine.InitGameWindow(800,600);
        ///    // TODO: Initialize more systems and load your content.
        /// }
        /// </code>
        /// </example>
        public abstract void Init();


        /// <summary>
        /// Override for control over rendering.  This is always required.  This will be called everytime a frame needs to be rendered.
        /// </summary>
        /// <example>Example render loop.  Clear must be called first, render anything you wish, then call Present() to swap the back buffer and show it on screen.
        /// <code>
        /// public override void Render()
        /// {
        ///    Engine.Clear();
        ///    // TODO: Render your game objects or call Scene.RenderAll(); if you wish for the scene manager to attempt to render them all.
        ///    Engine.Present();
        /// }
        /// </code>
        /// </example>
        public abstract void Render();

        /// <summary>
        /// Override for control over updating.  This is always required.  This will be called everytime a frame needs updating.
        /// This is also where you can check for input and things as well.
        /// </summary>
        /// <example>A typical update loop.  Anything your game needs to update should be called within this method.  If an object has an Update() method, it needs to be added somewhere within this method block flow.
        /// <code>public override void Update()
        /// {
        ///    camera.Update();
        ///    mesh.Update();
        ///    //etc...
        /// }
        /// </code>
        /// </example>
        public abstract void Update();

        /// <summary>
        /// Releases all resource used by the <see cref="RGame"/> object.  This is where you would also remove any user loaded content.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Reactor.RGame"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="Reactor.RGame"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the <see cref="Reactor.RGame"/> so the garbage
        /// collector can reclaim the memory that the <see cref="Reactor.RGame"/> was occupying.</remarks>
        public abstract void Dispose();

        /// <summary>
        /// Gets called when the underlying <see cref="RGameWindow"/> is resized;
        /// </summary>
        public abstract void Resized(int Width, int Height);

        /// <summary>
        /// Run the game.  This starts the message pump and loops required to receive events in <see cref="Render"/> and <see cref="Update"/> 
        /// </summary>
        public void Run()
        {
            Init();
            gameWindow.Run();
        }

        void GameWindow_Resize(object sender, EventArgs e)
        {
            Resized(GameWindow.Width, GameWindow.Height);
        }

        void GameWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            var now = DateTime.UtcNow;
            gameTime.TotalGameTime += (now - startTime);
            gameTime.ElapsedGameTime = (now - lastTime);
            Update();
            lastTime = now;
        }

        void GameWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            Render();
        }

        void IDisposable.Dispose()
        {
            this.Dispose();
        }
    }
}
