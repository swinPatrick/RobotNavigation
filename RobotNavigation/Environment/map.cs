using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class Map
    {
        private readonly int _width;
        private readonly int _height;
        private Cell[,] _cells;
        private Cell _start;
        private List<Cell> _ends;

        public int Width { get { return _width; } }

        public int Height { get { return _height; } }
        
        public Cell[,] Cells { get { return _cells; } }

        public Cell Start { get { return _start; } }
        public List<Cell> Ends { get { return _ends; } }
        
        public Map(int width, int height)
        {
            _width = width;
            _height = height;
            _cells = new Cell[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _cells[x, y] = new Cell(x, y, cellType.EMPTY);
                }
            }
            _ends = new List<Cell>();
        }

        public void setCell(int x, int y, cellType aType)
        {
            _cells[x, y].Type = aType;
            if(aType == cellType.START)
                _start = _cells[x, y];
            else if(aType == cellType.END)
                _ends.Add(_cells[x, y]);
        }

        // print the map format into the console
        public void printMap()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    Console.Write("[");
                    switch (_cells[x, y].Type)
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
