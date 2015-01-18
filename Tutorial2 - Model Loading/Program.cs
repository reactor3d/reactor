using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial2___Model_Loading
{
    class Program
    {
        static void Main(string[] args)
        {
            int t = 0;
            using (Game game = new Game())
            {

                game.Run();
            }
        }
    }
}
