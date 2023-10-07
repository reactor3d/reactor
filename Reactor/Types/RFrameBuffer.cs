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
using Reactor.Platform.OpenGL;
using Reactor.Utilities;

namespace Reactor.Types
{
    public class RFrameBuffer : IDisposable
    {
        public RFrameBuffer(int width, int height, bool mipMap, RSurfaceFormat preferredFormat,
            RDepthFormat preferredDepthFormat, int preferredMultiSampleCount, bool shared)
        {
            Width = width;
            Height = height;
            DepthStencilFormat = preferredDepthFormat;
            SurfaceFormat = preferredFormat;
            MultiSampleCount = preferredMultiSampleCount;
            GL.GenFramebuffers(1, Allocator.UInt32_1);
            REngine.CheckGLError();
            Id = Allocator.UInt32_1[0];
            Bind();
            Build(width, height, mipMap, preferredFormat, preferredDepthFormat, preferredMultiSampleCount, shared);
            Unbind();
        }

        public RFrameBuffer(int width, int height, bool mipMap, RSurfaceFormat preferredFormat,
            RDepthFormat preferredDepthFormat, int preferredMultiSampleCount)
            : this(width, height, mipMap, preferredFormat, preferredDepthFormat, preferredMultiSampleCount, false)
        {
        }

        public RFrameBuffer(int width, int height, bool mipMap, RSurfaceFormat preferredFormat,
            RDepthFormat preferredDepthFormat)
            : this(width, height, mipMap, preferredFormat, preferredDepthFormat, 0)
        {
        }

        public RFrameBuffer(int width, int height)
            : this(width, height, false, RSurfaceFormat.Color, RDepthFormat.None)
        {
        }

        public uint Id { get; set; }
        public RDepthFormat DepthStencilFormat { get; private set; }
        public RSurfaceFormat SurfaceFormat { get; private set; }
        public int MultiSampleCount { get; private set; }

        public List<RTexture2D> ColorBuffers { get; } = new List<RTexture2D>();
        public RTexture2D DepthBuffer { get; private set; }
        public RTexture2D StencilBuffer { get; }
        public int Width { get; }
        public int Height { get; }


        public void Dispose()
        {
            uint[] v = { Id };
            GL.DeleteFramebuffers(1, v);
            DepthBuffer.Dispose();
            foreach (var b in ColorBuffers) b.Dispose();
            REngine.CheckGLError();
        }

        public void SetCubeAttachment(RTextureCube texture)
        {
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0,
                texture.textureTarget, texture.Id, 0);
            ColorBuffers.Add((RTexture2D)texture.PositiveX);
            ColorBuffers.Add((RTexture2D)texture.NegativeX);
            ColorBuffers.Add((RTexture2D)texture.PositiveY);
            ColorBuffers.Add((RTexture2D)texture.NegativeY);
            ColorBuffers.Add((RTexture2D)texture.PositiveZ);
            ColorBuffers.Add((RTexture2D)texture.NegativeZ);
        }

        public void SetColorAttachment(RTexture2D texture, int slot)
        {
            ColorBuffers[slot] = texture;
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0 + slot,
                texture.textureTarget, texture.Id, 0);
        }

        public void SetDepthAttachment(RTexture2D texture)
        {
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment,
                texture.textureTarget, texture.Id, 0);
            REngine.CheckGLError();
            DepthBuffer = texture;
        }

        public void SetStencilAttachment(RTexture2D texture)
        {
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.StencilAttachment,
                texture.textureTarget, texture.Id, 0);
            //Do you want to read from stencil outside of render pass? If so, email me and I'll add a property.
        }

        public void SetDepthAndStencilAttachment(RTexture2D texture)
        {
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment,
                texture.textureTarget, texture.Id, 0);
            REngine.CheckGLError();
            DepthBuffer = texture;
        }

        /// <summary>
        ///     Checks the RFrameBuffer for completeness. Completeness is the ability to perform draw calls on this buffer.
        ///     If this check fails, it's due to either not having <see cref="ColorBuffers" /> set using
        ///     <see cref="SetColorAttachment" />
        ///     or not having <see cref="DepthBuffer" /> set. You can have a "none" depth buffer and no stencil, but you can't
        ///     have empty color buffers. Add at least one <see cref="RTexture2D" /> as a color texture to draw to.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            var code = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            return code == FramebufferErrorCode.FramebufferComplete;
        }

        private void Build(int width, int height, bool mipMap,
            RSurfaceFormat preferredFormat, RDepthFormat preferredDepthFormat, int preferredMultiSampleCount,
            bool shared)
        {
            var depth = 0;
            var stencil = 0;


            var BackBuffer = new RTexture2D();
            BackBuffer.Create(width, height, RPixelFormat.Rgba, preferredFormat);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0,
                TextureTarget.Texture2D, BackBuffer.Id, 0);


            if (preferredDepthFormat != RDepthFormat.None)
            {
                DepthBuffer = new RTexture2D();

                if (preferredDepthFormat == RDepthFormat.Depth24Stencil8 ||
                    preferredDepthFormat == RDepthFormat.Depth32Stencil8)
                {
                    DepthBuffer.CreateDepth(width, height, RPixelFormat.DepthStencil, preferredDepthFormat);
                    GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment,
                        TextureTarget.Texture2D, DepthBuffer.Id, 0);
                }
                else
                {
                    DepthBuffer.CreateDepth(width, height, RPixelFormat.DepthComponent, preferredDepthFormat);
                    GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment,
                        TextureTarget.Texture2D, DepthBuffer.Id, 0);
                }

                REngine.CheckGLError();
            }

            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("Error creating frame buffer: framebuffer is not complete");


            ColorBuffers.Add(BackBuffer);
        }


        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Id);
            GL.Viewport(0, 0, Width, Height);
            REngine.CheckGLError();
        }

        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            REngine.CheckGLError();
        }

        private static RenderbufferStorage GetStorageForFormat(RSurfaceFormat surfaceFormat)
        {
            switch (surfaceFormat)
            {
                case RSurfaceFormat.Alpha8:
                    return RenderbufferStorage.R8;
                case RSurfaceFormat.Color:
                    return RenderbufferStorage.Rgba8;
                case RSurfaceFormat.HalfVector4:
                case RSurfaceFormat.HdrBlendable:
                    return RenderbufferStorage.Rgba16f;
                case RSurfaceFormat.HdrFull:
                    return RenderbufferStorage.Rgba32f;
                default:
                    return RenderbufferStorage.Rgba8;
            }
        }
    }
}