using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotNavigation
{
    public class AStarSearch : SearchMethod
    {
        public AStarSearch()
        {
            Code = "AS";
            Description = "A* Search";
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

        private int CalculateHeuristic(State aState)
        {
            // Calculate the cost of a state based on the cost of the path travelled so far + the distance to the closest end cell
            // furthest distance possible is width + height of map
            int heuristic = aState.GetMap.Width + aState.GetMap.Height;
            // check all end cells and find the one with the lowest distance to the current node
            foreach (Cell endCell in aState.GetMap.Ends)
            {
                int lCost = Math.Abs(aState.CurrentNode.X - endCell.X) + Math.Abs(aState.CurrentNode.Y - endCell.Y);
                if (lCost < heuristic)
                {
                    heuristic = lCost;
                }
            }
            return heuristic + aState.CalculatePathCost();
        }
    }
}