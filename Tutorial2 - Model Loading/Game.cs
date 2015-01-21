using Reactor;
using Reactor.Math;
using Reactor.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tutorial2___Model_Loading
{
    class Game : RGame
    {
        RMesh mesh;
        RCamera camera;
        RViewport viewport;
        RTexture2D texture;
        RFont font;
        public Game()
        {
        }

        public override void Init()
        {
            Engine.InitGameWindow(new RDisplayMode(1920, 1080, -1, RSurfaceFormat.Color), RWindowStyle.Normal);
            camera = Engine.GetCamera();
            camera.Position = Vector3.Zero;
            camera.LookAt(Vector3.Forward);
            camera.SetClipPlanes(0.1f, 1000f);
            camera.Update();
            mesh = Engine.Scene.Create<RMesh>("test");
            mesh.LoadSourceModel("/meshes/bunny.x");
            mesh.IsDrawable = true;
            mesh.IsEnabled = true;
            mesh.SetScale(0.1f);
            mesh.SetPosition(Vector3.Zero);

            Engine.Screen.Init();
            viewport = Engine.GetViewport();
            texture = (RTexture2D)Engine.Textures.CreateTexture<RTexture2D>("test", "/textures/fields.jpg");
            font = new RFont();
        }

        public override void Render()
        {
            Engine.Clear();

            mesh.Render();

            Engine.Screen.Begin();
            Engine.Screen.RenderText(font, new Vector2(10, 200), "This is a test on PC");
            Engine.Screen.RenderTexture(texture, new Rectangle(200,400, 100, 100));
            Engine.Screen.End();
            Engine.Present();
        }

        public override void Update()
        {
            if (Engine.Input.IsKeyDown(RKey.A))
                camera.RotateY(-5f);
            if (Engine.Input.IsKeyDown(RKey.D))
                camera.RotateY(5f);
            if (Engine.Input.IsKeyDown(RKey.W))
                camera.Move(Vector3.Normalize(camera.ViewDirection) * -0.5f);
            if (Engine.Input.IsKeyDown(RKey.S))
                camera.Move(Vector3.Normalize(camera.ViewDirection) * 0.5f);
            mesh.RotateY(-0.1f);
            mesh.Update();
            camera.Update();

        }

        public override void Dispose()
        {
            Engine.Dispose();
        }

        public override void Resized(int Width, int Height)
        {
            viewport.Width = Width;
            viewport.Height = Height;
            Engine.SetViewport(viewport);
        }
    }
}
