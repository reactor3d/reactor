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
using System;
using Reactor.Types;
using Reactor.Math;
using OpenTK.Graphics.OpenGL4;

namespace Reactor
{
    /// <summary>
    /// RCamera provides the cameras in the game.  This is only a very matrix Quaternion based camera, much like <see ref="RUpdateNode"> which is the basis of all moving things in Reactor.
    /// This class can be inherited to provide specific camera operation funtionality.
    /// Simply listen to the <see ref="OnUpdate"> event handler.
    /// </summary>
    public class RCamera : RCameraNode
    {
        public Vector3 ViewDirection { get; set; }
        public Vector3 Up { get; set; }
        public float FieldOfView { get; set; }

        public float Near { get; set; }
        public float Far { get; set; }
        public RCamera()
        {
            Near = 1.0f;
            Far = 100.0f;
            FieldOfView = 70f;
            IsEnabled = true;
            ViewDirection = Vector3.UnitZ;
            Up = -Vector3.UnitY;
            OnUpdate += (sender, e) => {

                Matrix = Matrix.CreateLookAt(Position, Position + ViewDirection, Up);
                RViewport viewport = REngine.Instance._viewport;
                Projection = Matrix.CreatePerspectiveFieldOfView (MathHelper.PiOver4, viewport.AspectRatio, Near, Far);
            };
        }

        public void SetClipPlanes(float near, float far)
        {
            Near = near;
            Far = far;
            GL.DepthRange(near, far);
        }

    }
}

