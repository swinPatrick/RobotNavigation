using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class Map
    {
        private int width;
        private int height;
        private cell[,] cells;
        private cell start;
        private List<cell> ends;

        public int Width { get { return width; } }

        public int Height { get { return height; } }
        
        public cell[,] Cells { get { return cells; } }

        public cell Start { get { return start; } }
        public List<cell> Ends { get { return ends; } }
        
        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            cells = new cell[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cells[x, y] = new cell(x, y, cellType.EMPTY);
                }
            }
            ends = new List<cell>();
        }

        public void setCell(int x, int y, cellType aType)
        {
            cells[x, y].Type = aType;
            if(aType == cellType.START)
                start = cells[x, y];
            else if(aType == cellType.END)
                ends.Add(cells[x, y]);
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
                        case cellType.START:
                            Console.Write("S");
                            break;
                        case cellType.END:
                            Console.Write("E");
                            break;
                        case cellType.WALL:
                            Console.Write("W");
                            break;
                        case cellType.EMPTY:
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
