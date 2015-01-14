
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

namespace Reactor
{
    public class REngine : RSingleton<REngine>
    {
        private RDisplayModes _supportedDisplayModes;
        private RenderControl _renderControl;
        private Stopwatch _stopWatch;
        private Timer _fpsTimer;
        public RScene Scene { get { return RScene.Instance; } }
        public RTextures Textures { get { return RTextures.Instance; } }
        public RMaterials Materials { get { return RMaterials.Instance; } }
        public RInput Input { get { return RInput.Instance; } }
        public RViewport Viewport { get { return _viewport; } }
        public RFileSystem FileSystem { get { return RFileSystem.Instance; } }
        internal RViewport _viewport;
        internal static RGame RGame;
        internal static string RootPath;
        internal static RCamera camera;


        private float _lastFps = 0;
        private float _fps = 0;
        private TimeSpan lastFrameTime;
        private REngine()
        {
            RootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            RLog.Info("Engine startup sequence activated.");

            _stopWatch = new Stopwatch();
            _fpsTimer = new Timer();
            _fpsTimer.Interval = 1000;
            _fpsTimer.Tick += _fpsTimer_Tick;
            _fpsTimer.Start();

            _viewport = new RViewport(0,0,800,600);
            camera = new RCamera();
            lastFrameTime = new TimeSpan();
            RShader.InitShaders();

        }

        void _fpsTimer_Tick(object sender, EventArgs e)
        {
            _lastFps = (float)System.Math.Floor((_fps / (lastFrameTime.TotalMilliseconds)) * 1000.0f);
            _fps = 0;
            lastFrameTime = _stopWatch.Elapsed;
            _stopWatch.Restart();
        }

        ~REngine()
        {
            _fpsTimer.Tick -= _fpsTimer_Tick;
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
            return (float)REngine.RGame.GameWindow.UpdateTime * 1000.0f;
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
                return new RDisplayMode(OpenTK.DisplayDevice.Default.Width, OpenTK.DisplayDevice.Default.Height, (int)OpenTK.DisplayDevice.Default.RefreshRate, RSurfaceFormat.Color);
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
                                    modes.Add(new RDisplayMode(resolution.Width, resolution.Height, (int)resolution.RefreshRate, format));
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
        public void Clear()
        {
            Clear(RColor.Black, false);
        }
        public void Clear(RColor color)
        {
            Clear(color, false);
        }
        public void Clear(RColor color, bool onlyDepth)
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


            
        }

        public void Present()
        {
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
            RDisplayMode mode = new RDisplayMode(width, height, -1, RSurfaceFormat.Color);
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
    }

    
}
