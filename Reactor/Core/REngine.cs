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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Timers;
using Reactor.Platform;
using Reactor.Platform.OpenGL;
using Reactor.Types;
using Reactor.Types.States;
using Image = Reactor.Platform.GLFW.Image;

namespace Reactor
{
    public class REngine : RSingleton<REngine>
    {
        private RDisplayModes _supportedDisplayModes;
        private RenderControl _renderControl;
        private readonly Stopwatch _stopWatch;
        private readonly Timer _fpsTimer;
        public RScene Scene => RScene.Instance;
        public RTextures Textures => RTextures.Instance;
        public RMaterials Materials => RMaterials.Instance;
        public RInput Input => RInput.Instance;
        public RViewport Viewport => _viewport;
        public RFileSystem FileSystem => RFileSystem.Instance;
        public RScreen Screen => RScreen.Instance;
        private RFrameBuffer hdrFrameBuffer;
        private RShader hdrShader;
        public RAtmosphere Atmosphere => RAtmosphere.Instance;
        internal RViewport _viewport;
        internal static RGame RGame;
        internal static string RootPath;
        internal static RCamera camera;
        internal static bool showFps = true;

        private float _lastFps;
        private float _fps;
        private TimeSpan lastFrameTime;

        public REngine()
        {
            RootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            RLog.Info("Engine startup sequence activated.");


            _stopWatch = new Stopwatch();
            _fpsTimer = new Timer();

            _fpsTimer.Interval = 1000;
            _fpsTimer.Elapsed += _fpsTimer_Tick;
            _fpsTimer.Start();

            _viewport = new RViewport(0, 0, 800, 600);
            camera = new RCamera(_viewport.AspectRatio);
            lastFrameTime = new TimeSpan();
        }

        private void _fpsTimer_Tick(object sender, ElapsedEventArgs e)
        {
            _lastFps = (float)System.Math.Ceiling(_fps / lastFrameTime.TotalMilliseconds * 1000.0f);
            _fps = 0;
            lastFrameTime = _stopWatch.Elapsed;
            _stopWatch.Restart();
        }

        ~REngine()
        {
            _fpsTimer.Elapsed -= _fpsTimer_Tick;
            _fpsTimer.Stop();
            RLog.Info("Engine shutdown sequence activated.");
        }

        private void StartClock()
        {
            if (!_stopWatch.IsRunning) _stopWatch.Start();
        }

        internal void Tick(float t)
        {
            _fps += t;
        }

        public float GetFPS()
        {
            return _lastFps;
        }

        public double GetRenderTime()
        {
            return lastFrameTime.TotalMilliseconds;
        }

        public double GetTotalTime()
        {
            return RGame.GameTime.TotalGameTime.TotalMilliseconds;
        }

        public double GetElapsedTime()
        {
            return RGame.GameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public static void CheckGLError()
        {
            var error = GL.GetError();
            if (error != ErrorCode.NoError)
            {
                var trace = new StackTrace(true);
                var frames = trace.GetFrames();
                RLog.Error("Stack Trace:");
                foreach (var f in frames) RLog.Error(f.ToString());
                throw new EngineGLException("GL.GetError() returned " + error);
            }
        }

        public RDisplayMode CurrentDisplayMode => RGame.GameWindow.Mode;
        public RDisplayModes SupportedDisplayModes => RGame.GameWindow.SupportedModes();

        public bool IsWideScreen
        {
            get
            {
                const float limit = 800.0f / 600.0f; // the CRT square domes of old.
                var aspect = CurrentDisplayMode.AspectRatio;
                return aspect > limit;
            }
        }

        public bool IsTripleScreen
        {
            get
            {
                var aspect = CurrentDisplayMode.AspectRatio;
                const float limit = 5.3333333f;
                return aspect > limit;
            }
        }

        public void SetDebug(bool value)
        {
            RLog.Enabled = value;
        }

        public void SetDebug(bool value, string logPath)
        {
            RLog.Enabled = value;
            RLog.LogPath = logPath;
        }

        public void SetShowFPS(bool value)
        {
            showFps = value;
        }

        public void InitHDR()
        {
            hdrFrameBuffer = new RFrameBuffer((int)_viewport.Width, (int)_viewport.Height, false,
                RSurfaceFormat.Vector4, RDepthFormat.Depth32Stencil8);
            hdrShader = new RShader();
            hdrShader.Load(RShaderResources.HDRVert, RShaderResources.HDRFrag);
        }

        public void BeginHDR()
        {
            hdrFrameBuffer.Bind();
            _viewport.Bind();
        }

        public void EndHDR()
        {
            hdrFrameBuffer.Unbind();
            hdrShader.Bind();
            hdrShader.SetSamplerValue(RTextureLayer.TEXTURE0, hdrFrameBuffer.ColorBuffers[0]);
            hdrShader.Unbind();
        }

        public void DrawHDR()
        {
            Screen.Begin();
            Screen.RenderFullscreenQuad(hdrShader);
            Screen.End();
        }

        public void SetClearColor(RColor color)
        {
            var c = color.ToVector4();
            GL.ClearColor(c.X, c.Y, c.Z, c.W);
            CheckGLError();
        }

        public void Clear()
        {
            Clear(true);
        }

        public void Clear(bool depth = true)
        {
            Clear(depth, true);
        }

        public void Clear(bool depth = false, bool stencil = false)
        {
            var mask = ClearBufferMask.ColorBufferBit;
            if (depth)
                mask |= ClearBufferMask.DepthBufferBit;
            if (stencil)
                mask |= ClearBufferMask.StencilBufferBit;
            GL.Clear(mask);
            CheckGLError();
            Atmosphere.Update();

            CheckGLError();
        }

        internal void Reset()
        {
            StartClock();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            CheckGLError();
            GL.Disable(EnableCap.CullFace);
            CheckGLError();
            GL.FrontFace(FrontFaceDirection.Ccw);
            CheckGLError();
            GL.Enable(EnableCap.DepthTest);
            CheckGLError();
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.DstAlpha);
            CheckGLError();
            RBlendState.Opaque.PlatformApplyState();
            CheckGLError();
            _viewport.Bind();
            CheckGLError();
        }

