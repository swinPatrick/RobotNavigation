using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    /// <summary>
    /// Defines the type of cells that can be used
    /// </summary>
    public enum CellType
    {
        START,
        END,
        WALL,
        EMPTY
    }

    public class Cell
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public CellType CellType { get; private set; }
        //public bool Visited { get; set; }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="aX">X coordinate of Cell</param>
        /// <param name="aY">Y coordinate of Cell</param>
        /// <param name="aCellType">Type of Cell</param>
        public Cell(int aX, int aY, CellType aCellType)
        {
            X = aX;
            Y = aY;
            CellType = aCellType;
        }
    }
}
