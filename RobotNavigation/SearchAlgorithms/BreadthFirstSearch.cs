using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class BreadthFirstSearch : SearchMethod
    {

        // the count of how many have been discovered while searching, wand how many have actually been explored
        private int _discovered;
        private int _searched;

        public BreadthFirstSearch() 
        {
            code = "BFS";
            description = "Breadth First Search";
        }

        // Add list to frontier in the appropriate order (FIFO)
        private void addListToFrontier(List<RobotScenario> _list)
        {
            foreach(RobotScenario lState in _list)
            {
                _frontier.AddLast(lState);
                _discovered++;
            }
        }

        public override List<Instruction> FindPath()
        {
            RobotScenario lState = null;

            // while the frontier is not empty
            while (_frontier.Count > 0)
            {
                lState = _frontier.First();
                _frontier.RemoveFirst();

                _map.Cells[lState.Robot.X, lState.Robot.Y].wasVisited = true;

                // increment searched cells
                _searched++;

                // check to see if robot is at an end
                if (lState.IsSolved())
                {
                    _frontier.Clear();
                    return lState.Robot.Path;
                }

                // determine what moves can be done
                addListToFrontier(lState.DetermineMoveSet());
            }

            // no solution was found
            return null;
        }
    }
}
