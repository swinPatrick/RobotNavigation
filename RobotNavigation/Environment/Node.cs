using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class Node
    {
        public Cell Cell { get; set; }
        public int X { get { return Cell.X; } }
        public int Y { get { return Cell.Y; } }
        public int Heuristic { get; set; }
        public Connection Connection { get; set; }

        public Node(Cell aCell, int aHeuristic, Connection aConnection)
        {
            Cell = aCell;
            Heuristic = aHeuristic;
            Connection = aConnection;
        }

        public Node(Cell aCell, Connection connection)
        {
            Cell=aCell;
            Connection = connection;
        }

    }
}
