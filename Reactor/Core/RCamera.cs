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
        public float Zoom { get; set; }


        public RCamera()
        {
            Near = 1.0f;
            Far = 100.0f;
            FieldOfView = 70f;
            Zoom = 1.0f;
            Up = Vector3.UnitY;
            View = Matrix.CreateLookAt (Vector3.Zero, -Vector3.UnitZ, Vector3.UnitY);
            ViewDirection = View.Forward;
            Projection = Matrix.CreatePerspectiveFieldOfView (MathHelper.ToRadians (FieldOfView),
                    REngine.Instance.GetViewport().AspectRatio, Near, Far);
            Position = Vector3.Zero;
            GL.DepthRange (Near, Far);
        }

        public RCamera(float AspectRatio)
        {
            Near = 1.0f;
            Far = 100.0f;
            FieldOfView = 70f;
            Zoom = 1.0f;
            Up = Vector3.UnitY;
            View = Matrix.CreateLookAt (Vector3.Zero, -Vector3.UnitZ, Vector3.UnitY);
            ViewDirection = View.Forward;
            Projection = Matrix.CreatePerspectiveFieldOfView (MathHelper.ToRadians (FieldOfView),
                                AspectRatio, Near, Far);
            Position = Vector3.Zero;
            GL.DepthRange (Near, Far);
        }

        public override void Update()
        {
            
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians (FieldOfView),
                                REngine.Instance.GetViewport().AspectRatio, Near, Far);
            View = Matrix.CreateLookAt(Position, Position + ViewDirection, Up);
            
            Matrix = View;

            GL.DepthRange (Near, Far);
        }
        

    }
}

