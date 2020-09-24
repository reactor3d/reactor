using Reactor;
using System;
using Reactor.Types;

namespace Red
{
    public class RedApp : RGame
    {
        private RViewport mainViewport;
        public override void Init()
        {
            mainViewport = new RViewport(1920, 1080);
            // Initialization Code Here
            Engine.InitGameWindow((int)mainViewport.Width, (int)mainViewport.Height, RWindowStyle.Normal, "Red");
            Engine.SetClearColor(RColor.DarkGray);
            Engine.SetShowFPS(true);
            
        }

        public override void Render()
        {
            Engine.Clear(RColor.DarkGray, true, true);
            // Rendering Code
            Engine.Present();
        }

        public override void Update()
        {
            // Update Code
            if (Engine.Input.IsKeyDown(RKey.Escape) == true)
            {
                this.GameWindow.Exit();
            }
            
        }

        public override void Dispose()
        {
            Engine.Dispose();
        }

        public override void Resized(int Width, int Height)
        {
            mainViewport.Width = (float) Width;
            mainViewport.Height = (float) Height;
            Engine.SetViewport(mainViewport);
        }
        
    }
}