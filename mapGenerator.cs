using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public static class mapGenerator
    {
        public static Map generateMap(Vector2 mapSize,int mineCount)
        {
            Map map = new Map(mapSize);
            Vector2[] mines = pickMineSlots(mapSize,mineCount);
            for (int y = 0; y < mapSize.y; y++)
            {
                for (int x = 0; x < mapSize.x; x++)
                {
                    if (mines.Contains(new Vector2(x, y)))
                    {
                        map.setMine(new Vector2(x, y));
                    }
                }
            }
            return map;
        }
        public static Vector2[] pickMineSlots(Vector2 mapSize, int mineCount)
        {
            Random r = new Random();
            Vector2[] slots = new Vector2[mineCount];
            for(int i=0; i < mineCount; i++)
            {
                Vector2 pick = new Vector2(r.Next(0, mapSize.x),r.Next(0, mapSize.y));
                if (!slots.Contains(pick))
                {
                    slots[i] = pick;
                }
                else
                {
                    i--;
                }
            }
            return slots;
        }
    }
}
