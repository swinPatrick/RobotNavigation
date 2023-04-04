using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    internal abstract class SearchMethod
    {
        protected Robot robot;
        protected map environment;
        protected List<cell> frontier;

        public map Environment { get { return environment; } }

        public List<cell> Frontier { get { return frontier; } }

        public SearchMethod(Robot aRobot, map aMap)
        {
            robot = aRobot;
            environment = aMap;

            frontier = new List<cell>();
            frontier.Add(environment.Cells[robot.X, robot.Y]);
        }

        public abstract void findPath();
    }
}
