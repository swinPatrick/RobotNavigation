using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    internal class readMap
    {
        public static Map readMapFromFile(string filename)
        {
            // char array of characters to remove from begining and end of lines
            char[] trimChars = { ' ', '(', ')', '[', ']' };
            string[] lines = null;

            try {
                lines = System.IO.File.ReadAllLines(filename);
            }
                catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                // stop the program
                Environment.Exit(0);
            }
            // parse the map file line by line

            // the first line contains the map size in the format [heigh,width]
            string[] size = lines[0].Trim(trimChars).Split(',');
            int width = int.Parse( size[1] );
            int height = int.Parse(size[0] );
            Map lMap = new Map(width, height);

            // the second line contains the start position in the format (x,y)
            string[] start = lines[1].Trim(trimChars).Split(',');
            int startX = int.Parse(start[0]);
            int startY = int.Parse(start[1]);
            lMap.setCell(startX, startY, cellType.START);

            // the third line contains the end position(s) in the format (x,y) | (x,y)
            string[] ends = lines[2].Split('|');
            foreach (string end in ends)
            {
                string[] endPos = end.Trim(trimChars).Split(',');
                int endX = int.Parse(endPos[0]);
                int endY = int.Parse(endPos[1]);
                lMap.setCell(endX, endY, cellType.END);
            }

            // the remaining lines contain the walls in the format (startX, startY, width, height)
            for (int i = 3; i < lines.Length; i++)
            {
                string[] wall = lines[i].Trim(trimChars).Split(',');
                int wallX = int.Parse(wall[0]);
                int wallY = int.Parse(wall[1]);
                int wallWidth = int.Parse(wall[2]);
                int wallHeight = int.Parse(wall[3]);
                for (int x = wallX; x < wallX + wallWidth; x++)
                {
                    for (int y = wallY; y < wallY + wallHeight; y++)
                    {
                        lMap.setCell(x, y, cellType.WALL);
                    }
                }
            }


            return lMap;
        }
    }
}
