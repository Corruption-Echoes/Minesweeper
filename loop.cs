using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public class loop
    {
        public Vector2 position;
        public Vector2 mapSize=new Vector2(15,15);
        public InputHandler inputHandler;
        public cursorHandler cursorHandler;
        public int mineCount = 15;
        bool wrapping = false;
        Map map;
        public void mainLoop()
        {
            Console.WriteLine("How large would you like the map to be?(5-50 reccomended)");
            int mapsize = int.Parse(Console.ReadLine());
            mapSize = new Vector2(mapsize, mapsize);
            Console.WriteLine("How many mines would you like us to plant?");
            mineCount = int.Parse(Console.ReadLine());
            Console.WriteLine("Alright have fun! You need to place a flag(backspace) on every mine to win! \n(Press any button to continue)");
            Console.ReadKey();
            Console.Clear();
            Console.CursorVisible = false;
            Program.SP.playSound("start");
            position = new Vector2(12, 2);
            inputHandler = new InputHandler();
            inputHandler.initializeMaps();
            cursorHandler=new cursorHandler(mapSize, Vector2.Zero() ,wrapping);
            map = mapGenerator.generateMap(mapSize, mineCount);
            printBoard();
            while (!Program.lost && Program.hiddenTiles != 0)
            {
                string input=inputHandler.getPlayerInput();
                position=cursorHandler.cursorLogic(input,position,map);
                printBoard();
            }
            Console.WriteLine("");
            if (Program.lost)
            {
                Program.SP.playSound("kaboom");
                Console.WriteLine("Kaboom! You lost!");
            }
            else if (Program.hiddenTiles == 0)
            {
                Program.SP.playSound("victory");
                Console.WriteLine("Victory!");
            }
            Console.WriteLine("Play again soon!");
            Console.ReadLine();
        }
        public void printBoard()
        {
            Console.SetCursorPosition(0, 0);
            Program.hiddenTiles = 0;
            for(int y = mapSize.y-1; y >-1 ; y--)
            {
                Console.Write("\n");
                for(int  x = 0; x < mapSize.x; x++)
                {
                    if (y == position.y && x == position.x)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("X ");
                    }
                    else if (map.tiles[y][x].isRevealed)
                    {
                        if (map.tiles[y][x].isMine)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("! ");
                            Program.lost = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(map.tiles[y][x].explosiveCount + " ");
                        }
                    }
                    else
                    {
                        if (map.tiles[y][x].isMarked)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("^ ");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("0 ");
                            if (!map.tiles[y][x].isMine)
                            {
                                Program.hiddenTiles++;
                            }
                        }
                    }
                }
            }
            Console.ResetColor();
        }

    }
}
