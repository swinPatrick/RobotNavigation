using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public enum CellContents
    {
        EMPTY,
        WALL,
        ROBOT
    }

    public class Cell
    {
        private readonly int _x;
        private readonly int _y;
        private CellContents _contents;
        private bool _visited;
        private Cell _parent = null;

        public int X { get { return _x; } }

        public int Y { get { return _y; } }

        public Cell Parent { get { return _parent; } set { _parent = value; } }

        public bool WasVisited { get { return _visited; } set { _visited = value; } }

        public CellContents Contents { get { return _contents; } set { _contents = value; } }

        public Cell(int aX, int aY, CellContents aContents = CellContents.EMPTY, Cell aParent = null)
        {
            _x = aX;
            _y = aY;
            _contents = aContents;
            _parent = aParent;
            _visited = false;
        }

        public Cell(Cell aCell)
        {
            _x = aCell.X;
            _y = aCell.Y;
            _contents = aCell.Contents;
            _visited = aCell.WasVisited;
            _parent = aCell.Parent;
        }
    }
}
