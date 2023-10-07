using System;
using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable 0649

namespace Reactor.Platform.OpenGL
{
    /// <summary>
    ///     The OpenGL 4.X Backend to Reactor 3D
    /// </summary>
    partial class GL
    {
        internal static class Delegates
        {
            internal static ActiveShaderProgram glActiveShaderProgram;
            internal static ActiveTexture glActiveTexture;
            internal static AttachShader glAttachShader;
            internal static BeginConditionalRender glBeginConditionalRender;
            internal static EndConditionalRender glEndConditionalRender;
            internal static BeginQuery glBeginQuery;
            internal static EndQuery glEndQuery;
            internal static BeginQueryIndexed glBeginQueryIndexed;
            internal static EndQueryIndexed glEndQueryIndexed;
            internal static BeginTransformFeedback glBeginTransformFeedback;
            internal static EndTransformFeedback glEndTransformFeedback;
            internal static BindAttribLocation glBindAttribLocation;
            internal static BindBuffer glBindBuffer;
            internal static BindBufferBase glBindBufferBase;
            internal static BindBufferRange glBindBufferRange;
            internal static BindBuffersBase glBindBuffersBase;
            internal static BindBuffersRange glBindBuffersRange;
            internal static BindFragDataLocation glBindFragDataLocation;
            internal static BindFragDataLocationIndexed glBindFragDataLocationIndexed;
            internal static BindFramebuffer glBindFramebuffer;
            internal static BindImageTexture glBindImageTexture;
            internal static BindImageTextures glBindImageTextures;
            internal static BindProgramPipeline glBindProgramPipeline;
            internal static BindRenderbuffer glBindRenderbuffer;
            internal static BindSampler glBindSampler;
            internal static BindSamplers glBindSamplers;
            internal static BindTexture glBindTexture;
            internal static BindTextures glBindTextures;
            internal static BindTextureUnit glBindTextureUnit;
            internal static BindTransformFeedback glBindTransformFeedback;
            internal static BindVertexArray glBindVertexArray;
            internal static BindVertexBuffer glBindVertexBuffer;
            internal static VertexArrayVertexBuffer glVertexArrayVertexBuffer;
            internal static BindVertexBuffers glBindVertexBuffers;
            internal static VertexArrayVertexBuffers glVertexArrayVertexBuffers;
            internal static BlendColor glBlendColor;
            internal static BlendEquation glBlendEquation;
            internal static BlendEquationi glBlendEquationi;
            internal static BlendEquationSeparate glBlendEquationSeparate;
            internal static BlendEquationSeparatei glBlendEquationSeparatei;
            internal static BlendFunc glBlendFunc;
            internal static BlendFunci glBlendFunci;
            internal static BlendFuncSeparate glBlendFuncSeparate;
            internal static BlendFuncSeparatei glBlendFuncSeparatei;
            internal static BlitFramebuffer glBlitFramebuffer;
            internal static BlitNamedFramebuffer glBlitNamedFramebuffer;
            internal static BufferData glBufferData;
            internal static NamedBufferData glNamedBufferData;
            internal static BufferStorage glBufferStorage;
            internal static NamedBufferStorage glNamedBufferStorage;
            internal static BufferSubData glBufferSubData;
            internal static NamedBufferSubData glNamedBufferSubData;
            internal static CheckFramebufferStatus glCheckFramebufferStatus;
            internal static CheckNamedFramebufferStatus glCheckNamedFramebufferStatus;
            internal static ClampColor glClampColor;
            internal static Clear glClear;
            internal static ClearBufferiv glClearBufferiv;
            internal static ClearBufferuiv glClearBufferuiv;
            internal static ClearBufferfv glClearBufferfv;
            internal static ClearBufferfi glClearBufferfi;
            internal static ClearNamedFramebufferiv glClearNamedFramebufferiv;
            internal static ClearNamedFramebufferuiv glClearNamedFramebufferuiv;
            internal static ClearNamedFramebufferfv glClearNamedFramebufferfv;
            internal static ClearNamedFramebufferfi glClearNamedFramebufferfi;
            internal static ClearBufferData glClearBufferData;
            internal static ClearNamedBufferData glClearNamedBufferData;
            internal static ClearBufferSubData glClearBufferSubData;
            internal static ClearNamedBufferSubData glClearNamedBufferSubData;
            internal static ClearColor glClearColor;
            internal static ClearDepth glClearDepth;
            internal static ClearDepthf glClearDepthf;
            internal static ClearStencil glClearStencil;
            internal static ClearTexImage glClearTexImage;
            internal static ClearTexSubImage glClearTexSubImage;
            internal static ClientWaitSync glClientWaitSync;
            internal static ClipControl glClipControl;
            internal static ColorMask glColorMask;
            internal static ColorMaski glColorMaski;
            internal static CompileShader glCompileShader;
            internal static CompressedTexImage1D glCompressedTexImage1D;
            internal static CompressedTexImage2D glCompressedTexImage2D;
            internal static CompressedTexImage3D glCompressedTexImage3D;
            internal static CompressedTexSubImage1D glCompressedTexSubImage1D;
            internal static CompressedTextureSubImage1D glCompressedTextureSubImage1D;
            internal static CompressedTexSubImage2D glCompressedTexSubImage2D;
            internal static CompressedTextureSubImage2D glCompressedTextureSubImage2D;
            internal static CompressedTexSubImage3D glCompressedTexSubImage3D;
            internal static CompressedTextureSubImage3D glCompressedTextureSubImage3D;
            internal static CopyBufferSubData glCopyBufferSubData;
            internal static CopyNamedBufferSubData glCopyNamedBufferSubData;
            internal static CopyImageSubData glCopyImageSubData;
            internal static CopyTexImage1D glCopyTexImage1D;
            internal static CopyTexImage2D glCopyTexImage2D;
            internal static CopyTexSubImage1D glCopyTexSubImage1D;
            internal static CopyTextureSubImage1D glCopyTextureSubImage1D;
            internal static CopyTexSubImage2D glCopyTexSubImage2D;
            internal static CopyTextureSubImage2D glCopyTextureSubImage2D;
            internal static CopyTexSubImage3D glCopyTexSubImage3D;
            internal static CopyTextureSubImage3D glCopyTextureSubImage3D;
            internal static CreateBuffers glCreateBuffers;
            internal static CreateFramebuffers glCreateFramebuffers;
            internal static CreateProgram glCreateProgram;
            internal static CreateProgramPipelines glCreateProgramPipelines;
            internal static CreateQueries glCreateQueries;
            internal static CreateRenderbuffers glCreateRenderbuffers;
            internal static CreateSamplers glCreateSamplers;
            internal static CreateShader glCreateShader;
            internal static CreateShaderProgramv glCreateShaderProgramv;
            internal static CreateTextures glCreateTextures;
            internal static CreateTransformFeedbacks glCreateTransformFeedbacks;
            internal static CreateVertexArrays glCreateVertexArrays;
            internal static CullFace glCullFace;
            internal static DeleteBuffers glDeleteBuffers;
            internal static DeleteFramebuffers glDeleteFramebuffers;
            internal static DeleteProgram glDeleteProgram;
            internal static DeleteProgramPipelines glDeleteProgramPipelines;
            internal static DeleteQueries glDeleteQueries;
            internal static DeleteRenderbuffers glDeleteRenderbuffers;
            internal static DeleteSamplers glDeleteSamplers;
            internal static DeleteShader glDeleteShader;
            internal static DeleteSync glDeleteSync;
            internal static DeleteTextures glDeleteTextures;
            internal static DeleteTransformFeedbacks glDeleteTransformFeedbacks;
            internal static DeleteVertexArrays glDeleteVertexArrays;
            internal static DepthFunc glDepthFunc;
            internal static DepthMask glDepthMask;
            internal static DepthRange glDepthRange;
            internal static DepthRangef glDepthRangef;
            internal static DepthRangeArrayv glDepthRangeArrayv;
            internal static DepthRangeIndexed glDepthRangeIndexed;
            internal static DetachShader glDetachShader;
            internal static DispatchCompute glDispatchCompute;
            internal static DispatchComputeIndirect glDispatchComputeIndirect;
            internal static DrawArrays glDrawArrays;
            internal static DrawArraysIndirect glDrawArraysIndirect;
            internal static DrawArraysInstanced glDrawArraysInstanced;
            internal static DrawArraysInstancedBaseInstance glDrawArraysInstancedBaseInstance;
            internal static DrawBuffer glDrawBuffer;
            internal static NamedFramebufferDrawBuffer glNamedFramebufferDrawBuffer;
            internal static DrawBuffers glDrawBuffers;
            internal static NamedFramebufferDrawBuffers glNamedFramebufferDrawBuffers;
            internal static DrawElements glDrawElements;
            internal static DrawElementsBaseVertex glDrawElementsBaseVertex;
            internal static DrawElementsIndirect glDrawElementsIndirect;
            internal static DrawElementsInstanced glDrawElementsInstanced;
            internal static DrawElementsInstancedBaseInstance glDrawElementsInstancedBaseInstance;
            internal static DrawElementsInstancedBaseVertex glDrawElementsInstancedBaseVertex;
            internal static DrawElementsInstancedBaseVertexBaseInstance glDrawElementsInstancedBaseVertexBaseInstance;
            internal static DrawRangeElements glDrawRangeElements;
            internal static DrawRangeElementsBaseVertex glDrawRangeElementsBaseVertex;
            internal static DrawTransformFeedback glDrawTransformFeedback;
            internal static DrawTransformFeedbackInstanced glDrawTransformFeedbackInstanced;
            internal static DrawTransformFeedbackStream glDrawTransformFeedbackStream;
            internal static DrawTransformFeedbackStreamInstanced glDrawTransformFeedbackStreamInstanced;
            internal static Enable glEnable;
            internal static Disable glDisable;
            internal static Enablei glEnablei;
            internal static Disablei glDisablei;
            internal static EnableVertexAttribArray glEnableVertexAttribArray;
            internal static DisableVertexAttribArray glDisableVertexAttribArray;
            internal static EnableVertexArrayAttrib glEnableVertexArrayAttrib;
            internal static DisableVertexArrayAttrib glDisableVertexArrayAttrib;
            internal static FenceSync glFenceSync;
            internal static Finish glFinish;
            internal static Flush glFlush;
            internal static FlushMappedBufferRange glFlushMappedBufferRange;
            internal static FlushMappedNamedBufferRange glFlushMappedNamedBufferRange;
            internal static FramebufferParameteri glFramebufferParameteri;
            internal static NamedFramebufferParameteri glNamedFramebufferParameteri;
            internal static FramebufferRenderbuffer glFramebufferRenderbuffer;
            internal static NamedFramebufferRenderbuffer glNamedFramebufferRenderbuffer;
            internal static FramebufferTexture glFramebufferTexture;
            internal static FramebufferTexture1D glFramebufferTexture1D;
            internal static FramebufferTexture2D glFramebufferTexture2D;
            internal static FramebufferTexture3D glFramebufferTexture3D;
            internal static NamedFramebufferTexture glNamedFramebufferTexture;
            internal static FramebufferTextureLayer glFramebufferTextureLayer;
            internal static NamedFramebufferTextureLayer glNamedFramebufferTextureLayer;
            internal static FrontFace glFrontFace;
            internal static GenBuffers glGenBuffers;
            internal static GenerateMipmap glGenerateMipmap;
            internal static GenerateTextureMipmap glGenerateTextureMipmap;
            internal static GenFramebuffers glGenFramebuffers;
            internal static GenProgramPipelines glGenProgramPipelines;
            internal static GenQueries glGenQueries;
            internal static GenRenderbuffers glGenRenderbuffers;
            internal static GenSamplers glGenSamplers;
            internal static GenTextures glGenTextures;
            internal static GenTransformFeedbacks glGenTransformFeedbacks;
            internal static GenVertexArrays glGenVertexArrays;
            internal static GetBooleanv glGetBooleanv;
            internal static GetDoublev glGetDoublev;
            internal static GetFloatv glGetFloatv;
            internal static GetIntegerv glGetIntegerv;
            internal static GetInteger64v glGetInteger64v;
            internal static GetBooleani_v glGetBooleani_v;
            internal static GetIntegeri_v glGetIntegeri_v;
            internal static GetFloati_v glGetFloati_v;
            internal static GetDoublei_v glGetDoublei_v;
            internal static GetInteger64i_v glGetInteger64i_v;
            internal static GetActiveAtomicCounterBufferiv glGetActiveAtomicCounterBufferiv;
            internal static GetActiveAttrib glGetActiveAttrib;
            internal static GetActiveSubroutineName glGetActiveSubroutineName;
            internal static GetActiveSubroutineUniformiv glGetActiveSubroutineUniformiv;
            internal static GetActiveSubroutineUniformName glGetActiveSubroutineUniformName;
            internal static GetActiveUniform glGetActiveUniform;
            internal static GetActiveUniformBlockiv glGetActiveUniformBlockiv;
            internal static GetActiveUniformBlockName glGetActiveUniformBlockName;
            internal static GetActiveUniformName glGetActiveUniformName;
            internal static GetActiveUniformsiv glGetActiveUniformsiv;
            internal static GetAttachedShaders glGetAttachedShaders;
            internal static GetAttribLocation glGetAttribLocation;
            internal static GetBufferParameteriv glGetBufferParameteriv;
            internal static GetBufferParameteri64v glGetBufferParameteri64v;
            internal static GetNamedBufferParameteriv glGetNamedBufferParameteriv;
            internal static GetNamedBufferParameteri64v glGetNamedBufferParameteri64v;
            internal static GetBufferPointerv glGetBufferPointerv;
            internal static GetNamedBufferPointerv glGetNamedBufferPointerv;
            internal static GetBufferSubData glGetBufferSubData;
            internal static GetNamedBufferSubData glGetNamedBufferSubData;
            internal static GetCompressedTexImage glGetCompressedTexImage;
            internal static GetnCompressedTexImage glGetnCompressedTexImage;
            internal static GetCompressedTextureImage glGetCompressedTextureImage;
            internal static GetCompressedTextureSubImage glGetCompressedTextureSubImage;
            internal static GetError glGetError;
            internal static GetFragDataIndex glGetFragDataIndex;
            internal static GetFragDataLocation glGetFragDataLocation;
            internal static GetFramebufferAttachmentParameteriv glGetFramebufferAttachmentParameteriv;
            internal static GetNamedFramebufferAttachmentParameteriv glGetNamedFramebufferAttachmentParameteriv;
            internal static GetFramebufferParameteriv glGetFramebufferParameteriv;
            internal static GetNamedFramebufferParameteriv glGetNamedFramebufferParameteriv;
            internal static GetGraphicsResetStatus glGetGraphicsResetStatus;
            internal static GetInternalformativ glGetInternalformativ;
            internal static GetInternalformati64v glGetInternalformati64v;
            internal static GetMultisamplefv glGetMultisamplefv;
            internal static GetObjectLabel glGetObjectLabel;
            internal static GetObjectPtrLabel glGetObjectPtrLabel;
            internal static GetPointerv glGetPointerv;
            internal static GetProgramiv glGetProgramiv;
            internal static GetProgramBinary glGetProgramBinary;
            internal static GetProgramInfoLog glGetProgramInfoLog;
            internal static GetProgramInterfaceiv glGetProgramInterfaceiv;
            internal static GetProgramPipelineiv glGetProgramPipelineiv;
            internal static GetProgramPipelineInfoLog glGetProgramPipelineInfoLog;
            internal static GetProgramResourceiv glGetProgramResourceiv;
            internal static GetProgramResourceIndex glGetProgramResourceIndex;
            internal static GetProgramResourceLocation glGetProgramResourceLocation;
            internal static GetProgramResourceLocationIndex glGetProgramResourceLocationIndex;
            internal static GetProgramResourceName glGetProgramResourceName;
            internal static GetProgramStageiv glGetProgramStageiv;
            internal static GetQueryIndexediv glGetQueryIndexediv;
            internal static GetQueryiv glGetQueryiv;
            internal static GetQueryObjectiv glGetQueryObjectiv;
            internal static GetQueryObjectuiv glGetQueryObjectuiv;
            internal static GetQueryObjecti64v glGetQueryObjecti64v;
            internal static GetQueryObjectui64v glGetQueryObjectui64v;
            internal static GetRenderbufferParameteriv glGetRenderbufferParameteriv;
            internal static GetNamedRenderbufferParameteriv glGetNamedRenderbufferParameteriv;
            internal static GetSamplerParameterfv glGetSamplerParameterfv;
            internal static GetSamplerParameteriv glGetSamplerParameteriv;
            internal static GetSamplerParameterIiv glGetSamplerParameterIiv;
            internal static GetSamplerParameterIuiv glGetSamplerParameterIuiv;
            internal static GetShaderiv glGetShaderiv;
            internal static GetShaderInfoLog glGetShaderInfoLog;
            internal static GetShaderPrecisionFormat glGetShaderPrecisionFormat;
            internal static GetShaderSource glGetShaderSource;
            internal static GetString glGetString;
            internal static GetStringi glGetStringi;
            internal static GetSubroutineIndex glGetSubroutineIndex;
            internal static GetSubroutineUniformLocation glGetSubroutineUniformLocation;
            internal static GetSynciv glGetSynciv;
            internal static GetTexImage glGetTexImage;
            internal static GetnTexImage glGetnTexImage;
            internal static GetTextureImage glGetTextureImage;
            internal static GetTexLevelParameterfv glGetTexLevelParameterfv;
            internal static GetTexLevelParameteriv glGetTexLevelParameteriv;
            internal static GetTextureLevelParameterfv glGetTextureLevelParameterfv;
            internal static GetTextureLevelParameteriv glGetTextureLevelParameteriv;
            internal static GetTexParameterfv glGetTexParameterfv;
            internal static GetTexParameteriv glGetTexParameteriv;
            internal static GetTexParameterIiv glGetTexParameterIiv;
            internal static GetTexParameterIuiv glGetTexParameterIuiv;
            internal static GetTextureParameterfv glGetTextureParameterfv;
            internal static GetTextureParameteriv glGetTextureParameteriv;
            internal static GetTextureParameterIiv glGetTextureParameterIiv;
            internal static GetTextureParameterIuiv glGetTextureParameterIuiv;
            internal static GetTextureSubImage glGetTextureSubImage;
            internal static GetTransformFeedbackiv glGetTransformFeedbackiv;
            internal static GetTransformFeedbacki_v glGetTransformFeedbacki_v;
            internal static GetTransformFeedbacki64_v glGetTransformFeedbacki64_v;
            internal static GetTransformFeedbackVarying glGetTransformFeedbackVarying;
            internal static GetUniformfv glGetUniformfv;
            internal static GetUniformiv glGetUniformiv;
            internal static GetUniformuiv glGetUniformuiv;
            internal static GetUniformdv glGetUniformdv;
            internal static GetnUniformfv glGetnUniformfv;
            internal static GetnUniformiv glGetnUniformiv;
            internal static GetnUniformuiv glGetnUniformuiv;
            internal static GetnUniformdv glGetnUniformdv;
            internal static GetUniformBlockIndex glGetUniformBlockIndex;
            internal static GetUniformIndices glGetUniformIndices;
            internal static GetUniformLocation glGetUniformLocation;
            internal static GetUniformSubroutineuiv glGetUniformSubroutineuiv;
            internal static GetVertexArrayIndexed64iv glGetVertexArrayIndexed64iv;
            internal static GetVertexArrayIndexediv glGetVertexArrayIndexediv;
            internal static GetVertexArrayiv glGetVertexArrayiv;
            internal static GetVertexAttribdv glGetVertexAttribdv;
            internal static GetVertexAttribfv glGetVertexAttribfv;
            internal static GetVertexAttribiv glGetVertexAttribiv;
            internal static GetVertexAttribIiv glGetVertexAttribIiv;
            internal static GetVertexAttribIuiv glGetVertexAttribIuiv;
            internal static GetVertexAttribLdv glGetVertexAttribLdv;
            internal static GetVertexAttribPointerv glGetVertexAttribPointerv;
            internal static Hint glHint;
            internal static InvalidateBufferData glInvalidateBufferData;
            internal static InvalidateBufferSubData glInvalidateBufferSubData;
            internal static InvalidateFramebuffer glInvalidateFramebuffer;
            internal static InvalidateNamedFramebufferData glInvalidateNamedFramebufferData;
            internal static InvalidateSubFramebuffer glInvalidateSubFramebuffer;
            internal static InvalidateNamedFramebufferSubData glInvalidateNamedFramebufferSubData;
            internal static InvalidateTexImage glInvalidateTexImage;
            internal static InvalidateTexSubImage glInvalidateTexSubImage;
            internal static IsBuffer glIsBuffer;
            internal static IsEnabled glIsEnabled;
            internal static IsEnabledi glIsEnabledi;
            internal static IsFramebuffer glIsFramebuffer;
            internal static IsProgram glIsProgram;
            internal static IsProgramPipeline glIsProgramPipeline;
            internal static IsQuery glIsQuery;
            internal static IsRenderbuffer glIsRenderbuffer;
            internal static IsSampler glIsSampler;
            internal static IsShader glIsShader;
            internal static IsSync glIsSync;
            internal static IsTexture glIsTexture;
            internal static IsTransformFeedback glIsTransformFeedback;
            internal static IsVertexArray glIsVertexArray;
            internal static LineWidth glLineWidth;
            internal static LinkProgram glLinkProgram;
            internal static LogicOp glLogicOp;
            internal static MapBuffer glMapBuffer;
            internal static MapNamedBuffer glMapNamedBuffer;
            internal static MapBufferRange glMapBufferRange;
            internal static MapNamedBufferRange glMapNamedBufferRange;
            internal static MemoryBarrier glMemoryBarrier;
            internal static MemoryBarrierByRegion glMemoryBarrierByRegion;
            internal static MinSampleShading glMinSampleShading;
            internal static MultiDrawArrays glMultiDrawArrays;
            internal static MultiDrawArraysIndirect glMultiDrawArraysIndirect;
            internal static MultiDrawElements glMultiDrawElements;
            internal static MultiDrawElementsBaseVertex glMultiDrawElementsBaseVertex;
            internal static MultiDrawElementsIndirect glMultiDrawElementsIndirect;
            internal static ObjectLabel glObjectLabel;
            internal static ObjectPtrLabel glObjectPtrLabel;
            internal static PatchParameteri glPatchParameteri;
            internal static PatchParameterfv glPatchParameterfv;
            internal static PixelStoref glPixelStoref;
            internal static PixelStorei glPixelStorei;
            internal static PointParameterf glPointParameterf;
            internal static PointParameteri glPointParameteri;
            internal static PointParameterfv glPointParameterfv;
            internal static PointParameteriv glPointParameteriv;
            internal static PointSize glPointSize;
            internal static PolygonMode glPolygonMode;
            internal static PolygonOffset glPolygonOffset;
            internal static PrimitiveRestartIndex glPrimitiveRestartIndex;
            internal static ProgramBinary glProgramBinary;
            internal static ProgramParameteri glProgramParameteri;
            internal static ProgramUniform1f glProgramUniform1f;
            internal static ProgramUniform2f glProgramUniform2f;
            internal static ProgramUniform3f glProgramUniform3f;
            internal static ProgramUniform4f glProgramUniform4f;
            internal static ProgramUniform1i glProgramUniform1i;
            internal static ProgramUniform2i glProgramUniform2i;
            internal static ProgramUniform3i glProgramUniform3i;
            internal static ProgramUniform4i glProgramUniform4i;
            internal static ProgramUniform1ui glProgramUniform1ui;
            internal static ProgramUniform2ui glProgramUniform2ui;
            internal static ProgramUniform3ui glProgramUniform3ui;
            internal static ProgramUniform4ui glProgramUniform4ui;
            internal static ProgramUniform1fv glProgramUniform1fv;
            internal static ProgramUniform2fv glProgramUniform2fv;
            internal static ProgramUniform3fv glProgramUniform3fv;
            internal static ProgramUniform4fv glProgramUniform4fv;
            internal static ProgramUniform1iv glProgramUniform1iv;
            internal static ProgramUniform2iv glProgramUniform2iv;
            internal static ProgramUniform3iv glProgramUniform3iv;
            internal static ProgramUniform4iv glProgramUniform4iv;
            internal static ProgramUniform1uiv glProgramUniform1uiv;
            internal static ProgramUniform2uiv glProgramUniform2uiv;
            internal static ProgramUniform3uiv glProgramUniform3uiv;
            internal static ProgramUniform4uiv glProgramUniform4uiv;
            internal static ProgramUniformMatrix2fv glProgramUniformMatrix2fv;
            internal static ProgramUniformMatrix3fv glProgramUniformMatrix3fv;
            internal static ProgramUniformMatrix4fv glProgramUniformMatrix4fv;
            internal static ProgramUniformMatrix2x3fv glProgramUniformMatrix2x3fv;
            internal static ProgramUniformMatrix3x2fv glProgramUniformMatrix3x2fv;
            internal static ProgramUniformMatrix2x4fv glProgramUniformMatrix2x4fv;
            internal static ProgramUniformMatrix4x2fv glProgramUniformMatrix4x2fv;
            internal static ProgramUniformMatrix3x4fv glProgramUniformMatrix3x4fv;
            internal static ProgramUniformMatrix4x3fv glProgramUniformMatrix4x3fv;
            internal static ProvokingVertex glProvokingVertex;
            internal static QueryCounter glQueryCounter;
            internal static ReadBuffer glReadBuffer;
            internal static NamedFramebufferReadBuffer glNamedFramebufferReadBuffer;
            internal static ReadPixels glReadPixels;
            internal static ReadnPixels glReadnPixels;
            internal static RenderbufferStorage glRenderbufferStorage;
            internal static NamedRenderbufferStorage glNamedRenderbufferStorage;
            internal static RenderbufferStorageMultisample glRenderbufferStorageMultisample;
            internal static NamedRenderbufferStorageMultisample glNamedRenderbufferStorageMultisample;
            internal static SampleCoverage glSampleCoverage;
            internal static SampleMaski glSampleMaski;
            internal static SamplerParameterf glSamplerParameterf;
            internal static SamplerParameteri glSamplerParameteri;
            internal static SamplerParameterfv glSamplerParameterfv;
            internal static SamplerParameteriv glSamplerParameteriv;
            internal static SamplerParameterIiv glSamplerParameterIiv;
            internal static SamplerParameterIuiv glSamplerParameterIuiv;
            internal static Scissor glScissor;
            internal static ScissorArrayv glScissorArrayv;
            internal static ScissorIndexed glScissorIndexed;
            internal static ScissorIndexedv glScissorIndexedv;
            internal static ShaderBinary glShaderBinary;
            internal static ShaderSource glShaderSource;
            internal static ShaderStorageBlockBinding glShaderStorageBlockBinding;
            internal static StencilFunc glStencilFunc;
            internal static StencilFuncSeparate glStencilFuncSeparate;
            internal static StencilMask glStencilMask;
            internal static StencilMaskSeparate glStencilMaskSeparate;
            internal static StencilOp glStencilOp;
            internal static StencilOpSeparate glStencilOpSeparate;
            internal static TexBuffer glTexBuffer;
            internal static TextureBuffer glTextureBuffer;
            internal static TexBufferRange glTexBufferRange;
            internal static TextureBufferRange glTextureBufferRange;
            internal static TexImage1D glTexImage1D;
            internal static TexImage2D glTexImage2D;
            internal static TexImage2DMultisample glTexImage2DMultisample;
            internal static TexImage3D glTexImage3D;
            internal static TexImage3DMultisample glTexImage3DMultisample;
            internal static TexParameterf glTexParameterf;
            internal static TexParameteri glTexParameteri;
            internal static TextureParameterf glTextureParameterf;
            internal static TextureParameteri glTextureParameteri;
            internal static TexParameterfv glTexParameterfv;
            internal static TexParameteriv glTexParameteriv;
            internal static TexParameterIiv glTexParameterIiv;
            internal static TexParameterIuiv glTexParameterIuiv;
            internal static TextureParameterfv glTextureParameterfv;
            internal static TextureParameteriv glTextureParameteriv;
            internal static TextureParameterIiv glTextureParameterIiv;
            internal static TextureParameterIuiv glTextureParameterIuiv;
            internal static TexStorage1D glTexStorage1D;
            internal static TextureStorage1D glTextureStorage1D;
            internal static TexStorage2D glTexStorage2D;
            internal static TextureStorage2D glTextureStorage2D;
            internal static TexStorage2DMultisample glTexStorage2DMultisample;
            internal static TextureStorage2DMultisample glTextureStorage2DMultisample;
            internal static TexStorage3D glTexStorage3D;
            internal static TextureStorage3D glTextureStorage3D;
            internal static TexStorage3DMultisample glTexStorage3DMultisample;
            internal static TextureStorage3DMultisample glTextureStorage3DMultisample;
            internal static TexSubImage1D glTexSubImage1D;
            internal static TextureSubImage1D glTextureSubImage1D;
            internal static TexSubImage2D glTexSubImage2D;
            internal static TextureSubImage2D glTextureSubImage2D;
            internal static TexSubImage3D glTexSubImage3D;
            internal static TextureSubImage3D glTextureSubImage3D;
            internal static TextureBarrier glTextureBarrier;
            internal static TextureView glTextureView;
            internal static TransformFeedbackBufferBase glTransformFeedbackBufferBase;
            internal static TransformFeedbackBufferRange glTransformFeedbackBufferRange;
            internal static TransformFeedbackVaryings glTransformFeedbackVaryings;
            internal static Uniform1f glUniform1f;
            internal static Uniform2f glUniform2f;
            internal static Uniform3f glUniform3f;
            internal static Uniform4f glUniform4f;
            internal static Uniform1i glUniform1i;
            internal static Uniform2i glUniform2i;
            internal static Uniform3i glUniform3i;
            internal static Uniform4i glUniform4i;
            internal static Uniform1ui glUniform1ui;
            internal static Uniform2ui glUniform2ui;
            internal static Uniform3ui glUniform3ui;
            internal static Uniform4ui glUniform4ui;
            internal static Uniform1fv glUniform1fv;
            internal static Uniform2fv glUniform2fv;
            internal static Uniform3fv glUniform3fv;
            internal static Uniform4fv glUniform4fv;
            internal static Uniform1iv glUniform1iv;
            internal static Uniform2iv glUniform2iv;
            internal static Uniform3iv glUniform3iv;
            internal static Uniform4iv glUniform4iv;
            internal static Uniform1uiv glUniform1uiv;
            internal static Uniform2uiv glUniform2uiv;
            internal static Uniform3uiv glUniform3uiv;
            internal static Uniform4uiv glUniform4uiv;
            internal static UniformMatrix2fv glUniformMatrix2fv;
            internal static UniformMatrix3fv glUniformMatrix3fv;
            internal static UniformMatrix4fv glUniformMatrix4fv;
            internal static UniformMatrix2x3fv glUniformMatrix2x3fv;
            internal static UniformMatrix3x2fv glUniformMatrix3x2fv;
            internal static UniformMatrix2x4fv glUniformMatrix2x4fv;
            internal static UniformMatrix4x2fv glUniformMatrix4x2fv;
            internal static UniformMatrix3x4fv glUniformMatrix3x4fv;
            internal static UniformMatrix4x3fv glUniformMatrix4x3fv;
            internal static UniformBlockBinding glUniformBlockBinding;
            internal static UniformSubroutinesuiv glUniformSubroutinesuiv;
            internal static UnmapBuffer glUnmapBuffer;
            internal static UnmapNamedBuffer glUnmapNamedBuffer;
            internal static UseProgram glUseProgram;
            internal static UseProgramStages glUseProgramStages;
            internal static ValidateProgram glValidateProgram;
            internal static ValidateProgramPipeline glValidateProgramPipeline;
            internal static VertexArrayElementBuffer glVertexArrayElementBuffer;
            internal static VertexAttrib1f glVertexAttrib1f;
            internal static VertexAttrib1s glVertexAttrib1s;
            internal static VertexAttrib1d glVertexAttrib1d;
            internal static VertexAttribI1i glVertexAttribI1i;
            internal static VertexAttribI1ui glVertexAttribI1ui;
            internal static VertexAttrib2f glVertexAttrib2f;
            internal static VertexAttrib2s glVertexAttrib2s;
            internal static VertexAttrib2d glVertexAttrib2d;
            internal static VertexAttribI2i glVertexAttribI2i;
            internal static VertexAttribI2ui glVertexAttribI2ui;
            internal static VertexAttrib3f glVertexAttrib3f;
            internal static VertexAttrib3s glVertexAttrib3s;
            internal static VertexAttrib3d glVertexAttrib3d;
            internal static VertexAttribI3i glVertexAttribI3i;
            internal static VertexAttribI3ui glVertexAttribI3ui;
            internal static VertexAttrib4f glVertexAttrib4f;
            internal static VertexAttrib4s glVertexAttrib4s;
            internal static VertexAttrib4d glVertexAttrib4d;
            internal static VertexAttrib4Nub glVertexAttrib4Nub;
            internal static VertexAttribI4i glVertexAttribI4i;
            internal static VertexAttribI4ui glVertexAttribI4ui;
            internal static VertexAttribL1d glVertexAttribL1d;
            internal static VertexAttribL2d glVertexAttribL2d;
            internal static VertexAttribL3d glVertexAttribL3d;
            internal static VertexAttribL4d glVertexAttribL4d;
            internal static VertexAttrib1fv glVertexAttrib1fv;
            internal static VertexAttrib1sv glVertexAttrib1sv;
            internal static VertexAttrib1dv glVertexAttrib1dv;
            internal static VertexAttribI1iv glVertexAttribI1iv;
            internal static VertexAttribI1uiv glVertexAttribI1uiv;
            internal static VertexAttrib2fv glVertexAttrib2fv;
            internal static VertexAttrib2sv glVertexAttrib2sv;
            internal static VertexAttrib2dv glVertexAttrib2dv;
            internal static VertexAttribI2iv glVertexAttribI2iv;
            internal static VertexAttribI2uiv glVertexAttribI2uiv;
            internal static VertexAttrib3fv glVertexAttrib3fv;
            internal static VertexAttrib3sv glVertexAttrib3sv;
            internal static VertexAttrib3dv glVertexAttrib3dv;
            internal static VertexAttribI3iv glVertexAttribI3iv;
            internal static VertexAttribI3uiv glVertexAttribI3uiv;
            internal static VertexAttrib4fv glVertexAttrib4fv;
            internal static VertexAttrib4sv glVertexAttrib4sv;
            internal static VertexAttrib4dv glVertexAttrib4dv;
            internal static VertexAttrib4iv glVertexAttrib4iv;
            internal static VertexAttrib4bv glVertexAttrib4bv;
            internal static VertexAttrib4ubv glVertexAttrib4ubv;
            internal static VertexAttrib4usv glVertexAttrib4usv;
            internal static VertexAttrib4uiv glVertexAttrib4uiv;
            internal static VertexAttrib4Nbv glVertexAttrib4Nbv;
            internal static VertexAttrib4Nsv glVertexAttrib4Nsv;
            internal static VertexAttrib4Niv glVertexAttrib4Niv;
            internal static VertexAttrib4Nubv glVertexAttrib4Nubv;
            internal static VertexAttrib4Nusv glVertexAttrib4Nusv;
            internal static VertexAttrib4Nuiv glVertexAttrib4Nuiv;
            internal static VertexAttribI4bv glVertexAttribI4bv;
            internal static VertexAttribI4ubv glVertexAttribI4ubv;
            internal static VertexAttribI4sv glVertexAttribI4sv;
            internal static VertexAttribI4usv glVertexAttribI4usv;
            internal static VertexAttribI4iv glVertexAttribI4iv;
            internal static VertexAttribI4uiv glVertexAttribI4uiv;
            internal static VertexAttribL1dv glVertexAttribL1dv;
            internal static VertexAttribL2dv glVertexAttribL2dv;
            internal static VertexAttribL3dv glVertexAttribL3dv;
            internal static VertexAttribL4dv glVertexAttribL4dv;
            internal static VertexAttribP1ui glVertexAttribP1ui;
            internal static VertexAttribP2ui glVertexAttribP2ui;
            internal static VertexAttribP3ui glVertexAttribP3ui;
            internal static VertexAttribP4ui glVertexAttribP4ui;
            internal static VertexAttribBinding glVertexAttribBinding;
            internal static VertexArrayAttribBinding glVertexArrayAttribBinding;
            internal static VertexAttribDivisor glVertexAttribDivisor;
            internal static VertexAttribFormat glVertexAttribFormat;
            internal static VertexAttribIFormat glVertexAttribIFormat;
            internal static VertexAttribLFormat glVertexAttribLFormat;
            internal static VertexArrayAttribFormat glVertexArrayAttribFormat;
            internal static VertexArrayAttribIFormat glVertexArrayAttribIFormat;
            internal static VertexArrayAttribLFormat glVertexArrayAttribLFormat;
            internal static VertexAttribPointer glVertexAttribPointer;
            internal static VertexAttribIPointer glVertexAttribIPointer;
            internal static VertexAttribLPointer glVertexAttribLPointer;
            internal static VertexBindingDivisor glVertexBindingDivisor;
            internal static VertexArrayBindingDivisor glVertexArrayBindingDivisor;
            internal static Viewport glViewport;
            internal static ViewportArrayv glViewportArrayv;
            internal static ViewportIndexedf glViewportIndexedf;
            internal static ViewportIndexedfv glViewportIndexedfv;
            internal static WaitSync glWaitSync;

