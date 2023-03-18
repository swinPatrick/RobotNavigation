using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    internal class map
    {
        private int width;
        private int height;
        private cell[,] cells;

        public int Width { get { return width; } }

        public int Height { get { return height; } }
        
        public cell[,] Cells { get { return cells; } }
        
        public map(int width, int height)
        {
            this.width = width;
            this.height = height;
            cells = new cell[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cells[x, y] = new cell(x, y, cellType.empty);
                }
            }
        }

        public void setCell(int x, int y, cellType aType)
        {
            cells[x, y].Type = aType;
        }

        // print the map format into the console
        public void printMap()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write("[");
                    switch (cells[x, y].Type)
                    {
                        case cellType.start:
                            Console.Write("S");
                            break;
                        case cellType.end:
                            Console.Write("E");
                            break;
                        case cellType.wall:
                            Console.Write("W");
                            break;
                        case cellType.empty:
                            Console.Write(" ");
                            break;
                    }
                    Console.Write("]");
                }
                Console.WriteLine();
            }
        }
    }
}
