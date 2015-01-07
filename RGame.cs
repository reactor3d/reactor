using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reactor
{
    /// <summary>
    /// Provides a facility for creating a main Game complete with a <see cref="Reactor.RGameWindow"/>.
    /// This class must be inherited in your game as the main component.
    /// </summary>
    public abstract class RGame : IDisposable
    {
        /// <summary>
        /// The internal game window.
        /// </summary>
        /// <value>The game window.</value>
        public RGameWindow GameWindow { get { return gameWindow; } }
        private RGameWindow gameWindow;

        /// <summary>
        /// Main Reactor Engine reference.  Use this within your <see cref="Reactor.RGame"/> class. 
        /// </summary>
        /// <value>The engine.</value>
        public REngine Engine { get { return REngine.Instance; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reactor.RGame"/> class.
        /// </summary>
        public RGame()
        {
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
        /// <example>
        /// public override void Init()
        /// {
        ///    Engine.InitGameWindow(800,600);
        ///    // TODO: Initialize more systems and load your content.
        /// }
        /// </example>
        public abstract void Init();


        /// <summary>
        /// Override for control over rendering.  This is always required.  This will be called everytime a frame needs to be rendered.
        /// </summary>
        /// <example>
        /// public override void Render()
        /// {
        ///    Engine.Clear();
        ///    // TODO: Render your game objects or call Scene.RenderAll(); if you wish for the scene manager to attempt to render them all.
        ///    Engine.Present();
        /// }
        /// </example>
        public abstract void Render();

        /// <summary>
        /// Override for control over updating.  This is always required.  This will be called everytime a frame needs updating.
        /// This is also where you can check for input and things as well.
        /// </summary>
        /// <example>
        /// public override void Update()
        /// {
        ///    camera.Update();
        ///    mesh.Update();
        ///    //etc...
        /// }
        /// </example>
        public abstract void Update();

        /// <summary>
        /// Releases all resource used by the <see cref="Reactor.RGame"/> object.  This is where you would also remove any user loaded content.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Reactor.RGame"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="Reactor.RGame"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the <see cref="Reactor.RGame"/> so the garbage
        /// collector can reclaim the memory that the <see cref="Reactor.RGame"/> was occupying.</remarks>
        public abstract void Dispose();

        /// <summary>
        /// Gets called when the underlying <see cref="RGameWindow"/> is resized;
        /// </summary>
        public abstract void Resized();

        /// <summary>
        /// Run the game.  This starts the message pump and loops required to receive events in <see cref="Render"/> and <see cref="Update"/> 
        /// </summary>
        public void Run()
        {
            Init();
            gameWindow.Run();
            Dispose();
        }

        void GameWindow_Resize(object sender, EventArgs e)
        {
            Resized();
        }

        void GameWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            Update();
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