        public void Present()
        {
            Tick(1);
            if (showFps && GetFPS() > 0)
                Screen.RenderFPS(GetFPS());

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            CheckGLError();
            _renderControl.SwapBuffers();
            CheckGLError();
        }

        /*
        public bool InitPictureBox(IntPtr handle)
        {
            try
            {
                PictureBoxRenderControl control = new PictureBoxRenderControl();
                control.PictureBox = (PictureBox)PictureBox.FromHandle(handle);
                control.Init();
                _renderControl = control;
                RShader.InitShaders();
                Screen.Init();
                RLog.Info(GetGLInfo());
                RLog.Info("Picture Box Renderer Initialized.");
                return true;
            } catch(Exception e) {
                RLog.Error(e);
                return false;
            }

        }*/
#if WINDOWS
        public bool InitForm(IntPtr handle)
        {
            try
            {
                FormRenderControl control = new FormRenderControl();
                control.Form = (Form)Form.FromHandle(handle);
                control.Init();
                _renderControl = control;
                RShader.InitShaders();
                Screen.Init();
                RLog.Info(GetGLInfo());
                RLog.Info("Form Renderer Initialized.");
                PrintExtensions();
                return true;
            }
            catch (Exception e)
            {
                RLog.Error(e);
                return false;
            }

        }
#endif
        private void PrintExtensions()
        {
            var extensions = GL.GetString(StringName.Extensions).Split(' ');
            foreach (var extension in extensions)
                if (extension.StartsWith("GL_EXT"))
                    RLog.Debug(extension);
        }

        [STAThread]
        public bool InitGameWindow(int width, int height, RWindowStyle windowStyle, string title = "Reactor")
        {
            var mode = new RDisplayMode(width, height, -1);
            return InitGameWindow(mode, windowStyle, title);
        }

        [STAThread]
        public bool InitGameWindow(RDisplayMode displayMode, string title = "Reactor")
        {
            return InitGameWindow(displayMode, RWindowStyle.Normal, title);
        }

        [STAThread]
        public bool InitGameWindow(RDisplayMode displayMode, RWindowStyle windowStyle, string title = "Reactor")
        {
            try
            {
                RGame.GameWindow.Title = title;

                var control = new GameWindowRenderControl(displayMode, windowStyle, title);

                control.GameWindow = RGame.GameWindow;
                _renderControl = control;

                RLog.Info(GetGLInfo());
                CheckGLError();
                RLog.Info("Game Window Renderer Initialized.");
                //PrintExtensions();
                CheckGLError();
                Input.Init();
                RShader.InitShaders();
                CheckGLError();
                Screen.Init();
                CheckGLError();
                return true;
            }
            catch (Exception e)
            {
                RLog.Error(e);
                return false;
            }
        }

        [STAThread]
        public bool Init()
        {
            try
            {
                _renderControl = new DummyRenderControl();
                _renderControl.Init();
                RLog.Info(GetGLInfo());
                RLog.Info("Dummy Non-Renderer Initialized.");
                PrintExtensions();
                return true;
            }
            catch (Exception e)
            {
                RLog.Error(e);
                return false;
            }
        }

        public void SetGameWindowIcon(Icon icon)
        {
            try
            {
                var images = new[] { new Image(icon.Width, icon.Height, icon.Handle) };

                RGame.GameWindow.SetIcons(images);
            }
            catch (Exception e)
            {
                RLog.Error(e);
            }
        }