            internal delegate void ActiveShaderProgram(uint pipeline, uint program);

            internal delegate void ActiveTexture(int texture);

            internal delegate void AttachShader(uint program, uint shader);

            internal delegate void BeginConditionalRender(uint id, ConditionalRenderType mode);

            internal delegate void EndConditionalRender();

            internal delegate void BeginQuery(QueryTarget target, uint id);

            internal delegate void EndQuery(QueryTarget target);

            internal delegate void BeginQueryIndexed(QueryTarget target, uint index, uint id);

            internal delegate void EndQueryIndexed(QueryTarget target, uint index);

            internal delegate void BeginTransformFeedback(BeginFeedbackMode primitiveMode);

            internal delegate void EndTransformFeedback();

            internal delegate void BindAttribLocation(uint program, uint index, string name);

            internal delegate void BindBuffer(BufferTarget target, uint buffer);

            internal delegate void BindBufferBase(BufferTarget target, uint index, uint buffer);

            internal delegate void BindBufferRange(BufferTarget target, uint index, uint buffer, IntPtr offset,
                IntPtr size);

            internal delegate void BindBuffersBase(BufferTarget target, uint first, int count, uint[] buffers);

            internal delegate void BindBuffersRange(BufferTarget target, uint first, int count, uint[] buffers,
                IntPtr[] offsets, IntPtr[] sizes);

