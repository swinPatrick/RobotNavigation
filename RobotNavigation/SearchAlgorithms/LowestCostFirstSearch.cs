using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class LowestCostFirstSearch : SearchMethod
    {
        public LowestCostFirstSearch() 
        {
            Code = "CUS1";
            Description = "Lowest Cost First Search";
        }

        // Add list to frontier in the appropriate order
        internal override void AddListToFrontier(List<State> aList)
        {
            _discovered += aList.Count;

            foreach (State newState in aList)
            {
                // set the heuristic value for the new location
                newState.CurrentNode.Heuristic = CalculateHeuristic(newState);

                // search based on the heuristic value
                int index = Frontier.BinarySearch(newState, new StateComparer());
                if (index < 0) { index = ~index; }

                Frontier.Insert(index, newState);
            }
        }

        private int CalculateHeuristic(State aScenario)
        {
            // return cost of path travelled so far
            return aScenario.CalculatePathCost();
        }
    }
}
