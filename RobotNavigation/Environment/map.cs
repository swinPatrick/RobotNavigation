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
        private readonly Cell[,] _cells;
        private readonly Cell _start;
        private readonly HashSet<Cell> _ends;

        public int Width { get { return _width; } }

        public int Height { get { return _height; } }
        
        public Cell[,] Cells { get { return _cells; } }

        public Cell Start { get { return _start; } }
        public HashSet<Cell> Ends { get { return _ends; } }
        
        public Map(int aWidth, int aHeight, Cell aStart, HashSet<Cell> aEnds)
        {
            _width = aWidth;
            _height = aHeight;

            _start = new Cell(aStart);
            _ends = new HashSet<Cell>(aEnds);

            _cells = new Cell[aWidth, aHeight];
            for (int x = 0; x < aWidth; x++)
            {
                for (int y = 0; y < aHeight; y++)
                {
                    _cells[x, y] = new Cell(x, y);
                }
            }
        }

        public void SetCell(int x, int y, CellContents aContents)
        {
            _cells[x, y].Contents = aContents;
        }
    }
}
