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

namespace Reactor.Math
{
    public class CurveKey : IEquatable<CurveKey>, IComparable<CurveKey>
    {
        #region Private Fields

        #endregion Private Fields


        #region Properties

        public CurveContinuity Continuity { get; set; }


        public float Position { get; }


        public float TangentIn { get; set; }


        public float TangentOut { get; set; }


        public float Value { get; set; }

        #endregion


        #region Constructors

        public CurveKey(float position, float value)
            : this(position, value, 0, 0, CurveContinuity.Smooth)
        {
        }

        public CurveKey(float position, float value, float tangentIn, float tangentOut)
            : this(position, value, tangentIn, tangentOut, CurveContinuity.Smooth)
        {
        }

        public CurveKey(float position, float value, float tangentIn, float tangentOut, CurveContinuity continuity)
        {
            Position = position;
            Value = value;
            TangentIn = tangentIn;
            TangentOut = tangentOut;
            Continuity = continuity;
        }

        #endregion Constructors


        #region Public Methods

        public static bool operator !=(CurveKey a, CurveKey b)
        {
            return !(a == b);
        }

        public static bool operator ==(CurveKey a, CurveKey b)
        {
            if (Equals(a, null))
                return Equals(b, null);

            if (Equals(b, null))
                return Equals(a, null);

            return a.Position == b.Position
                   && a.Value == b.Value
                   && a.TangentIn == b.TangentIn
                   && a.TangentOut == b.TangentOut
                   && a.Continuity == b.Continuity;
        }

        public CurveKey Clone()
        {
            return new CurveKey(Position, Value, TangentIn, TangentOut, Continuity);
        }

        public int CompareTo(CurveKey other)
        {
            return Position.CompareTo(other.Position);
        }

        public bool Equals(CurveKey other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return obj is CurveKey ? (CurveKey)obj == this : false;
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode() ^ Value.GetHashCode() ^ TangentIn.GetHashCode() ^
                   TangentOut.GetHashCode() ^ Continuity.GetHashCode();
        }

        #endregion
    }
}