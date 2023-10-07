using System;

namespace Red;

internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        using (var app = new RedApp())
        {
            app.Run();
        }
    }
}