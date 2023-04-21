using System;
using System.Collections.Generic;

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
                newState.CurrentNode.Heuristic = CalculateCost(newState);

                // search based on the heuristic value
                int index = Frontier.BinarySearch(newState, new StateComparer());
                if (index < 0) { index = ~index; }

                Frontier.Insert(index, newState);
            }
        }

        private int CalculateCost(State aState)
        {
            // Calculate distance to end
            // furthest distance possible is width + height of map
            int heuristic = aState.GetMap.Width + aState.GetMap.Height;
            // check all end cells and find the one with the lowest distance
            foreach (Cell endCell in aState.GetMap.Ends)
            {
                int endCost = 
                    Math.Abs(aState.CurrentNode.X - endCell.X) + 
                    Math.Abs(aState.CurrentNode.Y - endCell.Y);
                if (endCost < heuristic)
                {
                    heuristic = endCost;
                }
            }
            // add path cost to heuristic
            return heuristic + aState.CalculatePathCost();
        }
    }
}
