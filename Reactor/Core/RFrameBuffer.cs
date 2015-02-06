using OpenTK.Graphics.OpenGL;
using Reactor.Platform;
using Reactor.Types;
using Reactor.Types.States;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Reactor
{
    public class RFrameBuffer
    {
        public int Id { get; set; }
        internal static FramebufferHelperEXT frameHelper = new FramebufferHelperEXT();
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
            : this(width, height, mipMap, preferredFormat, preferredDepthFormat, 0, RRenderTargetUsage.DiscardContents)
        { }

        public RFrameBuffer(int width, int height)
            : this(width, height, false, RSurfaceFormat.Color, RDepthFormat.None)
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
                var color = 0;
                var depth = 0;
                var stencil = 0;

                if (preferredMultiSampleCount > 0 && frameHelper.SupportsBlitFramebuffer)
                {
                    frameHelper.GenRenderbuffer(out color);
                    frameHelper.BindRenderbuffer(color);
#if GLES
                this.framebufferHelper.RenderbufferStorageMultisample(preferredMultiSampleCount, (int)RenderbufferStorage.Rgba8Oes, width, height);
#else
                    frameHelper.RenderbufferStorageMultisample(preferredMultiSampleCount, (int)RenderbufferStorage.Rgba8, width, height);
#endif
                }

                if (preferredDepthFormat != RDepthFormat.None)
                {
                    var depthInternalFormat = RenderbufferStorage.DepthComponent16;
                    var stencilInternalFormat = (RenderbufferStorage)0;
                    switch (preferredDepthFormat)
                    {
                        case RDepthFormat.Depth16:
                            depthInternalFormat = RenderbufferStorage.DepthComponent16; break;
#if GLES
                    case RDepthFormat.Depth24:
                        if (GraphicsCapabilities.SupportsDepth24)
                            depthInternalFormat = RenderbufferStorage.DepthComponent24Oes;
                        else if (GraphicsCapabilities.SupportsDepthNonLinear)
                            depthInternalFormat = (RenderbufferStorage)0x8E2C;
                        else
                            depthInternalFormat = RenderbufferStorage.DepthComponent16;
                        break;
                    case RDepthFormat.Depth24Stencil8:
                        if (GraphicsCapabilities.SupportsPackedDepthStencil)
                            depthInternalFormat = RenderbufferStorage.Depth24Stencil8Oes;
                        else
                        {
                            if (GraphicsCapabilities.SupportsDepth24)
                                depthInternalFormat = RenderbufferStorage.DepthComponent24Oes;
                            else if (GraphicsCapabilities.SupportsDepthNonLinear)
                                depthInternalFormat = (RenderbufferStorage)0x8E2C;
                            else
                                depthInternalFormat = RenderbufferStorage.DepthComponent16;
                            stencilInternalFormat = RenderbufferStorage.StencilIndex8;
                            break;
                        }
                        break;
#else
                        case RDepthFormat.Depth24: depthInternalFormat = RenderbufferStorage.DepthComponent24; break;
                        case RDepthFormat.Depth24Stencil8: depthInternalFormat = RenderbufferStorage.Depth24Stencil8; break;
#endif
                    }

                    if (depthInternalFormat != 0)
                    {
                        frameHelper.GenRenderbuffer(out depth);
                        frameHelper.BindRenderbuffer(depth);
                        frameHelper.RenderbufferStorageMultisample(preferredMultiSampleCount, (int)depthInternalFormat, width, height);
                        if (preferredDepthFormat == RDepthFormat.Depth24Stencil8)
                        {
                            stencil = depth;
                            if (stencilInternalFormat != 0)
                            {
                                frameHelper.GenRenderbuffer(out stencil);
                                frameHelper.BindRenderbuffer(stencil);
                                frameHelper.RenderbufferStorageMultisample(preferredMultiSampleCount, (int)stencilInternalFormat, width, height);
                            }
                        }
                    }
                }

                
                    if (color != 0)
                        glColorBuffer = color;
                    else
                        glColorBuffer = 0;
                    glDepthBuffer = depth;
                    glStencilBuffer = stencil;
                
                
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
#if GLES
    internal class FramebufferHelper
        {
            public bool SupportsInvalidateFramebuffer { get; private set; }

            public bool SupportsBlitFramebuffer { get; private set; }
#if IOS
            internal const string OpenGLLibrary = MonoTouch.Constants.OpenGLESLibrary;
#elif ANDROID
            internal const string OpenGLLibrary = "libGLESv2.dll";
            [DllImport("libEGL.dll", EntryPoint = "eglGetProcAddress")]
            public static extern IntPtr EGLGetProcAddress(string funcname);
#endif
    #region GL_EXT_discard_framebuffer

            internal const All AllColorExt = (All)0x1800;
            internal const All AllDepthExt = (All)0x1801;
            internal const All AllStencilExt = (All)0x1802;

            [SuppressUnmanagedCodeSecurity]
            [DllImport(OpenGLLibrary, EntryPoint = "glDiscardFramebufferEXT", ExactSpelling = true)]
            internal extern static void GLDiscardFramebufferExt(All target, int numAttachments, [MarshalAs(UnmanagedType.LPArray)] All[] attachments);

    #endregion

    #region GL_APPLE_framebuffer_multisample

            internal const All AllFramebufferIncompleteMultisampleApple = (All)0x8D56;
            internal const All AllMaxSamplesApple = (All)0x8D57;
            internal const All AllReadFramebufferApple = (All)0x8CA8;
            internal const All AllDrawFramebufferApple = (All)0x8CA9;
            internal const All AllRenderBufferSamplesApple = (All)0x8CAB;

            [SuppressUnmanagedCodeSecurity]
            [DllImport(OpenGLLibrary, EntryPoint = "glRenderbufferStorageMultisampleAPPLE", ExactSpelling = true)]
            internal extern static void GLRenderbufferStorageMultisampleApple(All target, int samples, All internalformat, int width, int height);

            [SuppressUnmanagedCodeSecurity]
            [DllImport(OpenGLLibrary, EntryPoint = "glResolveMultisampleFramebufferAPPLE", ExactSpelling = true)]
            internal extern static void GLResolveMultisampleFramebufferApple();

            internal void GLBlitFramebufferApple(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, ClearBufferMask mask, TextureMagFilter filter)
            {
                GLResolveMultisampleFramebufferApple();
            }

            #endregion

    #region GL_NV_framebuffer_multisample

            internal const All AllFramebufferIncompleteMultisampleNV = (All)0x8D56;
            internal const All AllMaxSamplesNV = (All)0x8D57;
            internal const All AllReadFramebufferNV = (All)0x8CA8;
            internal const All AllDrawFramebufferNV = (All)0x8CA9;
            internal const All AllRenderBufferSamplesNV = (All)0x8CAB;

            #endregion

    #region GL_IMG_multisampled_render_to_texture

            internal const All AllFramebufferIncompleteMultisampleImg = (All)0x9134;
            internal const All AllMaxSamplesImg = (All)0x9135;

            #endregion

    #region GL_EXT_multisampled_render_to_texture

            internal const All AllFramebufferIncompleteMultisampleExt = (All)0x8D56;
            internal const All AllMaxSamplesExt = (All)0x8D57;

            #endregion

            internal delegate void GLInvalidateFramebufferDelegate(All target, int numAttachments, All[] attachments);
            internal delegate void GLRenderbufferStorageMultisampleDelegate(All target, int samples, All internalFormat, int width, int height);
            internal delegate void GLBlitFramebufferDelegate(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, ClearBufferMask mask, TextureMagFilter filter);
            internal delegate void GLFramebufferTexture2DMultisampleDelegate(All target, All attachment, All textarget, int texture, int level, int samples);

            internal GLInvalidateFramebufferDelegate GLInvalidateFramebuffer;
            internal GLRenderbufferStorageMultisampleDelegate GLRenderbufferStorageMultisample;
            internal GLFramebufferTexture2DMultisampleDelegate GLFramebufferTexture2DMultisample;
            internal GLBlitFramebufferDelegate GLBlitFramebuffer;

            internal All AllReadFramebuffer = All.Framebuffer;
            internal All AllDrawFramebuffer = All.Framebuffer;

            internal FramebufferHelper(GraphicsDevice graphicsDevice)
            {
#if IOS
                if (graphicsDevice._extensions.Contains("GL_EXT_discard_framebuffer"))
                {
                    this.GLInvalidateFramebuffer = new GLInvalidateFramebufferDelegate(GLDiscardFramebufferExt);
                    this.SupportsInvalidateFramebuffer = true;
                }

                if (graphicsDevice._extensions.Contains("GL_APPLE_framebuffer_multisample"))
                {
                    this.GLRenderbufferStorageMultisample = new GLRenderbufferStorageMultisampleDelegate(GLRenderbufferStorageMultisampleApple);
                    this.GLBlitFramebuffer = new GLBlitFramebufferDelegate(GLBlitFramebufferApple);
                    this.AllReadFramebuffer = AllReadFramebufferApple;
                    this.AllDrawFramebuffer = AllDrawFramebufferApple;
                }
#elif ANDROID
                // eglGetProcAddress doesn't guarantied returning NULL if the entry point doesn't exist. The returned address *should* be the same for all invalid entry point
                var invalidFuncPtr = EGLGetProcAddress("InvalidFunctionName");

                if (graphicsDevice._extensions.Contains("GL_EXT_discard_framebuffer"))
                {
                    var glDiscardFramebufferEXTPtr = EGLGetProcAddress("glDiscardFramebufferEXT");
                    if (glDiscardFramebufferEXTPtr != invalidFuncPtr)
                    {
                        this.GLInvalidateFramebuffer = Marshal.GetDelegateForFunctionPointer<GLInvalidateFramebufferDelegate>(glDiscardFramebufferEXTPtr);
                        this.SupportsInvalidateFramebuffer = true;
                    }
                }
                if (graphicsDevice._extensions.Contains("GL_EXT_multisampled_render_to_texture"))
                {
                    var glRenderbufferStorageMultisampleEXTPtr = EGLGetProcAddress("glRenderbufferStorageMultisampleEXT");
                    var glFramebufferTexture2DMultisampleEXTPtr = EGLGetProcAddress("glFramebufferTexture2DMultisampleEXT");
                    if (glRenderbufferStorageMultisampleEXTPtr != invalidFuncPtr && glFramebufferTexture2DMultisampleEXTPtr != invalidFuncPtr)
                    {
                        this.GLRenderbufferStorageMultisample = Marshal.GetDelegateForFunctionPointer<GLRenderbufferStorageMultisampleDelegate>(glRenderbufferStorageMultisampleEXTPtr);
                        this.GLFramebufferTexture2DMultisample = Marshal.GetDelegateForFunctionPointer<GLFramebufferTexture2DMultisampleDelegate>(glFramebufferTexture2DMultisampleEXTPtr);
                    }
                }
                else if (graphicsDevice._extensions.Contains("GL_IMG_multisampled_render_to_texture"))
                {
                    var glRenderbufferStorageMultisampleIMGPtr = EGLGetProcAddress("glRenderbufferStorageMultisampleIMG");
                    var glFramebufferTexture2DMultisampleIMGPtr = EGLGetProcAddress("glFramebufferTexture2DMultisampleIMG");
                    if (glRenderbufferStorageMultisampleIMGPtr != invalidFuncPtr && glFramebufferTexture2DMultisampleIMGPtr != invalidFuncPtr)
                    {
                        this.GLRenderbufferStorageMultisample = Marshal.GetDelegateForFunctionPointer<GLRenderbufferStorageMultisampleDelegate>(glRenderbufferStorageMultisampleIMGPtr);
                        this.GLFramebufferTexture2DMultisample = Marshal.GetDelegateForFunctionPointer<GLFramebufferTexture2DMultisampleDelegate>(glFramebufferTexture2DMultisampleIMGPtr);
                    }
                }
                else if (graphicsDevice._extensions.Contains("GL_NV_framebuffer_multisample"))
                {
                    var glRenderbufferStorageMultisampleNVPtr = EGLGetProcAddress("glRenderbufferStorageMultisampleNV");
                    var glBlitFramebufferNVPtr = EGLGetProcAddress("glBlitFramebufferNV");
                    if (glRenderbufferStorageMultisampleNVPtr != invalidFuncPtr && glBlitFramebufferNVPtr != invalidFuncPtr)
                    {
                        this.GLRenderbufferStorageMultisample = Marshal.GetDelegateForFunctionPointer<GLRenderbufferStorageMultisampleDelegate>(glRenderbufferStorageMultisampleNVPtr);
                        this.GLBlitFramebuffer = Marshal.GetDelegateForFunctionPointer<GLBlitFramebufferDelegate>(glBlitFramebufferNVPtr);
                        this.AllReadFramebuffer = AllReadFramebufferNV;
                        this.AllDrawFramebuffer = AllDrawFramebufferNV;
                    }
                }
#endif

                this.SupportsBlitFramebuffer = this.GLBlitFramebuffer != null;
            }

            internal virtual void GenRenderbuffer(out int renderbuffer)
            {
                renderbuffer = 0;
#if !ANDROID
                GL.GenRenderbuffers(1, ref renderbuffer);
#else
                GL.GenRenderbuffers(1, out renderbuffer);
#endif
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void BindRenderbuffer(int renderbuffer)
            {
                GL.BindRenderbuffer(All.Renderbuffer, renderbuffer);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void DeleteRenderbuffer(int renderbuffer)
            {
                GL.DeleteRenderbuffers(1, ref renderbuffer);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void RenderbufferStorageMultisample(int samples, int internalFormat, int width, int height)
            {
                if (samples > 0 && this.GLRenderbufferStorageMultisample != null)
                    GLRenderbufferStorageMultisample(All.Renderbuffer, samples, (All)internalFormat, width, height);
                else
                    GL.RenderbufferStorage(All.Renderbuffer, (All)internalFormat, width, height);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void GenFramebuffer(out int framebuffer)
            {
                framebuffer = 0;
#if !ANDROID
                GL.GenFramebuffers(1, ref framebuffer);
#else
                GL.GenFramebuffers(1, out framebuffer);
#endif
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void BindFramebuffer(int framebuffer)
            {
                GL.BindFramebuffer(All.Framebuffer, framebuffer);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void BindReadFramebuffer(int readFramebuffer)
            {
                GL.BindFramebuffer(AllReadFramebuffer, readFramebuffer);
                GraphicsExtensions.CheckGLError();
            }

            internal readonly All[] GLDiscardAttachementsDefault = { AllColorExt, AllDepthExt, AllStencilExt, };
            internal readonly All[] GLDiscardAttachements = { All.ColorAttachment0, All.DepthAttachment, All.StencilAttachment, };

            internal virtual void InvalidateDrawFramebuffer()
            {
                Debug.Assert(this.SupportsInvalidateFramebuffer);
                this.GLInvalidateFramebuffer(AllDrawFramebuffer, 3, GLDiscardAttachements);
            }

            internal virtual void InvalidateReadFramebuffer()
            {
                Debug.Assert(this.SupportsInvalidateFramebuffer);
                this.GLInvalidateFramebuffer(AllReadFramebuffer, 3, GLDiscardAttachements);
            }

            internal virtual void DeleteFramebuffer(int framebuffer)
            {
                GL.DeleteFramebuffers(1, ref framebuffer);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void FramebufferTexture2D(int attachement, int target, int texture, int level = 0, int samples = 0)
            {
                if (samples > 0 && this.GLFramebufferTexture2DMultisample != null)
                    this.GLFramebufferTexture2DMultisample(All.Framebuffer, (All)attachement, (All)target, texture, level, samples);
                else
                    GL.FramebufferTexture2D(All.Framebuffer, (All)attachement, (All)target, texture, level);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void FramebufferRenderbuffer(int attachement, int renderbuffer, int level = 0)
            {
                GL.FramebufferRenderbuffer(All.Framebuffer, (All)attachement, All.Renderbuffer, renderbuffer);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void GenerateMipmap(int target)
            {
                GL.GenerateMipmap((All)target);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void BlitFramebuffer(int iColorAttachment, int width, int height)
            {
                this.GLBlitFramebuffer(0, 0, width, height, 0, 0, width, height, ClearBufferMask.ColorBufferBit, TextureMagFilter.Nearest);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void CheckFramebufferStatus()
            {
                var status = GL.CheckFramebufferStatus(All.Framebuffer);
                if (status != All.FramebufferComplete)
                {
                    string message = "Framebuffer Incomplete.";
                    switch (status)
                    {
                        case All.FramebufferIncompleteAttachment: message = "Not all framebuffer attachment points are framebuffer attachment complete."; break;
                        case All.FramebufferIncompleteDimensions: message = "Not all attached images have the same width and height."; break;
                        case All.FramebufferIncompleteMissingAttachment: message = "No images are attached to the framebuffer."; break;
                        case All.FramebufferUnsupported: message = "The combination of internal formats of the attached images violates an implementation-dependent set of restrictions."; break; 
                    }
                    throw new InvalidOperationException(message);
                }
            }
        }

#else
    internal class FramebufferHelper
    {
        public bool SupportsInvalidateFramebuffer { get; private set; }

        public bool SupportsBlitFramebuffer { get; private set; }

#if MONOMAC
			[DllImport(Constants.OpenGLLibrary, EntryPoint = "glRenderbufferStorageMultisampleEXT")]
		    internal extern static void GLRenderbufferStorageMultisampleExt(All target, int samples, All internalformat, int width, int height);

			[DllImport(Constants.OpenGLLibrary, EntryPoint = "glBlitFramebufferEXT")]
			internal extern static void GLBlitFramebufferExt(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, ClearBufferMask mask, BlitFramebufferFilter filter);

			[DllImport(Constants.OpenGLLibrary, EntryPoint = "glGenerateMipmapEXT")]
			internal extern static void GLGenerateMipmapExt(GenerateMipmapTarget target);
#endif

        internal FramebufferHelper()
        {
            this.SupportsBlitFramebuffer = true;
            this.SupportsInvalidateFramebuffer = false;
        }

        internal virtual void GenRenderbuffer(out int renderbuffer)
        {
            GL.GenRenderbuffers(1, out renderbuffer);
            REngine.CheckGLError();
        }
        internal virtual void GenRenderbuffer(out uint renderbuffer)
        {
            GL.GenRenderbuffers(1, out renderbuffer);
            REngine.CheckGLError();
        }

        internal virtual void BindRenderbuffer(int renderbuffer)
        {
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderbuffer);
            REngine.CheckGLError();
        }
        internal virtual void BindRenderbuffer(uint renderbuffer)
        {
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderbuffer);
            REngine.CheckGLError();
        }

        internal virtual void DeleteRenderbuffer(int renderbuffer)
        {
            GL.DeleteRenderbuffers(1, ref renderbuffer);
            REngine.CheckGLError();
        }
        internal virtual void DeleteRenderbuffer(uint renderbuffer)
        {
            GL.DeleteRenderbuffers(1, ref renderbuffer);
            REngine.CheckGLError();
        }

        internal virtual void RenderbufferStorageMultisample(int samples, int internalFormat, int width, int height)
        {
#if !MONOMAC
            GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, samples, (RenderbufferStorage)internalFormat, width, height);
#else
				GLRenderbufferStorageMultisampleExt(All.Renderbuffer, samples, (All)internalFormat, width, height);
#endif
            REngine.CheckGLError();
        }

        internal virtual void GenFramebuffer(out int framebuffer)
        {
            GL.GenFramebuffers(1, out framebuffer);
            REngine.CheckGLError();
        }
        internal virtual void GenFramebuffer(out uint framebuffer)
        {
            GL.GenFramebuffers(1, out framebuffer);
            REngine.CheckGLError();
        }

        internal virtual void BindFramebuffer(int framebuffer)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebuffer);
            REngine.CheckGLError();
        }
        internal virtual void BindFramebuffer(uint framebuffer)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebuffer);
            REngine.CheckGLError();
        }

        internal virtual void BindReadFramebuffer(int readFramebuffer)
        {
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, readFramebuffer);
            REngine.CheckGLError();
        }
        internal virtual void BindReadFramebuffer(uint readFramebuffer)
        {
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, readFramebuffer);
            REngine.CheckGLError();
        }

        internal virtual void InvalidateDrawFramebuffer()
        {
            Debug.Assert(this.SupportsInvalidateFramebuffer);
        }

        internal virtual void InvalidateReadFramebuffer()
        {
            Debug.Assert(this.SupportsInvalidateFramebuffer);
        }

        internal virtual void DeleteFramebuffer(int framebuffer)
        {
            GL.DeleteFramebuffers(1, ref framebuffer);
            REngine.CheckGLError();
        }
        internal virtual void DeleteFramebuffer(uint framebuffer)
        {
            GL.DeleteFramebuffers(1, ref framebuffer);
            REngine.CheckGLError();
        }

        internal virtual void FramebufferTexture2D(int attachement, int target, int texture, int level = 0, int samples = 0)
        {
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, (FramebufferAttachment)attachement, (TextureTarget)target, texture, level);
            REngine.CheckGLError();
        }
        internal virtual void FramebufferTexture2D(int attachement, int target, uint texture, int level = 0, int samples = 0)
        {
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, (FramebufferAttachment)attachement, (TextureTarget)target, texture, level);
            REngine.CheckGLError();
        }

        internal virtual void FramebufferRenderbuffer(int attachement, int renderbuffer, int level = 0)
        {
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, (FramebufferAttachment)attachement, RenderbufferTarget.Renderbuffer, renderbuffer);
            REngine.CheckGLError();
        }
        internal virtual void FramebufferRenderbuffer(int attachement, uint renderbuffer, int level = 0)
        {
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, (FramebufferAttachment)attachement, RenderbufferTarget.Renderbuffer, renderbuffer);
            REngine.CheckGLError();
        }

        internal virtual void GenerateMipmap(int target)
        {
#if !MONOMAC
            GL.GenerateMipmap((GenerateMipmapTarget)target);
#else
				GLGenerateMipmapExt((GenerateMipmapTarget)target);
#endif
            REngine.CheckGLError();

        }

        internal virtual void BlitFramebuffer(int iColorAttachment, int width, int height)
        {

            GL.ReadBuffer(ReadBufferMode.ColorAttachment0 + iColorAttachment);
            REngine.CheckGLError();
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0 + iColorAttachment);
            REngine.CheckGLError();
#if !MONOMAC
            GL.BlitFramebuffer(0, 0, width, height, 0, 0, width, height, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest);
#else
				GLBlitFramebufferExt(0, 0, width, height, 0, 0, width, height, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest);
#endif
            REngine.CheckGLError();

        }

        internal virtual void CheckFramebufferStatus()
        {
            var status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (status != FramebufferErrorCode.FramebufferComplete)
            {
                string message = "Framebuffer Incomplete.";
                switch (status)
                {
                    case FramebufferErrorCode.FramebufferIncompleteAttachment: message = "Not all framebuffer attachment points are framebuffer attachment complete."; break;
                    case FramebufferErrorCode.FramebufferIncompleteMissingAttachment: message = "No images are attached to the framebuffer."; break;
                    case FramebufferErrorCode.FramebufferUnsupported: message = "The combination of internal formats of the attached images violates an implementation-dependent set of restrictions."; break;
                    case FramebufferErrorCode.FramebufferIncompleteMultisample: message = "Not all attached images have the same number of samples."; break;
                }
                throw new InvalidOperationException(message);
            }
        }
    }

