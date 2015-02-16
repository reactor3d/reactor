//
// RCamera.cs
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
using Reactor.Types;
using Reactor.Math;


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

        public float FieldOfView { get; set; }

        public float Near { get; set; }
        public float Far { get; set; }
        public RCamera()
        {
            Near = 1.0f;
            Far = 100.0f;
            FieldOfView = 70f;
            IsEnabled = true;
            OnUpdate += (sender, e) => {
                //ViewDirection = Vector3.Transform(new Vector3(0, 0, -1f), GetRotation());
                viewMatrix = new Matrix(
                    matrix.M11, matrix.M12, matrix.M13, 0,
                    matrix.M21, matrix.M22, matrix.M23, 0,
                    matrix.M31, matrix.M32, matrix.M33, 0,
                    -Vector3.Dot(Position, matrix.Right), -Vector3.Dot(Position, matrix.Up), -Vector3.Dot(Position, matrix.Forward), 1
                );
                //viewMatrix = Matrix.CreateLookAt(this.Position, this.Position + matrix.Forward, matrix.Up);

                ViewDirection = viewMatrix.Forward;
                RViewport viewport = REngine.Instance._viewport;
                projMatrix = Matrix.Identity * Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(FieldOfView), viewport.AspectRatio, Near, Far);
            };
        }

        public void SetClipPlanes(float near, float far)
        {
            Near = near;
            Far = far;
        }
    }
}

