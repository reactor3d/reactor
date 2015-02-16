using Reactor;
using Reactor.Math;
using Reactor.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBed
{
    class Game : RGame
    {
        RMesh sponza;
        public override void Dispose()
        {
            
        }

        public override void Init()
        {
            Engine.InitGameWindow(800, 600, RWindowStyle.Normal);
            Engine.SetShowFPS(true);
            Engine.SetViewport(new RViewport(0,0,800, 600));

            sponza = Engine.Scene.Create<RMesh>("sponza");
            sponza.LoadSourceModel("/models/sponza.obj");
            sponza.CullEnable = false;
            sponza.CullMode = Reactor.Types.States.RCullMode.CullClockwiseFace;
            sponza.SetScale(10f);
            sponza.SetPosition(0, 0, 0);
            var cam = Engine.GetCamera();
            cam.SetPosition(0, 20, -1f);
            cam.LookAt(Vector3.Zero);
            cam.SetClipPlanes(0.01f, 100000f);
        }

        public override void Render()
        {
            Engine.Clear(RColor.Gray);

            sponza.Render();
            Engine.Present();
        }

        public override void Resized(int Width, int Height)
        {
            Engine.SetViewport(new RViewport(0,0,Width, Height));
        }

        public override void Update()
        {
            var cam = Engine.GetCamera();
            if (Engine.Input.IsKeyDown(RKey.W))
                cam.Move(-cam.Matrix.Forward * 0.1f);
            if (Engine.Input.IsKeyDown(RKey.S))
                cam.Move(cam.Matrix.Forward * 0.1f);

            sponza.Update();
            cam.Update();
        }
    }
}
