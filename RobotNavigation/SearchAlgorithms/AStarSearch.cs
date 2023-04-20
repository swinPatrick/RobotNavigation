using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotNavigation
{
    public class AStarSearch : SearchMethod
    {
        public AStarSearch()
        {
            Code = "A*";
            Description = "A* Search";
        }

        // Add list to frontier in the appropriate order
        internal override void AddListToFrontier(List<State> aList)
        {
            int newScenarioCost;
            int listScenarioCost;

            _discovered += aList.Count;

            foreach (State aState in aList)
            {
                // set the heuristic value for the new location
                aState.CurrentNode.Heuristic = CalculateHeuristic(aState);

                // insert the scenario into the frontier in the correct position
                // based on the cost of the path travelled so far + the distance to the closest end cell

                newScenarioCost = CalculateCost(aState);
                bool inserted = false;
                State lElement;
                for (int i = 0; i < Frontier.Count; i++)
                {
                    lElement = Frontier.ElementAt(i);
                    listScenarioCost = CalculateCost(lElement);
                    if (listScenarioCost > newScenarioCost)
                    {
                        Frontier.Insert(i, aState);
                        inserted = true;
                        break;
                    }
                }
                if (!inserted)
                {
                    Frontier.Add(aState);
                }
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
            return heuristic;
        }

        private int CalculateCost(State aState)
        {
            // return cost of path travelled so far + distance to closest end cell
            return aState.CalculatePathCost() + aState.CurrentNode.Heuristic;
        }
    }
}