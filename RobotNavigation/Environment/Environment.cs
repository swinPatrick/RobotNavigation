using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation.Environment
{
    internal class Environment
    {
        private Map _map;
        private Cell _robot;
        private LinkedList<Instruction> _path;

        public Map Map { get { return _map; } }

        public Cell Robot { get { return _robot; } }
        
        public Environment(Map aMap, Cell aRobot, LinkedList<Instruction> aPath)
        {
            _map = aMap;
            _robot = aRobot;
            _path = new LinkedList<Instruction>(aPath);
            _map.Cells[_robot.X, _robot.Y].WasVisited = true;
        }

        public Environment(Map aMap)
        {
            _map = aMap;
            _robot = _map.Start;
            _path = new LinkedList<Instruction>();
            _map.Cells[_robot.X, _robot.Y].WasVisited = true;
        }

        // transform map into string to display on console, including start and end points
        public override string ToString()
        {
            string mapString = "";
            for (int y = 0; y < _map.Height; y++)
            {
                for (int x = 0; x < _map.Width; x++)
                {
                    if (_map.Cells[x, y].Contents == CellContents.EMPTY)
                    {
                        if (_map.Cells[x, y].Equals(_map.Start))
                        {
                            mapString += "S";
                        }
                        else if (_map.Ends.Contains(_map.Cells[x, y]))
                        {
                            mapString += "E";
                        }
                        else
                        {
                            mapString += " ";
                        }
                    }
                    else if (_map.Cells[x, y].Contents == CellContents.WALL)
                    {
                        mapString += "#";
                    }
                    else if (_map.Cells[x, y].Contents == CellContents.ROBOT)
                    {
                        mapString += "R";
                    }
                }
                mapString += "\r";
            }

            return mapString;
        }
    }

}
