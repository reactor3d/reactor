using Reactor;
using System;
using System.Threading;
using Microsoft.Win32;
using Reactor.Types;

namespace Red
{
    public class RedApp : RGame
    {
        private RViewport mainViewport;
        public override void Init()
        {
            mainViewport = new RViewport(1280, 720);
            // Initialization Code Here
            Engine.InitGameWindow((int)mainViewport.Width, (int)mainViewport.Height, RWindowStyle.Normal, "Red");
            // Now that we have initialized, we can use our game window.
            GameWindow.CenterOnScreen();
            Engine.SetClearColor(RColor.DarkGray);
            Engine.SetShowFPS(false);
            
        }

        public override void Render()
        {
            Engine.Clear(true, true);
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