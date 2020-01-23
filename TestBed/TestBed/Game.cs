using Reactor;
using Reactor.Math;
using Reactor.Platform;
using Reactor.Types;
using Reactor.Types.States;
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
        RFont font;
        public override void Dispose()
        {
            sponza.Dispose();
        }

        public override void Init()
        {
            Engine.InitGameWindow(1920, 1080, RWindowStyle.Normal);
            GameWindow.VSync = OpenTK.VSyncMode.On;
            Engine.SetShowFPS(true);
            Engine.SetViewport(new RViewport(0,0,1920, 1080));
            //Engine.InitHDR();

            GameWindow.CursorVisible = true;
            GameWindow.Location = new System.Drawing.Point(0, 0);
            sponza = Engine.Scene.Create<RMesh>("sponza");
            sponza.LoadSourceModel("/models/sponza.fbx");
            sponza.CullEnable = false;
            sponza.CullMode = RCullMode.None;
            sponza.SetScale(1f);
            sponza.SetPosition(0, 0, 0);
            sponza.BlendEnable = false;
            var cam = Engine.GetCamera();
            cam.SetPosition(0, 20, 1f);
            cam.LookAt(Vector3.Zero - Vector3.UnitZ);
            cam.SetClipPlanes(0.01f, 10000f);

            font = RScreen.Instance.LoadFont("/vcr_osd_mono.ttf", 16);

            RTexture2D posX = (RTexture2D)Engine.Textures.CreateTexture<RTexture2D>("posX", "/textures/sky-posX.png");
            RTexture2D posY = (RTexture2D)Engine.Textures.CreateTexture<RTexture2D>("posY", "/textures/sky-posY.png");
            RTexture2D posZ = (RTexture2D)Engine.Textures.CreateTexture<RTexture2D>("posZ", "/textures/sky-posZ.png");
            RTexture2D negX = (RTexture2D)Engine.Textures.CreateTexture<RTexture2D>("negX", "/textures/sky-negX.png");
            RTexture2D negY = (RTexture2D)Engine.Textures.CreateTexture<RTexture2D>("negY", "/textures/sky-negY.png");
            RTexture2D negZ = (RTexture2D)Engine.Textures.CreateTexture<RTexture2D>("negZ", "/textures/sky-negZ.png");

            var skyboxTexture = new RTexture3D();
            skyboxTexture.Create(RPixelFormat.Rgb, ref posX, ref posY, ref posZ, ref negX, ref negY, ref negZ);

            Engine.Atmosphere.CreateSkyBox(skyboxTexture);
            GameWindow.Visible = true;
            
        }

        public override void Render()
        {
            Engine.Clear(RColor.Black, true, true);
            var cam = Engine.GetCamera();

            //Engine.BeginHDR();
            Engine.Clear(RColor.Black, true);
            //cam.SetClipPlanes(0.01f, 10000f);
            Engine.Atmosphere.RenderSkybox();
            sponza.Render();
            //Engine.EndHDR();

            //Engine.DrawHDR();

            Engine.Screen.Begin();

            Engine.Screen.RenderText(font, new Vector2(5, 30), String.Format("{0} MB P", Profiler.GetPhysicalMemory()));
            Engine.Screen.RenderText(font, new Vector2(5, 50), String.Format("{0} MB V", Profiler.GetVirtualMemory()));
            Engine.Screen.RenderText(font, new Vector2(5, 70), String.Format("{0} MB GC", Profiler.GetGCMemory()));

            Engine.Screen.End();
            Engine.Present();
        }

        public override void Resized(int Width, int Height)
        {
            var rect = GameWindow.ClientRectangle;
            Engine.SetViewport(new RViewport(0,0,rect.Width, rect.Height));
        }
        Vector2 panning = Vector2.Zero;
        Vector3 position = Vector3.Zero;
        Vector2 prev_mouse = Vector2.Zero;
        public override void Update()
        {
            var dt = Engine.GetTime();
            int X, Y, Wheel = 0;
            Engine.Input.GetMouse(out X, out Y, out Wheel);
            
            var window_bounds = GameWindow.ClientRectangle;
            Vector2 mouse_position = new Vector2(X, Y);
            Vector2 mouse_direction = mouse_position - prev_mouse;
            RLog.Info(String.Format("Mouse [ X:{0}, Y:{1} ]", mouse_direction.X, mouse_direction.Y));

            var cam = Engine.GetCamera();

            if (Engine.Input.IsKeyDown(RKey.W))
                position += cam.ViewDirection * 0.5f;
            if (Engine.Input.IsKeyDown(RKey.S))
                position -= cam.ViewDirection * 0.5f;
            if (Engine.Input.IsKeyDown(RKey.A))
                position += Vector3.Cross(cam.Up, cam.ViewDirection) * 0.5f;
            if (Engine.Input.IsKeyDown(RKey.D))
                position -= Vector3.Cross(cam.Up, cam.ViewDirection) * 0.5f;

            sponza.Update();
            cam.Position = position;
            
            cam.ViewDirection = Vector3.Transform(cam.ViewDirection, Matrix.CreateFromAxisAngle(
                                cam.Up, (-MathHelper.PiOver4 / 150) * mouse_direction.X));

            cam.ViewDirection = Vector3.Transform(cam.ViewDirection, Matrix.CreateFromAxisAngle(
                                Vector3.Cross(cam.Up, cam.ViewDirection),
                                (MathHelper.PiOver4 / 100) * mouse_direction.Y));

            cam.Up = Vector3.Transform(cam.Up, Matrix.CreateFromAxisAngle(Vector3.Cross(cam.Up, cam.ViewDirection),
                      (MathHelper.PiOver4 / 100) * mouse_direction.Y));
            cam.View = Matrix.CreateLookAt(cam.Position, cam.Position + cam.ViewDirection, cam.Up);
            cam.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(70f), Engine.GetViewport().AspectRatio, 0.1f, 1000f);


            prev_mouse = mouse_position;
            Engine.Input.CenterMouse();

            if (Engine.Input.IsKeyDown(RKey.Escape))
                GameWindow.Exit();
            
        }
    }
}
