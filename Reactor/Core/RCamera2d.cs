// Author:
//       Gabriel Reiser <gabe@reisergames.com>
//
// Copyright (c) 2010-2016 Reiser Games, LLC.
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

using Reactor.Math;
using Reactor.Platform.OpenGL;

namespace Reactor
{
    public class RCamera2d : RCamera
    {
        public RCamera2d()
        {
            var viewport = REngine.Instance._viewport;
            Near = 0.1f;
            Far = 10.0f;
            FieldOfView = 70f;
            Zoom = 1.0f;
            Up = Vector3.UnitY;

            View = Matrix.CreateLookAt(Vector3.Zero, -Vector3.UnitZ, Vector3.UnitY);
            ViewDirection = View.Forward;
            Projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, Near, Far);
            Position = Vector3.Zero;
            GL.DepthRange(Near, Far);
        }

        public override void Update()
        {
            var viewport = REngine.Instance._viewport;
            Projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, Near, Far);
        }
    }
}