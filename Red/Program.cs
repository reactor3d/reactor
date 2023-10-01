using System;

namespace Red
{

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var app = new RedApp())
            {
                app.Run();
            }
        }
    }
}