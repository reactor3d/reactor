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
using System.Collections;
using System.Collections.Generic;

namespace Reactor.Math
{
    public class CurveKeyCollection : ICollection<CurveKey>, IEnumerable<CurveKey>, IEnumerable
    {
        #region Constructors

        public CurveKeyCollection()
        {
            innerlist = new List<CurveKey>();
        }

        #endregion Constructors

        #region Private Fields

        private readonly List<CurveKey> innerlist;

        #endregion Private Fields


        #region Properties

        public int Count => innerlist.Count;

        public bool IsReadOnly { get; } = false;

        public CurveKey this[int index]
        {
            get => innerlist[index];
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                if (index >= innerlist.Count)
                    throw new IndexOutOfRangeException();

                if (innerlist[index].Position == value.Position)
                {
                    innerlist[index] = value;
                }
                else
                {
                    innerlist.RemoveAt(index);
                    innerlist.Add(value);
                }
            }
        }

        #endregion Properties


        #region Public Methods

        public void Add(CurveKey item)
        {
            if (item == null)
                throw new ArgumentNullException();

            if (innerlist.Count == 0)
            {
                innerlist.Add(item);
                return;
            }

            for (var i = 0; i < innerlist.Count; i++)
                if (item.Position < innerlist[i].Position)
                {
                    innerlist.Insert(i, item);
                    return;
                }

            innerlist.Add(item);
        }

        public void Clear()
        {
            innerlist.Clear();
        }

        public CurveKeyCollection Clone()
        {
            var ckc = new CurveKeyCollection();
            foreach (var key in innerlist)
                ckc.Add(key);
            return ckc;
        }

        public bool Contains(CurveKey item)
        {
            return innerlist.Contains(item);
        }

        public void CopyTo(CurveKey[] array, int arrayIndex)
        {
            innerlist.CopyTo(array, arrayIndex);
        }

        public IEnumerator<CurveKey> GetEnumerator()
        {
            return innerlist.GetEnumerator();
        }

        public int IndexOf(CurveKey item)
        {
            return innerlist.IndexOf(item);
        }

        public bool Remove(CurveKey item)
        {
            return innerlist.Remove(item);
        }

        public void RemoveAt(int index)
        {
            innerlist.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerlist.GetEnumerator();
        }

        #endregion Public Methods
    }
}