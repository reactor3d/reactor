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
using System.Collections.Generic;
using Reactor.Loaders;
using Reactor.Math;
using Reactor.Platform.OpenGL;
using Reactor.Types.States;

namespace Reactor.Types
{
    public class RMesh : RRenderNode, IDisposable
    {

        internal List<RMeshPart> Parts { get; set; }

        RMesh()
        {
            this.Matrix = Matrix.Identity;
            this.Position = Vector3.Zero;
            this.Rotation = Quaternion.Identity;
            Parts = new List<RMeshPart>();
            this.CullEnable = true;
            this.CullMode = RCullMode.CullClockwiseFace;
            this.BlendEnable = true;
            this.DepthWrite= true;
            this.IsDrawable = true;

        }

        #region IDisposable implementation
        public void Dispose()
        {
            Parts.Clear();
            Parts = null;
        }
        #endregion


        public void LoadSourceModel(string filename)
        {
            this.LoadSource(RFileSystem.Instance.GetFilePath(filename));
        }

        public override void Render()
        {
            if (IsDrawable)
            {
                ApplyState();
                base.Render();
                foreach (RMeshPart part in Parts)
                {
                    part.Draw(BeginMode.Triangles, this.Matrix);
                }
            }
        }
    }
}