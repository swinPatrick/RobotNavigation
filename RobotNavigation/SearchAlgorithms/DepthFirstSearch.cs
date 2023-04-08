using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class DepthFirstSearch : SearchMethod
    {
        public DepthFirstSearch() 
        {
            _code = "DFS";
            _description = "Depth First Search";
        }

        // Add list to frontier in the appropriate order (LIFO)
        internal override void AddListToFrontier(List<RobotScenario> aList)
        {

            for( int i = aList.Count - 1; i >= 0; i--)
            {
                _frontier.AddFirst(aList.ElementAt(i));
                _discovered++;
            }
        }
    }
}
