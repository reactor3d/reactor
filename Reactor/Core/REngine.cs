
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Reactor.Platform;
using Reactor.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Reactor.Math;
using System.Diagnostics;
using OpenTK;
using Reactor.Types.States;
using System.Timers;

namespace Reactor
{
    public class REngine : RSingleton<REngine>
    {
        private RDisplayModes _supportedDisplayModes;
        private RenderControl _renderControl;
        private Stopwatch _stopWatch;
        private System.Timers.Timer _fpsTimer;
        public RScene Scene { get { return RScene.Instance; } }
        public RTextures Textures { get { return RTextures.Instance; } }
        public RMaterials Materials { get { return RMaterials.Instance; } }
        public RInput Input { get { return RInput.Instance; } }
        public RViewport Viewport { get { return _viewport; } }
        public RFileSystem FileSystem { get { return RFileSystem.Instance; } }
        public RScreen Screen { get { return RScreen.Instance; } }
        internal RViewport _viewport;
        internal static RGame RGame;
        internal static string RootPath;
        internal static RCamera camera;
        internal static bool showFps = false;

        private float _lastFps = 0;
        private float _fps = 0;
        private TimeSpan lastFrameTime;
        public REngine()
        {
            RootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            RLog.Info("Engine startup sequence activated.");

            _stopWatch = new Stopwatch();
            _fpsTimer = new System.Timers.Timer();

            _fpsTimer.Interval = 1000;
            _fpsTimer.Elapsed += _fpsTimer_Tick;
            _fpsTimer.Start();

            _viewport = new RViewport(0,0,800,600);
            camera = new RCamera();
            lastFrameTime = new TimeSpan();
            RShader.InitShaders();


        }

        void _fpsTimer_Tick(object sender, ElapsedEventArgs e)
        {
            _lastFps = (float)System.Math.Ceiling((_fps / (lastFrameTime.TotalMilliseconds)) * 1000.0f);
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
            if (!_stopWatch.IsRunning){
                _stopWatch.Start();
            } 
        }
        internal void Tick(float t)
        {
            _fps += t;
        }
        public double GetFPS()
        {
            return _lastFps;
        }
        public float GetTime()
        {
            return (float)(REngine.RGame.GameWindow.UpdateTime * 1000.0);
        }
        public static void CheckGLError()
        {
            ErrorCode error = GL.GetError();
            if (error != ErrorCode.NoError)
                throw new EngineGLException("GL.GetError() returned " + error.ToString());
        }
        public RDisplayMode CurrentDisplayMode
        {
            get
            {
                return new RDisplayMode(OpenTK.DisplayDevice.Default.Width, OpenTK.DisplayDevice.Default.Height, (int)OpenTK.DisplayDevice.Default.RefreshRate);
            }
        }
        public RDisplayModes SupportedDisplayModes
        {
            get
            {

                if (_supportedDisplayModes == null)
                {
                    var modes = new List<RDisplayMode>(new[] { CurrentDisplayMode, });


                    //IList<OpenTK.DisplayDevice> displays = OpenTK.DisplayDevice.AvailableDisplays;
                    var displays = new List<OpenTK.DisplayDevice>();

                    OpenTK.DisplayIndex[] displayIndices = {
						OpenTK.DisplayIndex.First,
						OpenTK.DisplayIndex.Second,
						OpenTK.DisplayIndex.Third,
						OpenTK.DisplayIndex.Fourth,
						OpenTK.DisplayIndex.Fifth,
						OpenTK.DisplayIndex.Sixth,
					};

                    foreach (var displayIndex in displayIndices)
                    {
                        var currentDisplay = OpenTK.DisplayDevice.GetDisplay(displayIndex);
                        if (currentDisplay != null) displays.Add(currentDisplay);
                    }

                    if (displays.Count > 0)
                    {
                        modes.Clear();
                        foreach (OpenTK.DisplayDevice display in displays)
                        {
                            foreach (OpenTK.DisplayResolution resolution in display.AvailableResolutions)
                            {
                                RSurfaceFormat format = RSurfaceFormat.Color;
                                switch (resolution.BitsPerPixel)
                                {
                                    case 32: format = RSurfaceFormat.Color; break;
                                    case 16: format = RSurfaceFormat.Bgr565; break;
                                    case 8: format = RSurfaceFormat.Bgr565; break;
                                    default:
                                        break;
                                }
                                // Just report the 32 bit surfaces for now
                                // Need to decide what to do about other surface formats
                                if (format == RSurfaceFormat.Color)
                                {
                                    modes.Add(new RDisplayMode(resolution.Width, resolution.Height, (int)resolution.RefreshRate));
                                }
                            }

                        }
                    }

                    _supportedDisplayModes = new RDisplayModes(modes);
                }

                return _supportedDisplayModes;
            }
        }
        public bool IsWideScreen
        {
            get
            {
                const float limit = 4.0f / 3.0f;
                var aspect = CurrentDisplayMode.AspectRatio;
                return aspect > limit;
            }
        }

