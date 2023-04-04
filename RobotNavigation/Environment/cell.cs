using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    internal enum cellType
    {
        START,
        END,
        WALL,
        EMPTY
    }

    internal class cell
    {
        private int _x;
        private int _y;
        private cellType _type;
        private bool _visited;

        public int X { get { return _x; } }

        public int Y { get { return _y; } }

        public bool Visited { get { return _visited; } set { _visited = value; } }

        public cellType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public cell(int aX, int aY, cellType aType)
        {
            _x = aX;
            _y = aY;
            _type = aType;
            _visited = false;
        }
    }
}
