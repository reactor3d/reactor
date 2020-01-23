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
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Types
{
    public class RCameraNode : RUpdateNode
    {

        public Matrix Projection { get; set; }

        public Matrix View { get { return Matrix; } set { Matrix = value; } }

        public virtual Vector3 Unproject(RViewport viewport, int x, int y, float depth)
        {
            Vector4 screen = new Vector4((x - viewport.X) / viewport.Width, (y - viewport.Y) / viewport.Height, depth, 1.0f);
            screen.X = screen.X * 2.0f - 1.0f;
            screen.Y = screen.Y * 2.0f - 1.0f;
            screen.Z = screen.Z * 2.0f - 1.0f;

            var inverseViewProjection = Matrix.Invert(Projection * Matrix);
            screen = inverseViewProjection * screen;

            if(screen.W != 0.0f)
            {
                screen.X /= screen.W;
                screen.Y /= screen.W;
                screen.Z /= screen.W;
            }
            return new Vector3(screen.X, screen.Y, screen.Z);
        }

        public virtual Ray MousePick(RViewport viewport, int x, int y)
        {
            Vector3 nearPoint = Unproject(viewport, x, y, 0.0f);

            Vector3 farPoint = Unproject(viewport, x, y, 1.0f);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            return new Ray(nearPoint, direction);
        }
    }
}
