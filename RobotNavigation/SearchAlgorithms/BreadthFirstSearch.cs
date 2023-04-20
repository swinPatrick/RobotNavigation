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
            Code = "BFS";
            Description = "Breadth First Search";
        }

        // Add list to frontier in the appropriate order (FIFO)
        internal override void AddListToFrontier(List<Node> aList)
        {
            foreach(Node lState in aList)
            {
                Frontier.Add(lState);
                _discovered++;
            }
        }
    }
}
