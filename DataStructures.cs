using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public struct vector2
    {
        public int x;
        public int y;

        public vector2(int initialX, int initialY)
        {
            x = initialX;
            y = initialY;
        }
        public static vector2 Zero()
        {
            return new vector2(0, 0);
        }public void modifyX(int xIn)
        {
            x += xIn;
        }
        public void modifyY(int yIn) {
            y += yIn;
        }
        public string printablePosition()
        {
            return "" + x + "/" + y;
        }
    }
}
