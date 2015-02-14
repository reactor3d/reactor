using Reactor;
using Reactor.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitGame
{
    public class Game : RGame
    {
        public override void Init()
        {
            Engine.InitGameWindow(800, 600, Reactor.Types.RWindowStyle.Normal, "Reactor Samples : Init Game");
            
        }

        public override void Render()
        {
            Engine.Clear();

            Engine.Present();
        }

        public override void Update()
        {

        }

        public override void Resized(int Width, int Height)
        {
            Engine.SetViewport(new RViewport(0, 0, Width, Height));
        }

        public override void Dispose()
        {

        }
    }
}
