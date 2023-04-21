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
            Code = "DFS";
            Description = "Depth First Search";
        }

        // Add list to frontier in the appropriate order (LIFO)
        internal override void AddListToFrontier(List<State> aList)
        {

            _discovered += aList.Count;
            Frontier.InsertRange(0, aList);
        }
    }
}
