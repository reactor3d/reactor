using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Reactor.Platform.OpenGL
{
    /// <summary>
    ///     The OpenGL 4.X Backend to Reactor 3D
    /// </summary>
    partial class GL
    {
        internal static partial class NativeMethods
        {
            [DllImport(Library, EntryPoint = "glActiveShaderProgram", ExactSpelling = true)]
            internal static extern void ActiveShaderProgram(uint pipeline, uint program);

            [DllImport(Library, EntryPoint = "glActiveTexture", ExactSpelling = true)]
            internal static extern void ActiveTexture(int texture);

            [DllImport(Library, EntryPoint = "glAttachShader", ExactSpelling = true)]
            internal static extern void AttachShader(uint program, uint shader);

            [DllImport(Library, EntryPoint = "glBeginConditionalRender", ExactSpelling = true)]
            internal static extern void BeginConditionalRender(uint id, ConditionalRenderType mode);

            [DllImport(Library, EntryPoint = "glEndConditionalRender", ExactSpelling = true)]
            internal static extern void EndConditionalRender();

            [DllImport(Library, EntryPoint = "glBeginQuery", ExactSpelling = true)]
            internal static extern void BeginQuery(QueryTarget target, uint id);

            [DllImport(Library, EntryPoint = "glEndQuery", ExactSpelling = true)]
            internal static extern void EndQuery(QueryTarget target);

            [DllImport(Library, EntryPoint = "glBeginQueryIndexed", ExactSpelling = true)]
            internal static extern void BeginQueryIndexed(QueryTarget target, uint index, uint id);

            [DllImport(Library, EntryPoint = "glEndQueryIndexed", ExactSpelling = true)]
            internal static extern void EndQueryIndexed(QueryTarget target, uint index);

            [DllImport(Library, EntryPoint = "glBeginTransformFeedback", ExactSpelling = true)]
            internal static extern void BeginTransformFeedback(BeginFeedbackMode primitiveMode);

            [DllImport(Library, EntryPoint = "glEndTransformFeedback", ExactSpelling = true)]
            internal static extern void EndTransformFeedback();

            [DllImport(Library, EntryPoint = "glBindAttribLocation", ExactSpelling = true)]
            internal static extern void BindAttribLocation(uint program, uint index, string name);

            [DllImport(Library, EntryPoint = "glBindBuffer", ExactSpelling = true)]
            internal static extern void BindBuffer(BufferTarget target, uint buffer);

            [DllImport(Library, EntryPoint = "glBindBufferBase", ExactSpelling = true)]
            internal static extern void BindBufferBase(BufferTarget target, uint index, uint buffer);

            [DllImport(Library, EntryPoint = "glBindBufferRange", ExactSpelling = true)]
            internal static extern void BindBufferRange(BufferTarget target, uint index, uint buffer, IntPtr offset,
                IntPtr size);

            [DllImport(Library, EntryPoint = "glBindBuffersBase", ExactSpelling = true)]
            internal static extern void BindBuffersBase(BufferTarget target, uint first, int count, uint[] buffers);

            [DllImport(Library, EntryPoint = "glBindBuffersRange", ExactSpelling = true)]
            internal static extern void BindBuffersRange(BufferTarget target, uint first, int count, uint[] buffers,
                IntPtr[] offsets, IntPtr[] sizes);

            [DllImport(Library, EntryPoint = "glBindFragDataLocation", ExactSpelling = true)]
            internal static extern void BindFragDataLocation(uint program, uint colorNumber, string name);

            [DllImport(Library, EntryPoint = "glBindFragDataLocationIndexed", ExactSpelling = true)]
            internal static extern void BindFragDataLocationIndexed(uint program, uint colorNumber, uint index,
                string name);

            [DllImport(Library, EntryPoint = "glBindFramebuffer", ExactSpelling = true)]
            internal static extern void BindFramebuffer(FramebufferTarget target, uint framebuffer);

            [DllImport(Library, EntryPoint = "glBindImageTexture", ExactSpelling = true)]
            internal static extern void BindImageTexture(uint unit, uint texture, int level, bool layered, int layer,
                BufferAccess access, PixelInternalFormat format);

            [DllImport(Library, EntryPoint = "glBindImageTextures", ExactSpelling = true)]
            internal static extern void BindImageTextures(uint first, int count, uint[] textures);

            [DllImport(Library, EntryPoint = "glBindProgramPipeline", ExactSpelling = true)]
            internal static extern void BindProgramPipeline(uint pipeline);

            [DllImport(Library, EntryPoint = "glBindRenderbuffer", ExactSpelling = true)]
            internal static extern void BindRenderbuffer(RenderbufferTarget target, uint renderbuffer);

            [DllImport(Library, EntryPoint = "glBindSampler", ExactSpelling = true)]
            internal static extern void BindSampler(uint unit, uint sampler);

            [DllImport(Library, EntryPoint = "glBindSamplers", ExactSpelling = true)]
            internal static extern void BindSamplers(uint first, int count, uint[] samplers);

            [DllImport(Library, EntryPoint = "glBindTexture", ExactSpelling = true)]
            internal static extern void BindTexture(TextureTarget target, uint texture);

            [DllImport(Library, EntryPoint = "glBindTextures", ExactSpelling = true)]
            internal static extern void BindTextures(uint first, int count, uint[] textures);

            [DllImport(Library, EntryPoint = "glBindTextureUnit", ExactSpelling = true)]
            internal static extern void BindTextureUnit(uint unit, uint texture);

            [DllImport(Library, EntryPoint = "glBindTransformFeedback", ExactSpelling = true)]
            internal static extern void BindTransformFeedback(NvTransformFeedback2 target, uint id);

            [DllImport(Library, EntryPoint = "glBindVertexArray", ExactSpelling = true)]
            internal static extern void BindVertexArray(uint array);

            [DllImport(Library, EntryPoint = "glBindVertexBuffer", ExactSpelling = true)]
            internal static extern void BindVertexBuffer(uint bindingindex, uint buffer, IntPtr offset, IntPtr stride);

            [DllImport(Library, EntryPoint = "glVertexArrayVertexBuffer", ExactSpelling = true)]
            internal static extern void VertexArrayVertexBuffer(uint vaobj, uint bindingindex, uint buffer,
                IntPtr offset, int stride);

            [DllImport(Library, EntryPoint = "glBindVertexBuffers", ExactSpelling = true)]
            internal static extern void BindVertexBuffers(uint first, int count, uint[] buffers, IntPtr[] offsets,
                int[] strides);

            [DllImport(Library, EntryPoint = "glVertexArrayVertexBuffers", ExactSpelling = true)]
            internal static extern void VertexArrayVertexBuffers(uint vaobj, uint first, int count, uint[] buffers,
                IntPtr[] offsets, int[] strides);

            [DllImport(Library, EntryPoint = "glBlendColor", ExactSpelling = true)]
            internal static extern void BlendColor(float red, float green, float blue, float alpha);

            [DllImport(Library, EntryPoint = "glBlendEquation", ExactSpelling = true)]
            internal static extern void BlendEquation(BlendEquationMode mode);

            [DllImport(Library, EntryPoint = "glBlendEquationi", ExactSpelling = true)]
            internal static extern void BlendEquationi(uint buf, BlendEquationMode mode);

            [DllImport(Library, EntryPoint = "glBlendEquationSeparate", ExactSpelling = true)]
            internal static extern void BlendEquationSeparate(BlendEquationMode modeRGB, BlendEquationMode modeAlpha);

            [DllImport(Library, EntryPoint = "glBlendEquationSeparatei", ExactSpelling = true)]
            internal static extern void BlendEquationSeparatei(uint buf, BlendEquationMode modeRGB,
                BlendEquationMode modeAlpha);

            [DllImport(Library, EntryPoint = "glBlendFunc", ExactSpelling = true)]
            internal static extern void BlendFunc(BlendingFactorSrc sfactor, BlendingFactorDest dfactor);

            [DllImport(Library, EntryPoint = "glBlendFunci", ExactSpelling = true)]
            internal static extern void BlendFunci(uint buf, BlendingFactorSrc sfactor, BlendingFactorDest dfactor);

            [DllImport(Library, EntryPoint = "glBlendFuncSeparate", ExactSpelling = true)]
            internal static extern void BlendFuncSeparate(BlendingFactorSrc srcRGB, BlendingFactorDest dstRGB,
                BlendingFactorSrc srcAlpha, BlendingFactorDest dstAlpha);

            [DllImport(Library, EntryPoint = "glBlendFuncSeparatei", ExactSpelling = true)]
            internal static extern void BlendFuncSeparatei(uint buf, BlendingFactorSrc srcRGB,
                BlendingFactorDest dstRGB, BlendingFactorSrc srcAlpha, BlendingFactorDest dstAlpha);

            [DllImport(Library, EntryPoint = "glBlitFramebuffer", ExactSpelling = true)]
            internal static extern void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0,
                int dstY0, int dstX1, int dstY1, ClearBufferMask mask, BlitFramebufferFilter filter);

            [DllImport(Library, EntryPoint = "glBlitNamedFramebuffer", ExactSpelling = true)]
            internal static extern void BlitNamedFramebuffer(uint readFramebuffer, uint drawFramebuffer, int srcX0,
                int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, ClearBufferMask mask,
                BlitFramebufferFilter filter);

            [DllImport(Library, EntryPoint = "glBufferData", ExactSpelling = true)]
            internal static extern void BufferData(BufferTarget target, IntPtr size, IntPtr data,
                BufferUsageHint usage);

            [DllImport(Library, EntryPoint = "glNamedBufferData", ExactSpelling = true)]
            internal static extern void NamedBufferData(uint buffer, int size, IntPtr data, BufferUsageHint usage);

            [DllImport(Library, EntryPoint = "glBufferStorage", ExactSpelling = true)]
            internal static extern void BufferStorage(BufferTarget target, IntPtr size, IntPtr data, uint flags);

            [DllImport(Library, EntryPoint = "glNamedBufferStorage", ExactSpelling = true)]
            internal static extern void NamedBufferStorage(uint buffer, int size, IntPtr data, uint flags);

            [DllImport(Library, EntryPoint = "glBufferSubData", ExactSpelling = true)]
            internal static extern void BufferSubData(BufferTarget target, IntPtr offset, IntPtr size, IntPtr data);

            [DllImport(Library, EntryPoint = "glNamedBufferSubData", ExactSpelling = true)]
            internal static extern void NamedBufferSubData(uint buffer, IntPtr offset, int size, IntPtr data);

            [DllImport(Library, EntryPoint = "glCheckFramebufferStatus", ExactSpelling = true)]
            internal static extern FramebufferErrorCode CheckFramebufferStatus(FramebufferTarget target);

            [DllImport(Library, EntryPoint = "glCheckNamedFramebufferStatus", ExactSpelling = true)]
            internal static extern FramebufferErrorCode CheckNamedFramebufferStatus(uint framebuffer,
                FramebufferTarget target);

            [DllImport(Library, EntryPoint = "glClampColor", ExactSpelling = true)]
            internal static extern void ClampColor(ClampColorTarget target, ClampColorMode clamp);

            [DllImport(Library, EntryPoint = "glClear", ExactSpelling = true)]
            internal static extern void Clear(ClearBufferMask mask);

            [DllImport(Library, EntryPoint = "glClearBufferiv", ExactSpelling = true)]
            internal static extern void ClearBufferiv(ClearBuffer buffer, int drawbuffer, int[] value);

            [DllImport(Library, EntryPoint = "glClearBufferuiv", ExactSpelling = true)]
            internal static extern void ClearBufferuiv(ClearBuffer buffer, int drawbuffer, uint[] value);

            [DllImport(Library, EntryPoint = "glClearBufferfv", ExactSpelling = true)]
            internal static extern void ClearBufferfv(ClearBuffer buffer, int drawbuffer, float[] value);

            [DllImport(Library, EntryPoint = "glClearBufferfi", ExactSpelling = true)]
            internal static extern void ClearBufferfi(ClearBuffer buffer, int drawbuffer, float depth, int stencil);

            [DllImport(Library, EntryPoint = "glClearNamedFramebufferiv", ExactSpelling = true)]
            internal static extern void ClearNamedFramebufferiv(uint framebuffer, ClearBuffer buffer, int drawbuffer,
                int[] value);

            [DllImport(Library, EntryPoint = "glClearNamedFramebufferuiv", ExactSpelling = true)]
            internal static extern void ClearNamedFramebufferuiv(uint framebuffer, ClearBuffer buffer, int drawbuffer,
                uint[] value);

            [DllImport(Library, EntryPoint = "glClearNamedFramebufferfv", ExactSpelling = true)]
            internal static extern void ClearNamedFramebufferfv(uint framebuffer, ClearBuffer buffer, int drawbuffer,
                float[] value);

            [DllImport(Library, EntryPoint = "glClearNamedFramebufferfi", ExactSpelling = true)]
            internal static extern void ClearNamedFramebufferfi(uint framebuffer, ClearBuffer buffer, int drawbuffer,
                float depth, int stencil);

            [DllImport(Library, EntryPoint = "glClearBufferData", ExactSpelling = true)]
            internal static extern void ClearBufferData(BufferTarget target, SizedInternalFormat internalFormat,
                PixelInternalFormat format, PixelType type, IntPtr data);

            [DllImport(Library, EntryPoint = "glClearNamedBufferData", ExactSpelling = true)]
            internal static extern void ClearNamedBufferData(uint buffer, SizedInternalFormat internalFormat,
                PixelInternalFormat format, PixelType type, IntPtr data);

            [DllImport(Library, EntryPoint = "glClearBufferSubData", ExactSpelling = true)]
            internal static extern void ClearBufferSubData(BufferTarget target, SizedInternalFormat internalFormat,
                IntPtr offset, IntPtr size, PixelInternalFormat format, PixelType type, IntPtr data);

            [DllImport(Library, EntryPoint = "glClearNamedBufferSubData", ExactSpelling = true)]
            internal static extern void ClearNamedBufferSubData(uint buffer, SizedInternalFormat internalFormat,
                IntPtr offset, int size, PixelInternalFormat format, PixelType type, IntPtr data);

            [DllImport(Library, EntryPoint = "glClearColor", ExactSpelling = true)]
            internal static extern void ClearColor(float red, float green, float blue, float alpha);

            [DllImport(Library, EntryPoint = "glClearDepth", ExactSpelling = true)]
            internal static extern void ClearDepth(double depth);

            [DllImport(Library, EntryPoint = "glClearDepthf", ExactSpelling = true)]
            internal static extern void ClearDepthf(float depth);

            [DllImport(Library, EntryPoint = "glClearStencil", ExactSpelling = true)]
            internal static extern void ClearStencil(int s);

            [DllImport(Library, EntryPoint = "glClearTexImage", ExactSpelling = true)]
            internal static extern void ClearTexImage(uint texture, int level, PixelInternalFormat format,
                PixelType type, IntPtr data);

            [DllImport(Library, EntryPoint = "glClearTexSubImage", ExactSpelling = true)]
            internal static extern void ClearTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset,
                int width, int height, int depth, PixelInternalFormat format, PixelType type, IntPtr data);

            [DllImport(Library, EntryPoint = "glClientWaitSync", ExactSpelling = true)]
            internal static extern ArbSync ClientWaitSync(IntPtr sync, uint flags, ulong timeout);

            [DllImport(Library, EntryPoint = "glClipControl", ExactSpelling = true)]
            internal static extern void ClipControl(ClipControlOrigin origin, ClipControlDepth depth);

            [DllImport(Library, EntryPoint = "glColorMask", ExactSpelling = true)]
            internal static extern void ColorMask(bool red, bool green, bool blue, bool alpha);

            [DllImport(Library, EntryPoint = "glColorMaski", ExactSpelling = true)]
            internal static extern void ColorMaski(uint buf, bool red, bool green, bool blue, bool alpha);

            [DllImport(Library, EntryPoint = "glCompileShader", ExactSpelling = true)]
            internal static extern void CompileShader(uint shader);

            [DllImport(Library, EntryPoint = "glCompressedTexImage1D", ExactSpelling = true)]
            internal static extern void CompressedTexImage1D(TextureTarget target, int level,
                PixelInternalFormat internalFormat, int width, int border, int imageSize, IntPtr data);

            [DllImport(Library, EntryPoint = "glCompressedTexImage2D", ExactSpelling = true)]
            internal static extern void CompressedTexImage2D(TextureTarget target, int level,
                PixelInternalFormat internalFormat, int width, int height, int border, int imageSize, IntPtr data);

            [DllImport(Library, EntryPoint = "glCompressedTexImage3D", ExactSpelling = true)]
            internal static extern void CompressedTexImage3D(TextureTarget target, int level,
                PixelInternalFormat internalFormat, int width, int height, int depth, int border, int imageSize,
                IntPtr data);

            [DllImport(Library, EntryPoint = "glCompressedTexSubImage1D", ExactSpelling = true)]
            internal static extern void CompressedTexSubImage1D(TextureTarget target, int level, int xoffset, int width,
                PixelFormat format, int imageSize, IntPtr data);

            [DllImport(Library, EntryPoint = "glCompressedTextureSubImage1D", ExactSpelling = true)]
            internal static extern void CompressedTextureSubImage1D(uint texture, int level, int xoffset, int width,
                PixelInternalFormat format, int imageSize, IntPtr data);

            [DllImport(Library, EntryPoint = "glCompressedTexSubImage2D", ExactSpelling = true)]
            internal static extern void CompressedTexSubImage2D(TextureTarget target, int level, int xoffset,
                int yoffset, int width, int height, PixelFormat format, int imageSize, IntPtr data);

            [DllImport(Library, EntryPoint = "glCompressedTextureSubImage2D", ExactSpelling = true)]
            internal static extern void CompressedTextureSubImage2D(uint texture, int level, int xoffset, int yoffset,
                int width, int height, PixelInternalFormat format, int imageSize, IntPtr data);

            [DllImport(Library, EntryPoint = "glCompressedTexSubImage3D", ExactSpelling = true)]
            internal static extern void CompressedTexSubImage3D(TextureTarget target, int level, int xoffset,
                int yoffset, int zoffset, int width, int height, int depth, PixelFormat format, int imageSize,
                IntPtr data);

            [DllImport(Library, EntryPoint = "glCompressedTextureSubImage3D", ExactSpelling = true)]
            internal static extern void CompressedTextureSubImage3D(uint texture, int level, int xoffset, int yoffset,
                int zoffset, int width, int height, int depth, PixelInternalFormat format, int imageSize, IntPtr data);

            [DllImport(Library, EntryPoint = "glCopyBufferSubData", ExactSpelling = true)]
            internal static extern void CopyBufferSubData(BufferTarget readTarget, BufferTarget writeTarget,
                IntPtr readOffset, IntPtr writeOffset, IntPtr size);

            [DllImport(Library, EntryPoint = "glCopyNamedBufferSubData", ExactSpelling = true)]
            internal static extern void CopyNamedBufferSubData(uint readBuffer, uint writeBuffer, IntPtr readOffset,
                IntPtr writeOffset, int size);

            [DllImport(Library, EntryPoint = "glCopyImageSubData", ExactSpelling = true)]
            internal static extern void CopyImageSubData(uint srcName, BufferTarget srcTarget, int srcLevel, int srcX,
                int srcY, int srcZ, uint dstName, BufferTarget dstTarget, int dstLevel, int dstX, int dstY, int dstZ,
                int srcWidth, int srcHeight, int srcDepth);

            [DllImport(Library, EntryPoint = "glCopyTexImage1D", ExactSpelling = true)]
            internal static extern void CopyTexImage1D(TextureTarget target, int level,
                PixelInternalFormat internalFormat, int x, int y, int width, int border);

            [DllImport(Library, EntryPoint = "glCopyTexImage2D", ExactSpelling = true)]
            internal static extern void CopyTexImage2D(TextureTarget target, int level,
                PixelInternalFormat internalFormat, int x, int y, int width, int height, int border);

            [DllImport(Library, EntryPoint = "glCopyTexSubImage1D", ExactSpelling = true)]
            internal static extern void CopyTexSubImage1D(TextureTarget target, int level, int xoffset, int x, int y,
                int width);

            [DllImport(Library, EntryPoint = "glCopyTextureSubImage1D", ExactSpelling = true)]
            internal static extern void CopyTextureSubImage1D(uint texture, int level, int xoffset, int x, int y,
                int width);

            [DllImport(Library, EntryPoint = "glCopyTexSubImage2D", ExactSpelling = true)]
            internal static extern void CopyTexSubImage2D(TextureTarget target, int level, int xoffset, int yoffset,
                int x, int y, int width, int height);

            [DllImport(Library, EntryPoint = "glCopyTextureSubImage2D", ExactSpelling = true)]
            internal static extern void CopyTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int x,
                int y, int width, int height);

            [DllImport(Library, EntryPoint = "glCopyTexSubImage3D", ExactSpelling = true)]
            internal static extern void CopyTexSubImage3D(TextureTarget target, int level, int xoffset, int yoffset,
                int zoffset, int x, int y, int width, int height);

            [DllImport(Library, EntryPoint = "glCopyTextureSubImage3D", ExactSpelling = true)]
            internal static extern void CopyTextureSubImage3D(uint texture, int level, int xoffset, int yoffset,
                int zoffset, int x, int y, int width, int height);

            [DllImport(Library, EntryPoint = "glCreateBuffers", ExactSpelling = true)]
            internal static extern void CreateBuffers(int n, uint[] buffers);

            [DllImport(Library, EntryPoint = "glCreateFramebuffers", ExactSpelling = true)]
            internal static extern void CreateFramebuffers(int n, uint[] ids);

            [DllImport(Library, EntryPoint = "glCreateProgram", ExactSpelling = true)]
            internal static extern uint CreateProgram();

            [DllImport(Library, EntryPoint = "glCreateProgramPipelines", ExactSpelling = true)]
            internal static extern void CreateProgramPipelines(int n, uint[] pipelines);

            [DllImport(Library, EntryPoint = "glCreateQueries", ExactSpelling = true)]
            internal static extern void CreateQueries(QueryTarget target, int n, uint[] ids);

            [DllImport(Library, EntryPoint = "glCreateRenderbuffers", ExactSpelling = true)]
            internal static extern void CreateRenderbuffers(int n, uint[] renderbuffers);

            [DllImport(Library, EntryPoint = "glCreateSamplers", ExactSpelling = true)]
            internal static extern void CreateSamplers(int n, uint[] samplers);

            [DllImport(Library, EntryPoint = "glCreateShader", ExactSpelling = true)]
            internal static extern uint CreateShader(ShaderType shaderType);

            [DllImport(Library, EntryPoint = "glCreateShaderProgramv", ExactSpelling = true)]
            internal static extern uint CreateShaderProgramv(ShaderType type, int count, string strings);

            [DllImport(Library, EntryPoint = "glCreateTextures", ExactSpelling = true)]
            internal static extern void CreateTextures(TextureTarget target, int n, uint[] textures);

            [DllImport(Library, EntryPoint = "glCreateTransformFeedbacks", ExactSpelling = true)]
            internal static extern void CreateTransformFeedbacks(int n, uint[] ids);

            [DllImport(Library, EntryPoint = "glCreateVertexArrays", ExactSpelling = true)]
            internal static extern void CreateVertexArrays(int n, uint[] arrays);

            [DllImport(Library, EntryPoint = "glCullFace", ExactSpelling = true)]
            internal static extern void CullFace(CullFaceMode mode);

            [DllImport(Library, EntryPoint = "glDeleteBuffers", ExactSpelling = true)]
            internal static extern void DeleteBuffers(int n, uint[] buffers);

            [DllImport(Library, EntryPoint = "glDeleteFramebuffers", ExactSpelling = true)]
            internal static extern void DeleteFramebuffers(int n, uint[] framebuffers);

            [DllImport(Library, EntryPoint = "glDeleteProgram", ExactSpelling = true)]
            internal static extern void DeleteProgram(uint program);

            [DllImport(Library, EntryPoint = "glDeleteProgramPipelines", ExactSpelling = true)]
            internal static extern void DeleteProgramPipelines(int n, uint[] pipelines);

            [DllImport(Library, EntryPoint = "glDeleteQueries", ExactSpelling = true)]
            internal static extern void DeleteQueries(int n, uint[] ids);

            [DllImport(Library, EntryPoint = "glDeleteRenderbuffers", ExactSpelling = true)]
            internal static extern void DeleteRenderbuffers(int n, uint[] renderbuffers);

            [DllImport(Library, EntryPoint = "glDeleteSamplers", ExactSpelling = true)]
            internal static extern void DeleteSamplers(int n, uint[] samplers);

            [DllImport(Library, EntryPoint = "glDeleteShader", ExactSpelling = true)]
            internal static extern void DeleteShader(uint shader);

            [DllImport(Library, EntryPoint = "glDeleteSync", ExactSpelling = true)]
            internal static extern void DeleteSync(IntPtr sync);

            [DllImport(Library, EntryPoint = "glDeleteTextures", ExactSpelling = true)]
            internal static extern void DeleteTextures(int n, uint[] textures);

            [DllImport(Library, EntryPoint = "glDeleteTransformFeedbacks", ExactSpelling = true)]
            internal static extern void DeleteTransformFeedbacks(int n, uint[] ids);

            [DllImport(Library, EntryPoint = "glDeleteVertexArrays", ExactSpelling = true)]
            internal static extern void DeleteVertexArrays(int n, uint[] arrays);

            [DllImport(Library, EntryPoint = "glDepthFunc", ExactSpelling = true)]
            internal static extern void DepthFunc(DepthFunction func);

            [DllImport(Library, EntryPoint = "glDepthMask", ExactSpelling = true)]
            internal static extern void DepthMask(bool flag);

            [DllImport(Library, EntryPoint = "glDepthRange", ExactSpelling = true)]
            internal static extern void DepthRange(double nearVal, double farVal);

            [DllImport(Library, EntryPoint = "glDepthRangef", ExactSpelling = true)]
            internal static extern void DepthRangef(float nearVal, float farVal);

            [DllImport(Library, EntryPoint = "glDepthRangeArrayv", ExactSpelling = true)]
            internal static extern void DepthRangeArrayv(uint first, int count, double[] v);

            [DllImport(Library, EntryPoint = "glDepthRangeIndexed", ExactSpelling = true)]
            internal static extern void DepthRangeIndexed(uint index, double nearVal, double farVal);

            [DllImport(Library, EntryPoint = "glDetachShader", ExactSpelling = true)]
            internal static extern void DetachShader(uint program, uint shader);

            [DllImport(Library, EntryPoint = "glDispatchCompute", ExactSpelling = true)]
            internal static extern void DispatchCompute(uint num_groups_x, uint num_groups_y, uint num_groups_z);

            [DllImport(Library, EntryPoint = "glDispatchComputeIndirect", ExactSpelling = true)]
            internal static extern void DispatchComputeIndirect(IntPtr indirect);

            [DllImport(Library, EntryPoint = "glDrawArrays", ExactSpelling = true)]
            internal static extern void DrawArrays(BeginMode mode, int first, int count);

            [DllImport(Library, EntryPoint = "glDrawArraysIndirect", ExactSpelling = true)]
            internal static extern void DrawArraysIndirect(BeginMode mode, IntPtr indirect);

            [DllImport(Library, EntryPoint = "glDrawArraysInstanced", ExactSpelling = true)]
            internal static extern void DrawArraysInstanced(BeginMode mode, int first, int count, int primcount);

            [DllImport(Library, EntryPoint = "glDrawArraysInstancedBaseInstance", ExactSpelling = true)]
            internal static extern void DrawArraysInstancedBaseInstance(BeginMode mode, int first, int count,
                int primcount, uint baseinstance);

            [DllImport(Library, EntryPoint = "glDrawBuffer", ExactSpelling = true)]
            internal static extern void DrawBuffer(DrawBufferMode buf);

            [DllImport(Library, EntryPoint = "glNamedFramebufferDrawBuffer", ExactSpelling = true)]
            internal static extern void NamedFramebufferDrawBuffer(uint framebuffer, DrawBufferMode buf);

            [DllImport(Library, EntryPoint = "glDrawBuffers", ExactSpelling = true)]
            internal static extern void DrawBuffers(int n, DrawBuffersEnum[] bufs);

            [DllImport(Library, EntryPoint = "glNamedFramebufferDrawBuffers", ExactSpelling = true)]
            internal static extern void NamedFramebufferDrawBuffers(uint framebuffer, int n, DrawBufferMode[] bufs);

            [DllImport(Library, EntryPoint = "glDrawElements", ExactSpelling = true)]
            internal static extern void DrawElements(BeginMode mode, int count, DrawElementsType type, IntPtr indices);

            [DllImport(Library, EntryPoint = "glDrawElementsBaseVertex", ExactSpelling = true)]
            internal static extern void DrawElementsBaseVertex(BeginMode mode, int count, DrawElementsType type,
                IntPtr indices, int basevertex);

            [DllImport(Library, EntryPoint = "glDrawElementsIndirect", ExactSpelling = true)]
            internal static extern void DrawElementsIndirect(BeginMode mode, DrawElementsType type, IntPtr indirect);

            [DllImport(Library, EntryPoint = "glDrawElementsInstanced", ExactSpelling = true)]
            internal static extern void DrawElementsInstanced(BeginMode mode, int count, DrawElementsType type,
                IntPtr indices, int primcount);

            [DllImport(Library, EntryPoint = "glDrawElementsInstancedBaseInstance", ExactSpelling = true)]
            internal static extern void DrawElementsInstancedBaseInstance(BeginMode mode, int count,
                DrawElementsType type, IntPtr indices, int primcount, uint baseinstance);

            [DllImport(Library, EntryPoint = "glDrawElementsInstancedBaseVertex", ExactSpelling = true)]
            internal static extern void DrawElementsInstancedBaseVertex(BeginMode mode, int count,
                DrawElementsType type, IntPtr indices, int primcount, int basevertex);

            [DllImport(Library, EntryPoint = "glDrawElementsInstancedBaseVertexBaseInstance", ExactSpelling = true)]
            internal static extern void DrawElementsInstancedBaseVertexBaseInstance(BeginMode mode, int count,
                DrawElementsType type, IntPtr indices, int primcount, int basevertex, uint baseinstance);

            [DllImport(Library, EntryPoint = "glDrawRangeElements", ExactSpelling = true)]
            internal static extern void DrawRangeElements(BeginMode mode, uint start, uint end, int count,
                DrawElementsType type, IntPtr indices);

            [DllImport(Library, EntryPoint = "glDrawRangeElementsBaseVertex", ExactSpelling = true)]
            internal static extern void DrawRangeElementsBaseVertex(BeginMode mode, uint start, uint end, int count,
                DrawElementsType type, IntPtr indices, int basevertex);

            [DllImport(Library, EntryPoint = "glDrawTransformFeedback", ExactSpelling = true)]
            internal static extern void DrawTransformFeedback(NvTransformFeedback2 mode, uint id);

            [DllImport(Library, EntryPoint = "glDrawTransformFeedbackInstanced", ExactSpelling = true)]
            internal static extern void DrawTransformFeedbackInstanced(BeginMode mode, uint id, int primcount);

            [DllImport(Library, EntryPoint = "glDrawTransformFeedbackStream", ExactSpelling = true)]
            internal static extern void DrawTransformFeedbackStream(NvTransformFeedback2 mode, uint id, uint stream);

            [DllImport(Library, EntryPoint = "glDrawTransformFeedbackStreamInstanced", ExactSpelling = true)]
            internal static extern void DrawTransformFeedbackStreamInstanced(BeginMode mode, uint id, uint stream,
                int primcount);

            [DllImport(Library, EntryPoint = "glEnable", ExactSpelling = true)]
            internal static extern void Enable(EnableCap cap);

            [DllImport(Library, EntryPoint = "glDisable", ExactSpelling = true)]
            internal static extern void Disable(EnableCap cap);

            [DllImport(Library, EntryPoint = "glEnablei", ExactSpelling = true)]
            internal static extern void Enablei(EnableCap cap, uint index);

            [DllImport(Library, EntryPoint = "glDisablei", ExactSpelling = true)]
            internal static extern void Disablei(EnableCap cap, uint index);

            [DllImport(Library, EntryPoint = "glEnableVertexAttribArray", ExactSpelling = true)]
            internal static extern void EnableVertexAttribArray(uint index);

            [DllImport(Library, EntryPoint = "glDisableVertexAttribArray", ExactSpelling = true)]
            internal static extern void DisableVertexAttribArray(uint index);

            [DllImport(Library, EntryPoint = "glEnableVertexArrayAttrib", ExactSpelling = true)]
            internal static extern void EnableVertexArrayAttrib(uint vaobj, uint index);

            [DllImport(Library, EntryPoint = "glDisableVertexArrayAttrib", ExactSpelling = true)]
            internal static extern void DisableVertexArrayAttrib(uint vaobj, uint index);

            [DllImport(Library, EntryPoint = "glFenceSync", ExactSpelling = true)]
            internal static extern IntPtr FenceSync(ArbSync condition, uint flags);

            [DllImport(Library, EntryPoint = "glFinish", ExactSpelling = true)]
            internal static extern void Finish();

            [DllImport(Library, EntryPoint = "glFlush", ExactSpelling = true)]
            internal static extern void Flush();

            [DllImport(Library, EntryPoint = "glFlushMappedBufferRange", ExactSpelling = true)]
            internal static extern void FlushMappedBufferRange(BufferTarget target, IntPtr offset, IntPtr length);

            [DllImport(Library, EntryPoint = "glFlushMappedNamedBufferRange", ExactSpelling = true)]
            internal static extern void FlushMappedNamedBufferRange(uint buffer, IntPtr offset, int length);

            [DllImport(Library, EntryPoint = "glFramebufferParameteri", ExactSpelling = true)]
            internal static extern void FramebufferParameteri(FramebufferTarget target, FramebufferPName pname,
                int param);

            [DllImport(Library, EntryPoint = "glNamedFramebufferParameteri", ExactSpelling = true)]
            internal static extern void NamedFramebufferParameteri(uint framebuffer, FramebufferPName pname, int param);

            [DllImport(Library, EntryPoint = "glFramebufferRenderbuffer", ExactSpelling = true)]
            internal static extern void FramebufferRenderbuffer(FramebufferTarget target,
                FramebufferAttachment attachment, RenderbufferTarget renderbuffertarget, uint renderbuffer);

            [DllImport(Library, EntryPoint = "glNamedFramebufferRenderbuffer", ExactSpelling = true)]
            internal static extern void NamedFramebufferRenderbuffer(uint framebuffer, FramebufferAttachment attachment,
                RenderbufferTarget renderbuffertarget, uint renderbuffer);

            [DllImport(Library, EntryPoint = "glFramebufferTexture", ExactSpelling = true)]
            internal static extern void FramebufferTexture(FramebufferTarget target, FramebufferAttachment attachment,
                uint texture, int level);

            [DllImport(Library, EntryPoint = "glFramebufferTexture1D", ExactSpelling = true)]
            internal static extern void FramebufferTexture1D(FramebufferTarget target, FramebufferAttachment attachment,
                TextureTarget textarget, uint texture, int level);

            [DllImport(Library, EntryPoint = "glFramebufferTexture2D", ExactSpelling = true)]
            internal static extern void FramebufferTexture2D(FramebufferTarget target, FramebufferAttachment attachment,
                TextureTarget textarget, uint texture, int level);

            [DllImport(Library, EntryPoint = "glFramebufferTexture3D", ExactSpelling = true)]
            internal static extern void FramebufferTexture3D(FramebufferTarget target, FramebufferAttachment attachment,
                TextureTarget textarget, uint texture, int level, int layer);

            [DllImport(Library, EntryPoint = "glNamedFramebufferTexture", ExactSpelling = true)]
            internal static extern void NamedFramebufferTexture(uint framebuffer, FramebufferAttachment attachment,
                uint texture, int level);

            [DllImport(Library, EntryPoint = "glFramebufferTextureLayer", ExactSpelling = true)]
            internal static extern void FramebufferTextureLayer(FramebufferTarget target,
                FramebufferAttachment attachment, uint texture, int level, int layer);

            [DllImport(Library, EntryPoint = "glNamedFramebufferTextureLayer", ExactSpelling = true)]
            internal static extern void NamedFramebufferTextureLayer(uint framebuffer, FramebufferAttachment attachment,
                uint texture, int level, int layer);

            [DllImport(Library, EntryPoint = "glFrontFace", ExactSpelling = true)]
            internal static extern void FrontFace(FrontFaceDirection mode);

            [DllImport(Library, EntryPoint = "glGenBuffers", ExactSpelling = true)]
            internal static extern void GenBuffers(int n, [OutAttribute] uint[] buffers);

            [DllImport(Library, EntryPoint = "glGenerateMipmap", ExactSpelling = true)]
            internal static extern void GenerateMipmap(GenerateMipmapTarget target);

            [DllImport(Library, EntryPoint = "glGenerateTextureMipmap", ExactSpelling = true)]
            internal static extern void GenerateTextureMipmap(uint texture);

            [DllImport(Library, EntryPoint = "glGenFramebuffers", ExactSpelling = true)]
            internal static extern void GenFramebuffers(int n, [OutAttribute] uint[] ids);

            [DllImport(Library, EntryPoint = "glGenProgramPipelines", ExactSpelling = true)]
            internal static extern void GenProgramPipelines(int n, [OutAttribute] uint[] pipelines);

            [DllImport(Library, EntryPoint = "glGenQueries", ExactSpelling = true)]
            internal static extern void GenQueries(int n, [OutAttribute] uint[] ids);

            [DllImport(Library, EntryPoint = "glGenRenderbuffers", ExactSpelling = true)]
            internal static extern void GenRenderbuffers(int n, [OutAttribute] uint[] renderbuffers);

            [DllImport(Library, EntryPoint = "glGenSamplers", ExactSpelling = true)]
            internal static extern void GenSamplers(int n, [OutAttribute] uint[] samplers);

            [DllImport(Library, EntryPoint = "glGenTextures", ExactSpelling = true)]
            internal static extern void GenTextures(int n, [OutAttribute] uint[] textures);

            [DllImport(Library, EntryPoint = "glGenTransformFeedbacks", ExactSpelling = true)]
            internal static extern void GenTransformFeedbacks(int n, [OutAttribute] uint[] ids);

            [DllImport(Library, EntryPoint = "glGenVertexArrays", ExactSpelling = true)]
            internal static extern void GenVertexArrays(int n, [OutAttribute] uint[] arrays);

            [DllImport(Library, EntryPoint = "glGetBooleanv", ExactSpelling = true)]
            internal static extern void GetBooleanv(GetPName pname, [OutAttribute] bool[] data);

            [DllImport(Library, EntryPoint = "glGetDoublev", ExactSpelling = true)]
            internal static extern void GetDoublev(GetPName pname, [OutAttribute] double[] data);

            [DllImport(Library, EntryPoint = "glGetFloatv", ExactSpelling = true)]
            internal static extern void GetFloatv(GetPName pname, [OutAttribute] float[] data);

            [DllImport(Library, EntryPoint = "glGetIntegerv", ExactSpelling = true)]
            internal static extern void GetIntegerv(GetPName pname, [OutAttribute] int[] data);

            [DllImport(Library, EntryPoint = "glGetInteger64v", ExactSpelling = true)]
            internal static extern void GetInteger64v(ArbSync pname, [OutAttribute] long[] data);

            [DllImport(Library, EntryPoint = "glGetBooleani_v", ExactSpelling = true)]
            internal static extern void GetBooleani_v(GetPName target, uint index, [OutAttribute] bool[] data);

            [DllImport(Library, EntryPoint = "glGetIntegeri_v", ExactSpelling = true)]
            internal static extern void GetIntegeri_v(GetPName target, uint index, [OutAttribute] int[] data);

            [DllImport(Library, EntryPoint = "glGetFloati_v", ExactSpelling = true)]
            internal static extern void GetFloati_v(GetPName target, uint index, [OutAttribute] float[] data);

            [DllImport(Library, EntryPoint = "glGetDoublei_v", ExactSpelling = true)]
            internal static extern void GetDoublei_v(GetPName target, uint index, [OutAttribute] double[] data);

            [DllImport(Library, EntryPoint = "glGetInteger64i_v", ExactSpelling = true)]
            internal static extern void GetInteger64i_v(GetPName target, uint index, [OutAttribute] long[] data);

            [DllImport(Library, EntryPoint = "glGetActiveAtomicCounterBufferiv", ExactSpelling = true)]
            internal static extern void GetActiveAtomicCounterBufferiv(uint program, uint bufferIndex,
                AtomicCounterParameterName pname, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetActiveAttrib", ExactSpelling = true)]
            internal static extern void GetActiveAttrib(uint program, uint index, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] int[] size, [OutAttribute] ActiveAttribType[] type,
                [OutAttribute] StringBuilder name);

            [DllImport(Library, EntryPoint = "glGetActiveSubroutineName", ExactSpelling = true)]
            internal static extern void GetActiveSubroutineName(uint program, ShaderType shadertype, uint index,
                int bufsize, [OutAttribute] int[] length, [OutAttribute] StringBuilder name);

            [DllImport(Library, EntryPoint = "glGetActiveSubroutineUniformiv", ExactSpelling = true)]
            internal static extern void GetActiveSubroutineUniformiv(uint program, ShaderType shadertype, uint index,
                SubroutineParameterName pname, [OutAttribute] int[] values);

            [DllImport(Library, EntryPoint = "glGetActiveSubroutineUniformName", ExactSpelling = true)]
            internal static extern void GetActiveSubroutineUniformName(uint program, ShaderType shadertype, uint index,
                int bufsize, [OutAttribute] int[] length, [OutAttribute] StringBuilder name);

            [DllImport(Library, EntryPoint = "glGetActiveUniform", ExactSpelling = true)]
            internal static extern void GetActiveUniform(uint program, uint index, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] int[] size, [OutAttribute] ActiveUniformType[] type,
                [OutAttribute] StringBuilder name);

            [DllImport(Library, EntryPoint = "glGetActiveUniformBlockiv", ExactSpelling = true)]
            internal static extern void GetActiveUniformBlockiv(uint program, uint uniformBlockIndex,
                ActiveUniformBlockParameter pname, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetActiveUniformBlockName", ExactSpelling = true)]
            internal static extern void GetActiveUniformBlockName(uint program, uint uniformBlockIndex, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] StringBuilder uniformBlockName);

            [DllImport(Library, EntryPoint = "glGetActiveUniformName", ExactSpelling = true)]
            internal static extern void GetActiveUniformName(uint program, uint uniformIndex, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] StringBuilder uniformName);

            [DllImport(Library, EntryPoint = "glGetActiveUniformsiv", ExactSpelling = true)]
            internal static extern void GetActiveUniformsiv(uint program, int uniformCount,
                [OutAttribute] uint[] uniformIndices, ActiveUniformType pname, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetAttachedShaders", ExactSpelling = true)]
            internal static extern void GetAttachedShaders(uint program, int maxCount, [OutAttribute] int[] count,
                [OutAttribute] uint[] shaders);

            [DllImport(Library, EntryPoint = "glGetAttribLocation", ExactSpelling = true)]
            internal static extern int GetAttribLocation(uint program, string name);

            [DllImport(Library, EntryPoint = "glGetBufferParameteriv", ExactSpelling = true)]
            internal static extern void GetBufferParameteriv(BufferTarget target, BufferParameterName value,
                [OutAttribute] int[] data);

            [DllImport(Library, EntryPoint = "glGetBufferParameteri64v", ExactSpelling = true)]
            internal static extern void GetBufferParameteri64v(BufferTarget target, BufferParameterName value,
                [OutAttribute] long[] data);

            [DllImport(Library, EntryPoint = "glGetNamedBufferParameteriv", ExactSpelling = true)]
            internal static extern void GetNamedBufferParameteriv(uint buffer, BufferParameterName pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetNamedBufferParameteri64v", ExactSpelling = true)]
            internal static extern void GetNamedBufferParameteri64v(uint buffer, BufferParameterName pname,
                [OutAttribute] long[] @params);

            [DllImport(Library, EntryPoint = "glGetBufferPointerv", ExactSpelling = true)]
            internal static extern void GetBufferPointerv(BufferTarget target, BufferPointer pname,
                [OutAttribute] IntPtr @params);

            [DllImport(Library, EntryPoint = "glGetNamedBufferPointerv", ExactSpelling = true)]
            internal static extern void GetNamedBufferPointerv(uint buffer, BufferPointer pname,
                [OutAttribute] IntPtr @params);

            [DllImport(Library, EntryPoint = "glGetBufferSubData", ExactSpelling = true)]
            internal static extern void GetBufferSubData(BufferTarget target, IntPtr offset, IntPtr size,
                [OutAttribute] IntPtr data);

            [DllImport(Library, EntryPoint = "glGetNamedBufferSubData", ExactSpelling = true)]
            internal static extern void GetNamedBufferSubData(uint buffer, IntPtr offset, int size,
                [OutAttribute] IntPtr data);

            [DllImport(Library, EntryPoint = "glGetCompressedTexImage", ExactSpelling = true)]
            internal static extern void GetCompressedTexImage(TextureTarget target, int level,
                [OutAttribute] IntPtr pixels);

            [DllImport(Library, EntryPoint = "glGetnCompressedTexImage", ExactSpelling = true)]
            internal static extern void GetnCompressedTexImage(TextureTarget target, int level, int bufSize,
                [OutAttribute] IntPtr pixels);

            [DllImport(Library, EntryPoint = "glGetCompressedTextureImage", ExactSpelling = true)]
            internal static extern void GetCompressedTextureImage(uint texture, int level, int bufSize,
                [OutAttribute] IntPtr pixels);

            [DllImport(Library, EntryPoint = "glGetCompressedTextureSubImage", ExactSpelling = true)]
            internal static extern void GetCompressedTextureSubImage(uint texture, int level, int xoffset, int yoffset,
                int zoffset, int width, int height, int depth, int bufSize, [OutAttribute] IntPtr pixels);

            [DllImport(Library, EntryPoint = "glGetError", ExactSpelling = true)]
            internal static extern ErrorCode GetError();

            [DllImport(Library, EntryPoint = "glGetFragDataIndex", ExactSpelling = true)]
            internal static extern int GetFragDataIndex(uint program, string name);

            [DllImport(Library, EntryPoint = "glGetFragDataLocation", ExactSpelling = true)]
            internal static extern int GetFragDataLocation(uint program, string name);

            [DllImport(Library, EntryPoint = "glGetFramebufferAttachmentParameteriv", ExactSpelling = true)]
            internal static extern void GetFramebufferAttachmentParameteriv(FramebufferTarget target,
                FramebufferAttachment attachment, FramebufferParameterName pname, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetNamedFramebufferAttachmentParameteriv", ExactSpelling = true)]
            internal static extern void GetNamedFramebufferAttachmentParameteriv(uint framebuffer,
                FramebufferAttachment attachment, FramebufferParameterName pname, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetFramebufferParameteriv", ExactSpelling = true)]
            internal static extern void GetFramebufferParameteriv(FramebufferTarget target, FramebufferPName pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetNamedFramebufferParameteriv", ExactSpelling = true)]
            internal static extern void GetNamedFramebufferParameteriv(uint framebuffer, FramebufferPName pname,
                [OutAttribute] int[] param);

            [DllImport(Library, EntryPoint = "glGetGraphicsResetStatus", ExactSpelling = true)]
            internal static extern GraphicResetStatus GetGraphicsResetStatus();

            [DllImport(Library, EntryPoint = "glGetInternalformativ", ExactSpelling = true)]
            internal static extern void GetInternalformativ(TextureTarget target, PixelInternalFormat internalFormat,
                GetPName pname, int bufSize, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetInternalformati64v", ExactSpelling = true)]
            internal static extern void GetInternalformati64v(TextureTarget target, PixelInternalFormat internalFormat,
                GetPName pname, int bufSize, [OutAttribute] long[] @params);

            [DllImport(Library, EntryPoint = "glGetMultisamplefv", ExactSpelling = true)]
            internal static extern void GetMultisamplefv(GetMultisamplePName pname, uint index,
                [OutAttribute] float[] val);

            [DllImport(Library, EntryPoint = "glGetObjectLabel", ExactSpelling = true)]
            internal static extern void GetObjectLabel(ObjectLabel identifier, uint name, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] StringBuilder label);

            [DllImport(Library, EntryPoint = "glGetObjectPtrLabel", ExactSpelling = true)]
            internal static extern void GetObjectPtrLabel([OutAttribute] IntPtr ptr, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] StringBuilder label);

            [DllImport(Library, EntryPoint = "glGetPointerv", ExactSpelling = true)]
            internal static extern void GetProgramv(uint program, ProgramParameter pname, [OutAttribute] int @params);

            [DllImport(Library, EntryPoint = "glGetProgramiv", ExactSpelling = true)]
            internal static extern void GetProgramiv(uint program, ProgramParameter pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetProgramBinary", ExactSpelling = true)]
            internal static extern void GetProgramBinary(uint program, int bufsize, [OutAttribute] int[] length,
                [OutAttribute] int[] binaryFormat, [OutAttribute] IntPtr binary);

            [DllImport(Library, EntryPoint = "glGetProgramInfoLog", ExactSpelling = true)]
            internal static extern void GetProgramInfoLog(uint program, int maxLength, [OutAttribute] int[] length,
                [OutAttribute] StringBuilder infoLog);

            [DllImport(Library, EntryPoint = "glGetProgramInterfaceiv", ExactSpelling = true)]
            internal static extern void GetProgramInterfaceiv(uint program, ProgramInterface programInterface,
                ProgramInterfaceParameterName pname, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetProgramPipelineiv", ExactSpelling = true)]
            internal static extern void GetProgramPipelineiv(uint pipeline, int pname, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetProgramPipelineInfoLog", ExactSpelling = true)]
            internal static extern void GetProgramPipelineInfoLog(uint pipeline, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] StringBuilder infoLog);

            [DllImport(Library, EntryPoint = "glGetProgramResourceiv", ExactSpelling = true)]
            internal static extern void GetProgramResourceiv(uint program, ProgramInterface programInterface,
                uint index, int propCount, [OutAttribute] ProgramResourceParameterName[] props, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetProgramResourceIndex", ExactSpelling = true)]
            internal static extern uint GetProgramResourceIndex(uint program, ProgramInterface programInterface,
                string name);

            [DllImport(Library, EntryPoint = "glGetProgramResourceLocation", ExactSpelling = true)]
            internal static extern int GetProgramResourceLocation(uint program, ProgramInterface programInterface,
                string name);

            [DllImport(Library, EntryPoint = "glGetProgramResourceLocationIndex", ExactSpelling = true)]
            internal static extern int GetProgramResourceLocationIndex(uint program, ProgramInterface programInterface,
                string name);

            [DllImport(Library, EntryPoint = "glGetProgramResourceName", ExactSpelling = true)]
            internal static extern void GetProgramResourceName(uint program, ProgramInterface programInterface,
                uint index, int bufSize, [OutAttribute] int[] length, [OutAttribute] StringBuilder name);

            [DllImport(Library, EntryPoint = "glGetProgramStageiv", ExactSpelling = true)]
            internal static extern void GetProgramStageiv(uint program, ShaderType shadertype,
                ProgramStageParameterName pname, [OutAttribute] int[] values);

            [DllImport(Library, EntryPoint = "glGetQueryIndexediv", ExactSpelling = true)]
            internal static extern void GetQueryIndexediv(QueryTarget target, uint index, GetQueryParam pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetQueryiv", ExactSpelling = true)]
            internal static extern void GetQueryiv(QueryTarget target, GetQueryParam pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetQueryObjectiv", ExactSpelling = true)]
            internal static extern void GetQueryObjectiv(uint id, GetQueryObjectParam pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetQueryObjectuiv", ExactSpelling = true)]
            internal static extern void GetQueryObjectuiv(uint id, GetQueryObjectParam pname,
                [OutAttribute] uint[] @params);

            [DllImport(Library, EntryPoint = "glGetQueryObjecti64v", ExactSpelling = true)]
            internal static extern void GetQueryObjecti64v(uint id, GetQueryObjectParam pname,
                [OutAttribute] long[] @params);

            [DllImport(Library, EntryPoint = "glGetQueryObjectui64v", ExactSpelling = true)]
            internal static extern void GetQueryObjectui64v(uint id, GetQueryObjectParam pname,
                [OutAttribute] ulong[] @params);

            [DllImport(Library, EntryPoint = "glGetRenderbufferParameteriv", ExactSpelling = true)]
            internal static extern void GetRenderbufferParameteriv(RenderbufferTarget target,
                RenderbufferParameterName pname, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetNamedRenderbufferParameteriv", ExactSpelling = true)]
            internal static extern void GetNamedRenderbufferParameteriv(uint renderbuffer,
                RenderbufferParameterName pname, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetSamplerParameterfv", ExactSpelling = true)]
            internal static extern void GetSamplerParameterfv(uint sampler, TextureParameterName pname,
                [OutAttribute] float[] @params);

            [DllImport(Library, EntryPoint = "glGetSamplerParameteriv", ExactSpelling = true)]
            internal static extern void GetSamplerParameteriv(uint sampler, TextureParameterName pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetSamplerParameterIiv", ExactSpelling = true)]
            internal static extern void GetSamplerParameterIiv(uint sampler, TextureParameterName pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetSamplerParameterIuiv", ExactSpelling = true)]
            internal static extern void GetSamplerParameterIuiv(uint sampler, TextureParameterName pname,
                [OutAttribute] uint[] @params);

            [DllImport(Library, EntryPoint = "glGetShaderiv", ExactSpelling = true)]
            internal static extern void GetShaderiv(uint shader, ShaderParameter pname, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetShaderInfoLog", ExactSpelling = true)]
            internal static extern void GetShaderInfoLog(uint shader, int maxLength, [OutAttribute] int[] length,
                [OutAttribute] StringBuilder infoLog);

            [DllImport(Library, EntryPoint = "glGetShaderPrecisionFormat", ExactSpelling = true)]
            internal static extern void GetShaderPrecisionFormat(ShaderType shaderType, int precisionType,
                [OutAttribute] int[] range, [OutAttribute] int[] precision);

            [DllImport(Library, EntryPoint = "glGetShaderSource", ExactSpelling = true)]
            internal static extern void GetShaderSource(uint shader, int bufSize, [OutAttribute] int[] length,
                [OutAttribute] StringBuilder source);

            [DllImport(Library, EntryPoint = "glGetString", ExactSpelling = true)]
            internal static extern IntPtr GetString(StringName name);

            [DllImport(Library, EntryPoint = "glGetStringi", ExactSpelling = true)]
            internal static extern IntPtr GetStringi(StringName name, uint index);

            [DllImport(Library, EntryPoint = "glGetSubroutineIndex", ExactSpelling = true)]
            internal static extern uint GetSubroutineIndex(uint program, ShaderType shadertype, string name);

            [DllImport(Library, EntryPoint = "glGetSubroutineUniformLocation", ExactSpelling = true)]
            internal static extern int GetSubroutineUniformLocation(uint program, ShaderType shadertype, string name);

            [DllImport(Library, EntryPoint = "glGetSynciv", ExactSpelling = true)]
            internal static extern void GetSynciv(IntPtr sync, ArbSync pname, int bufSize, [OutAttribute] int[] length,
                [OutAttribute] int[] values);

            [DllImport(Library, EntryPoint = "glGetTexImage", ExactSpelling = true)]
            internal static extern void GetTexImage(TextureTarget target, int level, PixelFormat format, PixelType type,
                [OutAttribute] IntPtr pixels);

            [DllImport(Library, EntryPoint = "glGetnTexImage", ExactSpelling = true)]
            internal static extern void GetnTexImage(TextureTarget target, int level, PixelFormat format,
                PixelType type, int bufSize, [OutAttribute] IntPtr pixels);

            [DllImport(Library, EntryPoint = "glGetTextureImage", ExactSpelling = true)]
            internal static extern void GetTextureImage(uint texture, int level, PixelFormat format, PixelType type,
                int bufSize, [OutAttribute] IntPtr pixels);

            [DllImport(Library, EntryPoint = "glGetTexLevelParameterfv", ExactSpelling = true)]
            internal static extern void GetTexLevelParameterfv(TextureTarget target, int level,
                GetTextureLevelParameter pname, [OutAttribute] float[] @params);

            [DllImport(Library, EntryPoint = "glGetTexLevelParameteriv", ExactSpelling = true)]
            internal static extern void GetTexLevelParameteriv(TextureTarget target, int level,
                GetTextureLevelParameter pname, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetTextureLevelParameterfv", ExactSpelling = true)]
            internal static extern void GetTextureLevelParameterfv(uint texture, int level,
                GetTextureLevelParameter pname, [OutAttribute] float[] @params);

            [DllImport(Library, EntryPoint = "glGetTextureLevelParameteriv", ExactSpelling = true)]
            internal static extern void GetTextureLevelParameteriv(uint texture, int level,
                GetTextureLevelParameter pname, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetTexParameterfv", ExactSpelling = true)]
            internal static extern void GetTexParameterfv(TextureTarget target, GetTextureParameter pname,
                [OutAttribute] float[] @params);

            [DllImport(Library, EntryPoint = "glGetTexParameteriv", ExactSpelling = true)]
            internal static extern void GetTexParameteriv(TextureTarget target, GetTextureParameter pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetTexParameterIiv", ExactSpelling = true)]
            internal static extern void GetTexParameterIiv(TextureTarget target, GetTextureParameter pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetTexParameterIuiv", ExactSpelling = true)]
            internal static extern void GetTexParameterIuiv(TextureTarget target, GetTextureParameter pname,
                [OutAttribute] uint[] @params);

            [DllImport(Library, EntryPoint = "glGetTextureParameterfv", ExactSpelling = true)]
            internal static extern void GetTextureParameterfv(uint texture, GetTextureParameter pname,
                [OutAttribute] float[] @params);

            [DllImport(Library, EntryPoint = "glGetTextureParameteriv", ExactSpelling = true)]
            internal static extern void GetTextureParameteriv(uint texture, GetTextureParameter pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetTextureParameterIiv", ExactSpelling = true)]
            internal static extern void GetTextureParameterIiv(uint texture, GetTextureParameter pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetTextureParameterIuiv", ExactSpelling = true)]
            internal static extern void GetTextureParameterIuiv(uint texture, GetTextureParameter pname,
                [OutAttribute] uint[] @params);

            [DllImport(Library, EntryPoint = "glGetTextureSubImage", ExactSpelling = true)]
            internal static extern void GetTextureSubImage(uint texture, int level, int xoffset, int yoffset,
                int zoffset, int width, int height, int depth, PixelFormat format, PixelType type, int bufSize,
                [OutAttribute] IntPtr pixels);

            [DllImport(Library, EntryPoint = "glGetTransformFeedbackiv", ExactSpelling = true)]
            internal static extern void GetTransformFeedbackiv(uint xfb, TransformFeedbackParameterName pname,
                [OutAttribute] int[] param);

            [DllImport(Library, EntryPoint = "glGetTransformFeedbacki_v", ExactSpelling = true)]
            internal static extern void GetTransformFeedbacki_v(uint xfb, TransformFeedbackParameterName pname,
                uint index, [OutAttribute] int[] param);

            [DllImport(Library, EntryPoint = "glGetTransformFeedbacki64_v", ExactSpelling = true)]
            internal static extern void GetTransformFeedbacki64_v(uint xfb, TransformFeedbackParameterName pname,
                uint index, [OutAttribute] long[] param);

            [DllImport(Library, EntryPoint = "glGetTransformFeedbackVarying", ExactSpelling = true)]
            internal static extern void GetTransformFeedbackVarying(uint program, uint index, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] int[] size, [OutAttribute] ActiveAttribType[] type,
                [OutAttribute] StringBuilder name);

            [DllImport(Library, EntryPoint = "glGetUniformfv", ExactSpelling = true)]
            internal static extern void GetUniformfv(uint program, int location, [OutAttribute] float[] @params);

            [DllImport(Library, EntryPoint = "glGetUniformiv", ExactSpelling = true)]
            internal static extern void GetUniformiv(uint program, int location, [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetUniformuiv", ExactSpelling = true)]
            internal static extern void GetUniformuiv(uint program, int location, [OutAttribute] uint[] @params);

            [DllImport(Library, EntryPoint = "glGetUniformdv", ExactSpelling = true)]
            internal static extern void GetUniformdv(uint program, int location, [OutAttribute] double[] @params);

            [DllImport(Library, EntryPoint = "glGetnUniformfv", ExactSpelling = true)]
            internal static extern void GetnUniformfv(uint program, int location, int bufSize,
                [OutAttribute] float[] @params);

            [DllImport(Library, EntryPoint = "glGetnUniformiv", ExactSpelling = true)]
            internal static extern void GetnUniformiv(uint program, int location, int bufSize,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetnUniformuiv", ExactSpelling = true)]
            internal static extern void GetnUniformuiv(uint program, int location, int bufSize,
                [OutAttribute] uint[] @params);

            [DllImport(Library, EntryPoint = "glGetnUniformdv", ExactSpelling = true)]
            internal static extern void GetnUniformdv(uint program, int location, int bufSize,
                [OutAttribute] double[] @params);

            [DllImport(Library, EntryPoint = "glGetUniformBlockIndex", ExactSpelling = true)]
            internal static extern uint GetUniformBlockIndex(uint program, string uniformBlockName);

            [DllImport(Library, EntryPoint = "glGetUniformIndices", ExactSpelling = true)]
            internal static extern void GetUniformIndices(uint program, int uniformCount, string uniformNames,
                [OutAttribute] uint[] uniformIndices);

            [DllImport(Library, EntryPoint = "glGetUniformLocation", ExactSpelling = true)]
            internal static extern int GetUniformLocation(uint program, string name);

            [DllImport(Library, EntryPoint = "glGetUniformSubroutineuiv", ExactSpelling = true)]
            internal static extern void GetUniformSubroutineuiv(ShaderType shadertype, int location,
                [OutAttribute] uint[] values);

            [DllImport(Library, EntryPoint = "glGetVertexArrayIndexed64iv", ExactSpelling = true)]
            internal static extern void GetVertexArrayIndexed64iv(uint vaobj, uint index, VertexAttribParameter pname,
                [OutAttribute] long[] param);

            [DllImport(Library, EntryPoint = "glGetVertexArrayIndexediv", ExactSpelling = true)]
            internal static extern void GetVertexArrayIndexediv(uint vaobj, uint index, VertexAttribParameter pname,
                [OutAttribute] int[] param);

            [DllImport(Library, EntryPoint = "glGetVertexArrayiv", ExactSpelling = true)]
            internal static extern void GetVertexArrayiv(uint vaobj, VertexAttribParameter pname,
                [OutAttribute] int[] param);

            [DllImport(Library, EntryPoint = "glGetVertexAttribdv", ExactSpelling = true)]
            internal static extern void GetVertexAttribdv(uint index, VertexAttribParameter pname,
                [OutAttribute] double[] @params);

            [DllImport(Library, EntryPoint = "glGetVertexAttribfv", ExactSpelling = true)]
            internal static extern void GetVertexAttribfv(uint index, VertexAttribParameter pname,
                [OutAttribute] float[] @params);

            [DllImport(Library, EntryPoint = "glGetVertexAttribiv", ExactSpelling = true)]
            internal static extern void GetVertexAttribiv(uint index, VertexAttribParameter pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetVertexAttribIiv", ExactSpelling = true)]
            internal static extern void GetVertexAttribIiv(uint index, VertexAttribParameter pname,
                [OutAttribute] int[] @params);

            [DllImport(Library, EntryPoint = "glGetVertexAttribIuiv", ExactSpelling = true)]
            internal static extern void GetVertexAttribIuiv(uint index, VertexAttribParameter pname,
                [OutAttribute] uint[] @params);

            [DllImport(Library, EntryPoint = "glGetVertexAttribLdv", ExactSpelling = true)]
            internal static extern void GetVertexAttribLdv(uint index, VertexAttribParameter pname,
                [OutAttribute] double[] @params);

            [DllImport(Library, EntryPoint = "glGetVertexAttribPointerv", ExactSpelling = true)]
            internal static extern void GetVertexAttribPointerv(uint index, VertexAttribPointerParameter pname,
                [OutAttribute] IntPtr pointer);

            [DllImport(Library, EntryPoint = "glHint", ExactSpelling = true)]
            internal static extern void Hint(HintTarget target, HintMode mode);

            [DllImport(Library, EntryPoint = "glInvalidateBufferData", ExactSpelling = true)]
            internal static extern void InvalidateBufferData(uint buffer);

            [DllImport(Library, EntryPoint = "glInvalidateBufferSubData", ExactSpelling = true)]
            internal static extern void InvalidateBufferSubData(uint buffer, IntPtr offset, IntPtr length);

            [DllImport(Library, EntryPoint = "glInvalidateFramebuffer", ExactSpelling = true)]
            internal static extern void InvalidateFramebuffer(FramebufferTarget target, int numAttachments,
                FramebufferAttachment[] attachments);

            [DllImport(Library, EntryPoint = "glInvalidateNamedFramebufferData", ExactSpelling = true)]
            internal static extern void InvalidateNamedFramebufferData(uint framebuffer, int numAttachments,
                FramebufferAttachment[] attachments);

            [DllImport(Library, EntryPoint = "glInvalidateSubFramebuffer", ExactSpelling = true)]
            internal static extern void InvalidateSubFramebuffer(FramebufferTarget target, int numAttachments,
                FramebufferAttachment[] attachments, int x, int y, int width, int height);

            [DllImport(Library, EntryPoint = "glInvalidateNamedFramebufferSubData", ExactSpelling = true)]
            internal static extern void InvalidateNamedFramebufferSubData(uint framebuffer, int numAttachments,
                FramebufferAttachment[] attachments, int x, int y, int width, int height);

            [DllImport(Library, EntryPoint = "glInvalidateTexImage", ExactSpelling = true)]
            internal static extern void InvalidateTexImage(uint texture, int level);

            [DllImport(Library, EntryPoint = "glInvalidateTexSubImage", ExactSpelling = true)]
            internal static extern void InvalidateTexSubImage(uint texture, int level, int xoffset, int yoffset,
                int zoffset, int width, int height, int depth);

            [DllImport(Library, EntryPoint = "glIsBuffer", ExactSpelling = true)]
            internal static extern bool IsBuffer(uint buffer);

            [DllImport(Library, EntryPoint = "glIsEnabled", ExactSpelling = true)]
            internal static extern bool IsEnabled(EnableCap cap);

            [DllImport(Library, EntryPoint = "glIsEnabledi", ExactSpelling = true)]
            internal static extern bool IsEnabledi(EnableCap cap, uint index);

            [DllImport(Library, EntryPoint = "glIsFramebuffer", ExactSpelling = true)]
            internal static extern bool IsFramebuffer(uint framebuffer);

            [DllImport(Library, EntryPoint = "glIsProgram", ExactSpelling = true)]
            internal static extern bool IsProgram(uint program);

            [DllImport(Library, EntryPoint = "glIsProgramPipeline", ExactSpelling = true)]
            internal static extern bool IsProgramPipeline(uint pipeline);

            [DllImport(Library, EntryPoint = "glIsQuery", ExactSpelling = true)]
            internal static extern bool IsQuery(uint id);

            [DllImport(Library, EntryPoint = "glIsRenderbuffer", ExactSpelling = true)]
            internal static extern bool IsRenderbuffer(uint renderbuffer);

            [DllImport(Library, EntryPoint = "glIsSampler", ExactSpelling = true)]
            internal static extern bool IsSampler(uint id);

            [DllImport(Library, EntryPoint = "glIsShader", ExactSpelling = true)]
            internal static extern bool IsShader(uint shader);

            [DllImport(Library, EntryPoint = "glIsSync", ExactSpelling = true)]
            internal static extern bool IsSync(IntPtr sync);

            [DllImport(Library, EntryPoint = "glIsTexture", ExactSpelling = true)]
            internal static extern bool IsTexture(uint texture);

            [DllImport(Library, EntryPoint = "glIsTransformFeedback", ExactSpelling = true)]
            internal static extern bool IsTransformFeedback(uint id);

            [DllImport(Library, EntryPoint = "glIsVertexArray", ExactSpelling = true)]
            internal static extern bool IsVertexArray(uint array);

            [DllImport(Library, EntryPoint = "glLineWidth", ExactSpelling = true)]
            internal static extern void LineWidth(float width);

            [DllImport(Library, EntryPoint = "glLinkProgram", ExactSpelling = true)]
            internal static extern void LinkProgram(uint program);

            [DllImport(Library, EntryPoint = "glLogicOp", ExactSpelling = true)]
            internal static extern void LogicOp(LogicOp opcode);

            [DllImport(Library, EntryPoint = "glMapBuffer", ExactSpelling = true)]
            internal static extern IntPtr MapBuffer(BufferTarget target, BufferAccess access);

            [DllImport(Library, EntryPoint = "glMapNamedBuffer", ExactSpelling = true)]
            internal static extern IntPtr MapNamedBuffer(uint buffer, BufferAccess access);

            [DllImport(Library, EntryPoint = "glMapBufferRange", ExactSpelling = true)]
            internal static extern IntPtr MapBufferRange(BufferTarget target, IntPtr offset, IntPtr length,
                BufferAccessMask access);

            [DllImport(Library, EntryPoint = "glMapNamedBufferRange", ExactSpelling = true)]
            internal static extern IntPtr MapNamedBufferRange(uint buffer, IntPtr offset, int length, uint access);

            [DllImport(Library, EntryPoint = "glMemoryBarrier", ExactSpelling = true)]
            internal static extern void MemoryBarrier(uint barriers);

            [DllImport(Library, EntryPoint = "glMemoryBarrierByRegion", ExactSpelling = true)]
            internal static extern void MemoryBarrierByRegion(uint barriers);

            [DllImport(Library, EntryPoint = "glMinSampleShading", ExactSpelling = true)]
            internal static extern void MinSampleShading(float value);

            [DllImport(Library, EntryPoint = "glMultiDrawArrays", ExactSpelling = true)]
            internal static extern void MultiDrawArrays(BeginMode mode, int[] first, int[] count, int drawcount);

            [DllImport(Library, EntryPoint = "glMultiDrawArraysIndirect", ExactSpelling = true)]
            internal static extern void MultiDrawArraysIndirect(BeginMode mode, IntPtr indirect, int drawcount,
                int stride);

            [DllImport(Library, EntryPoint = "glMultiDrawElements", ExactSpelling = true)]
            internal static extern void MultiDrawElements(BeginMode mode, int[] count, DrawElementsType type,
                IntPtr indices, int drawcount);

            [DllImport(Library, EntryPoint = "glMultiDrawElementsBaseVertex", ExactSpelling = true)]
            internal static extern void MultiDrawElementsBaseVertex(BeginMode mode, int[] count, DrawElementsType type,
                IntPtr indices, int drawcount, int[] basevertex);

            [DllImport(Library, EntryPoint = "glMultiDrawElementsIndirect", ExactSpelling = true)]
            internal static extern void MultiDrawElementsIndirect(BeginMode mode, DrawElementsType type,
                IntPtr indirect, int drawcount, int stride);

            [DllImport(Library, EntryPoint = "glObjectLabel", ExactSpelling = true)]
            internal static extern void ObjectLabel(ObjectLabel identifier, uint name, int length, string label);

            [DllImport(Library, EntryPoint = "glObjectPtrLabel", ExactSpelling = true)]
            internal static extern void ObjectPtrLabel(IntPtr ptr, int length, string label);

            [DllImport(Library, EntryPoint = "glPatchParameteri", ExactSpelling = true)]
            internal static extern void PatchParameteri(int pname, int value);

            [DllImport(Library, EntryPoint = "glPatchParameterfv", ExactSpelling = true)]
            internal static extern void PatchParameterfv(int pname, float[] values);

            [DllImport(Library, EntryPoint = "glPixelStoref", ExactSpelling = true)]
            internal static extern void PixelStoref(PixelStoreParameter pname, float param);

            [DllImport(Library, EntryPoint = "glPixelStorei", ExactSpelling = true)]
            internal static extern void PixelStorei(PixelStoreParameter pname, int param);

            [DllImport(Library, EntryPoint = "glPointParameterf", ExactSpelling = true)]
            internal static extern void PointParameterf(PointParameterName pname, float param);

            [DllImport(Library, EntryPoint = "glPointParameteri", ExactSpelling = true)]
            internal static extern void PointParameteri(PointParameterName pname, int param);

            [DllImport(Library, EntryPoint = "glPointParameterfv", ExactSpelling = true)]
            internal static extern void PointParameterfv(PointParameterName pname, float[] @params);

            [DllImport(Library, EntryPoint = "glPointParameteriv", ExactSpelling = true)]
            internal static extern void PointParameteriv(PointParameterName pname, int[] @params);

            [DllImport(Library, EntryPoint = "glPointSize", ExactSpelling = true)]
            internal static extern void PointSize(float size);

            [DllImport(Library, EntryPoint = "glPolygonMode", ExactSpelling = true)]
            internal static extern void PolygonMode(MaterialFace face, PolygonMode mode);

            [DllImport(Library, EntryPoint = "glPolygonOffset", ExactSpelling = true)]
            internal static extern void PolygonOffset(float factor, float units);

            [DllImport(Library, EntryPoint = "glPrimitiveRestartIndex", ExactSpelling = true)]
            internal static extern void PrimitiveRestartIndex(uint index);

            [DllImport(Library, EntryPoint = "glProgramBinary", ExactSpelling = true)]
            internal static extern void ProgramBinary(uint program, int binaryFormat, IntPtr binary, int length);

            [DllImport(Library, EntryPoint = "glProgramParameteri", ExactSpelling = true)]
            internal static extern void ProgramParameteri(uint program, Version32 pname, int value);

            [DllImport(Library, EntryPoint = "glProgramUniform1f", ExactSpelling = true)]
            internal static extern void ProgramUniform1f(uint program, int location, float v0);

            [DllImport(Library, EntryPoint = "glProgramUniform2f", ExactSpelling = true)]
            internal static extern void ProgramUniform2f(uint program, int location, float v0, float v1);

            [DllImport(Library, EntryPoint = "glProgramUniform3f", ExactSpelling = true)]
            internal static extern void ProgramUniform3f(uint program, int location, float v0, float v1, float v2);

            [DllImport(Library, EntryPoint = "glProgramUniform4f", ExactSpelling = true)]
            internal static extern void ProgramUniform4f(uint program, int location, float v0, float v1, float v2,
                float v3);

            [DllImport(Library, EntryPoint = "glProgramUniform1i", ExactSpelling = true)]
            internal static extern void ProgramUniform1i(uint program, int location, int v0);

            [DllImport(Library, EntryPoint = "glProgramUniform2i", ExactSpelling = true)]
            internal static extern void ProgramUniform2i(uint program, int location, int v0, int v1);

            [DllImport(Library, EntryPoint = "glProgramUniform3i", ExactSpelling = true)]
            internal static extern void ProgramUniform3i(uint program, int location, int v0, int v1, int v2);

            [DllImport(Library, EntryPoint = "glProgramUniform4i", ExactSpelling = true)]
            internal static extern void ProgramUniform4i(uint program, int location, int v0, int v1, int v2, int v3);

            [DllImport(Library, EntryPoint = "glProgramUniform1ui", ExactSpelling = true)]
            internal static extern void ProgramUniform1ui(uint program, int location, uint v0);

            [DllImport(Library, EntryPoint = "glProgramUniform2ui", ExactSpelling = true)]
            internal static extern void ProgramUniform2ui(uint program, int location, int v0, uint v1);

            [DllImport(Library, EntryPoint = "glProgramUniform3ui", ExactSpelling = true)]
            internal static extern void ProgramUniform3ui(uint program, int location, int v0, int v1, uint v2);

            [DllImport(Library, EntryPoint = "glProgramUniform4ui", ExactSpelling = true)]
            internal static extern void ProgramUniform4ui(uint program, int location, int v0, int v1, int v2, uint v3);

            [DllImport(Library, EntryPoint = "glProgramUniform1fv", ExactSpelling = true)]
            internal static extern void ProgramUniform1fv(uint program, int location, int count, float[] value);

            [DllImport(Library, EntryPoint = "glProgramUniform2fv", ExactSpelling = true)]
            internal static extern void ProgramUniform2fv(uint program, int location, int count, float[] value);

            [DllImport(Library, EntryPoint = "glProgramUniform3fv", ExactSpelling = true)]
            internal static extern void ProgramUniform3fv(uint program, int location, int count, float[] value);

            [DllImport(Library, EntryPoint = "glProgramUniform4fv", ExactSpelling = true)]
            internal static extern void ProgramUniform4fv(uint program, int location, int count, float[] value);

            [DllImport(Library, EntryPoint = "glProgramUniform1iv", ExactSpelling = true)]
            internal static extern void ProgramUniform1iv(uint program, int location, int count, int[] value);

            [DllImport(Library, EntryPoint = "glProgramUniform2iv", ExactSpelling = true)]
            internal static extern void ProgramUniform2iv(uint program, int location, int count, int[] value);

            [DllImport(Library, EntryPoint = "glProgramUniform3iv", ExactSpelling = true)]
            internal static extern void ProgramUniform3iv(uint program, int location, int count, int[] value);

            [DllImport(Library, EntryPoint = "glProgramUniform4iv", ExactSpelling = true)]
            internal static extern void ProgramUniform4iv(uint program, int location, int count, int[] value);

            [DllImport(Library, EntryPoint = "glProgramUniform1uiv", ExactSpelling = true)]
            internal static extern void ProgramUniform1uiv(uint program, int location, int count, uint[] value);

            [DllImport(Library, EntryPoint = "glProgramUniform2uiv", ExactSpelling = true)]
            internal static extern void ProgramUniform2uiv(uint program, int location, int count, uint[] value);

            [DllImport(Library, EntryPoint = "glProgramUniform3uiv", ExactSpelling = true)]
            internal static extern void ProgramUniform3uiv(uint program, int location, int count, uint[] value);

            [DllImport(Library, EntryPoint = "glProgramUniform4uiv", ExactSpelling = true)]
            internal static extern void ProgramUniform4uiv(uint program, int location, int count, uint[] value);

            [DllImport(Library, EntryPoint = "glProgramUniformMatrix2fv", ExactSpelling = true)]
            internal static extern void ProgramUniformMatrix2fv(uint program, int location, int count, bool transpose,
                float[] value);

            [DllImport(Library, EntryPoint = "glProgramUniformMatrix3fv", ExactSpelling = true)]
            internal static extern void ProgramUniformMatrix3fv(uint program, int location, int count, bool transpose,
                float[] value);

            [DllImport(Library, EntryPoint = "glProgramUniformMatrix4fv", ExactSpelling = true)]
            internal static extern void ProgramUniformMatrix4fv(uint program, int location, int count, bool transpose,
                float[] value);

            [DllImport(Library, EntryPoint = "glProgramUniformMatrix2x3fv", ExactSpelling = true)]
            internal static extern void ProgramUniformMatrix2x3fv(uint program, int location, int count, bool transpose,
                float[] value);

            [DllImport(Library, EntryPoint = "glProgramUniformMatrix3x2fv", ExactSpelling = true)]
            internal static extern void ProgramUniformMatrix3x2fv(uint program, int location, int count, bool transpose,
                float[] value);

            [DllImport(Library, EntryPoint = "glProgramUniformMatrix2x4fv", ExactSpelling = true)]
            internal static extern void ProgramUniformMatrix2x4fv(uint program, int location, int count, bool transpose,
                float[] value);

            [DllImport(Library, EntryPoint = "glProgramUniformMatrix4x2fv", ExactSpelling = true)]
            internal static extern void ProgramUniformMatrix4x2fv(uint program, int location, int count, bool transpose,
                float[] value);

            [DllImport(Library, EntryPoint = "glProgramUniformMatrix3x4fv", ExactSpelling = true)]
            internal static extern void ProgramUniformMatrix3x4fv(uint program, int location, int count, bool transpose,
                float[] value);

            [DllImport(Library, EntryPoint = "glProgramUniformMatrix4x3fv", ExactSpelling = true)]
            internal static extern void ProgramUniformMatrix4x3fv(uint program, int location, int count, bool transpose,
                float[] value);

            [DllImport(Library, EntryPoint = "glProvokingVertex", ExactSpelling = true)]
            internal static extern void ProvokingVertex(ProvokingVertexMode provokeMode);

            [DllImport(Library, EntryPoint = "glQueryCounter", ExactSpelling = true)]
            internal static extern void QueryCounter(uint id, QueryTarget target);

            [DllImport(Library, EntryPoint = "glReadBuffer", ExactSpelling = true)]
            internal static extern void ReadBuffer(ReadBufferMode mode);

            [DllImport(Library, EntryPoint = "glNamedFramebufferReadBuffer", ExactSpelling = true)]
            internal static extern void NamedFramebufferReadBuffer(ReadBufferMode framebuffer, BeginMode mode);

            [DllImport(Library, EntryPoint = "glReadPixels", ExactSpelling = true)]
            internal static extern void ReadPixels(int x, int y, int width, int height, PixelFormat format,
                PixelType type, int[] data);

            [DllImport(Library, EntryPoint = "glReadnPixels", ExactSpelling = true)]
            internal static extern void ReadnPixels(int x, int y, int width, int height, PixelFormat format,
                PixelType type, int bufSize, int[] data);

            [DllImport(Library, EntryPoint = "glRenderbufferStorage", ExactSpelling = true)]
            internal static extern void RenderbufferStorage(RenderbufferTarget target,
                RenderbufferStorage internalFormat, int width, int height);

            [DllImport(Library, EntryPoint = "glNamedRenderbufferStorage", ExactSpelling = true)]
            internal static extern void NamedRenderbufferStorage(uint renderbuffer, RenderbufferStorage internalFormat,
                int width, int height);

            [DllImport(Library, EntryPoint = "glRenderbufferStorageMultisample", ExactSpelling = true)]
            internal static extern void RenderbufferStorageMultisample(RenderbufferTarget target, int samples,
                RenderbufferStorage internalFormat, int width, int height);

            [DllImport(Library, EntryPoint = "glNamedRenderbufferStorageMultisample", ExactSpelling = true)]
            internal static extern void NamedRenderbufferStorageMultisample(uint renderbuffer, int samples,
                RenderbufferStorage internalFormat, int width, int height);

            [DllImport(Library, EntryPoint = "glSampleCoverage", ExactSpelling = true)]
            internal static extern void SampleCoverage(float value, bool invert);

            [DllImport(Library, EntryPoint = "glSampleMaski", ExactSpelling = true)]
            internal static extern void SampleMaski(uint maskNumber, uint mask);

            [DllImport(Library, EntryPoint = "glSamplerParameterf", ExactSpelling = true)]
            internal static extern void SamplerParameterf(uint sampler, TextureParameterName pname, float param);

            [DllImport(Library, EntryPoint = "glSamplerParameteri", ExactSpelling = true)]
            internal static extern void SamplerParameteri(uint sampler, TextureParameterName pname, int param);

            [DllImport(Library, EntryPoint = "glSamplerParameterfv", ExactSpelling = true)]
            internal static extern void SamplerParameterfv(uint sampler, TextureParameterName pname, float[] @params);

            [DllImport(Library, EntryPoint = "glSamplerParameteriv", ExactSpelling = true)]
            internal static extern void SamplerParameteriv(uint sampler, TextureParameterName pname, int[] @params);

            [DllImport(Library, EntryPoint = "glSamplerParameterIiv", ExactSpelling = true)]
            internal static extern void SamplerParameterIiv(uint sampler, TextureParameterName pname, int[] @params);

            [DllImport(Library, EntryPoint = "glSamplerParameterIuiv", ExactSpelling = true)]
            internal static extern void SamplerParameterIuiv(uint sampler, TextureParameterName pname, uint[] @params);

            [DllImport(Library, EntryPoint = "glScissor", ExactSpelling = true)]
            internal static extern void Scissor(int x, int y, int width, int height);

            [DllImport(Library, EntryPoint = "glScissorArrayv", ExactSpelling = true)]
            internal static extern void ScissorArrayv(uint first, int count, int[] v);

            [DllImport(Library, EntryPoint = "glScissorIndexed", ExactSpelling = true)]
            internal static extern void ScissorIndexed(uint index, int left, int bottom, int width, int height);

            [DllImport(Library, EntryPoint = "glScissorIndexedv", ExactSpelling = true)]
            internal static extern void ScissorIndexedv(uint index, int[] v);

            [DllImport(Library, EntryPoint = "glShaderBinary", ExactSpelling = true)]
            internal static extern void ShaderBinary(int count, uint[] shaders, int binaryFormat, IntPtr binary,
                int length);

            [DllImport(Library, EntryPoint = "glShaderSource", ExactSpelling = true)]
            internal static extern void ShaderSource(uint shader, int count, string[] @string, int[] length);

            [DllImport(Library, EntryPoint = "glShaderStorageBlockBinding", ExactSpelling = true)]
            internal static extern void ShaderStorageBlockBinding(uint program, uint storageBlockIndex,
                uint storageBlockBinding);

            [DllImport(Library, EntryPoint = "glStencilFunc", ExactSpelling = true)]
            internal static extern void StencilFunc(StencilFunction func, int @ref, uint mask);

            [DllImport(Library, EntryPoint = "glStencilFuncSeparate", ExactSpelling = true)]
            internal static extern void
                StencilFuncSeparate(StencilFace face, StencilFunction func, int @ref, uint mask);

            [DllImport(Library, EntryPoint = "glStencilMask", ExactSpelling = true)]
            internal static extern void StencilMask(uint mask);

            [DllImport(Library, EntryPoint = "glStencilMaskSeparate", ExactSpelling = true)]
            internal static extern void StencilMaskSeparate(StencilFace face, uint mask);

            [DllImport(Library, EntryPoint = "glStencilOp", ExactSpelling = true)]
            internal static extern void StencilOp(StencilOp sfail, StencilOp dpfail, StencilOp dppass);

            [DllImport(Library, EntryPoint = "glStencilOpSeparate", ExactSpelling = true)]
            internal static extern void StencilOpSeparate(StencilFace face, StencilOp sfail, StencilOp dpfail,
                StencilOp dppass);

            [DllImport(Library, EntryPoint = "glTexBuffer", ExactSpelling = true)]
            internal static extern void TexBuffer(TextureBufferTarget target, SizedInternalFormat internalFormat,
                uint buffer);

            [DllImport(Library, EntryPoint = "glTextureBuffer", ExactSpelling = true)]
            internal static extern void TextureBuffer(uint texture, SizedInternalFormat internalFormat, uint buffer);

            [DllImport(Library, EntryPoint = "glTexBufferRange", ExactSpelling = true)]
            internal static extern void TexBufferRange(BufferTarget target, SizedInternalFormat internalFormat,
                uint buffer, IntPtr offset, IntPtr size);

            [DllImport(Library, EntryPoint = "glTextureBufferRange", ExactSpelling = true)]
            internal static extern void TextureBufferRange(uint texture, SizedInternalFormat internalFormat,
                uint buffer, IntPtr offset, int size);

            [DllImport(Library, EntryPoint = "glTexImage1D", ExactSpelling = true)]
            internal static extern void TexImage1D(TextureTarget target, int level, PixelInternalFormat internalFormat,
                int width, int border, PixelFormat format, PixelType type, IntPtr data);

            [DllImport(Library, EntryPoint = "glTexImage2D", ExactSpelling = true)]
            internal static extern void TexImage2D(TextureTarget target, int level, PixelInternalFormat internalFormat,
                int width, int height, int border, PixelFormat format, PixelType type, IntPtr data);

            [DllImport(Library, EntryPoint = "glTexImage2DMultisample", ExactSpelling = true)]
            internal static extern void TexImage2DMultisample(TextureTargetMultisample target, int samples,
                PixelInternalFormat internalFormat, int width, int height, bool fixedsamplelocations);

            [DllImport(Library, EntryPoint = "glTexImage3D", ExactSpelling = true)]
            internal static extern void TexImage3D(TextureTarget target, int level, PixelInternalFormat internalFormat,
                int width, int height, int depth, int border, PixelFormat format, PixelType type, IntPtr data);

            [DllImport(Library, EntryPoint = "glTexImage3DMultisample", ExactSpelling = true)]
            internal static extern void TexImage3DMultisample(TextureTargetMultisample target, int samples,
                PixelInternalFormat internalFormat, int width, int height, int depth, bool fixedsamplelocations);

            [DllImport(Library, EntryPoint = "glTexParameterf", ExactSpelling = true)]
            internal static extern void TexParameterf(TextureTarget target, TextureParameterName pname, float param);

            [DllImport(Library, EntryPoint = "glTexParameteri", ExactSpelling = true)]
            internal static extern void TexParameteri(TextureTarget target, TextureParameterName pname, int param);

            [DllImport(Library, EntryPoint = "glTextureParameterf", ExactSpelling = true)]
            internal static extern void TextureParameterf(uint texture, TextureParameter pname, float param);

            [DllImport(Library, EntryPoint = "glTextureParameteri", ExactSpelling = true)]
            internal static extern void TextureParameteri(uint texture, TextureParameter pname, int param);

            [DllImport(Library, EntryPoint = "glTexParameterfv", ExactSpelling = true)]
            internal static extern void TexParameterfv(TextureTarget target, TextureParameterName pname,
                float[] @params);

            [DllImport(Library, EntryPoint = "glTexParameteriv", ExactSpelling = true)]
            internal static extern void TexParameteriv(TextureTarget target, TextureParameterName pname, int[] @params);

            [DllImport(Library, EntryPoint = "glTexParameterIiv", ExactSpelling = true)]
            internal static extern void TexParameterIiv(TextureTarget target, TextureParameterName pname,
                int[] @params);

            [DllImport(Library, EntryPoint = "glTexParameterIuiv", ExactSpelling = true)]
            internal static extern void TexParameterIuiv(TextureTarget target, TextureParameterName pname,
                uint[] @params);

            [DllImport(Library, EntryPoint = "glTextureParameterfv", ExactSpelling = true)]
            internal static extern void TextureParameterfv(uint texture, TextureParameter pname, float[] paramtexture);

            [DllImport(Library, EntryPoint = "glTextureParameteriv", ExactSpelling = true)]
            internal static extern void TextureParameteriv(uint texture, TextureParameter pname, int[] param);

            [DllImport(Library, EntryPoint = "glTextureParameterIiv", ExactSpelling = true)]
            internal static extern void TextureParameterIiv(uint texture, TextureParameter pname, int[] @params);

            [DllImport(Library, EntryPoint = "glTextureParameterIuiv", ExactSpelling = true)]
            internal static extern void TextureParameterIuiv(uint texture, TextureParameter pname, uint[] @params);

            [DllImport(Library, EntryPoint = "glTexStorage1D", ExactSpelling = true)]
            internal static extern void TexStorage1D(TextureTarget target, int levels,
                SizedInternalFormat internalFormat, int width);

            [DllImport(Library, EntryPoint = "glTextureStorage1D", ExactSpelling = true)]
            internal static extern void TextureStorage1D(uint texture, int levels, SizedInternalFormat internalFormat,
                int width);

            [DllImport(Library, EntryPoint = "glTexStorage2D", ExactSpelling = true)]
            internal static extern void TexStorage2D(TextureTarget target, int levels,
                SizedInternalFormat internalFormat, int width, int height);

            [DllImport(Library, EntryPoint = "glTextureStorage2D", ExactSpelling = true)]
            internal static extern void TextureStorage2D(uint texture, int levels, SizedInternalFormat internalFormat,
                int width, int height);

            [DllImport(Library, EntryPoint = "glTexStorage2DMultisample", ExactSpelling = true)]
            internal static extern void TexStorage2DMultisample(TextureTarget target, int samples,
                SizedInternalFormat internalFormat, int width, int height, bool fixedsamplelocations);

            [DllImport(Library, EntryPoint = "glTextureStorage2DMultisample", ExactSpelling = true)]
            internal static extern void TextureStorage2DMultisample(uint texture, int samples,
                SizedInternalFormat internalFormat, int width, int height, bool fixedsamplelocations);

            [DllImport(Library, EntryPoint = "glTexStorage3D", ExactSpelling = true)]
            internal static extern void TexStorage3D(TextureTarget target, int levels,
                SizedInternalFormat internalFormat, int width, int height, int depth);

            [DllImport(Library, EntryPoint = "glTextureStorage3D", ExactSpelling = true)]
            internal static extern void TextureStorage3D(uint texture, int levels, SizedInternalFormat internalFormat,
                int width, int height, int depth);

            [DllImport(Library, EntryPoint = "glTexStorage3DMultisample", ExactSpelling = true)]
            internal static extern void TexStorage3DMultisample(TextureTarget target, int samples,
                SizedInternalFormat internalFormat, int width, int height, int depth, bool fixedsamplelocations);

            [DllImport(Library, EntryPoint = "glTextureStorage3DMultisample", ExactSpelling = true)]
            internal static extern void TextureStorage3DMultisample(uint texture, int samples,
                SizedInternalFormat internalFormat, int width, int height, int depth, bool fixedsamplelocations);

            [DllImport(Library, EntryPoint = "glTexSubImage1D", ExactSpelling = true)]
            internal static extern void TexSubImage1D(TextureTarget target, int level, int xoffset, int width,
                PixelFormat format, PixelType type, IntPtr pixels);

            [DllImport(Library, EntryPoint = "glTextureSubImage1D", ExactSpelling = true)]
            internal static extern void TextureSubImage1D(uint texture, int level, int xoffset, int width,
                PixelFormat format, PixelType type, IntPtr pixels);

            [DllImport(Library, EntryPoint = "glTexSubImage2D", ExactSpelling = true)]
            internal static extern void TexSubImage2D(TextureTarget target, int level, int xoffset, int yoffset,
                int width, int height, PixelFormat format, PixelType type, IntPtr pixels);

            [DllImport(Library, EntryPoint = "glTextureSubImage2D", ExactSpelling = true)]
            internal static extern void TextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int width,
                int height, PixelFormat format, PixelType type, IntPtr pixels);

            [DllImport(Library, EntryPoint = "glTexSubImage3D", ExactSpelling = true)]
            internal static extern void TexSubImage3D(TextureTarget target, int level, int xoffset, int yoffset,
                int zoffset, int width, int height, int depth, PixelFormat format, PixelType type, IntPtr pixels);

            [DllImport(Library, EntryPoint = "glTextureSubImage3D", ExactSpelling = true)]
            internal static extern void TextureSubImage3D(uint texture, int level, int xoffset, int yoffset,
                int zoffset, int width, int height, int depth, PixelFormat format, PixelType type, IntPtr pixels);

            [DllImport(Library, EntryPoint = "glTextureBarrier", ExactSpelling = true)]
            internal static extern void TextureBarrier();

            [DllImport(Library, EntryPoint = "glTextureView", ExactSpelling = true)]
            internal static extern void TextureView(uint texture, TextureTarget target, uint origtexture,
                PixelInternalFormat internalFormat, uint minlevel, uint numlevels, uint minlayer, uint numlayers);

            [DllImport(Library, EntryPoint = "glTransformFeedbackBufferBase", ExactSpelling = true)]
            internal static extern void TransformFeedbackBufferBase(uint xfb, uint index, uint buffer);

            [DllImport(Library, EntryPoint = "glTransformFeedbackBufferRange", ExactSpelling = true)]
            internal static extern void TransformFeedbackBufferRange(uint xfb, uint index, uint buffer, IntPtr offset,
                int size);

            [DllImport(Library, EntryPoint = "glTransformFeedbackVaryings", ExactSpelling = true)]
            internal static extern void TransformFeedbackVaryings(uint program, int count, string[] varyings,
                TransformFeedbackMode bufferMode);

            [DllImport(Library, EntryPoint = "glUniform1f", ExactSpelling = true)]
            internal static extern void Uniform1f(int location, float v0);

            [DllImport(Library, EntryPoint = "glUniform2f", ExactSpelling = true)]
            internal static extern void Uniform2f(int location, float v0, float v1);

            [DllImport(Library, EntryPoint = "glUniform3f", ExactSpelling = true)]
            internal static extern void Uniform3f(int location, float v0, float v1, float v2);

            [DllImport(Library, EntryPoint = "glUniform4f", ExactSpelling = true)]
            internal static extern void Uniform4f(int location, float v0, float v1, float v2, float v3);

            [DllImport(Library, EntryPoint = "glUniform1i", ExactSpelling = true)]
            internal static extern void Uniform1i(int location, int v0);

            [DllImport(Library, EntryPoint = "glUniform2i", ExactSpelling = true)]
            internal static extern void Uniform2i(int location, int v0, int v1);

            [DllImport(Library, EntryPoint = "glUniform3i", ExactSpelling = true)]
            internal static extern void Uniform3i(int location, int v0, int v1, int v2);

            [DllImport(Library, EntryPoint = "glUniform4i", ExactSpelling = true)]
            internal static extern void Uniform4i(int location, int v0, int v1, int v2, int v3);

            [DllImport(Library, EntryPoint = "glUniform1ui", ExactSpelling = true)]
            internal static extern void Uniform1ui(int location, uint v0);

            [DllImport(Library, EntryPoint = "glUniform2ui", ExactSpelling = true)]
            internal static extern void Uniform2ui(int location, uint v0, uint v1);

            [DllImport(Library, EntryPoint = "glUniform3ui", ExactSpelling = true)]
            internal static extern void Uniform3ui(int location, uint v0, uint v1, uint v2);

            [DllImport(Library, EntryPoint = "glUniform4ui", ExactSpelling = true)]
            internal static extern void Uniform4ui(int location, uint v0, uint v1, uint v2, uint v3);

            [DllImport(Library, EntryPoint = "glUniform1fv", ExactSpelling = true)]
            internal static extern void Uniform1fv(int location, int count, float[] value);

            [DllImport(Library, EntryPoint = "glUniform2fv", ExactSpelling = true)]
            internal static extern void Uniform2fv(int location, int count, float[] value);

            [DllImport(Library, EntryPoint = "glUniform3fv", ExactSpelling = true)]
            internal static extern void Uniform3fv(int location, int count, float[] value);

            [DllImport(Library, EntryPoint = "glUniform4fv", ExactSpelling = true)]
            internal static extern void Uniform4fv(int location, int count, float[] value);

            [DllImport(Library, EntryPoint = "glUniform1iv", ExactSpelling = true)]
            internal static extern void Uniform1iv(int location, int count, int[] value);

            [DllImport(Library, EntryPoint = "glUniform2iv", ExactSpelling = true)]
            internal static extern void Uniform2iv(int location, int count, int[] value);

            [DllImport(Library, EntryPoint = "glUniform3iv", ExactSpelling = true)]
            internal static extern void Uniform3iv(int location, int count, int[] value);

            [DllImport(Library, EntryPoint = "glUniform4iv", ExactSpelling = true)]
            internal static extern void Uniform4iv(int location, int count, int[] value);

            [DllImport(Library, EntryPoint = "glUniform1uiv", ExactSpelling = true)]
            internal static extern void Uniform1uiv(int location, int count, uint[] value);

            [DllImport(Library, EntryPoint = "glUniform2uiv", ExactSpelling = true)]
            internal static extern void Uniform2uiv(int location, int count, uint[] value);

            [DllImport(Library, EntryPoint = "glUniform3uiv", ExactSpelling = true)]
            internal static extern void Uniform3uiv(int location, int count, uint[] value);

            [DllImport(Library, EntryPoint = "glUniform4uiv", ExactSpelling = true)]
            internal static extern void Uniform4uiv(int location, int count, uint[] value);

            [DllImport(Library, EntryPoint = "glUniformMatrix2fv", ExactSpelling = true)]
            internal static extern void UniformMatrix2fv(int location, int count, bool transpose, float[] value);

            [DllImport(Library, EntryPoint = "glUniformMatrix3fv", ExactSpelling = true)]
            internal static extern void UniformMatrix3fv(int location, int count, bool transpose, float[] value);

            [DllImport(Library, EntryPoint = "glUniformMatrix4fv", ExactSpelling = true)]
            internal static extern void UniformMatrix4fv(int location, int count, bool transpose, float[] value);

            [DllImport(Library, EntryPoint = "glUniformMatrix2x3fv", ExactSpelling = true)]
            internal static extern void UniformMatrix2x3fv(int location, int count, bool transpose, float[] value);

            [DllImport(Library, EntryPoint = "glUniformMatrix3x2fv", ExactSpelling = true)]
            internal static extern void UniformMatrix3x2fv(int location, int count, bool transpose, float[] value);

            [DllImport(Library, EntryPoint = "glUniformMatrix2x4fv", ExactSpelling = true)]
            internal static extern void UniformMatrix2x4fv(int location, int count, bool transpose, float[] value);

            [DllImport(Library, EntryPoint = "glUniformMatrix4x2fv", ExactSpelling = true)]
            internal static extern void UniformMatrix4x2fv(int location, int count, bool transpose, float[] value);

            [DllImport(Library, EntryPoint = "glUniformMatrix3x4fv", ExactSpelling = true)]
            internal static extern void UniformMatrix3x4fv(int location, int count, bool transpose, float[] value);

            [DllImport(Library, EntryPoint = "glUniformMatrix4x3fv", ExactSpelling = true)]
            internal static extern void UniformMatrix4x3fv(int location, int count, bool transpose, float[] value);

            [DllImport(Library, EntryPoint = "glUniformBlockBinding", ExactSpelling = true)]
            internal static extern void UniformBlockBinding(uint program, uint uniformBlockIndex,
                uint uniformBlockBinding);

            [DllImport(Library, EntryPoint = "glUniformSubroutinesuiv", ExactSpelling = true)]
            internal static extern void UniformSubroutinesuiv(ShaderType shadertype, int count, uint[] indices);

            [DllImport(Library, EntryPoint = "glUnmapBuffer", ExactSpelling = true)]
            internal static extern bool UnmapBuffer(BufferTarget target);

            [DllImport(Library, EntryPoint = "glUnmapNamedBuffer", ExactSpelling = true)]
            internal static extern bool UnmapNamedBuffer(uint buffer);

            [DllImport(Library, EntryPoint = "glUseProgram", ExactSpelling = true)]
            internal static extern void UseProgram(uint program);

            [DllImport(Library, EntryPoint = "glUseProgramStages", ExactSpelling = true)]
            internal static extern void UseProgramStages(uint pipeline, uint stages, uint program);

            [DllImport(Library, EntryPoint = "glValidateProgram", ExactSpelling = true)]
            internal static extern void ValidateProgram(uint program);

            [DllImport(Library, EntryPoint = "glValidateProgramPipeline", ExactSpelling = true)]
            internal static extern void ValidateProgramPipeline(uint pipeline);

            [DllImport(Library, EntryPoint = "glVertexArrayElementBuffer", ExactSpelling = true)]
            internal static extern void VertexArrayElementBuffer(uint vaobj, uint buffer);

            [DllImport(Library, EntryPoint = "glVertexAttrib1f", ExactSpelling = true)]
            internal static extern void VertexAttrib1f(uint index, float v0);

            [DllImport(Library, EntryPoint = "glVertexAttrib1s", ExactSpelling = true)]
            internal static extern void VertexAttrib1s(uint index, short v0);

            [DllImport(Library, EntryPoint = "glVertexAttrib1d", ExactSpelling = true)]
            internal static extern void VertexAttrib1d(uint index, double v0);

            [DllImport(Library, EntryPoint = "glVertexAttribI1i", ExactSpelling = true)]
            internal static extern void VertexAttribI1i(uint index, int v0);

            [DllImport(Library, EntryPoint = "glVertexAttribI1ui", ExactSpelling = true)]
            internal static extern void VertexAttribI1ui(uint index, uint v0);

            [DllImport(Library, EntryPoint = "glVertexAttrib2f", ExactSpelling = true)]
            internal static extern void VertexAttrib2f(uint index, float v0, float v1);

            [DllImport(Library, EntryPoint = "glVertexAttrib2s", ExactSpelling = true)]
            internal static extern void VertexAttrib2s(uint index, short v0, short v1);

            [DllImport(Library, EntryPoint = "glVertexAttrib2d", ExactSpelling = true)]
            internal static extern void VertexAttrib2d(uint index, double v0, double v1);

            [DllImport(Library, EntryPoint = "glVertexAttribI2i", ExactSpelling = true)]
            internal static extern void VertexAttribI2i(uint index, int v0, int v1);

            [DllImport(Library, EntryPoint = "glVertexAttribI2ui", ExactSpelling = true)]
            internal static extern void VertexAttribI2ui(uint index, uint v0, uint v1);

            [DllImport(Library, EntryPoint = "glVertexAttrib3f", ExactSpelling = true)]
            internal static extern void VertexAttrib3f(uint index, float v0, float v1, float v2);

            [DllImport(Library, EntryPoint = "glVertexAttrib3s", ExactSpelling = true)]
            internal static extern void VertexAttrib3s(uint index, short v0, short v1, short v2);

            [DllImport(Library, EntryPoint = "glVertexAttrib3d", ExactSpelling = true)]
            internal static extern void VertexAttrib3d(uint index, double v0, double v1, double v2);

            [DllImport(Library, EntryPoint = "glVertexAttribI3i", ExactSpelling = true)]
            internal static extern void VertexAttribI3i(uint index, int v0, int v1, int v2);

            [DllImport(Library, EntryPoint = "glVertexAttribI3ui", ExactSpelling = true)]
            internal static extern void VertexAttribI3ui(uint index, uint v0, uint v1, uint v2);

            [DllImport(Library, EntryPoint = "glVertexAttrib4f", ExactSpelling = true)]
            internal static extern void VertexAttrib4f(uint index, float v0, float v1, float v2, float v3);

            [DllImport(Library, EntryPoint = "glVertexAttrib4s", ExactSpelling = true)]
            internal static extern void VertexAttrib4s(uint index, short v0, short v1, short v2, short v3);

            [DllImport(Library, EntryPoint = "glVertexAttrib4d", ExactSpelling = true)]
            internal static extern void VertexAttrib4d(uint index, double v0, double v1, double v2, double v3);

            [DllImport(Library, EntryPoint = "glVertexAttrib4Nub", ExactSpelling = true)]
            internal static extern void VertexAttrib4Nub(uint index, byte v0, byte v1, byte v2, byte v3);

            [DllImport(Library, EntryPoint = "glVertexAttribI4i", ExactSpelling = true)]
            internal static extern void VertexAttribI4i(uint index, int v0, int v1, int v2, int v3);

            [DllImport(Library, EntryPoint = "glVertexAttribI4ui", ExactSpelling = true)]
            internal static extern void VertexAttribI4ui(uint index, uint v0, uint v1, uint v2, uint v3);

            [DllImport(Library, EntryPoint = "glVertexAttribL1d", ExactSpelling = true)]
            internal static extern void VertexAttribL1d(uint index, double v0);

            [DllImport(Library, EntryPoint = "glVertexAttribL2d", ExactSpelling = true)]
            internal static extern void VertexAttribL2d(uint index, double v0, double v1);

            [DllImport(Library, EntryPoint = "glVertexAttribL3d", ExactSpelling = true)]
            internal static extern void VertexAttribL3d(uint index, double v0, double v1, double v2);

            [DllImport(Library, EntryPoint = "glVertexAttribL4d", ExactSpelling = true)]
            internal static extern void VertexAttribL4d(uint index, double v0, double v1, double v2, double v3);

            [DllImport(Library, EntryPoint = "glVertexAttrib1fv", ExactSpelling = true)]
            internal static extern void VertexAttrib1fv(uint index, float[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib1sv", ExactSpelling = true)]
            internal static extern void VertexAttrib1sv(uint index, short[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib1dv", ExactSpelling = true)]
            internal static extern void VertexAttrib1dv(uint index, double[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribI1iv", ExactSpelling = true)]
            internal static extern void VertexAttribI1iv(uint index, int[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribI1uiv", ExactSpelling = true)]
            internal static extern void VertexAttribI1uiv(uint index, uint[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib2fv", ExactSpelling = true)]
            internal static extern void VertexAttrib2fv(uint index, float[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib2sv", ExactSpelling = true)]
            internal static extern void VertexAttrib2sv(uint index, short[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib2dv", ExactSpelling = true)]
            internal static extern void VertexAttrib2dv(uint index, double[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribI2iv", ExactSpelling = true)]
            internal static extern void VertexAttribI2iv(uint index, int[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribI2uiv", ExactSpelling = true)]
            internal static extern void VertexAttribI2uiv(uint index, uint[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib3fv", ExactSpelling = true)]
            internal static extern void VertexAttrib3fv(uint index, float[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib3sv", ExactSpelling = true)]
            internal static extern void VertexAttrib3sv(uint index, short[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib3dv", ExactSpelling = true)]
            internal static extern void VertexAttrib3dv(uint index, double[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribI3iv", ExactSpelling = true)]
            internal static extern void VertexAttribI3iv(uint index, int[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribI3uiv", ExactSpelling = true)]
            internal static extern void VertexAttribI3uiv(uint index, uint[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4fv", ExactSpelling = true)]
            internal static extern void VertexAttrib4fv(uint index, float[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4sv", ExactSpelling = true)]
            internal static extern void VertexAttrib4sv(uint index, short[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4dv", ExactSpelling = true)]
            internal static extern void VertexAttrib4dv(uint index, double[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4iv", ExactSpelling = true)]
            internal static extern void VertexAttrib4iv(uint index, int[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4bv", ExactSpelling = true)]
            internal static extern void VertexAttrib4bv(uint index, sbyte[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4ubv", ExactSpelling = true)]
            internal static extern void VertexAttrib4ubv(uint index, byte[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4usv", ExactSpelling = true)]
            internal static extern void VertexAttrib4usv(uint index, ushort[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4uiv", ExactSpelling = true)]
            internal static extern void VertexAttrib4uiv(uint index, uint[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4Nbv", ExactSpelling = true)]
            internal static extern void VertexAttrib4Nbv(uint index, sbyte[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4Nsv", ExactSpelling = true)]
            internal static extern void VertexAttrib4Nsv(uint index, short[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4Niv", ExactSpelling = true)]
            internal static extern void VertexAttrib4Niv(uint index, int[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4Nubv", ExactSpelling = true)]
            internal static extern void VertexAttrib4Nubv(uint index, byte[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4Nusv", ExactSpelling = true)]
            internal static extern void VertexAttrib4Nusv(uint index, ushort[] v);

            [DllImport(Library, EntryPoint = "glVertexAttrib4Nuiv", ExactSpelling = true)]
            internal static extern void VertexAttrib4Nuiv(uint index, uint[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribI4bv", ExactSpelling = true)]
            internal static extern void VertexAttribI4bv(uint index, sbyte[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribI4ubv", ExactSpelling = true)]
            internal static extern void VertexAttribI4ubv(uint index, byte[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribI4sv", ExactSpelling = true)]
            internal static extern void VertexAttribI4sv(uint index, short[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribI4usv", ExactSpelling = true)]
            internal static extern void VertexAttribI4usv(uint index, ushort[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribI4iv", ExactSpelling = true)]
            internal static extern void VertexAttribI4iv(uint index, int[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribI4uiv", ExactSpelling = true)]
            internal static extern void VertexAttribI4uiv(uint index, uint[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribL1dv", ExactSpelling = true)]
            internal static extern void VertexAttribL1dv(uint index, double[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribL2dv", ExactSpelling = true)]
            internal static extern void VertexAttribL2dv(uint index, double[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribL3dv", ExactSpelling = true)]
            internal static extern void VertexAttribL3dv(uint index, double[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribL4dv", ExactSpelling = true)]
            internal static extern void VertexAttribL4dv(uint index, double[] v);

            [DllImport(Library, EntryPoint = "glVertexAttribP1ui", ExactSpelling = true)]
            internal static extern void VertexAttribP1ui(uint index, VertexAttribPType type, bool normalized,
                uint value);

            [DllImport(Library, EntryPoint = "glVertexAttribP2ui", ExactSpelling = true)]
            internal static extern void VertexAttribP2ui(uint index, VertexAttribPType type, bool normalized,
                uint value);

            [DllImport(Library, EntryPoint = "glVertexAttribP3ui", ExactSpelling = true)]
            internal static extern void VertexAttribP3ui(uint index, VertexAttribPType type, bool normalized,
                uint value);

            [DllImport(Library, EntryPoint = "glVertexAttribP4ui", ExactSpelling = true)]
            internal static extern void VertexAttribP4ui(uint index, VertexAttribPType type, bool normalized,
                uint value);

            [DllImport(Library, EntryPoint = "glVertexAttribBinding", ExactSpelling = true)]
            internal static extern void VertexAttribBinding(uint attribindex, uint bindingindex);

            [DllImport(Library, EntryPoint = "glVertexArrayAttribBinding", ExactSpelling = true)]
            internal static extern void VertexArrayAttribBinding(uint vaobj, uint attribindex, uint bindingindex);

            [DllImport(Library, EntryPoint = "glVertexAttribDivisor", ExactSpelling = true)]
            internal static extern void VertexAttribDivisor(uint index, uint divisor);

            [DllImport(Library, EntryPoint = "glVertexAttribFormat", ExactSpelling = true)]
            internal static extern void VertexAttribFormat(uint attribindex, int size, VertexAttribFormat type,
                bool normalized, uint relativeoffset);

            [DllImport(Library, EntryPoint = "glVertexAttribIFormat", ExactSpelling = true)]
            internal static extern void VertexAttribIFormat(uint attribindex, int size, VertexAttribFormat type,
                uint relativeoffset);

            [DllImport(Library, EntryPoint = "glVertexAttribLFormat", ExactSpelling = true)]
            internal static extern void VertexAttribLFormat(uint attribindex, int size, VertexAttribFormat type,
                uint relativeoffset);

            [DllImport(Library, EntryPoint = "glVertexArrayAttribFormat", ExactSpelling = true)]
            internal static extern void VertexArrayAttribFormat(uint vaobj, uint attribindex, int size,
                VertexAttribFormat type, bool normalized, uint relativeoffset);

            [DllImport(Library, EntryPoint = "glVertexArrayAttribIFormat", ExactSpelling = true)]
            internal static extern void VertexArrayAttribIFormat(uint vaobj, uint attribindex, int size,
                VertexAttribFormat type, uint relativeoffset);

            [DllImport(Library, EntryPoint = "glVertexArrayAttribLFormat", ExactSpelling = true)]
            internal static extern void VertexArrayAttribLFormat(uint vaobj, uint attribindex, int size,
                VertexAttribFormat type, uint relativeoffset);

            [DllImport(Library, EntryPoint = "glVertexAttribPointer", ExactSpelling = true)]
            internal static extern void VertexAttribPointer(uint index, int size, VertexAttribPointerType type,
                bool normalized, int stride, IntPtr pointer);

            [DllImport(Library, EntryPoint = "glVertexAttribIPointer", ExactSpelling = true)]
            internal static extern void VertexAttribIPointer(uint index, int size, VertexAttribPointerType type,
                int stride, IntPtr pointer);

            [DllImport(Library, EntryPoint = "glVertexAttribLPointer", ExactSpelling = true)]
            internal static extern void VertexAttribLPointer(uint index, int size, VertexAttribPointerType type,
                int stride, IntPtr pointer);

            [DllImport(Library, EntryPoint = "glVertexBindingDivisor", ExactSpelling = true)]
            internal static extern void VertexBindingDivisor(uint bindingindex, uint divisor);

            [DllImport(Library, EntryPoint = "glVertexArrayBindingDivisor", ExactSpelling = true)]
            internal static extern void VertexArrayBindingDivisor(uint vaobj, uint bindingindex, uint divisor);

            [DllImport(Library, EntryPoint = "glViewport", ExactSpelling = true)]
            internal static extern void Viewport(int x, int y, int width, int height);

            [DllImport(Library, EntryPoint = "glViewportArrayv", ExactSpelling = true)]
            internal static extern void ViewportArrayv(uint first, int count, float[] v);

            [DllImport(Library, EntryPoint = "glViewportIndexedf", ExactSpelling = true)]
            internal static extern void ViewportIndexedf(uint index, float x, float y, float w, float h);

            [DllImport(Library, EntryPoint = "glViewportIndexedfv", ExactSpelling = true)]
            internal static extern void ViewportIndexedfv(uint index, float[] v);

            [DllImport(Library, EntryPoint = "glWaitSync", ExactSpelling = true)]
            internal static extern void WaitSync(IntPtr sync, uint flags, ulong timeout);
        }
    }
}