using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public struct Vector2
    {
        public int x;
        public int y;

        public Vector2(int initialX, int initialY)
        {
            x = initialX;
            y = initialY;
        }
        public static Vector2 Zero()
        {
            return new Vector2(0, 0);
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
    public struct Tile
    {
        public Vector2 position;
        public bool isMine;
        public Vector2[] neighbours;
        public bool isRevealed;
        public int explosiveCount;
        public Map parent;
        public Tile(Vector2 position, bool isMine, Map map)
        {
            this.position = position;
            this.isMine = isMine;
            neighbours = null;
            isRevealed = false;
            explosiveCount = 0;
            parent = map;
        }
        public void setNeighbours(Vector2[] neighbours)
        {
            this.neighbours = neighbours;
            Console.WriteLine("");
        }
        public void armMine()
        {
            isMine = true;
            foreach(Vector2 n in neighbours)
            {
                parent.updateExplosiveCounts(n);//TODO FIX
            }
        }
        public void updateExplosiveCount()
        {
            explosiveCount = getNeighbourMineCount();
        }
        public void reveal()
        {
            isRevealed = true;
        }
        public int getNeighbourMineCount()
        {
            int count = 0;
            foreach(Vector2 t in neighbours)
            {
                if (parent.tiles[t.y][t.x].isMine)
                {
                    count++;
                }
            }
            return count;
        }
    }
    public struct Map
    {
        public Tile[][] tiles;
        public Map(Vector2 mapSize)
        {
            tiles = new Tile[mapSize.y][];
            for (int y = 0; y < mapSize.x; y++)
            {
                tiles[y] = new Tile[mapSize.x]; 
                for (int x = 0; x < mapSize.x; x++)
                {
                    tiles[y][x] = new Tile(new Vector2(x,y),false,this);
                }
            }
            setNeighbours();
        }
        public void setMine(Vector2 coord)
        {
            tiles[coord.y][coord.x].armMine();
        }
        public void updateExplosiveCounts(Vector2 coord)
        {
            tiles[coord.y][coord.x].updateExplosiveCount();
        }
        public void setNeighbours()
        {
            for (int y = 0; y < tiles.Length; y++)
            {
                for (int x = 0; x < tiles[y].Length; x++)
                {
                    tiles[y][x].setNeighbours(determineNeighbours(new Vector2(x,y)));
                }
            }
        }
        public Vector2[] determineNeighbours(Vector2 position)
        {
            Vector2[] neighbours=null;
            if (position.x != 0 && position.x != tiles[position.y].Length-1)
            {//It's not an X edged tile
                if (position.y != 0 && position.y != tiles.Length-1)
                {
                    //It's also not a Y edged tile, so it has all 8 potential neighbours
                    neighbours = new Vector2[8];
                    neighbours[0] = (new Vector2(position.y - 1,position.x - 1));
                    neighbours[1] = (new Vector2(position.y - 1,position.x));
                    neighbours[2] = (new Vector2(position.y - 1,position.x + 1));
                    neighbours[3] = (new Vector2(position.y,position.x - 1));
                    neighbours[4] = (new Vector2(position.y,position.x + 1));
                    neighbours[5] = (new Vector2(position.y + 1,position.x - 1));
                    neighbours[6] = (new Vector2(position.y + 1,position.x));
                    neighbours[7] = (new Vector2(position.y + 1,position.x + 1));
                }
                else
                {//It's a Y edged tile -3 to neighbours
                    neighbours = new Vector2[5];
                    neighbours[0] = (new Vector2(position.y,position.x - 1));//We get to keep the ones directly left and right of us for sure!
                    neighbours[1] = (new Vector2(position.y,position.x + 1));
                    //Check which way we're Y edged
                    if (position.y == 0)//Are we on the bottom?
                    {//Then we get the 3 above us
                        neighbours[2] = (new Vector2(position.y + 1,position.x - 1));
                        neighbours[3] = (new Vector2(position.y + 1,position.x));
                        neighbours[4] = (new Vector2(position.y + 1,position.x + 1));
                    }
                    else
                    {//Otherwise it's the 3 below us
                        neighbours[2] = (new Vector2(position.y - 1,position.x - 1));
                        neighbours[3] = (new Vector2(position.y - 1,position.x));
                        neighbours[4] = (new Vector2(position.y - 1,position.x + 1));
                    }
                }
            }//It's an X edged tile, so -3, but is it a Y edged? 
            else if (position.y != 0 && position.y != tiles.Length-1) 
            { //No, so just -3
                neighbours = new Vector2[5];
                neighbours[0] = (new Vector2(position.y - 1,position.x));//We get to keep the ones directly above and below us for sure
                neighbours[1] = (new Vector2(position.y + 1,position.x));
                if (position.x == 0)//Are we crammed against the left edge?
                {//Then we get the 3 to our right
                    neighbours[2] = (new Vector2(position.y - 1,position.x + 1));
                    neighbours[3] = (new Vector2(position.y,position.x + 1));
                    neighbours[4] = (new Vector2(position.y + 1,position.x + 1));
                }
                else
                {//Otherwise it's the 3 to our left!
                    neighbours[2] = (new Vector2(position.y + 1,position.x - 1));
                    neighbours[3] = (new Vector2(position.y,position.x - 1));
                    neighbours[4] = (new Vector2(position.y - 1,position.x - 1));
                }
            }
            else
            {//Yes, so a further -2, this is a corner tile!
                neighbours = new Vector2[3];
                if (position.x == 0)
                {//We're against the left wall
                    if (position.y == 0)
                    {//And we're against the bottom wall
                        neighbours[0] = (new Vector2(position.y + 1,position.x)); //So we get these 3
                        neighbours[1] = (new Vector2(position.y + 1,position.x + 1));
                        neighbours[2] = (new Vector2(position.y,position.x + 1));
                    }
                    else
                    {//Otherwise we're against the left and top walls
                        neighbours[0] = (new Vector2(position.y,position.x + 1));//So we get these 3
                        neighbours[1] = (new Vector2(position.y - 1,position.x + 1));
                        neighbours[2] = (new Vector2(position.y - 1,position.x));
                    }
                }
                else
                {//We're against the right wall, so no x+1 tiles
                    if (position.y == 0)
                    {//And we're at the bottom
                        neighbours[0] = (new Vector2(position.y + 1,position.x));//So we get these 3
                        neighbours[1] = (new Vector2(position.y + 1,position.x-1));
                        neighbours[2] = (new Vector2(position.y,position.x-1));
                    }
                    else
                    {//We're in the top right corner
                        neighbours[0] = (new Vector2(position.y - 1,position.x));//So we get these 3
                        neighbours[1] = (new Vector2(position.y - 1,position.x-1));
                        neighbours[2] = (new Vector2(position.y,position.x-1));
                    }

                }
            }
            return neighbours;
        }
    }
}

