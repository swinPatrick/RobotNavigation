using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public enum CellType
    {
        START,
        END,
        WALL,
        EMPTY
    }

    public class Cell
    {

        private Coordinate Coords;
        public int X { get { return Coords.X; } }
        public int Y { get { return Coords.Y; } }
        public bool Visited { get; set; }

        /// <summary>
        ///  Constructor for Cell
        /// </summary>
        /// <param name="aCoords"></param>
        public Cell(Coordinate aCoords)
        {
            Coords = aCoords;
            Visited = false;
        }

        public Cell(int startX, int startY, CellType sTART): this(new Coordinate(startX, startY, sTART))
        {}
    }
}
