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
using System.Runtime.CompilerServices;
using Reactor.Geometry;
using Reactor.Types.States;
using Reactor.Graphics.OpenGL;

namespace Reactor.Types
{
    public class RRenderNode : RUpdateNode
    {
        public RVertexBuffer VertexBuffer { get; set; }
        private RBlendState blendState = RBlendState.Opaque;
        public RBlendState BlendState { get { return blendState; } set { blendState = value; } }
        public virtual void Render()
        {
            if (IsDrawable)
            {
                if (OnRender != null)
                    OnRender(this, null);
            }
            
        }

        protected void ApplyState()
        {
            if(DepthWrite)
            {
                GL.Enable(EnableCap.DepthTest);
                GL.DepthMask(true);
                REngine.CheckGLError();
                GL.DepthFunc(DepthFunction.Less);
            }
            else
            {
                GL.Disable(EnableCap.DepthTest);
                GL.DepthMask(false);
                REngine.CheckGLError();
                GL.DepthFunc(DepthFunction.Less);
            }

            REngine.CheckGLError();

            if(CullEnable)
                GL.Enable(EnableCap.CullFace);
            else
                GL.Disable(EnableCap.CullFace);

            switch(CullMode)
            {
                case RCullMode.None:
                    GL.Disable(EnableCap.CullFace);
                    REngine.CheckGLError();
                    break;
                case RCullMode.CullClockwiseFace:
                    GL.FrontFace(FrontFaceDirection.Ccw);
                    GL.CullFace(CullFaceMode.Back);
                    REngine.CheckGLError();
                    break;
                case RCullMode.CullCounterClockwiseFace:
                    GL.FrontFace(FrontFaceDirection.Cw);
                    GL.CullFace(CullFaceMode.Back);
                    REngine.CheckGLError();
                    break;
                default:
                    break;
            }
            if(BlendEnable)
            {
                GL.Enable(EnableCap.Blend);
                blendState.PlatformApplyState();
            }
            else
            {
                GL.Disable(EnableCap.Blend);
            }
            REngine.CheckGLError();
        }
        public event EventHandler OnRender;

        public bool IsDrawable { get; set; }

        public RBlendFunc AlphaBlendMode
        {
            get { return blendState.AlphaBlendFunction; }
            set { blendState.AlphaBlendFunction = value; }
        }

        public RBlend AlphaDestinationBlend
        {
            get { return blendState.AlphaDestinationBlend;}
            set { blendState.AlphaDestinationBlend = value; }
        }
        public RBlend AlphaSourceBlend
        {
            get { return blendState.AlphaSourceBlend;}
            set { blendState.AlphaSourceBlend = value; }
        }
        public RColor BlendFactor
        {
            get { return blendState.BlendFactor; }
            set { blendState.BlendFactor=value; }
        }
        public RBlendFunc ColorBlendMode
        {
            get { return blendState.ColorBlendFunction; }
            set { blendState.ColorBlendFunction = value; }
        }
        public RBlend ColorDestinationBlend
        {
            get { return blendState.ColorDestinationBlend;}
            set { blendState.ColorDestinationBlend = value; }
        }
        public RBlend ColorSourceBlend
        {
            get { return blendState.ColorSourceBlend;}
            set { blendState.ColorSourceBlend = value; }
        }
        public int MultiSampleMask
        {
            get { return blendState.MultiSampleMask; }
            set { blendState.MultiSampleMask = value; }
        }

        public bool DepthWrite
        {
            get; set;
        }
        public bool CullEnable
        {
            get; set;
        }
        public bool BlendEnable
        {
            get; set;
        }
        public RCullMode CullMode
        {
            get; set;
        }
    }
}