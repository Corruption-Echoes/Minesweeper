using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public class loop
    {
        public Vector2 position;
        public Vector2 mapSize=new Vector2(10,10);
        public InputHandler inputHandler;
        public cursorHandler cursorHandler;
        public int mineCount = 10;
        bool wrapping = false;
        Map map;
        public void mainLoop()
        {
            position = new Vector2(5, 5);
            inputHandler = new InputHandler();
            inputHandler.initializeMaps();
            cursorHandler=new cursorHandler(mapSize, Vector2.Zero() ,wrapping);
            map = mapGenerator.generateMap(mapSize, mineCount);
            printBoard();
            while (true)
            {
                string input=inputHandler.getPlayerInput();
                position=cursorHandler.cursorLogic(input,position);
                printBoard();
            }
        }public void printBoard()
        {
            Console.WriteLine("--------------------");
            for(int y = mapSize.y-1; y >-1 ; y--)
            {
                Console.Write("\n");
                for(int  x = 0; x < mapSize.x; x++)
                {
                    if (y == position.y && x == position.x)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X ");
                    }
                    else if (!map.tiles[y][x].isRevealed)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(map.tiles[y][x].explosiveCount+" ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("0 ");
                    }
                    /*if (i != position.y || j != position.x)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("0 ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X ");
                    }*/
                }
            }
            Console.ResetColor();
        }

    }
}
