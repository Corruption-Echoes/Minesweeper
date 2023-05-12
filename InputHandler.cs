using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    public class InputHandler
    {
        public Dictionary<string, string[]> keyMaps;

        public void initializeMaps()
        {
            keyMaps= new Dictionary<string, string[]>();
            keyMaps["Left"] = new string[] {"LeftArrow","A"};
            keyMaps["Right"] = new string[] { "RightArrow", "D" };
            keyMaps["Up"] = new string[] { "UpArrow", "W" };
            keyMaps["Down"] = new string[] { "DownArrow", "S" };
            keyMaps["Confirm"] = new string[] { "Enter", "Spacebar" ,"X"};
            keyMaps["Cancel"] = new string[] { "Backspace", "Z" };
        }
        public string checkResult(string input)
        {
            Console.Clear();
            foreach(string key in keyMaps.Keys)
            {
                if (keyMaps[key].Contains(input)){
                    return key;
                }
            }
            return null;
        }

        public string getPlayerInput()
        {
            string intake = checkResult(Console.ReadKey().Key.ToString());
            return intake;
        }
    }
}
