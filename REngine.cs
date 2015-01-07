using OpenTK;
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

namespace Reactor
{
    public class REngine : RSingleton<REngine>
    {
        private RDisplayModes _supportedDisplayModes;
        private RenderControl _renderControl;
        
        public RScene Scene { get { return RScene.Instance; } }
        public RTextures Textures { get { return RTextures.Instance; } }
        public RMaterials Materials { get { return RMaterials.Instance; } }

        public RViewport Viewport { get { return _viewport; } }

        internal RViewport _viewport;
        internal static RGame RGame;
        internal static string RootPath;
        public REngine()
        {
            RootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            RLog.Init();
        }

        ~REngine()
        {
            RLog.Dispose();
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
            Reactor.Math.Vector4 clearColor = color.ToVector4();
            GL.ClearColor(clearColor.X, clearColor.Y, clearColor.Z, clearColor.W);
            if (onlyDepth)
                GL.Clear(ClearBufferMask.DepthBufferBit);
            else
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
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
                return false;
            }

        }

        public bool InitGameWindow(RDisplayMode displayMode)
        {
            try
            {
                GameWindowRenderControl control = new GameWindowRenderControl();
                control.GameWindow = RGame.GameWindow;
                control.GameWindow.Size = new System.Drawing.Size(displayMode.Width, displayMode.Height);
                _renderControl = control;
                RLog.Info(GetGLInfo());
                RLog.Info("Game Window Renderer Initialized.");
                return true;
            } catch(Exception e) {
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

    
}
