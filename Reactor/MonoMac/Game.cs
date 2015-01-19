//
// Game.cs
//
// Author:
//       Gabriel Reiser <gabriel@reisergames.com>
//
// Copyright (c) 2015 2014
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
using System;
using Reactor;
using Reactor.Types;
using Reactor.Math;
using Reactor.Platform;
using Reactor.Types.States;


namespace MonoMac
{
    public class Game : RGame
    {
        RMesh mesh;
        RCamera camera;
        RTexture2D bg;
        RViewport viewport;
        RFont font;
        float alpha = 0;
        public Game()
        {
        }

        public override void Init()
        {
            Engine.InitGameWindow(Engine.CurrentDisplayMode, RWindowStyle.Borderless);
            camera = new RCamera();
            camera.SetPosition(Vector3.UnitZ * -10f);
            camera.LookAt(new Vector3(0, 0, -1f) * 10f);
            camera.SetClipPlanes(1f, 100f);
            camera.Update();
            Engine.SetCamera(camera);
            mesh = Engine.Scene.Create<RMesh>("test");
            mesh.LoadSourceModel("/meshes/bunny.x");
            mesh.IsDrawable = true;
            mesh.IsEnabled = true;
            mesh.SetScale(0.01f);
            mesh.Update();
            viewport = Engine.GetViewport();
            Engine.Screen.Init();
            bg = (RTexture2D)Engine.Textures.CreateTexture<RTexture2D>("bg", "/reisergames.png");

            font = new RFont();
        }

        public override void Render()
        {
            Engine.Clear(RColor.DarkGray * 0.2f);

            mesh.Render();

            //Engine.Screen.AlphaBlendMode = RBlendFunc.Add;
            //Engine.Screen.AlphaSourceBlend = RBlend.SourceAlpha;
            //Engine.Screen.AlphaDestinationBlend = RBlend.One;
            Engine.Screen.BlendState = RBlendState.AlphaBlend;
            Engine.Screen.Begin();
            float a = (float)(Math.Sin(alpha));
            //Engine.Screen.RenderFullscreenQuad();

            Engine.Screen.RenderTexture(bg, new Rectangle(10,400, 1400, 900), RColor.White);
            Engine.Screen.RenderText(font, new Vector2(10,100), 8, "Testing font rendering and \n multiline data");
            Engine.Screen.End();
            Engine.Present();
        }

        public override void Update()
        {
            alpha*=1.0000001f;

            if(Engine.Input.IsKeyDown(RKey.A))
                camera.RotateY(-5f * Engine.GetTime());
            if(Engine.Input.IsKeyDown(RKey.D))
                camera.RotateY(5f * Engine.GetTime());
            if(Engine.Input.IsKeyDown(RKey.W))
                camera.Move(Vector3.Normalize(camera.ViewDirection) * -5f * Engine.GetTime());
            if(Engine.Input.IsKeyDown(RKey.S))
                camera.Move(Vector3.Normalize(camera.ViewDirection) * 5f * Engine.GetTime());
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

