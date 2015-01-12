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
using OpenTK;

namespace Reactor
{
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
            FieldOfView = MathHelper.DegreesToRadians(70.0f);
            IsEnabled = true;
            OnUpdate += (sender, e) => {
                //ViewDirection = Vector3.Transform(new Vector3(0, 0, -1f), GetRotation());
                viewMatrix = new Matrix4(
                    matrix.Column0.X, matrix.Column1.X, matrix.Column2.X, 0,
                    matrix.Column0.Y, matrix.Column1.Y, matrix.Column2.Y, 0,
                    matrix.Column0.Z, matrix.Column1.Z, matrix.Column2.Z, 0,
                    Vector3.Dot(Position, matrix.Column0.Xyz), Vector3.Dot(Position, matrix.Column1.Xyz), Vector3.Dot(Position, matrix.Column2.Xyz), 1
                );

                ViewDirection = new Vector3(viewMatrix.M31, viewMatrix.M32, -viewMatrix.M33);

                Projection = Matrix4.CreatePerspectiveFieldOfView(FieldOfView, REngine.Instance._viewport.AspectRatio, Near, Far);
            };
        }

        public void SetClipPlanes(float near, float far)
        {
            Near = near;
            Far = far;
        }
    }
}

