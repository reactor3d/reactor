using Reactor;
using System;
using Reactor.Input;
using Reactor.Types;

namespace Red
{
    public class RedApp : RGame
    {
        private RViewport mainViewport;
        public override void Init()
        {
            mainViewport = new RViewport(800, 600);
            // Initialization Code Here
            Engine.InitGameWindow((int)mainViewport.Width, (int)mainViewport.Height, RWindowStyle.Normal, "Red");
            Engine.SetClearColor(RColor.Black);
        }

        public override void Render()
        {
            // Rendering Code
        }

        public override void Update()
        {
            // Update Code
            if (Engine.Input.IsKeyDown(RKey.Escape) == true)
            {
                this.Dispose();
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