using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    internal class Program
    {
        public static bool lost = false;
        public static int hiddenTiles = 0;
        public static SoundPlayer SP;
        static void Main(string[] args)
        {
            SP = new SoundPlayer();
            SP.init();
            loop centralLoop=new loop();
            centralLoop.mainLoop();
        }
    }
}
