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

namespace PlatformTest
{
    public class Game : RGame
    {
        RMesh mesh;
        public Game()
        {
        }

        public override void Init()
        {
            Engine.InitGameWindow(Engine.CurrentDisplayMode, RWindowStyle.Borderless);
            mesh = Engine.Scene.Create<RMesh>("test");
            mesh.LoadSourceModel("/meshes/test.dae");
            mesh.IsDrawable = true;
            mesh.IsEnabled = true;

        }

        public override void Render()
        {
            Engine.Clear();

            mesh.Render();
            Engine.Present();
        }

        public override void Update()
        {
            mesh.Update();
        }

        public override void Dispose()
        {
            Engine.Dispose();
        }

        public override void Resized(int Width, int Height)
        {
            RViewport viewport = Engine.GetViewport();
            viewport.Width = Width;
            viewport.Height = Height;
            Engine.SetViewport(viewport);
        }
    }
}

