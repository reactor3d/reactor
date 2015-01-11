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
        internal Vector3 rotation = Vector3.Zero;
        internal Matrix matrix = Matrix.Identity;
        internal Quaternion quaternion = Quaternion.CreateFromRotationMatrix(Matrix.Identity);
        internal Vector3 scale = Vector3.One;

        public Vector3 Position { get { return position; } set { position = value; UpdateMatrix(); } }
        public Vector3 Rotation { get { return rotation; } set { rotation = value; UpdateMatrix();} }
        public Matrix Matrix { get { return matrix; } set { matrix = value; UpdateMatrix();} }
        public Quaternion Quaternion { get { return quaternion; } set { quaternion = value; UpdateMatrix();} }
        public Vector3 Scale { get { return scale; } set { scale = value; } }

        void UpdateMatrix()
        {
            rotation = quaternion.Xyz;
            matrix = Matrix.Identity * Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(quaternion) * Matrix.CreateTranslation(position);

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix GetRotationMatrix()
        {
            return quaternion.ToMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion GetRotation()
        {
            return quaternion;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetRotationMatrix(ref Matrix rotationMatrix)
        {
            matrix = rotationMatrix;
            matrix.Translation = position;
            matrix.Decompose(out scale, out quaternion, out position);
            UpdateMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetRotation(ref Quaternion quaternion)
        {
            this.quaternion = quaternion;
            UpdateMatrix();
            
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateX(float value)
        {
            quaternion.X += value;
            UpdateMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateY(float value)
        {
            quaternion.Y += value;
            UpdateMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateZ(float value)
        {
            quaternion.Z += value;
            UpdateMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Rotate(Vector3 value)
        {
            rotation = value;
            quaternion.Xyz = rotation;
            UpdateMatrix();
        }

        public void SetPosition(Vector3 value)
        {
            position = value;
            UpdateMatrix();
        }

        public void SetPosition(float X, float Y, float Z)
        {
            position.X = X;
            position.Y = Y;
            position.Z = Z;
            UpdateMatrix();
        }

        public Vector3 GetPosition()
        {
            return position;
        }

        public void SetScale(Vector3 value)
        {
            scale = value;
        }

        public void SetScale(float value)
        {
            scale = new Vector3(value, value, value);
        }

        public void SetScale(float x, float y, float z)
        {
            scale.X = x;
            scale.Y = y;
            scale.Z = z;
        }

        public Vector3 GetScale()
        {
            return scale;
        }
        public void Move(float X, float Y, float Z)
        {
            position.X += X;
            position.Y += Y;
            position.Z += Z;
            UpdateMatrix();
        }

        public void Move(Vector3 dir)
        {
            position += dir;
            UpdateMatrix();
        }
        public void LookAt(Vector3 target)
        {
            matrix = Matrix.Identity * Matrix.CreateScale(scale) * Matrix.CreateLookAt(position, target, Vector3.UnitY);
            matrix.Translation = position;
            matrix.Decompose(out scale, out quaternion, out position);
            rotation = quaternion.Xyz;

        }
    }
}
