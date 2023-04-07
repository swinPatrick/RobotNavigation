using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class DepthFirstSearch : SearchMethod
    {

        // the count of how many have been discovered while searching, wand how many have actually been explored
        private int _discovered;
        private int _searched;
        private string _path;

        public DepthFirstSearch() 
        {
            code = "DFS";
            description = "Depth First Search";
        }

        // Add list to frontier in the appropriate order (LIFO)
        private void addListToFrontier(List<RobotScenario> _list)
        {

            for( int i = _list.Count - 1; i >= 0; i--)
            {
                _frontier.AddFirst(_list.ElementAt(i));
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

        public string Path()
        {
            _path = string.Empty;
            foreach(Instruction i in _frontier.Last().Robot.Path)
            {
                _path += i.ToString().ToLower();
                _path += ", ";
            }
            _path = _path.TrimEnd(' ').TrimEnd(',');
            return _path;
        }
    }
}
