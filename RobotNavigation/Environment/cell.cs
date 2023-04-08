using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public enum cellType
    {
        START,
        END,
        WALL,
        EMPTY
    }

    public class Cell
    {
        private readonly int _x;
        private readonly int _y;
        private cellType _type;
        private bool _visited;

        public int X { get { return _x; } }

        public int Y { get { return _y; } }

        public bool wasVisited { get { return _visited; } set { _visited = value; } }

        public cellType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Cell(int aX, int aY, cellType aType)
        {
            _x = aX;
            _y = aY;
            _type = aType;
            _visited = false;
        }
    }
}
