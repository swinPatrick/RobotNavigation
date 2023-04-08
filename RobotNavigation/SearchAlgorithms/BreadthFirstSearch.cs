using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class BreadthFirstSearch : SearchMethod
    {
        public BreadthFirstSearch() 
        {
            _code = "BFS";
            _description = "Breadth First Search";
        }

        // Add list to frontier in the appropriate order (FIFO)
        internal override void AddListToFrontier(List<RobotScenario> aList)
        {
            foreach(RobotScenario lState in aList)
            {
                _frontier.AddLast(lState);
                _discovered++;
            }
        }
    }
}