        public bool IsTripleScreen
        {
            get
            {
                var aspect = CurrentDisplayMode.AspectRatio;
                const float limit = 1.8f;
                return (aspect > limit);
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
        public void Clear()
        {
            Clear(RColor.Black, false);
        }
        public void Clear(RColor color, bool onlyDepth = false)
        {
            StartClock();
            
            
            _viewport.Bind();
            
            Reactor.Math.Vector4 clearColor = color.ToVector4();
            GL.ClearColor(clearColor.X, clearColor.Y, clearColor.Z, clearColor.W);
            if (onlyDepth)
                GL.Clear(ClearBufferMask.DepthBufferBit);
            else
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //camera.Update();

            REngine.CheckGLError();
            
        }

        public void Present()
        {
            if(showFps)
                Screen.RenderFPS((int)GetFPS());
            _renderControl.SwapBuffers();
        }

        public bool InitPictureBox(IntPtr handle)
        {
            try
            {
                PictureBoxRenderControl control = new PictureBoxRenderControl();
                control.PictureBox = (PictureBox)PictureBox.FromHandle(handle);
                control.Init();
                _renderControl = control;
                RLog.Info(GetGLInfo());
                RLog.Info("Picture Box Renderer Initialized.");
                return true;
            } catch(Exception e) {
                RLog.Error(e);
                return false;
            }

        }

        public bool InitGameWindow(int width, int height, RWindowStyle windowStyle)
        {
            RDisplayMode mode = new RDisplayMode(width, height, -1);
            return InitGameWindow(mode, windowStyle);
        }
        public bool InitGameWindow(RDisplayMode displayMode)
        {
            return InitGameWindow(displayMode, RWindowStyle.Normal);
        }
        public bool InitGameWindow(RDisplayMode displayMode, RWindowStyle windowStyle)
        {
            try
            {
                GameWindowRenderControl control = new GameWindowRenderControl();
                control.GameWindow = RGame.GameWindow;
                control.GameWindow.Size = new System.Drawing.Size(displayMode.Width, displayMode.Height);
                if(windowStyle == RWindowStyle.Borderless)
                    control.GameWindow.WindowBorder = WindowBorder.Hidden;
                control.GameWindow.X = 0;
                control.GameWindow.Y = 0;
                control.Context = (GraphicsContext)control.GameWindow.Context;
                _renderControl = control;
                RLog.Info(GetGLInfo());
                RLog.Info("Game Window Renderer Initialized.");
                return true;
            } catch(Exception e) {
                RLog.Error(e);
                return false;
            }
        }

        public bool Init()
        {
            try
            {
                _renderControl = new DummyRenderControl();
                _renderControl.Init();
                RLog.Info(GetGLInfo());
                RLog.Info("Dummy Non-Renderer Initialized.");
                return true;
            } catch(Exception e) {
                RLog.Error(e);
                return false;
            }
        }

        public bool ToggleFullscreen(RDisplayMode displayMode)
        {
            try
            {
                if (_renderControl.IsFullscreen)
                {
                    DisplayDevice.Default.RestoreResolution();
                    if (_renderControl.GetType() == typeof(GameWindowRenderControl))
                        (_renderControl as GameWindowRenderControl).GameWindow.WindowState = WindowState.Normal;

                    _renderControl.IsFullscreen = false;
                    RLog.Info("No longer in fullscreen mode.");
                }
                else
                {
                    if (_renderControl.GetType() == typeof(GameWindowRenderControl))
                    {
                        DisplayDevice.Default.ChangeResolution(displayMode.Width, displayMode.Height, 32, -1);
                        (_renderControl as GameWindowRenderControl).GameWindow.Size = new System.Drawing.Size(displayMode.Width, displayMode.Height);
                        (_renderControl as GameWindowRenderControl).GameWindow.WindowState = WindowState.Fullscreen;
                        _renderControl.IsFullscreen = true;
                        RLog.Info(String.Format("Fullscreen mode activated : {0}", displayMode));
                    }
                }
                return true;
            }
            catch(Exception e)
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
            return REngine.camera;
        }

        public bool Dispose()
        {
            try
            {
                RLog.Info("Shutting down the engine.");
                _renderControl.Destroy();
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

        internal static string GetGLInfo() {
            var version = GL.GetString(StringName.Version);
            var vendor = GL.GetString(StringName.Vendor);
            var renderer = GL.GetString(StringName.Renderer);
            var glslVersion = GL.GetString(StringName.ShadingLanguageVersion);

            return String.Format("Using OpenGL version {0} from {1}, renderer {2}, GLSL version {3}", version, vendor, renderer, glslVersion);
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
                    return (BlendingFactorSrc)All.OneMinusSrcColor;
                case RBlend.One:
                    return BlendingFactorSrc.One;
                case RBlend.SourceAlpha:
                    return BlendingFactorSrc.SrcAlpha;
                case RBlend.SourceAlphaSaturation:
                    return BlendingFactorSrc.SrcAlphaSaturate;
                case RBlend.SourceColor:
                    return (BlendingFactorSrc)All.SrcColor;
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
                //			case Blend.DestinationColor:
                //				return BlendingFactorDest.DstColor;
                case RBlend.InverseDestinationAlpha:
                    return BlendingFactorDest.OneMinusDstAlpha;
                //			case Blend.InverseDestinationColor:
                //				return BlendingFactorDest.OneMinusDstColor;
                case RBlend.InverseSourceAlpha:
                    return BlendingFactorDest.OneMinusSrcAlpha;
                case RBlend.InverseSourceColor:
                    return (BlendingFactorDest)All.OneMinusSrcColor;
                case RBlend.One:
                    return BlendingFactorDest.One;
                case RBlend.SourceAlpha:
                    return BlendingFactorDest.SrcAlpha;
                //			case Blend.SourceAlphaSaturation:
                //				return BlendingFactorDest.SrcAlphaSaturate;
                case RBlend.SourceColor:
                    return (BlendingFactorDest)All.SrcColor;

                case RBlend.Zero:
                    return BlendingFactorDest.Zero;
                default:
                    return BlendingFactorDest.One;
            }

        }


        /// <summary>
        /// Convert a <see cref="SurfaceFormat"/> to an OpenTK.Graphics.ColorFormat.
        /// This is used for setting up the backbuffer format of the OpenGL context.
        /// </summary>
        /// <returns>An OpenTK.Graphics.ColorFormat instance.</returns>
        /// <param name="format">The <see cref="SurfaceFormat"/> to convert.</param>
        internal static ColorFormat GetColorFormat(this RSurfaceFormat format)
        {
            switch (format)
            {
                case RSurfaceFormat.Alpha8:
                    return new ColorFormat(0, 0, 0, 8);
                case RSurfaceFormat.Bgr565:
                    return new ColorFormat(5, 6, 5, 0);
                case RSurfaceFormat.Bgra4444:
                    return new ColorFormat(4, 4, 4, 4);
                case RSurfaceFormat.Bgra5551:
                    return new ColorFormat(5, 5, 5, 1);
                case RSurfaceFormat.Bgr32:
                    return new ColorFormat(8, 8, 8, 0);
                case RSurfaceFormat.Bgra32:
                case RSurfaceFormat.Color:
                    return new ColorFormat(8, 8, 8, 8);
                case RSurfaceFormat.Rgba1010102:
                    return new ColorFormat(10, 10, 10, 2);
                default:
                    // Floating point backbuffers formats could be implemented
                    // but they are not typically used on the backbuffer. In
                    // those cases it is better to create a render target instead.
                    throw new NotSupportedException();
            }
        }
    }
}
