using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public class loop
    {
        public vector2 position;
        public vector2 mapSize=new vector2(10,10);
        public InputHandler inputHandler;
        public cursorHandler cursorHandler;
        bool wrapping = false;
        public void mainLoop()
        {
            position = new vector2(0, 0);
            inputHandler = new InputHandler();
            inputHandler.initializeMaps();
            cursorHandler=new cursorHandler(mapSize, vector2.Zero() ,wrapping);
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
            for(int i = mapSize.y; i >-1 ; i--)
            {
                Console.Write("\n");
                for(int  j = 0; j < mapSize.x+1; j++)
                {
                    if (i != position.y || j != position.x)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("0 ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("X ");
                    }
                }
            }
            Console.ResetColor();
        }

    }
}
