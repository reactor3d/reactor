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
        internal Vector3 Position = Vector3.Zero;
        internal Vector3 Rotation = Vector3.Zero;
        internal Matrix Matrix = Matrix.Identity;
        internal Quaternion Quaternion = Quaternion.CreateFromRotationMatrix(Matrix.Identity);
        internal Vector3 Scale = Vector3.One;
        void UpdateMatrix()
        {
            Rotation = Quaternion.Xyz;
            Matrix = Matrix.Identity * Matrix.CreateScale(Scale) * Matrix.CreateFromQuaternion(Quaternion) * Matrix.CreateTranslation(Position);

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix GetRotationMatrix()
        {
            return Quaternion.ToMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion GetRotation()
        {
            return Quaternion;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetRotationMatrix(ref Matrix matrix)
        {
            Matrix = matrix;
            Matrix.Translation = Position;
            Matrix.Decompose(out Scale, out Quaternion, out Position);
            UpdateMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetRotation(ref Quaternion quaternion)
        {
            Quaternion = quaternion;
            UpdateMatrix();
            
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateX(float value)
        {
            Quaternion.X += value;
            UpdateMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateY(float value)
        {
            Quaternion.Y += value;
            UpdateMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateZ(float value)
        {
            Quaternion.Z += value;
            UpdateMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Rotate(Vector3 value)
        {
            Quaternion.Xyz = Rotation;
            UpdateMatrix();
        }

        public void SetPosition(Vector3 value)
        {
            Position = value;
            UpdateMatrix();
        }

        public void SetPosition(float X, float Y, float Z)
        {
            Position.X = X;
            Position.Y = Y;
            Position.Z = Z;
            UpdateMatrix();
        }

        public void Move(float X, float Y, float Z)
        {
            Position.X += X;
            Position.Y += Y;
            Position.Z += Z;
            UpdateMatrix();
        }

        public void Move(Vector3 dir)
        {
            Position += dir;
            UpdateMatrix();
        }
        public void LookAt(Vector3 target)
        {
            Matrix = Matrix.Identity * Matrix.CreateScale(Scale) * Matrix.CreateLookAt(Position, target, Vector3.UnitY);
            Matrix.Translation = Position;
            Matrix.Decompose(out Scale, out Quaternion, out Position);
            Rotation = Quaternion.Xyz;

        }
    }
}
