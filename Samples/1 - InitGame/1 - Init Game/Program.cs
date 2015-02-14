using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InitGame
{
    public class Program
    {
        [STAThread]
        public static void Main(String[] args)
        {
            using(Game g = new Game())
            {
                g.Run();
            }
        }
    }
}