#if !MONOMAC
    internal sealed class FramebufferHelperEXT : FramebufferHelper
    {
        internal FramebufferHelperEXT()
            : base()
        {
        }

        internal override void GenRenderbuffer(out int id)
        {
            GL.Ext.GenRenderbuffers(1, out id);
            REngine.CheckGLError();
        }
        internal override void GenRenderbuffer(out uint id)
        {
            GL.Ext.GenRenderbuffers(1, out id);
            REngine.CheckGLError();
        }

        internal override void BindRenderbuffer(int id)
        {
            GL.Ext.BindRenderbuffer(RenderbufferTarget.RenderbufferExt, id);
            REngine.CheckGLError();
        }
        internal override void BindRenderbuffer(uint id)
        {
            GL.Ext.BindRenderbuffer(RenderbufferTarget.RenderbufferExt, id);
            REngine.CheckGLError();
        }

        internal override void DeleteRenderbuffer(int id)
        {
            GL.Ext.DeleteRenderbuffers(1, ref id);
            REngine.CheckGLError();
        }
        internal override void DeleteRenderbuffer(uint id)
        {
            GL.Ext.DeleteRenderbuffers(1, ref id);
            REngine.CheckGLError();
        }

        internal override void RenderbufferStorageMultisample(int samples, int internalFormat, int width, int height)
        {
            GL.Ext.RenderbufferStorageMultisample(RenderbufferTarget.RenderbufferExt, samples, (RenderbufferStorage)internalFormat, width, height);
            REngine.CheckGLError();
        }

        internal override void GenFramebuffer(out int id)
        {
            GL.Ext.GenFramebuffers(1, out id);
            REngine.CheckGLError();
        }
        internal override void GenFramebuffer(out uint id)
        {
            GL.Ext.GenFramebuffers(1, out id);
            REngine.CheckGLError();
        }

        internal override void BindFramebuffer(int id)
        {
            GL.Ext.BindFramebuffer(FramebufferTarget.Framebuffer, id);
            REngine.CheckGLError();
        }
        internal override void BindFramebuffer(uint id)
        {
            GL.Ext.BindFramebuffer(FramebufferTarget.Framebuffer, id);
            REngine.CheckGLError();
        }

        internal override void BindReadFramebuffer(int readFramebuffer)
        {
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, readFramebuffer);
            REngine.CheckGLError();
        }
        internal override void BindReadFramebuffer(uint readFramebuffer)
        {
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, readFramebuffer);
            REngine.CheckGLError();
        }

        internal override void DeleteFramebuffer(int id)
        {
            GL.Ext.DeleteFramebuffers(1, ref id);
            REngine.CheckGLError();
        }
        internal override void DeleteFramebuffer(uint id)
        {
            GL.Ext.DeleteFramebuffers(1, ref id);
            REngine.CheckGLError();
        }

        internal override void FramebufferTexture2D(int attachement, int target, int texture, int level = 0, int samples = 0)
        {
            GL.Ext.FramebufferTexture2D(FramebufferTarget.FramebufferExt, (FramebufferAttachment)attachement, (TextureTarget)target, texture, level);
            REngine.CheckGLError();
        }
        internal override void FramebufferTexture2D(int attachement, int target, uint texture, int level = 0, int samples = 0)
        {
            GL.Ext.FramebufferTexture2D(FramebufferTarget.FramebufferExt, (FramebufferAttachment)attachement, (TextureTarget)target, texture, level);
            REngine.CheckGLError();
        }

        internal override void FramebufferRenderbuffer(int attachement, int renderbuffer, int level = 0)
        {
            GL.Ext.FramebufferRenderbuffer(FramebufferTarget.FramebufferExt, (FramebufferAttachment)attachement, RenderbufferTarget.Renderbuffer, renderbuffer);
            REngine.CheckGLError();
        }
        internal override void FramebufferRenderbuffer(int attachement, uint renderbuffer, int level = 0)
        {
            GL.Ext.FramebufferRenderbuffer(FramebufferTarget.FramebufferExt, (FramebufferAttachment)attachement, RenderbufferTarget.Renderbuffer, renderbuffer);
            REngine.CheckGLError();
        }

        internal override void GenerateMipmap(int target)
        {
            GL.Ext.GenerateMipmap((GenerateMipmapTarget)target);
            REngine.CheckGLError();
        }

        internal override void BlitFramebuffer(int iColorAttachment, int width, int height)
        {
            GL.ReadBuffer(ReadBufferMode.ColorAttachment0 + iColorAttachment);
            REngine.CheckGLError();
            GL.DrawBuffer(DrawBufferMode.ColorAttachment0 + iColorAttachment);
            REngine.CheckGLError();
            GL.Ext.BlitFramebuffer(0, 0, width, height, 0, 0, width, height, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest);
            REngine.CheckGLError();
        }

        internal override void CheckFramebufferStatus()
        {
            var status = GL.Ext.CheckFramebufferStatus(FramebufferTarget.FramebufferExt);
            if (status != FramebufferErrorCode.FramebufferComplete)
            {
                string message = "Framebuffer Incomplete.";
                switch (status)
                {
                    case FramebufferErrorCode.FramebufferIncompleteAttachmentExt: message = "Not all framebuffer attachment points are framebuffer attachment complete."; break;
                    case FramebufferErrorCode.FramebufferIncompleteMissingAttachmentExt: message = "No images are attached to the framebuffer."; break;
                    case FramebufferErrorCode.FramebufferUnsupportedExt: message = "The combination of internal formats of the attached images violates an implementation-dependent set of restrictions."; break;
                    case FramebufferErrorCode.FramebufferIncompleteMultisample: message = "Not all attached images have the same number of samples."; break;
                }
                throw new InvalidOperationException(message);
            }
        }
    }
#endif
#endif
}
