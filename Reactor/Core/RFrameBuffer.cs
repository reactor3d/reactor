using OpenTK.Graphics.OpenGL;
using Reactor.Platform;
using Reactor.Types;
using Reactor.Types.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor
{
    public class RFrameBuffer : RTexture2D
    {
        public RDepthFormat DepthStencilFormat { get; private set; }

        public int MultiSampleCount { get; private set; }

        public RRenderTargetUsage RenderTargetUsage { get; private set; }

        public bool IsContentLost { get { return false; } }

        public event EventHandler<EventArgs> ContentLost;

        private bool SuppressEventHandlerWarningsUntilEventsAreProperlyImplemented()
        {
            return ContentLost != null;
        }

        public RFrameBuffer(int width, int height, bool mipMap, RSurfaceFormat preferredFormat, RDepthFormat preferredDepthFormat, int preferredMultiSampleCount, RRenderTargetUsage usage, bool shared)
        {
            DepthStencilFormat = preferredDepthFormat;
            MultiSampleCount = preferredMultiSampleCount;
            RenderTargetUsage = usage;

            PlatformConstruct(width, height, mipMap, preferredFormat, preferredDepthFormat, preferredMultiSampleCount, usage, shared);
        }

        public RFrameBuffer(int width, int height, bool mipMap, RSurfaceFormat preferredFormat, RDepthFormat preferredDepthFormat, int preferredMultiSampleCount, RRenderTargetUsage usage)
            : this(width, height, mipMap, preferredFormat, preferredDepthFormat, preferredMultiSampleCount, usage, false)
        { }

        public RFrameBuffer(int width, int height, bool mipMap, RSurfaceFormat preferredFormat, RDepthFormat preferredDepthFormat)
            : this( width, height, mipMap, preferredFormat, preferredDepthFormat, 0, RRenderTargetUsage.DiscardContents)
        { }

        public RFrameBuffer(int width, int height)
            : this( width, height, false, RSurfaceFormat.Color, RDepthFormat.None)
        { }

        
        #if GLES
        const RenderbufferTarget GLRenderbuffer = RenderbufferTarget.Renderbuffer;
        const RenderbufferStorage GLDepthComponent16 = RenderbufferStorage.DepthComponent16;
        const RenderbufferStorage GLDepthComponent16NonLinear = (RenderbufferStorage)0x8E2C;
        const RenderbufferStorage GLDepthComponent24 = RenderbufferStorage.DepthComponent24Oes;
        const RenderbufferStorage GLDepth24Stencil8 = RenderbufferStorage.Depth24Stencil8Oes;
        const RenderbufferStorage GLStencilIndex8 = RenderbufferStorage.StencilIndex8;
        #else
        const RenderbufferTarget GLRenderbuffer = RenderbufferTarget.RenderbufferExt;
        const RenderbufferStorage GLDepthComponent16 = RenderbufferStorage.DepthComponent16;
        const RenderbufferStorage GLDepthComponent24 = RenderbufferStorage.DepthComponent24;
        const RenderbufferStorage GLDepth24Stencil8 = RenderbufferStorage.Depth24Stencil8;
        const RenderbufferStorage GLStencilIndex8 = RenderbufferStorage.StencilIndex8;
        #endif

        internal int glColorBuffer;
        internal int glDepthBuffer;
        internal int glStencilBuffer;

        private void PlatformConstruct(int width, int height, bool mipMap,
            RSurfaceFormat preferredFormat, RDepthFormat preferredDepthFormat, int preferredMultiSampleCount, RRenderTargetUsage usage, bool shared)
        {
            Threading.BlockOnUIThread(() =>
            {
                //graphicsDevice.PlatformCreateRenderTarget(this, width, height, mipMap, preferredFormat, preferredDepthFormat, preferredMultiSampleCount, usage);
            });
            
            
        }


        public void Bind()
        {
            GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, Id);
            REngine.CheckGLError();
        }

        public void Unbind()
        {
            GL.Ext.BindFramebuffer(FramebufferTarget.FramebufferExt, 0);
            REngine.CheckGLError();
        }

        public RTexture GetDepthTexture()
        {
            RTexture depth = new RTexture();
            depth.Id = (uint)glDepthBuffer;
            return depth;
        }

        public RTexture GetStencilTexture()
        {
            RTexture stencil = new RTexture();
            stencil.Id = (uint)glStencilBuffer;
            return stencil;
        }

        public RTexture GetTexture()
        {
            RTexture color = new RTexture();
            color.Id = (uint)glColorBuffer;
            return color;
        }

    }
    
}
