using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public class cursorHandler
    {
        Vector2 mapSize;
        Vector2 vector0;
        bool wrapping;
        public cursorHandler(Vector2 mapSize, Vector2 vector0, bool wrapping)
        {
            this.mapSize = mapSize;
            this.vector0 = vector0;
            this.wrapping = wrapping;
        }

        public Vector2 cursorLogic(string input, Vector2 position)
        {
            if (input == "Left")
            {
                position.modifyX(-1);
            }
            else if (input == "Right")
            {
                position.modifyX(1);
            }
            else if (input == "Up")
            {
                position.modifyY(1);
            }
            else if (input == "Down")
            {
                position.modifyY(-1);
            }
            position = handleWrapping(position, vector0, mapSize, wrapping);
            return position;
        }
        public Vector2 handleWrapping(Vector2 pos, Vector2 min, Vector2 max, bool wraps)
        {
            if (wraps)
            {
                if (pos.x < min.x)
                {
                    pos.x=(max.x - 1);
                }
                if (pos.y < min.y)
                {
                    pos.y=(max.y - 1);
                }
                if (pos.x > max.x)
                {
                    pos.x=(min.x);
                }
                if (pos.y > max.y)
                {
                    pos.y=(min.y);
                }
            }
            else
            {
                if (pos.x < min.x)
                {
                    pos.x=(min.x);
                }
                if (pos.y < min.y)
                {
                    pos.y=(min.y);
                }
                if (pos.x > max.x)
                {
                    pos.x=(max.x);
                }
                if (pos.y > max.y)
                {
                    pos.y = (max.y);
                }
            }
            return pos;
        }


    }
}
