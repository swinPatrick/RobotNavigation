using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    internal enum cellType
    {
        start,
        end,
        wall,
        empty
    }

    internal class cell
    {
        private int x;
        private int y;
        private cellType type;

        public int X { get { return x; } }

        public int Y { get { return y; } }

        public cellType Type
        {
            get { return type; }
            set { type = value; }
        }

        public cell(int aX, int aY, cellType aType)
        {
            x = aX;
            y = aY;
            type = aType;
        }
    }
}