            internal delegate void BindFragDataLocation(uint program, uint colorNumber, string name);

            internal delegate void BindFragDataLocationIndexed(uint program, uint colorNumber, uint index, string name);

            internal delegate void BindFramebuffer(FramebufferTarget target, uint framebuffer);

            internal delegate void BindImageTexture(uint unit, uint texture, int level, bool layered, int layer,
                BufferAccess access, PixelInternalFormat format);

            internal delegate void BindImageTextures(uint first, int count, uint[] textures);

            internal delegate void BindProgramPipeline(uint pipeline);

            internal delegate void BindRenderbuffer(RenderbufferTarget target, uint renderbuffer);

            internal delegate void BindSampler(uint unit, uint sampler);

            internal delegate void BindSamplers(uint first, int count, uint[] samplers);

            internal delegate void BindTexture(TextureTarget target, uint texture);

            internal delegate void BindTextures(uint first, int count, uint[] textures);

            internal delegate void BindTextureUnit(uint unit, uint texture);

            internal delegate void BindTransformFeedback(NvTransformFeedback2 target, uint id);

            internal delegate void BindVertexArray(uint array);

            internal delegate void BindVertexBuffer(uint bindingindex, uint buffer, IntPtr offset, IntPtr stride);

            internal delegate void VertexArrayVertexBuffer(uint vaobj, uint bindingindex, uint buffer, IntPtr offset,
                int stride);

