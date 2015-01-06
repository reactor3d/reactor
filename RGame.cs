using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reactor
{
    public class RGame : IDisposable
    {
        public GameWindow GameWindow { get { return gameWindow; } }
        private GameWindow gameWindow;

        public REngine Engine { get { return REngine.Instance; } }

        public RGame()
        {
            REngine.RGame = this;
            gameWindow = new GameWindow(800, 600);
            REngine.Instance.SetViewport(new RViewport(0, 0, 800, 600));
            gameWindow.RenderFrame += GameWindow_RenderFrame;
            gameWindow.UpdateFrame += GameWindow_UpdateFrame;
            gameWindow.Resize += GameWindow_Resize;
            gameWindow.Closed += gameWindow_Closed;
        }

        void gameWindow_Closed(object sender, EventArgs e)
        {
            gameWindow.Exit();
        }
        public virtual void Init()
        {
            
        }
        public virtual void Render()
        {

        }
        public virtual void Update()
        {

        }
        public virtual void Dispose()
        {

        }

        public void Run()
        {
            Init();
            gameWindow.Run();
            Dispose();
        }

        void GameWindow_Resize(object sender, EventArgs e)
        {
            REngine.Instance.SetViewport(new RViewport(0,0,GameWindow.ClientSize.Width, GameWindow.ClientSize.Height));
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
