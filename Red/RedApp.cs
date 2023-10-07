using System.Drawing;
using System.IO;
using System.Reflection;
using Reactor;
using Reactor.Types;

namespace Red;

public class RedApp : RGame
{
    private RViewport mainViewport;

    private static Stream GetResourceStream(string resource)
    {
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
        return stream;
    }

    public override void Init()
    {
        // Initialization Code Here
        Engine.InitGameWindow(1280, 720, RWindowStyle.Normal, "Red");
        // Now that we have initialized, we can use our game window.
        GameWindow.CenterOnScreen();
        GameWindow.VSync = true;
        var iconStream = GetResourceStream("Red.icon.ico");
        GameWindow.SetIcons(new Reactor.Platform.GLFW.Image((Bitmap)Image.FromStream(iconStream)));
        //Engine.SetClearColor(new RColor(0xFF252120));

        Engine.SetShowFPS(true);
    }

    public override void Render()
    {
        Engine.Clear(true, true);
        // Rendering Code
        Engine.Present();
    }

    public override void Update()
    {
        // Update Code
        if (Engine.Input.IsKeyDown(RKey.Escape)) GameWindow.Exit();
    }

    public override void Dispose()
    {
        Engine.Dispose();
    }

    public override void Resized(int Width, int Height)
    {
        mainViewport.Width = Width;
        mainViewport.Height = Height;
        Engine.SetViewport(mainViewport);
    }
}