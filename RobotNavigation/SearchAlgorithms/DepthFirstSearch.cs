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

            for( int i = aList.Count - 1; i >= 0; i--)
            {
                Frontier.Insert(0,aList.ElementAt(i));
                _discovered++;
            }
        }
    }
}