            internal delegate void BindVertexBuffers(uint first, int count, uint[] buffers, IntPtr[] offsets,
                int[] strides);

            internal delegate void VertexArrayVertexBuffers(uint vaobj, uint first, int count, uint[] buffers,
                IntPtr[] offsets, int[] strides);

            internal delegate void BlendColor(float red, float green, float blue, float alpha);

            internal delegate void BlendEquation(BlendEquationMode mode);

            internal delegate void BlendEquationi(uint buf, BlendEquationMode mode);

            internal delegate void BlendEquationSeparate(BlendEquationMode modeRGB, BlendEquationMode modeAlpha);

            internal delegate void BlendEquationSeparatei(uint buf, BlendEquationMode modeRGB,
                BlendEquationMode modeAlpha);

            internal delegate void BlendFunc(BlendingFactorSrc sfactor, BlendingFactorDest dfactor);

            internal delegate void BlendFunci(uint buf, BlendingFactorSrc sfactor, BlendingFactorDest dfactor);

            internal delegate void BlendFuncSeparate(BlendingFactorSrc srcRGB, BlendingFactorDest dstRGB,
                BlendingFactorSrc srcAlpha, BlendingFactorDest dstAlpha);

            internal delegate void BlendFuncSeparatei(uint buf, BlendingFactorSrc srcRGB, BlendingFactorDest dstRGB,
                BlendingFactorSrc srcAlpha, BlendingFactorDest dstAlpha);

            internal delegate void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0,
                int dstX1, int dstY1, ClearBufferMask mask, BlitFramebufferFilter filter);

            internal delegate void BlitNamedFramebuffer(uint readFramebuffer, uint drawFramebuffer, int srcX0,
                int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, ClearBufferMask mask,
                BlitFramebufferFilter filter);

            internal delegate void BufferData(BufferTarget target, IntPtr size, IntPtr data, BufferUsageHint usage);

            internal delegate void NamedBufferData(uint buffer, int size, IntPtr data, BufferUsageHint usage);

            internal delegate void BufferStorage(BufferTarget target, IntPtr size, IntPtr data, uint flags);

            internal delegate void NamedBufferStorage(uint buffer, int size, IntPtr data, uint flags);

            internal delegate void BufferSubData(BufferTarget target, IntPtr offset, IntPtr size, IntPtr data);

            internal delegate void NamedBufferSubData(uint buffer, IntPtr offset, int size, IntPtr data);

            internal delegate FramebufferErrorCode CheckFramebufferStatus(FramebufferTarget target);

            internal delegate FramebufferErrorCode CheckNamedFramebufferStatus(uint framebuffer,
                FramebufferTarget target);

            internal delegate void ClampColor(ClampColorTarget target, ClampColorMode clamp);

            internal delegate void Clear(ClearBufferMask mask);

            internal delegate void ClearBufferiv(ClearBuffer buffer, int drawbuffer, int[] value);

            internal delegate void ClearBufferuiv(ClearBuffer buffer, int drawbuffer, uint[] value);

            internal delegate void ClearBufferfv(ClearBuffer buffer, int drawbuffer, float[] value);

            internal delegate void ClearBufferfi(ClearBuffer buffer, int drawbuffer, float depth, int stencil);

            internal delegate void ClearNamedFramebufferiv(uint framebuffer, ClearBuffer buffer, int drawbuffer,
                int[] value);

            internal delegate void ClearNamedFramebufferuiv(uint framebuffer, ClearBuffer buffer, int drawbuffer,
                uint[] value);

            internal delegate void ClearNamedFramebufferfv(uint framebuffer, ClearBuffer buffer, int drawbuffer,
                float[] value);

            internal delegate void ClearNamedFramebufferfi(uint framebuffer, ClearBuffer buffer, int drawbuffer,
                float depth, int stencil);

            internal delegate void ClearBufferData(BufferTarget target, SizedInternalFormat internalFormat,
                PixelInternalFormat format, PixelType type, IntPtr data);

            internal delegate void ClearNamedBufferData(uint buffer, SizedInternalFormat internalFormat,
                PixelInternalFormat format, PixelType type, IntPtr data);

            internal delegate void ClearBufferSubData(BufferTarget target, SizedInternalFormat internalFormat,
                IntPtr offset, IntPtr size, PixelInternalFormat format, PixelType type, IntPtr data);

            internal delegate void ClearNamedBufferSubData(uint buffer, SizedInternalFormat internalFormat,
                IntPtr offset, int size, PixelInternalFormat format, PixelType type, IntPtr data);

            internal delegate void ClearColor(float red, float green, float blue, float alpha);

            internal delegate void ClearDepth(double depth);

            internal delegate void ClearDepthf(float depth);

            internal delegate void ClearStencil(int s);

            internal delegate void ClearTexImage(uint texture, int level, PixelInternalFormat format, PixelType type,
                IntPtr data);

            internal delegate void ClearTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset,
                int width, int height, int depth, PixelInternalFormat format, PixelType type, IntPtr data);

            internal delegate ArbSync ClientWaitSync(IntPtr sync, uint flags, ulong timeout);

            internal delegate void ClipControl(ClipControlOrigin origin, ClipControlDepth depth);

            internal delegate void ColorMask(bool red, bool green, bool blue, bool alpha);

            internal delegate void ColorMaski(uint buf, bool red, bool green, bool blue, bool alpha);

            internal delegate void CompileShader(uint shader);

            internal delegate void CompressedTexImage1D(TextureTarget target, int level,
                PixelInternalFormat internalFormat, int width, int border, int imageSize, IntPtr data);

            internal delegate void CompressedTexImage2D(TextureTarget target, int level,
                PixelInternalFormat internalFormat, int width, int height, int border, int imageSize, IntPtr data);

            internal delegate void CompressedTexImage3D(TextureTarget target, int level,
                PixelInternalFormat internalFormat, int width, int height, int depth, int border, int imageSize,
                IntPtr data);

            internal delegate void CompressedTexSubImage1D(TextureTarget target, int level, int xoffset, int width,
                PixelFormat format, int imageSize, IntPtr data);

            internal delegate void CompressedTextureSubImage1D(uint texture, int level, int xoffset, int width,
                PixelInternalFormat format, int imageSize, IntPtr data);

            internal delegate void CompressedTexSubImage2D(TextureTarget target, int level, int xoffset, int yoffset,
                int width, int height, PixelFormat format, int imageSize, IntPtr data);

            internal delegate void CompressedTextureSubImage2D(uint texture, int level, int xoffset, int yoffset,
                int width, int height, PixelInternalFormat format, int imageSize, IntPtr data);

            internal delegate void CompressedTexSubImage3D(TextureTarget target, int level, int xoffset, int yoffset,
                int zoffset, int width, int height, int depth, PixelFormat format, int imageSize, IntPtr data);

            internal delegate void CompressedTextureSubImage3D(uint texture, int level, int xoffset, int yoffset,
                int zoffset, int width, int height, int depth, PixelInternalFormat format, int imageSize, IntPtr data);

            internal delegate void CopyBufferSubData(BufferTarget readTarget, BufferTarget writeTarget,
                IntPtr readOffset, IntPtr writeOffset, IntPtr size);

            internal delegate void CopyNamedBufferSubData(uint readBuffer, uint writeBuffer, IntPtr readOffset,
                IntPtr writeOffset, int size);

            internal delegate void CopyImageSubData(uint srcName, BufferTarget srcTarget, int srcLevel, int srcX,
                int srcY, int srcZ, uint dstName, BufferTarget dstTarget, int dstLevel, int dstX, int dstY, int dstZ,
                int srcWidth, int srcHeight, int srcDepth);

            internal delegate void CopyTexImage1D(TextureTarget target, int level, PixelInternalFormat internalFormat,
                int x, int y, int width, int border);

            internal delegate void CopyTexImage2D(TextureTarget target, int level, PixelInternalFormat internalFormat,
                int x, int y, int width, int height, int border);

            internal delegate void CopyTexSubImage1D(TextureTarget target, int level, int xoffset, int x, int y,
                int width);

            internal delegate void CopyTextureSubImage1D(uint texture, int level, int xoffset, int x, int y, int width);

            internal delegate void CopyTexSubImage2D(TextureTarget target, int level, int xoffset, int yoffset, int x,
                int y, int width, int height);

