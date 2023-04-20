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

            List<State> lList = new List<State>(Frontier);

            foreach (State aScenario in aList)
            {
                // insert the scenario into the frontier in the correct position
                // based on the cost of the path travelled so far + the distance to the closest end cell
                newScenarioCost = CalculateCost(aScenario);
                bool inserted = false;
                for (int i = 0; i < lList.Count; i++)
                {
                    State lElement = lList.ElementAt(i);
                    listScenarioCost = CalculateCost(lElement);
                    if (listScenarioCost > newScenarioCost)
                    {
                        lList.Insert(i, aScenario);
                        inserted = true;
                        break;
                    }
                }
                if (!inserted)
                {
                    lList.Add(aScenario);
                }
            }

            Frontier = null;
            Frontier = new List<State>(lList);
        }

        private int CalculateCost(State aState)
        {
            // Calculate the cost of a state based on the cost of the path travelled so far + the distance to the closest end cell
            // furthest distance possible is width + height of map
            int lLowestCost = aState.GetMap.Width + aState.GetMap.Height;
            // check all end cells and find the one with the lowest distance to the current node
            foreach (Cell endCell in aState.GetMap.Ends)
            {
                int lCost = Math.Abs(aState.CurrentNode.X - endCell.X) + Math.Abs(aState.CurrentNode.Y - endCell.Y);
                if (lCost < lLowestCost)
                {
                    lLowestCost = lCost;
                }
            }

            // return cost of path travelled so far + distance to closest end cell
            return aState.CalculatePathCost() + lLowestCost;
        }
    }
}