using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class Coordinate
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public CellType CellType { get; private set; }

        /// <summary>
        ///  Constructor for Coordinate
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="aCellType"></param>
        public Coordinate(int aX, int aY, CellType aCellType)
        {
            X = aX;
            Y = aY;
            CellType = aCellType;
        }
    }
}
