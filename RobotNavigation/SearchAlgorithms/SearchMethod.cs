using RobotNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public abstract class SearchMethod
    {
        internal string _code;
        internal string _description;
        public string Code { get { return _code; } }
        public string Description { get { return _description; } }

        protected RobotScenario _scenario;
        protected Map _map;
        protected LinkedList<RobotScenario> _frontier;

        
        internal int _discovered;
        internal int _searched;

        public void Initialise(Map aMap)
        {
            _map = aMap;
            Cell lStart = _map.Start;

            _frontier = new LinkedList<RobotScenario>();
            _frontier.AddFirst(new RobotScenario(_map, new Robot(lStart.X, lStart.Y)));
            _map.Cells[lStart.X, lStart.Y].WasVisited = true;
        }

        public virtual List<Instruction> FindPath()
        {
            RobotScenario lState;

            // while the frontier is not empty
            while (_frontier.Count > 0)
            {
                lState = _frontier.First();
                _frontier.RemoveFirst();

                _map.Cells[lState.Robot.X, lState.Robot.Y].WasVisited = true;

                // increment searched cells
                _searched++;

                // check to see if robot is at an end
                if (lState.IsSolved())
                {
                    _frontier.Clear();
                    return lState.Robot.Path;
                }

                // determine what moves can be done
                AddListToFrontier(lState.DetermineMoveSet());
            }

            // no solution was found
            return null;
        }

        internal abstract void AddListToFrontier(List<RobotScenario> aList);

    }
}
