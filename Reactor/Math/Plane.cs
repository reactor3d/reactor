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
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Reactor.Math
{
    internal class PlaneHelper
    {
        /// <summary>
        ///     Returns a value indicating what side (positive/negative) of a plane a point is
        /// </summary>
        /// <param name="point">The point to check with</param>
        /// <param name="plane">The plane to check against</param>
        /// <returns>Greater than zero if on the positive side, less than zero if on the negative size, 0 otherwise</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ClassifyPoint(ref Vector3 point, ref Plane plane)
        {
            return point.X * plane.Normal.X + point.Y * plane.Normal.Y + point.Z * plane.Normal.Z + plane.D;
        }

        /// <summary>
        ///     Returns the perpendicular distance from a point to a plane
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <param name="plane">The place to check</param>
        /// <returns>The perpendicular distance from the point to the plane</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float PerpendicularDistance(ref Vector3 point, ref Plane plane)
        {
            // dist = (ax + by + cz + d) / sqrt(a*a + b*b + c*c)
            return (float)System.Math.Abs(
                (plane.Normal.X * point.X + plane.Normal.Y * point.Y + plane.Normal.Z * point.Z)
                / System.Math.Sqrt(plane.Normal.X * plane.Normal.X + plane.Normal.Y * plane.Normal.Y +
                                   plane.Normal.Z * plane.Normal.Z));
        }
    }

    [DebuggerDisplay("{DebugDisplayString,nq}")]
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    public struct Plane : IEquatable<Plane>
    {
        #region Public Fields

        [FieldOffset(0)] public float D;

        [FieldOffset(4)] public Vector3 Normal;

        #endregion Public Fields


        #region Constructors

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Plane(Vector4 value)
            : this(new Vector3(value.X, value.Y, value.Z), value.W)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Plane(Vector3 normal, float d)
        {
            Normal = normal;
            D = d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Plane(Vector3 a, Vector3 b, Vector3 c)
        {
            var ab = b - a;
            var ac = c - a;

            var cross = Vector3.Cross(ab, ac);
            Normal = Vector3.Normalize(cross);
            D = -Vector3.Dot(Normal, a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Plane(float a, float b, float c, float d)
            : this(new Vector3(a, b, c), d)
        {
        }

        #endregion Constructors


        #region Public Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Dot(Vector4 value)
        {
            return Normal.X * value.X + Normal.Y * value.Y + Normal.Z * value.Z + D * value.W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dot(ref Vector4 value, out float result)
        {
            result = Normal.X * value.X + Normal.Y * value.Y + Normal.Z * value.Z + D * value.W;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float DotCoordinate(Vector3 value)
        {
            return Normal.X * value.X + Normal.Y * value.Y + Normal.Z * value.Z + D;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DotCoordinate(ref Vector3 value, out float result)
        {
            result = Normal.X * value.X + Normal.Y * value.Y + Normal.Z * value.Z + D;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float DotNormal(Vector3 value)
        {
            return Normal.X * value.X + Normal.Y * value.Y + Normal.Z * value.Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DotNormal(ref Vector3 value, out float result)
        {
            result = Normal.X * value.X + Normal.Y * value.Y + Normal.Z * value.Z;
        }

        /*
        public static void Transform(ref Plane plane, ref Quaternion rotation, out Plane result)
        {
            throw new NotImplementedException();
        }

        public static void Transform(ref Plane plane, ref Matrix matrix, out Plane result)
        {
            throw new NotImplementedException();
        }

        public static Plane Transform(Plane plane, Quaternion rotation)
        {
            throw new NotImplementedException();
        }

        public static Plane Transform(Plane plane, Matrix matrix)
        {
            throw new NotImplementedException();
        }
        */
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
        {
            float factor;
            var normal = Normal;
            Normal = Vector3.Normalize(Normal);
            factor = (float)System.Math.Sqrt(Normal.X * Normal.X + Normal.Y * Normal.Y + Normal.Z * Normal.Z) /
                     (float)System.Math.Sqrt(normal.X * normal.X + normal.Y * normal.Y + normal.Z * normal.Z);
            D = D * factor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Plane Normalize(Plane value)
        {
            Plane ret;
            Normalize(ref value, out ret);
            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Normalize(ref Plane value, out Plane result)
        {
            float factor;
            result.Normal = Vector3.Normalize(value.Normal);
            factor = (float)System.Math.Sqrt(result.Normal.X * result.Normal.X + result.Normal.Y * result.Normal.Y +
                                             result.Normal.Z * result.Normal.Z) /
                     (float)System.Math.Sqrt(value.Normal.X * value.Normal.X + value.Normal.Y * value.Normal.Y +
                                             value.Normal.Z * value.Normal.Z);
            result.D = value.D * factor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Plane plane1, Plane plane2)
        {
            return !plane1.Equals(plane2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Plane plane1, Plane plane2)
        {
            return plane1.Equals(plane2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other)
        {
            return other is Plane ? Equals((Plane)other) : false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Plane other)
        {
            return Normal == other.Normal && D == other.D;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return Normal.GetHashCode() ^ D.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PlaneIntersectionType Intersects(BoundingBox box)
        {
            return box.Intersects(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Intersects(ref BoundingBox box, out PlaneIntersectionType result)
        {
            box.Intersects(ref this, out result);
        }

        /*
        public PlaneIntersectionType Intersects(BoundingFrustum frustum)
        {
            return frustum.Intersects(this);
        }
        */
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PlaneIntersectionType Intersects(BoundingSphere sphere)
        {
            return sphere.Intersects(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Intersects(ref BoundingSphere sphere, out PlaneIntersectionType result)
        {
            sphere.Intersects(ref this, out result);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return "{{Normal:" + Normal + " D:" + D + "}}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator System.Numerics.Plane(Plane plane)
        {
            return new System.Numerics.Plane(plane.Normal, plane.D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Plane(System.Numerics.Plane plane)
        {
            return new Plane(plane.Normal, plane.D);
        }

        #endregion
    }
}