            internal delegate void CopyTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int x,
                int y, int width, int height);

            internal delegate void CopyTexSubImage3D(TextureTarget target, int level, int xoffset, int yoffset,
                int zoffset, int x, int y, int width, int height);

            internal delegate void CopyTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset,
                int x, int y, int width, int height);

            internal delegate void CreateBuffers(int n, uint[] buffers);

            internal delegate void CreateFramebuffers(int n, uint[] ids);

            internal delegate uint CreateProgram();

            internal delegate void CreateProgramPipelines(int n, uint[] pipelines);

            internal delegate void CreateQueries(QueryTarget target, int n, uint[] ids);

            internal delegate void CreateRenderbuffers(int n, uint[] renderbuffers);

            internal delegate void CreateSamplers(int n, uint[] samplers);

            internal delegate uint CreateShader(ShaderType shaderType);

            internal delegate uint CreateShaderProgramv(ShaderType type, int count, string strings);

            internal delegate void CreateTextures(TextureTarget target, int n, uint[] textures);

            internal delegate void CreateTransformFeedbacks(int n, uint[] ids);

            internal delegate void CreateVertexArrays(int n, uint[] arrays);

            internal delegate void CullFace(CullFaceMode mode);

            internal delegate void DeleteBuffers(int n, uint[] buffers);

            internal delegate void DeleteFramebuffers(int n, uint[] framebuffers);

            internal delegate void DeleteProgram(uint program);

            internal delegate void DeleteProgramPipelines(int n, uint[] pipelines);

            internal delegate void DeleteQueries(int n, uint[] ids);

            internal delegate void DeleteRenderbuffers(int n, uint[] renderbuffers);

            internal delegate void DeleteSamplers(int n, uint[] samplers);

            internal delegate void DeleteShader(uint shader);

            internal delegate void DeleteSync(IntPtr sync);

            internal delegate void DeleteTextures(int n, uint[] textures);

            internal delegate void DeleteTransformFeedbacks(int n, uint[] ids);

            internal delegate void DeleteVertexArrays(int n, uint[] arrays);

            internal delegate void DepthFunc(DepthFunction func);

            internal delegate void DepthMask(bool flag);

            internal delegate void DepthRange(double nearVal, double farVal);

            internal delegate void DepthRangef(float nearVal, float farVal);

            internal delegate void DepthRangeArrayv(uint first, int count, double[] v);

            internal delegate void DepthRangeIndexed(uint index, double nearVal, double farVal);

            internal delegate void DetachShader(uint program, uint shader);

            internal delegate void DispatchCompute(uint num_groups_x, uint num_groups_y, uint num_groups_z);

            internal delegate void DispatchComputeIndirect(IntPtr indirect);

            internal delegate void DrawArrays(BeginMode mode, int first, int count);

            internal delegate void DrawArraysIndirect(BeginMode mode, IntPtr indirect);

            internal delegate void DrawArraysInstanced(BeginMode mode, int first, int count, int primcount);

            internal delegate void DrawArraysInstancedBaseInstance(BeginMode mode, int first, int count, int primcount,
                uint baseinstance);

            internal delegate void DrawBuffer(DrawBufferMode buf);

            internal delegate void NamedFramebufferDrawBuffer(uint framebuffer, DrawBufferMode buf);

            internal delegate void DrawBuffers(int n, DrawBuffersEnum[] bufs);

            internal delegate void NamedFramebufferDrawBuffers(uint framebuffer, int n, DrawBufferMode[] bufs);

            internal delegate void DrawElements(BeginMode mode, int count, DrawElementsType type, IntPtr indices);

            internal delegate void DrawElementsBaseVertex(BeginMode mode, int count, DrawElementsType type,
                IntPtr indices, int basevertex);

            internal delegate void DrawElementsIndirect(BeginMode mode, DrawElementsType type, IntPtr indirect);

            internal delegate void DrawElementsInstanced(BeginMode mode, int count, DrawElementsType type,
                IntPtr indices, int primcount);

            internal delegate void DrawElementsInstancedBaseInstance(BeginMode mode, int count, DrawElementsType type,
                IntPtr indices, int primcount, uint baseinstance);

            internal delegate void DrawElementsInstancedBaseVertex(BeginMode mode, int count, DrawElementsType type,
                IntPtr indices, int primcount, int basevertex);

            internal delegate void DrawElementsInstancedBaseVertexBaseInstance(BeginMode mode, int count,
                DrawElementsType type, IntPtr indices, int primcount, int basevertex, uint baseinstance);

            internal delegate void DrawRangeElements(BeginMode mode, uint start, uint end, int count,
                DrawElementsType type, IntPtr indices);

            internal delegate void DrawRangeElementsBaseVertex(BeginMode mode, uint start, uint end, int count,
                DrawElementsType type, IntPtr indices, int basevertex);

            internal delegate void DrawTransformFeedback(NvTransformFeedback2 mode, uint id);

            internal delegate void DrawTransformFeedbackInstanced(BeginMode mode, uint id, int primcount);

            internal delegate void DrawTransformFeedbackStream(NvTransformFeedback2 mode, uint id, uint stream);

            internal delegate void DrawTransformFeedbackStreamInstanced(BeginMode mode, uint id, uint stream,
                int primcount);

            internal delegate void Enable(EnableCap cap);

            internal delegate void Disable(EnableCap cap);

            internal delegate void Enablei(EnableCap cap, uint index);

            internal delegate void Disablei(EnableCap cap, uint index);

            internal delegate void EnableVertexAttribArray(uint index);

            internal delegate void DisableVertexAttribArray(uint index);

            internal delegate void EnableVertexArrayAttrib(uint vaobj, uint index);

            internal delegate void DisableVertexArrayAttrib(uint vaobj, uint index);

            internal delegate IntPtr FenceSync(ArbSync condition, uint flags);

            internal delegate void Finish();

            internal delegate void Flush();

            internal delegate void FlushMappedBufferRange(BufferTarget target, IntPtr offset, IntPtr length);

            internal delegate void FlushMappedNamedBufferRange(uint buffer, IntPtr offset, int length);

            internal delegate void FramebufferParameteri(FramebufferTarget target, FramebufferPName pname, int param);

            internal delegate void NamedFramebufferParameteri(uint framebuffer, FramebufferPName pname, int param);

            internal delegate void FramebufferRenderbuffer(FramebufferTarget target, FramebufferAttachment attachment,
                RenderbufferTarget renderbuffertarget, uint renderbuffer);

            internal delegate void NamedFramebufferRenderbuffer(uint framebuffer, FramebufferAttachment attachment,
                RenderbufferTarget renderbuffertarget, uint renderbuffer);

            internal delegate void FramebufferTexture(FramebufferTarget target, FramebufferAttachment attachment,
                uint texture, int level);

            internal delegate void FramebufferTexture1D(FramebufferTarget target, FramebufferAttachment attachment,
                TextureTarget textarget, uint texture, int level);

            internal delegate void FramebufferTexture2D(FramebufferTarget target, FramebufferAttachment attachment,
                TextureTarget textarget, uint texture, int level);

            internal delegate void FramebufferTexture3D(FramebufferTarget target, FramebufferAttachment attachment,
                TextureTarget textarget, uint texture, int level, int layer);

            internal delegate void NamedFramebufferTexture(uint framebuffer, FramebufferAttachment attachment,
                uint texture, int level);

            internal delegate void FramebufferTextureLayer(FramebufferTarget target, FramebufferAttachment attachment,
                uint texture, int level, int layer);

            internal delegate void NamedFramebufferTextureLayer(uint framebuffer, FramebufferAttachment attachment,
                uint texture, int level, int layer);

            internal delegate void FrontFace(FrontFaceDirection mode);

            internal delegate void GenBuffers(int n, [OutAttribute] uint[] buffers);

            internal delegate void GenerateMipmap(GenerateMipmapTarget target);

            internal delegate void GenerateTextureMipmap(uint texture);

            internal delegate void GenFramebuffers(int n, [OutAttribute] uint[] ids);

            internal delegate void GenProgramPipelines(int n, [OutAttribute] uint[] pipelines);

            internal delegate void GenQueries(int n, [OutAttribute] uint[] ids);

            internal delegate void GenRenderbuffers(int n, [OutAttribute] uint[] renderbuffers);

            internal delegate void GenSamplers(int n, [OutAttribute] uint[] samplers);

            internal delegate void GenTextures(int n, [OutAttribute] uint[] textures);

            internal delegate void GenTransformFeedbacks(int n, [OutAttribute] uint[] ids);

            internal delegate void GenVertexArrays(int n, [OutAttribute] uint[] arrays);

            internal delegate void GetBooleanv(GetPName pname, [OutAttribute] bool[] data);

            internal delegate void GetDoublev(GetPName pname, [OutAttribute] double[] data);

            internal delegate void GetFloatv(GetPName pname, [OutAttribute] float[] data);

            internal delegate void GetIntegerv(GetPName pname, [OutAttribute] int[] data);

            internal delegate void GetInteger64v(ArbSync pname, [OutAttribute] long[] data);

            internal delegate void GetBooleani_v(GetPName target, uint index, [OutAttribute] bool[] data);

            internal delegate void GetIntegeri_v(GetPName target, uint index, [OutAttribute] int[] data);

            internal delegate void GetFloati_v(GetPName target, uint index, [OutAttribute] float[] data);

            internal delegate void GetDoublei_v(GetPName target, uint index, [OutAttribute] double[] data);

            internal delegate void GetInteger64i_v(GetPName target, uint index, [OutAttribute] long[] data);

            internal delegate void GetActiveAtomicCounterBufferiv(uint program, uint bufferIndex,
                AtomicCounterParameterName pname, [OutAttribute] int[] @params);

            internal delegate void GetActiveAttrib(uint program, uint index, int bufSize, [OutAttribute] int[] length,
                [OutAttribute] int[] size, [OutAttribute] ActiveAttribType[] type, [OutAttribute] StringBuilder name);

            internal delegate void GetActiveSubroutineName(uint program, ShaderType shadertype, uint index, int bufsize,
                [OutAttribute] int[] length, [OutAttribute] StringBuilder name);

            internal delegate void GetActiveSubroutineUniformiv(uint program, ShaderType shadertype, uint index,
                SubroutineParameterName pname, [OutAttribute] int[] values);

            internal delegate void GetActiveSubroutineUniformName(uint program, ShaderType shadertype, uint index,
                int bufsize, [OutAttribute] int[] length, [OutAttribute] StringBuilder name);

            internal delegate void GetActiveUniform(uint program, uint index, int bufSize, [OutAttribute] int[] length,
                [OutAttribute] int[] size, [OutAttribute] ActiveUniformType[] type, [OutAttribute] StringBuilder name);

            internal delegate void GetActiveUniformBlockiv(uint program, uint uniformBlockIndex,
                ActiveUniformBlockParameter pname, [OutAttribute] int[] @params);

            internal delegate void GetActiveUniformBlockName(uint program, uint uniformBlockIndex, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] StringBuilder uniformBlockName);

            internal delegate void GetActiveUniformName(uint program, uint uniformIndex, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] StringBuilder uniformName);

            internal delegate void GetActiveUniformsiv(uint program, int uniformCount,
                [OutAttribute] uint[] uniformIndices, ActiveUniformType pname, [OutAttribute] int[] @params);

            internal delegate void GetAttachedShaders(uint program, int maxCount, [OutAttribute] int[] count,
                [OutAttribute] uint[] shaders);

            internal delegate int GetAttribLocation(uint program, string name);

            internal delegate void GetBufferParameteriv(BufferTarget target, BufferParameterName value,
                [OutAttribute] int[] data);

            internal delegate void GetBufferParameteri64v(BufferTarget target, BufferParameterName value,
                [OutAttribute] long[] data);

            internal delegate void GetNamedBufferParameteriv(uint buffer, BufferParameterName pname,
                [OutAttribute] int[] @params);

            internal delegate void GetNamedBufferParameteri64v(uint buffer, BufferParameterName pname,
                [OutAttribute] long[] @params);

            internal delegate void GetBufferPointerv(BufferTarget target, BufferPointer pname,
                [OutAttribute] IntPtr @params);

            internal delegate void GetNamedBufferPointerv(uint buffer, BufferPointer pname,
                [OutAttribute] IntPtr @params);

            internal delegate void GetBufferSubData(BufferTarget target, IntPtr offset, IntPtr size,
                [OutAttribute] IntPtr data);

            internal delegate void GetNamedBufferSubData(uint buffer, IntPtr offset, int size,
                [OutAttribute] IntPtr data);

            internal delegate void GetCompressedTexImage(TextureTarget target, int level, [OutAttribute] IntPtr pixels);

            internal delegate void GetnCompressedTexImage(TextureTarget target, int level, int bufSize,
                [OutAttribute] IntPtr pixels);

            internal delegate void GetCompressedTextureImage(uint texture, int level, int bufSize,
                [OutAttribute] IntPtr pixels);

            internal delegate void GetCompressedTextureSubImage(uint texture, int level, int xoffset, int yoffset,
                int zoffset, int width, int height, int depth, int bufSize, [OutAttribute] IntPtr pixels);

            internal delegate ErrorCode GetError();

            internal delegate int GetFragDataIndex(uint program, string name);

            internal delegate int GetFragDataLocation(uint program, string name);

            internal delegate void GetFramebufferAttachmentParameteriv(FramebufferTarget target,
                FramebufferAttachment attachment, FramebufferParameterName pname, [OutAttribute] int[] @params);

            internal delegate void GetNamedFramebufferAttachmentParameteriv(uint framebuffer,
                FramebufferAttachment attachment, FramebufferParameterName pname, [OutAttribute] int[] @params);

            internal delegate void GetFramebufferParameteriv(FramebufferTarget target, FramebufferPName pname,
                [OutAttribute] int[] @params);

            internal delegate void GetNamedFramebufferParameteriv(uint framebuffer, FramebufferPName pname,
                [OutAttribute] int[] param);

            internal delegate GraphicResetStatus GetGraphicsResetStatus();

            internal delegate void GetInternalformativ(TextureTarget target, PixelInternalFormat internalFormat,
                GetPName pname, int bufSize, [OutAttribute] int[] @params);

            internal delegate void GetInternalformati64v(TextureTarget target, PixelInternalFormat internalFormat,
                GetPName pname, int bufSize, [OutAttribute] long[] @params);

            internal delegate void GetMultisamplefv(GetMultisamplePName pname, uint index, [OutAttribute] float[] val);

            internal delegate void GetObjectLabel(OpenGL.ObjectLabel identifier, uint name, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] StringBuilder label);

            internal delegate void GetObjectPtrLabel([OutAttribute] IntPtr ptr, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] StringBuilder label);

            internal delegate void GetPointerv(GetPointerParameter pname, [OutAttribute] IntPtr @params);

            internal delegate void GetProgramiv(uint program, ProgramParameter pname, [OutAttribute] int[] @params);

            internal delegate void GetProgramBinary(uint program, int bufsize, [OutAttribute] int[] length,
                [OutAttribute] int[] binaryFormat, [OutAttribute] IntPtr binary);

            internal delegate void GetProgramInfoLog(uint program, int maxLength, [OutAttribute] int[] length,
                [OutAttribute] StringBuilder infoLog);

            internal delegate void GetProgramInterfaceiv(uint program, ProgramInterface programInterface,
                ProgramInterfaceParameterName pname, [OutAttribute] int[] @params);

            internal delegate void GetProgramPipelineiv(uint pipeline, int pname, [OutAttribute] int[] @params);

            internal delegate void GetProgramPipelineInfoLog(uint pipeline, int bufSize, [OutAttribute] int[] length,
                [OutAttribute] StringBuilder infoLog);

            internal delegate void GetProgramResourceiv(uint program, ProgramInterface programInterface, uint index,
                int propCount, [OutAttribute] ProgramResourceParameterName[] props, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] int[] @params);

            internal delegate uint
                GetProgramResourceIndex(uint program, ProgramInterface programInterface, string name);

            internal delegate int GetProgramResourceLocation(uint program, ProgramInterface programInterface,
                string name);

            internal delegate int GetProgramResourceLocationIndex(uint program, ProgramInterface programInterface,
                string name);

            internal delegate void GetProgramResourceName(uint program, ProgramInterface programInterface, uint index,
                int bufSize, [OutAttribute] int[] length, [OutAttribute] StringBuilder name);

            internal delegate void GetProgramStageiv(uint program, ShaderType shadertype,
                ProgramStageParameterName pname, [OutAttribute] int[] values);

            internal delegate void GetQueryIndexediv(QueryTarget target, uint index, GetQueryParam pname,
                [OutAttribute] int[] @params);

            internal delegate void GetQueryiv(QueryTarget target, GetQueryParam pname, [OutAttribute] int[] @params);

            internal delegate void GetQueryObjectiv(uint id, GetQueryObjectParam pname, [OutAttribute] int[] @params);

            internal delegate void GetQueryObjectuiv(uint id, GetQueryObjectParam pname, [OutAttribute] uint[] @params);

            internal delegate void GetQueryObjecti64v(uint id, GetQueryObjectParam pname,
                [OutAttribute] long[] @params);

            internal delegate void GetQueryObjectui64v(uint id, GetQueryObjectParam pname,
                [OutAttribute] ulong[] @params);

            internal delegate void GetRenderbufferParameteriv(RenderbufferTarget target,
                RenderbufferParameterName pname, [OutAttribute] int[] @params);

            internal delegate void GetNamedRenderbufferParameteriv(uint renderbuffer, RenderbufferParameterName pname,
                [OutAttribute] int[] @params);

            internal delegate void GetSamplerParameterfv(uint sampler, TextureParameterName pname,
                [OutAttribute] float[] @params);

            internal delegate void GetSamplerParameteriv(uint sampler, TextureParameterName pname,
                [OutAttribute] int[] @params);

            internal delegate void GetSamplerParameterIiv(uint sampler, TextureParameterName pname,
                [OutAttribute] int[] @params);

            internal delegate void GetSamplerParameterIuiv(uint sampler, TextureParameterName pname,
                [OutAttribute] uint[] @params);

            internal delegate void GetShaderiv(uint shader, ShaderParameter pname, [OutAttribute] int[] @params);

            internal delegate void GetShaderInfoLog(uint shader, int maxLength, [OutAttribute] int[] length,
                [OutAttribute] StringBuilder infoLog);

            internal delegate void GetShaderPrecisionFormat(ShaderType shaderType, int precisionType,
                [OutAttribute] int[] range, [OutAttribute] int[] precision);

            internal delegate void GetShaderSource(uint shader, int bufSize, [OutAttribute] int[] length,
                [OutAttribute] StringBuilder source);

            internal delegate IntPtr GetString(StringName name);

            internal delegate IntPtr GetStringi(StringName name, uint index);

            internal delegate uint GetSubroutineIndex(uint program, ShaderType shadertype, string name);

            internal delegate int GetSubroutineUniformLocation(uint program, ShaderType shadertype, string name);

            internal delegate void GetSynciv(IntPtr sync, ArbSync pname, int bufSize, [OutAttribute] int[] length,
                [OutAttribute] int[] values);

            internal delegate void GetTexImage(TextureTarget target, int level, PixelFormat format, PixelType type,
                [OutAttribute] IntPtr pixels);

            internal delegate void GetnTexImage(TextureTarget target, int level, PixelFormat format, PixelType type,
                int bufSize, [OutAttribute] IntPtr pixels);

            internal delegate void GetTextureImage(uint texture, int level, PixelFormat format, PixelType type,
                int bufSize, [OutAttribute] IntPtr pixels);

            internal delegate void GetTexLevelParameterfv(GetPName target, int level, GetTextureLevelParameter pname,
                [OutAttribute] float[] @params);

            internal delegate void GetTexLevelParameteriv(TextureTarget target, int level,
                GetTextureLevelParameter pname, [OutAttribute] int[] @params);

            internal delegate void GetTextureLevelParameterfv(uint texture, int level, GetTextureLevelParameter pname,
                [OutAttribute] float[] @params);

            internal delegate void GetTextureLevelParameteriv(uint texture, int level, GetTextureLevelParameter pname,
                [OutAttribute] int[] @params);

            internal delegate void GetTexParameterfv(TextureTarget target, GetTextureParameter pname,
                [OutAttribute] float[] @params);

            internal delegate void GetTexParameteriv(TextureTarget target, GetTextureParameter pname,
                [OutAttribute] int[] @params);

            internal delegate void GetTexParameterIiv(TextureTarget target, GetTextureParameter pname,
                [OutAttribute] int[] @params);

            internal delegate void GetTexParameterIuiv(TextureTarget target, GetTextureParameter pname,
                [OutAttribute] uint[] @params);

            internal delegate void GetTextureParameterfv(uint texture, GetTextureParameter pname,
                [OutAttribute] float[] @params);

            internal delegate void GetTextureParameteriv(uint texture, GetTextureParameter pname,
                [OutAttribute] int[] @params);

            internal delegate void GetTextureParameterIiv(uint texture, GetTextureParameter pname,
                [OutAttribute] int[] @params);

            internal delegate void GetTextureParameterIuiv(uint texture, GetTextureParameter pname,
                [OutAttribute] uint[] @params);

            internal delegate void GetTextureSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset,
                int width, int height, int depth, PixelFormat format, PixelType type, int bufSize,
                [OutAttribute] IntPtr pixels);

            internal delegate void GetTransformFeedbackiv(uint xfb, TransformFeedbackParameterName pname,
                [OutAttribute] int[] param);

            internal delegate void GetTransformFeedbacki_v(uint xfb, TransformFeedbackParameterName pname, uint index,
                [OutAttribute] int[] param);

            internal delegate void GetTransformFeedbacki64_v(uint xfb, TransformFeedbackParameterName pname, uint index,
                [OutAttribute] long[] param);

            internal delegate void GetTransformFeedbackVarying(uint program, uint index, int bufSize,
                [OutAttribute] int[] length, [OutAttribute] int[] size, [OutAttribute] ActiveAttribType[] type,
                [OutAttribute] StringBuilder name);

            internal delegate void GetUniformfv(uint program, int location, [OutAttribute] float[] @params);

            internal delegate void GetUniformiv(uint program, int location, [OutAttribute] int[] @params);

            internal delegate void GetUniformuiv(uint program, int location, [OutAttribute] uint[] @params);

            internal delegate void GetUniformdv(uint program, int location, [OutAttribute] double[] @params);

            internal delegate void GetnUniformfv(uint program, int location, int bufSize,
                [OutAttribute] float[] @params);

            internal delegate void GetnUniformiv(uint program, int location, int bufSize, [OutAttribute] int[] @params);

            internal delegate void GetnUniformuiv(uint program, int location, int bufSize,
                [OutAttribute] uint[] @params);

            internal delegate void GetnUniformdv(uint program, int location, int bufSize,
                [OutAttribute] double[] @params);

            internal delegate uint GetUniformBlockIndex(uint program, string uniformBlockName);

            internal delegate void GetUniformIndices(uint program, int uniformCount, string uniformNames,
                [OutAttribute] uint[] uniformIndices);

            internal delegate int GetUniformLocation(uint program, string name);

            internal delegate void GetUniformSubroutineuiv(ShaderType shadertype, int location,
                [OutAttribute] uint[] values);

            internal delegate void GetVertexArrayIndexed64iv(uint vaobj, uint index, VertexAttribParameter pname,
                [OutAttribute] long[] param);

            internal delegate void GetVertexArrayIndexediv(uint vaobj, uint index, VertexAttribParameter pname,
                [OutAttribute] int[] param);

            internal delegate void GetVertexArrayiv(uint vaobj, VertexAttribParameter pname,
                [OutAttribute] int[] param);

            internal delegate void GetVertexAttribdv(uint index, VertexAttribParameter pname,
                [OutAttribute] double[] @params);

            internal delegate void GetVertexAttribfv(uint index, VertexAttribParameter pname,
                [OutAttribute] float[] @params);

            internal delegate void GetVertexAttribiv(uint index, VertexAttribParameter pname,
                [OutAttribute] int[] @params);

            internal delegate void GetVertexAttribIiv(uint index, VertexAttribParameter pname,
                [OutAttribute] int[] @params);

            internal delegate void GetVertexAttribIuiv(uint index, VertexAttribParameter pname,
                [OutAttribute] uint[] @params);

            internal delegate void GetVertexAttribLdv(uint index, VertexAttribParameter pname,
                [OutAttribute] double[] @params);

            internal delegate void GetVertexAttribPointerv(uint index, VertexAttribPointerParameter pname,
                [OutAttribute] IntPtr pointer);

            internal delegate void Hint(HintTarget target, HintMode mode);

            internal delegate void InvalidateBufferData(uint buffer);

            internal delegate void InvalidateBufferSubData(uint buffer, IntPtr offset, IntPtr length);

            internal delegate void InvalidateFramebuffer(FramebufferTarget target, int numAttachments,
                FramebufferAttachment[] attachments);

            internal delegate void InvalidateNamedFramebufferData(uint framebuffer, int numAttachments,
                FramebufferAttachment[] attachments);

            internal delegate void InvalidateSubFramebuffer(FramebufferTarget target, int numAttachments,
                FramebufferAttachment[] attachments, int x, int y, int width, int height);

            internal delegate void InvalidateNamedFramebufferSubData(uint framebuffer, int numAttachments,
                FramebufferAttachment[] attachments, int x, int y, int width, int height);

            internal delegate void InvalidateTexImage(uint texture, int level);

            internal delegate void InvalidateTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset,
                int width, int height, int depth);

            internal delegate bool IsBuffer(uint buffer);

            internal delegate bool IsEnabled(EnableCap cap);

            internal delegate bool IsEnabledi(EnableCap cap, uint index);

            internal delegate bool IsFramebuffer(uint framebuffer);

            internal delegate bool IsProgram(uint program);

            internal delegate bool IsProgramPipeline(uint pipeline);

            internal delegate bool IsQuery(uint id);

            internal delegate bool IsRenderbuffer(uint renderbuffer);

            internal delegate bool IsSampler(uint id);

            internal delegate bool IsShader(uint shader);

            internal delegate bool IsSync(IntPtr sync);

            internal delegate bool IsTexture(uint texture);

            internal delegate bool IsTransformFeedback(uint id);

            internal delegate bool IsVertexArray(uint array);

            internal delegate void LineWidth(float width);

            internal delegate void LinkProgram(uint program);

            internal delegate void LogicOp(OpenGL.LogicOp opcode);

            internal delegate IntPtr MapBuffer(BufferTarget target, BufferAccess access);

            internal delegate IntPtr MapNamedBuffer(uint buffer, BufferAccess access);

            internal delegate IntPtr MapBufferRange(BufferTarget target, IntPtr offset, IntPtr length,
                BufferAccessMask access);

            internal delegate IntPtr MapNamedBufferRange(uint buffer, IntPtr offset, int length, uint access);

            internal delegate void MemoryBarrier(uint barriers);

            internal delegate void MemoryBarrierByRegion(uint barriers);

            internal delegate void MinSampleShading(float value);

            internal delegate void MultiDrawArrays(BeginMode mode, int[] first, int[] count, int drawcount);

            internal delegate void MultiDrawArraysIndirect(BeginMode mode, IntPtr indirect, int drawcount, int stride);

            internal delegate void MultiDrawElements(BeginMode mode, int[] count, DrawElementsType type, IntPtr indices,
                int drawcount);

            internal delegate void MultiDrawElementsBaseVertex(BeginMode mode, int[] count, DrawElementsType type,
                IntPtr indices, int drawcount, int[] basevertex);

            internal delegate void MultiDrawElementsIndirect(BeginMode mode, DrawElementsType type, IntPtr indirect,
                int drawcount, int stride);

            internal delegate void ObjectLabel(OpenGL.ObjectLabel identifier, uint name, int length, string label);

            internal delegate void ObjectPtrLabel(IntPtr ptr, int length, string label);

            internal delegate void PatchParameteri(int pname, int value);

            internal delegate void PatchParameterfv(int pname, float[] values);

            internal delegate void PixelStoref(PixelStoreParameter pname, float param);

            internal delegate void PixelStorei(PixelStoreParameter pname, int param);

            internal delegate void PointParameterf(PointParameterName pname, float param);

            internal delegate void PointParameteri(PointParameterName pname, int param);

            internal delegate void PointParameterfv(PointParameterName pname, float[] @params);

            internal delegate void PointParameteriv(PointParameterName pname, int[] @params);

            internal delegate void PointSize(float size);

            internal delegate void PolygonMode(MaterialFace face, OpenGL.PolygonMode mode);

            internal delegate void PolygonOffset(float factor, float units);

            internal delegate void PrimitiveRestartIndex(uint index);

            internal delegate void ProgramBinary(uint program, int binaryFormat, IntPtr binary, int length);

            internal delegate void ProgramParameteri(uint program, Version32 pname, int value);

            internal delegate void ProgramUniform1f(uint program, int location, float v0);

            internal delegate void ProgramUniform2f(uint program, int location, float v0, float v1);

            internal delegate void ProgramUniform3f(uint program, int location, float v0, float v1, float v2);

            internal delegate void ProgramUniform4f(uint program, int location, float v0, float v1, float v2, float v3);

            internal delegate void ProgramUniform1i(uint program, int location, int v0);

            internal delegate void ProgramUniform2i(uint program, int location, int v0, int v1);

            internal delegate void ProgramUniform3i(uint program, int location, int v0, int v1, int v2);

            internal delegate void ProgramUniform4i(uint program, int location, int v0, int v1, int v2, int v3);

            internal delegate void ProgramUniform1ui(uint program, int location, uint v0);

            internal delegate void ProgramUniform2ui(uint program, int location, int v0, uint v1);

            internal delegate void ProgramUniform3ui(uint program, int location, int v0, int v1, uint v2);

            internal delegate void ProgramUniform4ui(uint program, int location, int v0, int v1, int v2, uint v3);

            internal delegate void ProgramUniform1fv(uint program, int location, int count, float[] value);

            internal delegate void ProgramUniform2fv(uint program, int location, int count, float[] value);

            internal delegate void ProgramUniform3fv(uint program, int location, int count, float[] value);

            internal delegate void ProgramUniform4fv(uint program, int location, int count, float[] value);

            internal delegate void ProgramUniform1iv(uint program, int location, int count, int[] value);

            internal delegate void ProgramUniform2iv(uint program, int location, int count, int[] value);

            internal delegate void ProgramUniform3iv(uint program, int location, int count, int[] value);

            internal delegate void ProgramUniform4iv(uint program, int location, int count, int[] value);

            internal delegate void ProgramUniform1uiv(uint program, int location, int count, uint[] value);

            internal delegate void ProgramUniform2uiv(uint program, int location, int count, uint[] value);

            internal delegate void ProgramUniform3uiv(uint program, int location, int count, uint[] value);

            internal delegate void ProgramUniform4uiv(uint program, int location, int count, uint[] value);

            internal delegate void ProgramUniformMatrix2fv(uint program, int location, int count, bool transpose,
                float[] value);

            internal delegate void ProgramUniformMatrix3fv(uint program, int location, int count, bool transpose,
                float[] value);

            internal delegate void ProgramUniformMatrix4fv(uint program, int location, int count, bool transpose,
                float[] value);

            internal delegate void ProgramUniformMatrix2x3fv(uint program, int location, int count, bool transpose,
                float[] value);

            internal delegate void ProgramUniformMatrix3x2fv(uint program, int location, int count, bool transpose,
                float[] value);

            internal delegate void ProgramUniformMatrix2x4fv(uint program, int location, int count, bool transpose,
                float[] value);

            internal delegate void ProgramUniformMatrix4x2fv(uint program, int location, int count, bool transpose,
                float[] value);

            internal delegate void ProgramUniformMatrix3x4fv(uint program, int location, int count, bool transpose,
                float[] value);

            internal delegate void ProgramUniformMatrix4x3fv(uint program, int location, int count, bool transpose,
                float[] value);

            internal delegate void ProvokingVertex(ProvokingVertexMode provokeMode);

            internal delegate void QueryCounter(uint id, QueryTarget target);

            internal delegate void ReadBuffer(ReadBufferMode mode);

            internal delegate void NamedFramebufferReadBuffer(ReadBufferMode framebuffer, BeginMode mode);

            internal delegate void ReadPixels(int x, int y, int width, int height, PixelFormat format, PixelType type,
                int[] data);

            internal delegate void ReadnPixels(int x, int y, int width, int height, PixelFormat format, PixelType type,
                int bufSize, int[] data);

            internal delegate void RenderbufferStorage(RenderbufferTarget target,
                OpenGL.RenderbufferStorage internalFormat, int width, int height);

            internal delegate void NamedRenderbufferStorage(uint renderbuffer,
                OpenGL.RenderbufferStorage internalFormat, int width, int height);

            internal delegate void RenderbufferStorageMultisample(RenderbufferTarget target, int samples,
                OpenGL.RenderbufferStorage internalFormat, int width, int height);

            internal delegate void NamedRenderbufferStorageMultisample(uint renderbuffer, int samples,
                OpenGL.RenderbufferStorage internalFormat, int width, int height);

            internal delegate void SampleCoverage(float value, bool invert);

            internal delegate void SampleMaski(uint maskNumber, uint mask);

            internal delegate void SamplerParameterf(uint sampler, TextureParameterName pname, float param);

            internal delegate void SamplerParameteri(uint sampler, TextureParameterName pname, int param);

            internal delegate void SamplerParameterfv(uint sampler, TextureParameterName pname, float[] @params);

            internal delegate void SamplerParameteriv(uint sampler, TextureParameterName pname, int[] @params);

            internal delegate void SamplerParameterIiv(uint sampler, TextureParameterName pname, int[] @params);

            internal delegate void SamplerParameterIuiv(uint sampler, TextureParameterName pname, uint[] @params);

            internal delegate void Scissor(int x, int y, int width, int height);

            internal delegate void ScissorArrayv(uint first, int count, int[] v);

            internal delegate void ScissorIndexed(uint index, int left, int bottom, int width, int height);

            internal delegate void ScissorIndexedv(uint index, int[] v);

            internal delegate void ShaderBinary(int count, uint[] shaders, int binaryFormat, IntPtr binary, int length);

            internal delegate void ShaderSource(uint shader, int count, string[] @string, int[] length);

            internal delegate void ShaderStorageBlockBinding(uint program, uint storageBlockIndex,
                uint storageBlockBinding);

            internal delegate void StencilFunc(StencilFunction func, int @ref, uint mask);

            internal delegate void StencilFuncSeparate(StencilFace face, StencilFunction func, int @ref, uint mask);

            internal delegate void StencilMask(uint mask);

            internal delegate void StencilMaskSeparate(StencilFace face, uint mask);

            internal delegate void StencilOp(OpenGL.StencilOp sfail, OpenGL.StencilOp dpfail, OpenGL.StencilOp dppass);

            internal delegate void StencilOpSeparate(StencilFace face, OpenGL.StencilOp sfail, OpenGL.StencilOp dpfail,
                OpenGL.StencilOp dppass);

            internal delegate void TexBuffer(TextureBufferTarget target, SizedInternalFormat internalFormat,
                uint buffer);

            internal delegate void TextureBuffer(uint texture, SizedInternalFormat internalFormat, uint buffer);

            internal delegate void TexBufferRange(BufferTarget target, SizedInternalFormat internalFormat, uint buffer,
                IntPtr offset, IntPtr size);

            internal delegate void TextureBufferRange(uint texture, SizedInternalFormat internalFormat, uint buffer,
                IntPtr offset, int size);

            internal delegate void TexImage1D(TextureTarget target, int level, PixelInternalFormat internalFormat,
                int width, int border, PixelFormat format, PixelType type, IntPtr data);

            internal delegate void TexImage2D(TextureTarget target, int level, PixelInternalFormat internalFormat,
                int width, int height, int border, PixelFormat format, PixelType type, IntPtr data);

            internal delegate void TexImage2DMultisample(TextureTargetMultisample target, int samples,
                PixelInternalFormat internalFormat, int width, int height, bool fixedsamplelocations);

            internal delegate void TexImage3D(TextureTarget target, int level, PixelInternalFormat internalFormat,
                int width, int height, int depth, int border, PixelFormat format, PixelType type, IntPtr data);

            internal delegate void TexImage3DMultisample(TextureTargetMultisample target, int samples,
                PixelInternalFormat internalFormat, int width, int height, int depth, bool fixedsamplelocations);

            internal delegate void TexParameterf(TextureTarget target, TextureParameterName pname, float param);

            internal delegate void TexParameteri(TextureTarget target, TextureParameterName pname, int param);

            internal delegate void TextureParameterf(uint texture, TextureParameter pname, float param);

            internal delegate void TextureParameteri(uint texture, TextureParameter pname, int param);

            internal delegate void TexParameterfv(TextureTarget target, TextureParameterName pname, float[] @params);

            internal delegate void TexParameteriv(TextureTarget target, TextureParameterName pname, int[] @params);

            internal delegate void TexParameterIiv(TextureTarget target, TextureParameterName pname, int[] @params);

            internal delegate void TexParameterIuiv(TextureTarget target, TextureParameterName pname, uint[] @params);

            internal delegate void TextureParameterfv(uint texture, TextureParameter pname, float[] paramtexture);

            internal delegate void TextureParameteriv(uint texture, TextureParameter pname, int[] param);

            internal delegate void TextureParameterIiv(uint texture, TextureParameter pname, int[] @params);

            internal delegate void TextureParameterIuiv(uint texture, TextureParameter pname, uint[] @params);

            internal delegate void TexStorage1D(TextureTarget target, int levels, SizedInternalFormat internalFormat,
                int width);

            internal delegate void TextureStorage1D(uint texture, int levels, SizedInternalFormat internalFormat,
                int width);

            internal delegate void TexStorage2D(TextureTarget target, int levels, SizedInternalFormat internalFormat,
                int width, int height);

            internal delegate void TextureStorage2D(uint texture, int levels, SizedInternalFormat internalFormat,
                int width, int height);

            internal delegate void TexStorage2DMultisample(TextureTarget target, int samples,
                SizedInternalFormat internalFormat, int width, int height, bool fixedsamplelocations);

            internal delegate void TextureStorage2DMultisample(uint texture, int samples,
                SizedInternalFormat internalFormat, int width, int height, bool fixedsamplelocations);

            internal delegate void TexStorage3D(TextureTarget target, int levels, SizedInternalFormat internalFormat,
                int width, int height, int depth);

            internal delegate void TextureStorage3D(uint texture, int levels, SizedInternalFormat internalFormat,
                int width, int height, int depth);

            internal delegate void TexStorage3DMultisample(TextureTarget target, int samples,
                SizedInternalFormat internalFormat, int width, int height, int depth, bool fixedsamplelocations);

            internal delegate void TextureStorage3DMultisample(uint texture, int samples,
                SizedInternalFormat internalFormat, int width, int height, int depth, bool fixedsamplelocations);

            internal delegate void TexSubImage1D(TextureTarget target, int level, int xoffset, int width,
                PixelFormat format, PixelType type, IntPtr pixels);

            internal delegate void TextureSubImage1D(uint texture, int level, int xoffset, int width,
                PixelFormat format, PixelType type, IntPtr pixels);

            internal delegate void TexSubImage2D(TextureTarget target, int level, int xoffset, int yoffset, int width,
                int height, PixelFormat format, PixelType type, IntPtr pixels);

            internal delegate void TextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int width,
                int height, PixelFormat format, PixelType type, IntPtr pixels);

            internal delegate void TexSubImage3D(TextureTarget target, int level, int xoffset, int yoffset, int zoffset,
                int width, int height, int depth, PixelFormat format, PixelType type, IntPtr pixels);

            internal delegate void TextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset,
                int width, int height, int depth, PixelFormat format, PixelType type, IntPtr pixels);

            internal delegate void TextureBarrier();

            internal delegate void TextureView(uint texture, TextureTarget target, uint origtexture,
                PixelInternalFormat internalFormat, uint minlevel, uint numlevels, uint minlayer, uint numlayers);

            internal delegate void TransformFeedbackBufferBase(uint xfb, uint index, uint buffer);

            internal delegate void TransformFeedbackBufferRange(uint xfb, uint index, uint buffer, IntPtr offset,
                int size);

            internal delegate void TransformFeedbackVaryings(uint program, int count, string[] varyings,
                TransformFeedbackMode bufferMode);

            internal delegate void Uniform1f(int location, float v0);

            internal delegate void Uniform2f(int location, float v0, float v1);

            internal delegate void Uniform3f(int location, float v0, float v1, float v2);

            internal delegate void Uniform4f(int location, float v0, float v1, float v2, float v3);

            internal delegate void Uniform1i(int location, int v0);

            internal delegate void Uniform2i(int location, int v0, int v1);

            internal delegate void Uniform3i(int location, int v0, int v1, int v2);

            internal delegate void Uniform4i(int location, int v0, int v1, int v2, int v3);

            internal delegate void Uniform1ui(int location, uint v0);

            internal delegate void Uniform2ui(int location, uint v0, uint v1);

            internal delegate void Uniform3ui(int location, uint v0, uint v1, uint v2);

            internal delegate void Uniform4ui(int location, uint v0, uint v1, uint v2, uint v3);

            internal delegate void Uniform1fv(int location, int count, float[] value);

            internal delegate void Uniform2fv(int location, int count, float[] value);

            internal delegate void Uniform3fv(int location, int count, float[] value);

            internal delegate void Uniform4fv(int location, int count, float[] value);

            internal delegate void Uniform1iv(int location, int count, int[] value);

            internal delegate void Uniform2iv(int location, int count, int[] value);

            internal delegate void Uniform3iv(int location, int count, int[] value);

            internal delegate void Uniform4iv(int location, int count, int[] value);

            internal delegate void Uniform1uiv(int location, int count, uint[] value);

            internal delegate void Uniform2uiv(int location, int count, uint[] value);

            internal delegate void Uniform3uiv(int location, int count, uint[] value);

            internal delegate void Uniform4uiv(int location, int count, uint[] value);

            internal delegate void UniformMatrix2fv(int location, int count, bool transpose, float[] value);

            internal delegate void UniformMatrix3fv(int location, int count, bool transpose, float[] value);

            internal delegate void UniformMatrix4fv(int location, int count, bool transpose, float[] value);

            internal delegate void UniformMatrix2x3fv(int location, int count, bool transpose, float[] value);

            internal delegate void UniformMatrix3x2fv(int location, int count, bool transpose, float[] value);

            internal delegate void UniformMatrix2x4fv(int location, int count, bool transpose, float[] value);

            internal delegate void UniformMatrix4x2fv(int location, int count, bool transpose, float[] value);

            internal delegate void UniformMatrix3x4fv(int location, int count, bool transpose, float[] value);

            internal delegate void UniformMatrix4x3fv(int location, int count, bool transpose, float[] value);

            internal delegate void UniformBlockBinding(uint program, uint uniformBlockIndex, uint uniformBlockBinding);

            internal delegate void UniformSubroutinesuiv(ShaderType shadertype, int count, uint[] indices);

            internal delegate bool UnmapBuffer(BufferTarget target);

            internal delegate bool UnmapNamedBuffer(uint buffer);

            internal delegate void UseProgram(uint program);

            internal delegate void UseProgramStages(uint pipeline, uint stages, uint program);

            internal delegate void ValidateProgram(uint program);

            internal delegate void ValidateProgramPipeline(uint pipeline);

            internal delegate void VertexArrayElementBuffer(uint vaobj, uint buffer);

            internal delegate void VertexAttrib1f(uint index, float v0);

            internal delegate void VertexAttrib1s(uint index, short v0);

            internal delegate void VertexAttrib1d(uint index, double v0);

            internal delegate void VertexAttribI1i(uint index, int v0);

            internal delegate void VertexAttribI1ui(uint index, uint v0);

            internal delegate void VertexAttrib2f(uint index, float v0, float v1);

            internal delegate void VertexAttrib2s(uint index, short v0, short v1);

            internal delegate void VertexAttrib2d(uint index, double v0, double v1);

            internal delegate void VertexAttribI2i(uint index, int v0, int v1);

            internal delegate void VertexAttribI2ui(uint index, uint v0, uint v1);

            internal delegate void VertexAttrib3f(uint index, float v0, float v1, float v2);

            internal delegate void VertexAttrib3s(uint index, short v0, short v1, short v2);

            internal delegate void VertexAttrib3d(uint index, double v0, double v1, double v2);

            internal delegate void VertexAttribI3i(uint index, int v0, int v1, int v2);

            internal delegate void VertexAttribI3ui(uint index, uint v0, uint v1, uint v2);

            internal delegate void VertexAttrib4f(uint index, float v0, float v1, float v2, float v3);

            internal delegate void VertexAttrib4s(uint index, short v0, short v1, short v2, short v3);

            internal delegate void VertexAttrib4d(uint index, double v0, double v1, double v2, double v3);

            internal delegate void VertexAttrib4Nub(uint index, byte v0, byte v1, byte v2, byte v3);

            internal delegate void VertexAttribI4i(uint index, int v0, int v1, int v2, int v3);

            internal delegate void VertexAttribI4ui(uint index, uint v0, uint v1, uint v2, uint v3);

            internal delegate void VertexAttribL1d(uint index, double v0);

            internal delegate void VertexAttribL2d(uint index, double v0, double v1);

            internal delegate void VertexAttribL3d(uint index, double v0, double v1, double v2);

            internal delegate void VertexAttribL4d(uint index, double v0, double v1, double v2, double v3);

            internal delegate void VertexAttrib1fv(uint index, float[] v);

            internal delegate void VertexAttrib1sv(uint index, short[] v);

            internal delegate void VertexAttrib1dv(uint index, double[] v);

            internal delegate void VertexAttribI1iv(uint index, int[] v);

            internal delegate void VertexAttribI1uiv(uint index, uint[] v);

            internal delegate void VertexAttrib2fv(uint index, float[] v);

            internal delegate void VertexAttrib2sv(uint index, short[] v);

            internal delegate void VertexAttrib2dv(uint index, double[] v);

            internal delegate void VertexAttribI2iv(uint index, int[] v);

            internal delegate void VertexAttribI2uiv(uint index, uint[] v);

            internal delegate void VertexAttrib3fv(uint index, float[] v);

            internal delegate void VertexAttrib3sv(uint index, short[] v);

            internal delegate void VertexAttrib3dv(uint index, double[] v);

            internal delegate void VertexAttribI3iv(uint index, int[] v);

            internal delegate void VertexAttribI3uiv(uint index, uint[] v);

            internal delegate void VertexAttrib4fv(uint index, float[] v);

            internal delegate void VertexAttrib4sv(uint index, short[] v);

            internal delegate void VertexAttrib4dv(uint index, double[] v);

            internal delegate void VertexAttrib4iv(uint index, int[] v);

            internal delegate void VertexAttrib4bv(uint index, sbyte[] v);

            internal delegate void VertexAttrib4ubv(uint index, byte[] v);

            internal delegate void VertexAttrib4usv(uint index, ushort[] v);

            internal delegate void VertexAttrib4uiv(uint index, uint[] v);

            internal delegate void VertexAttrib4Nbv(uint index, sbyte[] v);

            internal delegate void VertexAttrib4Nsv(uint index, short[] v);

            internal delegate void VertexAttrib4Niv(uint index, int[] v);

            internal delegate void VertexAttrib4Nubv(uint index, byte[] v);

            internal delegate void VertexAttrib4Nusv(uint index, ushort[] v);

            internal delegate void VertexAttrib4Nuiv(uint index, uint[] v);

            internal delegate void VertexAttribI4bv(uint index, sbyte[] v);

            internal delegate void VertexAttribI4ubv(uint index, byte[] v);

            internal delegate void VertexAttribI4sv(uint index, short[] v);

            internal delegate void VertexAttribI4usv(uint index, ushort[] v);

            internal delegate void VertexAttribI4iv(uint index, int[] v);

            internal delegate void VertexAttribI4uiv(uint index, uint[] v);

            internal delegate void VertexAttribL1dv(uint index, double[] v);

            internal delegate void VertexAttribL2dv(uint index, double[] v);

            internal delegate void VertexAttribL3dv(uint index, double[] v);

            internal delegate void VertexAttribL4dv(uint index, double[] v);

            internal delegate void VertexAttribP1ui(uint index, VertexAttribPType type, bool normalized, uint value);

            internal delegate void VertexAttribP2ui(uint index, VertexAttribPType type, bool normalized, uint value);

            internal delegate void VertexAttribP3ui(uint index, VertexAttribPType type, bool normalized, uint value);

            internal delegate void VertexAttribP4ui(uint index, VertexAttribPType type, bool normalized, uint value);

            internal delegate void VertexAttribBinding(uint attribindex, uint bindingindex);

            internal delegate void VertexArrayAttribBinding(uint vaobj, uint attribindex, uint bindingindex);

            internal delegate void VertexAttribDivisor(uint index, uint divisor);

            internal delegate void VertexAttribFormat(uint attribindex, int size, OpenGL.VertexAttribFormat type,
                bool normalized, uint relativeoffset);

            internal delegate void VertexAttribIFormat(uint attribindex, int size, OpenGL.VertexAttribFormat type,
                uint relativeoffset);

            internal delegate void VertexAttribLFormat(uint attribindex, int size, OpenGL.VertexAttribFormat type,
                uint relativeoffset);

            internal delegate void VertexArrayAttribFormat(uint vaobj, uint attribindex, int size,
                OpenGL.VertexAttribFormat type, bool normalized, uint relativeoffset);

            internal delegate void VertexArrayAttribIFormat(uint vaobj, uint attribindex, int size,
                OpenGL.VertexAttribFormat type, uint relativeoffset);

            internal delegate void VertexArrayAttribLFormat(uint vaobj, uint attribindex, int size,
                OpenGL.VertexAttribFormat type, uint relativeoffset);

            internal delegate void VertexAttribPointer(uint index, int size, VertexAttribPointerType type,
                bool normalized, int stride, IntPtr pointer);

            internal delegate void VertexAttribIPointer(uint index, int size, VertexAttribPointerType type, int stride,
                IntPtr pointer);

            internal delegate void VertexAttribLPointer(uint index, int size, VertexAttribPointerType type, int stride,
                IntPtr pointer);

            internal delegate void VertexBindingDivisor(uint bindingindex, uint divisor);

            internal delegate void VertexArrayBindingDivisor(uint vaobj, uint bindingindex, uint divisor);

            internal delegate void Viewport(int x, int y, int width, int height);

            internal delegate void ViewportArrayv(uint first, int count, float[] v);

            internal delegate void ViewportIndexedf(uint index, float x, float y, float w, float h);

            internal delegate void ViewportIndexedfv(uint index, float[] v);

            internal delegate void WaitSync(IntPtr sync, uint flags, ulong timeout);
        }
    }
}