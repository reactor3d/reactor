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
            if (IsEnabled)
            {
                if (OnUpdate != null)
                    OnUpdate(this, null);
            }
        }
        public Matrix ObjectMatrix { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix GetRotationMatrix()
        {
            Vector3 scale, translation;
            Quaternion rotation;
            ObjectMatrix.Decompose(out scale, out rotation, out translation);
            return rotation.ToMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion GetRotation()
        {
            Vector3 scale, translation;
            Quaternion rotation;
            ObjectMatrix.Decompose(out scale, out rotation, out translation);
            return rotation;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetRotationMatrix(ref Matrix matrix)
        {
            Vector3 scale, translation;
            Quaternion rotation;
            ObjectMatrix.Decompose(out scale, out rotation, out translation);
            rotation = Quaternion.CreateFromRotationMatrix(matrix);
            ObjectMatrix = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(translation);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetRotation(ref Quaternion quaternion)
        {
            Vector3 scale, translation;
            Quaternion rotation;
            ObjectMatrix.Decompose(out scale, out rotation, out translation);
            rotation = quaternion;
            ObjectMatrix = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(translation);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateX(float value)
        {
            Vector3 scale, translation;
            Quaternion rotation;
            ObjectMatrix.Decompose(out scale, out rotation, out translation);
            rotation.X += value;
            ObjectMatrix = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(translation);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateY(float value)
        {
            Vector3 scale, translation;
            Quaternion rotation;
            ObjectMatrix.Decompose(out scale, out rotation, out translation);
            rotation.Y += value;
            ObjectMatrix = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(translation);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateZ(float value)
        {
            Vector3 scale, translation;
            Quaternion rotation;
            ObjectMatrix.Decompose(out scale, out rotation, out translation);
            rotation.Z += value;
            ObjectMatrix = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(translation);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Rotate(Vector3 value)
        {
            RotateX(value.X);
            RotateY(value.Y);
            RotateZ(value.Z);
        }

        public void SetPosition(Vector3 value)
        {
            Vector3 scale, translation;
            Quaternion rotation;
            ObjectMatrix.Decompose(out scale, out rotation, out translation);
            translation = value;
            ObjectMatrix = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(rotation) * Matrix.CreateTranslation(translation);
        }

        public void SetPosition(float X, float Y, float Z)
        {
            SetPosition(new Vector3(X, Y, Z));
        }
    }
}