        public bool ToggleFullscreen(RDisplayMode displayMode)
        {
            try
            {
                if (_renderControl.IsFullscreen)
                {
                    RGame.GameWindow.SetMode(RGame.GameWindow.Mode, false);
                    _renderControl.IsFullscreen = false;
                    RLog.Info("No longer in fullscreen mode.");
                }
                else
                {
                    if (_renderControl.GetType() == typeof(GameWindowRenderControl))
                    {
                        RGame.GameWindow.SetMode(displayMode, true);
                        _renderControl.IsFullscreen = true;
                        RLog.Info(string.Format("Fullscreen mode activated : {0}", displayMode));
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                RLog.Error("Error attempting to go fullscreen.");
                RLog.Error(e);
                return false;
            }
        }

        public void SetViewport(RViewport viewport)
        {
            _viewport = viewport;
        }

        public RViewport GetViewport()
        {
            return _viewport;
        }

        public void SetCamera(RCamera camera)
        {
            REngine.camera = camera;
        }

        public RCamera GetCamera()
        {
            return camera;
        }

        public bool Dispose()
        {
            try
            {
                RLog.Info("Shutting down the engine.");
                if (hdrFrameBuffer != null)
                    hdrFrameBuffer.Dispose();
                _renderControl.Dispose();
                RLog.Info("Shutdown complete.\r\n\r\n\r\n\r\n");
                return true;
            }
            catch (Exception e)
            {
                RLog.Error("Error shutting down the engine.");
                RLog.Error(e);
                return false;
            }
        }

        internal static string GetGLInfo()
        {
            var version = GL.GetString(StringName.Version);
            var vendor = GL.GetString(StringName.Vendor);
            var renderer = GL.GetString(StringName.Renderer);
            var glslVersion = GL.GetString(StringName.ShadingLanguageVersion);

            return string.Format("Using OpenGL version {0} from {1}, renderer {2}, GLSL version {3}", version, vendor,
                renderer, glslVersion);
        }
    }

    internal class EngineGLException : Exception
    {
        public EngineGLException(string message)
            : base(message)
        {
        }
    }

    public class ReactorException : Exception
    {
        public ReactorException(string message, Exception baseException)
            : base(message, baseException)
        {
        }

        public ReactorException(string message) : base(message)
        {
        }
    }

    public static class Extentions
    {
        public static BlendEquationMode GetBlendEquationMode(this RBlendFunc function)
        {
            switch (function)
            {
                case RBlendFunc.Add:
                    return BlendEquationMode.FuncAdd;
                case RBlendFunc.Max:
                    return BlendEquationMode.Max;
                case RBlendFunc.Min:
                    return BlendEquationMode.Min;
                case RBlendFunc.ReverseSubtract:
                    return BlendEquationMode.FuncReverseSubtract;
                case RBlendFunc.Subtract:
                    return BlendEquationMode.FuncSubtract;

                default:
                    throw new ArgumentException();
            }
        }

        public static BlendingFactorSrc GetBlendFactorSrc(this RBlend blend)
        {
            switch (blend)
            {
                case RBlend.DestinationAlpha:
                    return BlendingFactorSrc.DstAlpha;
                case RBlend.DestinationColor:
                    return BlendingFactorSrc.DstColor;
                case RBlend.InverseDestinationAlpha:
                    return BlendingFactorSrc.OneMinusDstAlpha;
                case RBlend.InverseDestinationColor:
                    return BlendingFactorSrc.OneMinusDstColor;
                case RBlend.InverseSourceAlpha:
                    return BlendingFactorSrc.OneMinusSrcAlpha;
                case RBlend.InverseSourceColor:
                    return BlendingFactorSrc.OneMinusSrcColor;
                case RBlend.One:
                    return BlendingFactorSrc.One;
                case RBlend.SourceAlpha:
                    return BlendingFactorSrc.SrcAlpha;
                case RBlend.SourceAlphaSaturation:
                    return BlendingFactorSrc.SrcAlphaSaturate;
                case RBlend.SourceColor:
                    return BlendingFactorSrc.ConstantColor;
                case RBlend.Zero:
                    return BlendingFactorSrc.Zero;
                default:
                    return BlendingFactorSrc.One;
            }
        }

        public static BlendingFactorDest GetBlendFactorDest(this RBlend blend)
        {
            switch (blend)
            {
                case RBlend.DestinationAlpha:
                    return BlendingFactorDest.DstAlpha;
                case RBlend.InverseDestinationAlpha:
                    return BlendingFactorDest.OneMinusDstAlpha;
                case RBlend.InverseSourceAlpha:
                    return BlendingFactorDest.OneMinusSrcAlpha;
                case RBlend.InverseSourceColor:
                    return BlendingFactorDest.OneMinusSrcColor;
                case RBlend.One:
                    return BlendingFactorDest.One;
                case RBlend.SourceAlpha:
                    return BlendingFactorDest.SrcAlpha;
                case RBlend.SourceColor:
                    throw new Exception("Invalid Enum for Dest");

                case RBlend.Zero:
                    return BlendingFactorDest.Zero;
                default:
                    return BlendingFactorDest.One;
            }
        }
    }
}