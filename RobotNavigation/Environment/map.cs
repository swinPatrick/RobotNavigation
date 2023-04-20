using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class Map
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Cell Start { get; private set; }
        public List<Cell> Ends { get; private set; }
        public List<Cell> Walls { get; private set; }
        
        /// <summary>
        ///  Constructor for Map, Can only be set once
        /// </summary>
        /// <param name="width">Width of the Map</param>
        /// <param name="height">Height of the Map</param>
        /// <param name="start">Starting cell</param>
        /// <param name="ends">Ending Cells</param>
        /// <param name="walls">Cells with Walls</param>
        public Map(int width, int height, Cell start, List<Cell> ends, List<Cell> walls)
        {
            Width = width;
            Height = height;
            Start = start;
            Ends = new List<Cell>(ends);
            Walls = walls;
        }

        public Map(Map aMap): this(aMap.Width, aMap.Height, aMap.Start, aMap.Ends, aMap.Walls)
        {}


        // print the map format into the console
        public void printMap()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write("[");
                    if(Ends.Any(c => c.X == x && c.Y == y))
                        Console.Write("E");
                    else if (Start.X == x && Start.Y == y)
                        Console.Write("S");
                    else if (Walls.Any(c => c.X == x && c.Y == y))
                        Console.Write("W");
                    else
                        Console.Write(" ");
                    Console.Write("]");
                }
                Console.WriteLine();
            }
        }

    }
}
