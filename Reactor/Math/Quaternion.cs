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
using System.Runtime.InteropServices;
using System.Text;

namespace Reactor.Math
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 16, Pack = 4)]
    public struct Quaternion : IEquatable<Quaternion>
    {
        #region Public Fields

        [FieldOffset(0)] public float X;

        [FieldOffset(4)] public float Y;

        [FieldOffset(8)] public float Z;

        [FieldOffset(12)] public float W;

        #endregion


        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }


        public Quaternion(Vector3 vectorPart, float scalarPart)
        {
            X = vectorPart.X;
            Y = vectorPart.Y;
            Z = vectorPart.Z;
            W = scalarPart;
        }

        public static Quaternion Identity { get; } = new Quaternion(0, 0, 0, 1);


        public static Quaternion Add(Quaternion quaternion1, Quaternion quaternion2)
        {
            //Syderis
            Quaternion quaternion;
            quaternion.X = quaternion1.X + quaternion2.X;
            quaternion.Y = quaternion1.Y + quaternion2.Y;
            quaternion.Z = quaternion1.Z + quaternion2.Z;
            quaternion.W = quaternion1.W + quaternion2.W;
            return quaternion;
        }


        public static void Add(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            //Syderis
            result.X = quaternion1.X + quaternion2.X;
            result.Y = quaternion1.Y + quaternion2.Y;
            result.Z = quaternion1.Z + quaternion2.Z;
            result.W = quaternion1.W + quaternion2.W;
        }

        public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
        {
            Quaternion quaternion;
            var x = value2.X;
            var y = value2.Y;
            var z = value2.Z;
            var w = value2.W;
            var num4 = value1.X;
            var num3 = value1.Y;
            var num2 = value1.Z;
            var num = value1.W;
            var num12 = y * num2 - z * num3;
            var num11 = z * num4 - x * num2;
            var num10 = x * num3 - y * num4;
            var num9 = x * num4 + y * num3 + z * num2;
            quaternion.X = x * num + num4 * w + num12;
            quaternion.Y = y * num + num3 * w + num11;
            quaternion.Z = z * num + num2 * w + num10;
            quaternion.W = w * num - num9;
            return quaternion;
        }

        public static void Concatenate(ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            var x = value2.X;
            var y = value2.Y;
            var z = value2.Z;
            var w = value2.W;
            var num4 = value1.X;
            var num3 = value1.Y;
            var num2 = value1.Z;
            var num = value1.W;
            var num12 = y * num2 - z * num3;
            var num11 = z * num4 - x * num2;
            var num10 = x * num3 - y * num4;
            var num9 = x * num4 + y * num3 + z * num2;
            result.X = x * num + num4 * w + num12;
            result.Y = y * num + num3 * w + num11;
            result.Z = z * num + num2 * w + num10;
            result.W = w * num - num9;
        }

        public void Conjugate()
        {
            X = -X;
            Y = -Y;
            Z = -Z;
        }

        public static Quaternion Conjugate(Quaternion value)
        {
            Quaternion quaternion;
            quaternion.X = -value.X;
            quaternion.Y = -value.Y;
            quaternion.Z = -value.Z;
            quaternion.W = value.W;
            return quaternion;
        }

        public static void Conjugate(ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = value.W;
        }

        public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
        {
            Quaternion quaternion;
            var num2 = angle * 0.5f;
            var num = (float)System.Math.Sin(num2);
            var num3 = (float)System.Math.Cos(num2);
            quaternion.X = axis.X * num;
            quaternion.Y = axis.Y * num;
            quaternion.Z = axis.Z * num;
            quaternion.W = num3;
            return quaternion;
        }


        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Quaternion result)
        {
            var num2 = angle * 0.5f;
            var num = (float)System.Math.Sin(num2);
            var num3 = (float)System.Math.Cos(num2);
            result.X = axis.X * num;
            result.Y = axis.Y * num;
            result.Z = axis.Z * num;
            result.W = num3;
        }


        public static Quaternion CreateFromRotationMatrix(Matrix matrix)
        {
            var num8 = matrix.M11 + matrix.M22 + matrix.M33;
            var quaternion = new Quaternion();
            if (num8 > 0f)
            {
                var num = (float)System.Math.Sqrt(num8 + 1f);
                quaternion.W = num * 0.5f;
                num = 0.5f / num;
                quaternion.X = (matrix.M23 - matrix.M32) * num;
                quaternion.Y = (matrix.M31 - matrix.M13) * num;
                quaternion.Z = (matrix.M12 - matrix.M21) * num;
                return quaternion;
            }

            if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                var num7 = (float)System.Math.Sqrt(1f + matrix.M11 - matrix.M22 - matrix.M33);
                var num4 = 0.5f / num7;
                quaternion.X = 0.5f * num7;
                quaternion.Y = (matrix.M12 + matrix.M21) * num4;
                quaternion.Z = (matrix.M13 + matrix.M31) * num4;
                quaternion.W = (matrix.M23 - matrix.M32) * num4;
                return quaternion;
            }

            if (matrix.M22 > matrix.M33)
            {
                var num6 = (float)System.Math.Sqrt(1f + matrix.M22 - matrix.M11 - matrix.M33);
                var num3 = 0.5f / num6;
                quaternion.X = (matrix.M21 + matrix.M12) * num3;
                quaternion.Y = 0.5f * num6;
                quaternion.Z = (matrix.M32 + matrix.M23) * num3;
                quaternion.W = (matrix.M31 - matrix.M13) * num3;
                return quaternion;
            }

            var num5 = (float)System.Math.Sqrt(1f + matrix.M33 - matrix.M11 - matrix.M22);
            var num2 = 0.5f / num5;
            quaternion.X = (matrix.M31 + matrix.M13) * num2;
            quaternion.Y = (matrix.M32 + matrix.M23) * num2;
            quaternion.Z = 0.5f * num5;
            quaternion.W = (matrix.M12 - matrix.M21) * num2;

            return quaternion;
        }


        public static void CreateFromRotationMatrix(ref Matrix matrix, out Quaternion result)
        {
            var num8 = matrix.M11 + matrix.M22 + matrix.M33;
            if (num8 > 0f)
            {
                var num = (float)System.Math.Sqrt(num8 + 1f);
                result.W = num * 0.5f;
                num = 0.5f / num;
                result.X = (matrix.M23 - matrix.M32) * num;
                result.Y = (matrix.M31 - matrix.M13) * num;
                result.Z = (matrix.M12 - matrix.M21) * num;
            }
            else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                var num7 = (float)System.Math.Sqrt(1f + matrix.M11 - matrix.M22 - matrix.M33);
                var num4 = 0.5f / num7;
                result.X = 0.5f * num7;
                result.Y = (matrix.M12 + matrix.M21) * num4;
                result.Z = (matrix.M13 + matrix.M31) * num4;
                result.W = (matrix.M23 - matrix.M32) * num4;
            }
            else if (matrix.M22 > matrix.M33)
            {
                var num6 = (float)System.Math.Sqrt(1f + matrix.M22 - matrix.M11 - matrix.M33);
                var num3 = 0.5f / num6;
                result.X = (matrix.M21 + matrix.M12) * num3;
                result.Y = 0.5f * num6;
                result.Z = (matrix.M32 + matrix.M23) * num3;
                result.W = (matrix.M31 - matrix.M13) * num3;
            }
            else
            {
                var num5 = (float)System.Math.Sqrt(1f + matrix.M33 - matrix.M11 - matrix.M22);
                var num2 = 0.5f / num5;
                result.X = (matrix.M31 + matrix.M13) * num2;
                result.Y = (matrix.M32 + matrix.M23) * num2;
                result.Z = 0.5f * num5;
                result.W = (matrix.M12 - matrix.M21) * num2;
            }
        }

        public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            Quaternion quaternion;
            var num9 = roll * 0.5f;
            var num6 = (float)System.Math.Sin(num9);
            var num5 = (float)System.Math.Cos(num9);
            var num8 = pitch * 0.5f;
            var num4 = (float)System.Math.Sin(num8);
            var num3 = (float)System.Math.Cos(num8);
            var num7 = yaw * 0.5f;
            var num2 = (float)System.Math.Sin(num7);
            var num = (float)System.Math.Cos(num7);
            quaternion.X = num * num4 * num5 + num2 * num3 * num6;
            quaternion.Y = num2 * num3 * num5 - num * num4 * num6;
            quaternion.Z = num * num3 * num6 - num2 * num4 * num5;
            quaternion.W = num * num3 * num5 + num2 * num4 * num6;
            return quaternion;
        }

        public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Quaternion result)
        {
            var num9 = roll * 0.5f;
            var num6 = (float)System.Math.Sin(num9);
            var num5 = (float)System.Math.Cos(num9);
            var num8 = pitch * 0.5f;
            var num4 = (float)System.Math.Sin(num8);
            var num3 = (float)System.Math.Cos(num8);
            var num7 = yaw * 0.5f;
            var num2 = (float)System.Math.Sin(num7);
            var num = (float)System.Math.Cos(num7);
            result.X = num * num4 * num5 + num2 * num3 * num6;
            result.Y = num2 * num3 * num5 - num * num4 * num6;
            result.Z = num * num3 * num6 - num2 * num4 * num5;
            result.W = num * num3 * num5 + num2 * num4 * num6;
        }

        public static Quaternion Divide(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            var x = quaternion1.X;
            var y = quaternion1.Y;
            var z = quaternion1.Z;
            var w = quaternion1.W;
            var num14 = quaternion2.X * quaternion2.X + quaternion2.Y * quaternion2.Y + quaternion2.Z * quaternion2.Z +
                        quaternion2.W * quaternion2.W;
            var num5 = 1f / num14;
            var num4 = -quaternion2.X * num5;
            var num3 = -quaternion2.Y * num5;
            var num2 = -quaternion2.Z * num5;
            var num = quaternion2.W * num5;
            var num13 = y * num2 - z * num3;
            var num12 = z * num4 - x * num2;
            var num11 = x * num3 - y * num4;
            var num10 = x * num4 + y * num3 + z * num2;
            quaternion.X = x * num + num4 * w + num13;
            quaternion.Y = y * num + num3 * w + num12;
            quaternion.Z = z * num + num2 * w + num11;
            quaternion.W = w * num - num10;
            return quaternion;
        }

        public static void Divide(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            var x = quaternion1.X;
            var y = quaternion1.Y;
            var z = quaternion1.Z;
            var w = quaternion1.W;
            var num14 = quaternion2.X * quaternion2.X + quaternion2.Y * quaternion2.Y + quaternion2.Z * quaternion2.Z +
                        quaternion2.W * quaternion2.W;
            var num5 = 1f / num14;
            var num4 = -quaternion2.X * num5;
            var num3 = -quaternion2.Y * num5;
            var num2 = -quaternion2.Z * num5;
            var num = quaternion2.W * num5;
            var num13 = y * num2 - z * num3;
            var num12 = z * num4 - x * num2;
            var num11 = x * num3 - y * num4;
            var num10 = x * num4 + y * num3 + z * num2;
            result.X = x * num + num4 * w + num13;
            result.Y = y * num + num3 * w + num12;
            result.Z = z * num + num2 * w + num11;
            result.W = w * num - num10;
        }


        public static float Dot(Quaternion quaternion1, Quaternion quaternion2)
        {
            return quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z +
                   quaternion1.W * quaternion2.W;
        }


        public static void Dot(ref Quaternion quaternion1, ref Quaternion quaternion2, out float result)
        {
            result = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z +
                     quaternion1.W * quaternion2.W;
        }


        public override bool Equals(object obj)
        {
            var flag = false;
            if (obj is Quaternion) flag = Equals((Quaternion)obj);
            return flag;
        }


        public bool Equals(Quaternion other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }


        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
        }


        public static Quaternion Inverse(Quaternion quaternion)
        {
            Quaternion quaternion2;
            var num2 = quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z +
                       quaternion.W * quaternion.W;
            var num = 1f / num2;
            quaternion2.X = -quaternion.X * num;
            quaternion2.Y = -quaternion.Y * num;
            quaternion2.Z = -quaternion.Z * num;
            quaternion2.W = quaternion.W * num;
            return quaternion2;
        }

        public static void Inverse(ref Quaternion quaternion, out Quaternion result)
        {
            var num2 = quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z +
                       quaternion.W * quaternion.W;
            var num = 1f / num2;
            result.X = -quaternion.X * num;
            result.Y = -quaternion.Y * num;
            result.Z = -quaternion.Z * num;
            result.W = quaternion.W * num;
        }

        public float Length()
        {
            var num = X * X + Y * Y + Z * Z + W * W;
            return (float)System.Math.Sqrt(num);
        }


        public float LengthSquared()
        {
            return X * X + Y * Y + Z * Z + W * W;
        }


        public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            var num = amount;
            var num2 = 1f - num;
            var quaternion = new Quaternion();
            var num5 = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z +
                       quaternion1.W * quaternion2.W;
            if (num5 >= 0f)
            {
                quaternion.X = num2 * quaternion1.X + num * quaternion2.X;
                quaternion.Y = num2 * quaternion1.Y + num * quaternion2.Y;
                quaternion.Z = num2 * quaternion1.Z + num * quaternion2.Z;
                quaternion.W = num2 * quaternion1.W + num * quaternion2.W;
            }
            else
            {
                quaternion.X = num2 * quaternion1.X - num * quaternion2.X;
                quaternion.Y = num2 * quaternion1.Y - num * quaternion2.Y;
                quaternion.Z = num2 * quaternion1.Z - num * quaternion2.Z;
                quaternion.W = num2 * quaternion1.W - num * quaternion2.W;
            }

            var num4 = quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z +
                       quaternion.W * quaternion.W;
            var num3 = 1f / (float)System.Math.Sqrt(num4);
            quaternion.X *= num3;
            quaternion.Y *= num3;
            quaternion.Z *= num3;
            quaternion.W *= num3;
            return quaternion;
        }


        public static void Lerp(ref Quaternion quaternion1, ref Quaternion quaternion2, float amount,
            out Quaternion result)
        {
            var num = amount;
            var num2 = 1f - num;
            var num5 = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z +
                       quaternion1.W * quaternion2.W;
            if (num5 >= 0f)
            {
                result.X = num2 * quaternion1.X + num * quaternion2.X;
                result.Y = num2 * quaternion1.Y + num * quaternion2.Y;
                result.Z = num2 * quaternion1.Z + num * quaternion2.Z;
                result.W = num2 * quaternion1.W + num * quaternion2.W;
            }
            else
            {
                result.X = num2 * quaternion1.X - num * quaternion2.X;
                result.Y = num2 * quaternion1.Y - num * quaternion2.Y;
                result.Z = num2 * quaternion1.Z - num * quaternion2.Z;
                result.W = num2 * quaternion1.W - num * quaternion2.W;
            }

            var num4 = result.X * result.X + result.Y * result.Y + result.Z * result.Z + result.W * result.W;
            var num3 = 1f / (float)System.Math.Sqrt(num4);
            result.X *= num3;
            result.Y *= num3;
            result.Z *= num3;
            result.W *= num3;
        }


        public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            float num2;
            float num3;
            Quaternion quaternion;
            var num = amount;
            var num4 = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z +
                       quaternion1.W * quaternion2.W;
            var flag = false;
            if (num4 < 0f)
            {
                flag = true;
                num4 = -num4;
            }

            if (num4 > 0.999999f)
            {
                num3 = 1f - num;
                num2 = flag ? -num : num;
            }
            else
            {
                var num5 = (float)System.Math.Acos(num4);
                var num6 = (float)(1.0 / System.Math.Sin(num5));
                num3 = (float)System.Math.Sin((1f - num) * num5) * num6;
                num2 = flag ? (float)-System.Math.Sin(num * num5) * num6 : (float)System.Math.Sin(num * num5) * num6;
            }

            quaternion.X = num3 * quaternion1.X + num2 * quaternion2.X;
            quaternion.Y = num3 * quaternion1.Y + num2 * quaternion2.Y;
            quaternion.Z = num3 * quaternion1.Z + num2 * quaternion2.Z;
            quaternion.W = num3 * quaternion1.W + num2 * quaternion2.W;
            return quaternion;
        }


        public static void Slerp(ref Quaternion quaternion1, ref Quaternion quaternion2, float amount,
            out Quaternion result)
        {
            float num2;
            float num3;
            var num = amount;
            var num4 = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z +
                       quaternion1.W * quaternion2.W;
            var flag = false;
            if (num4 < 0f)
            {
                flag = true;
                num4 = -num4;
            }

            if (num4 > 0.999999f)
            {
                num3 = 1f - num;
                num2 = flag ? -num : num;
            }
            else
            {
                var num5 = (float)System.Math.Acos(num4);
                var num6 = (float)(1.0 / System.Math.Sin(num5));
                num3 = (float)System.Math.Sin((1f - num) * num5) * num6;
                num2 = flag ? (float)-System.Math.Sin(num * num5) * num6 : (float)System.Math.Sin(num * num5) * num6;
            }

            result.X = num3 * quaternion1.X + num2 * quaternion2.X;
            result.Y = num3 * quaternion1.Y + num2 * quaternion2.Y;
            result.Z = num3 * quaternion1.Z + num2 * quaternion2.Z;
            result.W = num3 * quaternion1.W + num2 * quaternion2.W;
        }


        public static Quaternion Subtract(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X - quaternion2.X;
            quaternion.Y = quaternion1.Y - quaternion2.Y;
            quaternion.Z = quaternion1.Z - quaternion2.Z;
            quaternion.W = quaternion1.W - quaternion2.W;
            return quaternion;
        }


        public static void Subtract(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            result.X = quaternion1.X - quaternion2.X;
            result.Y = quaternion1.Y - quaternion2.Y;
            result.Z = quaternion1.Z - quaternion2.Z;
            result.W = quaternion1.W - quaternion2.W;
        }


        public static Quaternion Multiply(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            var x = quaternion1.X;
            var y = quaternion1.Y;
            var z = quaternion1.Z;
            var w = quaternion1.W;
            var num4 = quaternion2.X;
            var num3 = quaternion2.Y;
            var num2 = quaternion2.Z;
            var num = quaternion2.W;
            var num12 = y * num2 - z * num3;
            var num11 = z * num4 - x * num2;
            var num10 = x * num3 - y * num4;
            var num9 = x * num4 + y * num3 + z * num2;
            quaternion.X = x * num + num4 * w + num12;
            quaternion.Y = y * num + num3 * w + num11;
            quaternion.Z = z * num + num2 * w + num10;
            quaternion.W = w * num - num9;
            return quaternion;
        }


        public static Quaternion Multiply(Quaternion quaternion1, float scaleFactor)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X * scaleFactor;
            quaternion.Y = quaternion1.Y * scaleFactor;
            quaternion.Z = quaternion1.Z * scaleFactor;
            quaternion.W = quaternion1.W * scaleFactor;
            return quaternion;
        }


        public static void Multiply(ref Quaternion quaternion1, float scaleFactor, out Quaternion result)
        {
            result.X = quaternion1.X * scaleFactor;
            result.Y = quaternion1.Y * scaleFactor;
            result.Z = quaternion1.Z * scaleFactor;
            result.W = quaternion1.W * scaleFactor;
        }


        public static void Multiply(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            var x = quaternion1.X;
            var y = quaternion1.Y;
            var z = quaternion1.Z;
            var w = quaternion1.W;
            var num4 = quaternion2.X;
            var num3 = quaternion2.Y;
            var num2 = quaternion2.Z;
            var num = quaternion2.W;
            var num12 = y * num2 - z * num3;
            var num11 = z * num4 - x * num2;
            var num10 = x * num3 - y * num4;
            var num9 = x * num4 + y * num3 + z * num2;
            result.X = x * num + num4 * w + num12;
            result.Y = y * num + num3 * w + num11;
            result.Z = z * num + num2 * w + num10;
            result.W = w * num - num9;
        }


        public static Quaternion Negate(Quaternion quaternion)
        {
            Quaternion quaternion2;
            quaternion2.X = -quaternion.X;
            quaternion2.Y = -quaternion.Y;
            quaternion2.Z = -quaternion.Z;
            quaternion2.W = -quaternion.W;
            return quaternion2;
        }


        public static void Negate(ref Quaternion quaternion, out Quaternion result)
        {
            result.X = -quaternion.X;
            result.Y = -quaternion.Y;
            result.Z = -quaternion.Z;
            result.W = -quaternion.W;
        }


        public void Normalize()
        {
            var num2 = X * X + Y * Y + Z * Z + W * W;
            var num = 1f / (float)System.Math.Sqrt(num2);
            X *= num;
            Y *= num;
            Z *= num;
            W *= num;
        }


        public static Quaternion Normalize(Quaternion quaternion)
        {
            Quaternion quaternion2;
            var num2 = quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z +
                       quaternion.W * quaternion.W;
            var num = 1f / (float)System.Math.Sqrt(num2);
            quaternion2.X = quaternion.X * num;
            quaternion2.Y = quaternion.Y * num;
            quaternion2.Z = quaternion.Z * num;
            quaternion2.W = quaternion.W * num;
            return quaternion2;
        }


        public static void Normalize(ref Quaternion quaternion, out Quaternion result)
        {
            var num2 = quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z +
                       quaternion.W * quaternion.W;
            var num = 1f / (float)System.Math.Sqrt(num2);
            result.X = quaternion.X * num;
            result.Y = quaternion.Y * num;
            result.Z = quaternion.Z * num;
            result.W = quaternion.W * num;
        }


        public static Quaternion operator +(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X + quaternion2.X;
            quaternion.Y = quaternion1.Y + quaternion2.Y;
            quaternion.Z = quaternion1.Z + quaternion2.Z;
            quaternion.W = quaternion1.W + quaternion2.W;
            return quaternion;
        }


        public static Quaternion operator /(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            var x = quaternion1.X;
            var y = quaternion1.Y;
            var z = quaternion1.Z;
            var w = quaternion1.W;
            var num14 = quaternion2.X * quaternion2.X + quaternion2.Y * quaternion2.Y + quaternion2.Z * quaternion2.Z +
                        quaternion2.W * quaternion2.W;
            var num5 = 1f / num14;
            var num4 = -quaternion2.X * num5;
            var num3 = -quaternion2.Y * num5;
            var num2 = -quaternion2.Z * num5;
            var num = quaternion2.W * num5;
            var num13 = y * num2 - z * num3;
            var num12 = z * num4 - x * num2;
            var num11 = x * num3 - y * num4;
            var num10 = x * num4 + y * num3 + z * num2;
            quaternion.X = x * num + num4 * w + num13;
            quaternion.Y = y * num + num3 * w + num12;
            quaternion.Z = z * num + num2 * w + num11;
            quaternion.W = w * num - num10;
            return quaternion;
        }


        public static bool operator ==(Quaternion quaternion1, Quaternion quaternion2)
        {
            return quaternion1.X == quaternion2.X && quaternion1.Y == quaternion2.Y && quaternion1.Z == quaternion2.Z &&
                   quaternion1.W == quaternion2.W;
        }


        public static bool operator !=(Quaternion quaternion1, Quaternion quaternion2)
        {
            if (quaternion1.X == quaternion2.X && quaternion1.Y == quaternion2.Y && quaternion1.Z == quaternion2.Z)
                return quaternion1.W != quaternion2.W;
            return true;
        }


        public static Quaternion operator *(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            var x = quaternion1.X;
            var y = quaternion1.Y;
            var z = quaternion1.Z;
            var w = quaternion1.W;
            var num4 = quaternion2.X;
            var num3 = quaternion2.Y;
            var num2 = quaternion2.Z;
            var num = quaternion2.W;
            var num12 = y * num2 - z * num3;
            var num11 = z * num4 - x * num2;
            var num10 = x * num3 - y * num4;
            var num9 = x * num4 + y * num3 + z * num2;
            quaternion.X = x * num + num4 * w + num12;
            quaternion.Y = y * num + num3 * w + num11;
            quaternion.Z = z * num + num2 * w + num10;
            quaternion.W = w * num - num9;
            return quaternion;
        }


        public static Quaternion operator *(Quaternion quaternion1, float scaleFactor)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X * scaleFactor;
            quaternion.Y = quaternion1.Y * scaleFactor;
            quaternion.Z = quaternion1.Z * scaleFactor;
            quaternion.W = quaternion1.W * scaleFactor;
            return quaternion;
        }


        public static Quaternion operator -(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion quaternion;
            quaternion.X = quaternion1.X - quaternion2.X;
            quaternion.Y = quaternion1.Y - quaternion2.Y;
            quaternion.Z = quaternion1.Z - quaternion2.Z;
            quaternion.W = quaternion1.W - quaternion2.W;
            return quaternion;
        }


        public static Quaternion operator -(Quaternion quaternion)
        {
            Quaternion quaternion2;
            quaternion2.X = -quaternion.X;
            quaternion2.Y = -quaternion.Y;
            quaternion2.Z = -quaternion.Z;
            quaternion2.W = -quaternion.W;
            return quaternion2;
        }


        public override string ToString()
        {
            var sb = new StringBuilder(32);
            sb.Append("{X:");
            sb.Append(X);
            sb.Append(" Y:");
            sb.Append(Y);
            sb.Append(" Z:");
            sb.Append(Z);
            sb.Append(" W:");
            sb.Append(W);
            sb.Append("}");
            return sb.ToString();
        }

        internal Matrix ToMatrix()
        {
            var matrix = Matrix.Identity;
            ToMatrix(out matrix);
            return matrix;
        }

        internal void ToMatrix(out Matrix matrix)
        {
            ToMatrix(this, out matrix);
        }

        internal static void ToMatrix(Quaternion quaternion, out Matrix matrix)
        {
            // source -> http://content.gpwiki.org/index.php/OpenGL:Tutorials:Using_Quaternions_to_represent_rotation#Quaternion_to_Matrix
            var x2 = quaternion.X * quaternion.X;
            var y2 = quaternion.Y * quaternion.Y;
            var z2 = quaternion.Z * quaternion.Z;
            var xy = quaternion.X * quaternion.Y;
            var xz = quaternion.X * quaternion.Z;
            var yz = quaternion.Y * quaternion.Z;
            var wx = quaternion.W * quaternion.X;
            var wy = quaternion.W * quaternion.Y;
            var wz = quaternion.W * quaternion.Z;

            // This calculation would be a lot more complicated for non-unit length quaternions
            // Note: The constructor of Matrix expects the Matrix in column-major format like expected by
            //   OpenGL
            matrix.M11 = 1.0f - 2.0f * (y2 + z2);
            matrix.M12 = 2.0f * (xy - wz);
            matrix.M13 = 2.0f * (xz + wy);
            matrix.M14 = 0.0f;

            matrix.M21 = 2.0f * (xy + wz);
            matrix.M22 = 1.0f - 2.0f * (x2 + z2);
            matrix.M23 = 2.0f * (yz - wx);
            matrix.M24 = 0.0f;

            matrix.M31 = 2.0f * (xz - wy);
            matrix.M32 = 2.0f * (yz + wx);
            matrix.M33 = 1.0f - 2.0f * (x2 + y2);
            matrix.M34 = 0.0f;

            matrix.M41 = 2.0f * (xz - wy);
            matrix.M42 = 2.0f * (yz + wx);
            matrix.M43 = 1.0f - 2.0f * (x2 + y2);
            matrix.M44 = 0.0f;

            //return Matrix( 1.0f - 2.0f * (y2 + z2), 2.0f * (xy - wz), 2.0f * (xz + wy), 0.0f,
            //		2.0f * (xy + wz), 1.0f - 2.0f * (x2 + z2), 2.0f * (yz - wx), 0.0f,
            //		2.0f * (xz - wy), 2.0f * (yz + wx), 1.0f - 2.0f * (x2 + y2), 0.0f,
            //		0.0f, 0.0f, 0.0f, 1.0f)
            //	}
        }

        public Vector3 Xyz
        {
            get => new Vector3(X, Y, Z);

            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>
        ///     Converts a Reactor Quaternion to a System Numerics Quaternion (supposedly they are fast).
        ///     This way you can use either Reactor or System.Numerics to do maths.
        /// </summary>
        /// <param name="value">Reactor Quaternion value</param>
        /// <returns>System Numerics Quaternion value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator System.Numerics.Quaternion(Quaternion value)
        {
            return new System.Numerics.Quaternion(value.X, value.Y, value.Z, value.W);
        }

        /// <summary>
        ///     Converts a System Numerics Quaternion to a Reactor Quaternion (supposedly they are just as fast).
        ///     This way you can use either Reactor or System.Numerics to do maths.
        /// </summary>
        /// <param name="value">Reactor Quaternion value</param>
        /// <returns>System Numerics Quaternion value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Quaternion(System.Numerics.Quaternion value)
        {
            return new Quaternion(value.X, value.Y, value.Z, value.W);
        }
    }
}