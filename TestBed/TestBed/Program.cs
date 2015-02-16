using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TestBed
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using(var game = new Game())
            {
                game.Run();
            }
        }
    }
}
