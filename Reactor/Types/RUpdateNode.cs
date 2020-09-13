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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Types
{
    public class RUpdateNode : RSceneNode
    {
        public bool IsEnabled { get; set; }

        public event EventHandler OnUpdate;
        public virtual void Update()
        {
            UpdateMatrix();
            if (IsEnabled)
            {
                if (OnUpdate != null)
                    OnUpdate(this, null);
            }
        }
        internal Vector3 position = Vector3.Zero;
        internal Matrix matrix = Matrix.Identity;
        internal Quaternion rotation = Quaternion.Identity;
        internal Vector3 scale = Vector3.One;

        public Vector3 Position { get { return position; } set { position = value; } }
        public Matrix Matrix { get { return matrix; } set { matrix = value; } }
        public Quaternion Rotation { get { return rotation; } set { rotation = value; } }
        public Vector3 Scale { get { return scale; } set { scale = value; } }

        void UpdateMatrix()
        {
            //rotation = Quaternion.FromAxisAngle(Vector3.UnitX, rotation.X) * Quaternion.FromAxisAngle(Vector3.UnitY, rotation.Y) * Quaternion.FromAxisAngle(Vector3.UnitZ, rotation.Z);
            //rotation.Normalize();
            //matrix = Matrix.Identity;
            BuildScalingMatrix(ref matrix);
            BuildRotationMatrix(ref matrix);
            BuildPositionMatrix(ref matrix);
            
            

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMatrix(Matrix Matrix)
        {
            matrix = Matrix;
            matrix.Decompose(out scale, out rotation, out position);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateX(float value)
        {
            rotation *= Quaternion.CreateFromAxisAngle(Vector3.UnitX, value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateY(float value)
        {
            rotation *= Quaternion.CreateFromAxisAngle (Vector3.UnitY, value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateZ(float value)
        {
            rotation *= Quaternion.CreateFromAxisAngle (Vector3.UnitZ, value);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Rotate(Quaternion value)
        {
            rotation *= value;
        }
        public void Rotate(float qx, float qy, float qz)
        {
            
            Quaternion q = Quaternion.CreateFromAxisAngle(Vector3.UnitX, qx) *
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
            SetMatrix(Matrix.CreateScale (scale) * Matrix.CreateLookAt (position, target, Vector3.UnitY) * Matrix.CreateTranslation (position));
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
