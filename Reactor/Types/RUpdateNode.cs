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
using System.Runtime.CompilerServices;
using Reactor.Math;

namespace Reactor.Types
{
    public class RUpdateNode : RSceneNode
    {
        internal Matrix matrix = Matrix.Identity;
        internal Vector3 position = Vector3.Zero;
        internal Quaternion rotation = Quaternion.Identity;
        internal Vector3 scale = Vector3.One;
        public bool IsEnabled { get; set; }

        public Vector3 Position
        {
            get => position;
            set => position = value;
        }

        public Matrix Matrix
        {
            get => matrix;
            set
            {
                matrix = value;
                Matrix.Decompose(out scale, out rotation, out position);
            }
        }

        public Quaternion Rotation
        {
            get => rotation;
            set => rotation = value;
        }

        public Vector3 Scale
        {
            get => scale;
            set => scale = value;
        }

        public event Action OnUpdate;

        public virtual void Update()
        {
            if (IsEnabled)
            {
                UpdateMatrix();
                if (OnUpdate != null)
                    OnUpdate();
            }
        }

        private void UpdateMatrix()
        {
            //rotation = Quaternion.FromAxisAngle(Vector3.UnitX, rotation.X) * Quaternion.FromAxisAngle(Vector3.UnitY, rotation.Y) * Quaternion.FromAxisAngle(Vector3.UnitZ, rotation.Z);
            //rotation.Normalize();
            //matrix = Matrix.Identity;
            BuildScalingMatrix(ref matrix);
            BuildRotationMatrix(ref matrix);
            BuildPositionMatrix(ref matrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateX(float value)
        {
            rotation *= Quaternion.CreateFromAxisAngle(Vector3.UnitX, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateY(float value)
        {
            rotation *= Quaternion.CreateFromAxisAngle(Vector3.UnitY, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateZ(float value)
        {
            rotation *= Quaternion.CreateFromAxisAngle(Vector3.UnitZ, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Rotate(Quaternion value)
        {
            rotation *= value;
        }

        public void Rotate(float qx, float qy, float qz)
        {
            var q = Quaternion.CreateFromAxisAngle(Vector3.UnitX, qx) *
                    Quaternion.CreateFromAxisAngle(Vector3.UnitY, qy) *
                    Quaternion.CreateFromAxisAngle(Vector3.UnitZ, qz);
            rotation *= q;
        }

        public void Move(float left, float up, float forward)
        {
            var p = Position;
            p += matrix.Left * left;
            p += matrix.Up * up;
            p += matrix.Forward * forward;
            Position = p;
        }

        public void Move(Vector3 m)
        {
            Move(m.X, m.Y, m.Z);
        }

        public void MoveTo(Vector3 value)
        {
            Position = value;
        }

        public void LookAt(Vector3 target)
        {
            Matrix = Matrix.CreateScale(scale) * Matrix.CreateLookAt(position, target, Vector3.UnitY) *
                     Matrix.CreateTranslation(position);
        }

        internal void BuildRotationMatrix(ref Matrix m)
        {
            //rotation.Normalize();
            m *= Matrix.CreateFromQuaternion(rotation);
            //m *= Matrix.CreateRotationX(rotation.X);
            //m *= Matrix.CreateRotationY(rotation.Y);
            //m *= Matrix.CreateRotationZ(rotation.Z);
            //return m;
        }

        internal void BuildScalingMatrix(ref Matrix m)
        {
            m *= Matrix.CreateScale(Scale);
        }

        internal void BuildPositionMatrix(ref Matrix m)
        {
            m *= Matrix.CreateTranslation(Position);
        }
    }
}