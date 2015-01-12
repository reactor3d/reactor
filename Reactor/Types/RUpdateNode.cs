using Reactor.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

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
        internal Matrix4 matrix = Matrix4.Identity;
        internal Quaternion rotation = Quaternion.FromMatrix(Matrix3.Identity);
        internal Vector3 scale = Vector3.One;

        public Vector3 Position { get { return position; } set { position = value; UpdateMatrix(); } }
        public Matrix4 Matrix { get { return matrix; } set { matrix = value; UpdateMatrix();} }
        public Quaternion Rotation { get { return rotation; } set { rotation = value; UpdateMatrix();} }
        public Vector3 Scale { get { return scale; } set { scale = value; } }

        void UpdateMatrix()
        {
            rotation = Quaternion.FromAxisAngle(Vector3.UnitX, rotation.X) * Quaternion.FromAxisAngle(Vector3.UnitY, rotation.Y) * Quaternion.FromAxisAngle(Vector3.UnitZ, rotation.Z);
            rotation.Normalize();
            matrix *= Matrix4.Identity * Matrix4.CreateScale(scale) * Matrix4.CreateRotationX(rotation.X) * Matrix4.CreateRotationY(rotation.Y) * Matrix4.CreateRotationZ(rotation.Z) * Matrix4.CreateTranslation(position);

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix3 GetRotationMatrix()
        {
            return Matrix3.CreateFromQuaternion(rotation);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Quaternion GetRotation()
        {
            return rotation;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMatrix(ref Matrix4 Matrix)
        {
            matrix = Matrix;
            scale = matrix.ExtractScale();
            rotation = matrix.ExtractRotation();
            position = matrix.ExtractTranslation();
            //UpdateMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetRotation(ref Quaternion rotation)
        {
            this.rotation = rotation;
            //UpdateMatrix();
            
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateX(float value)
        {
            rotation.X += Quaternion.FromAxisAngle(Vector3.UnitX, value).X;
            //UpdateMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateY(float value)
        {
            rotation.Y += Quaternion.FromAxisAngle(Vector3.UnitY, value).Y;
            //UpdateMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RotateZ(float value)
        {
            rotation.Z += Quaternion.FromAxisAngle(Vector3.UnitZ, value).Z;
            //UpdateMatrix();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Rotate(Quaternion value)
        {
            rotation = value;
            //UpdateMatrix();
        }

        public void SetPosition(Vector3 value)
        {
            position = value;
            //UpdateMatrix();
        }

        public void SetPosition(float X, float Y, float Z)
        {
            position.X = X;
            position.Y = Y;
            position.Z = Z;
            //UpdateMatrix();
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
        public void Move(float x, float y, float z)
        {
            position.X += x;
            position.Y += y;
            position.Z += z;
            //UpdateMatrix();
        }

        public void Move(Vector3 dir)
        {
            position += dir;
            //UpdateMatrix();
        }
        public void LookAt(Vector3 target)
        {
            matrix = Matrix4.Identity * Matrix4.CreateScale(scale) * Matrix4.LookAt(position, target, Vector3.UnitY);
            //UpdateMatrix();

        }
    }
}
