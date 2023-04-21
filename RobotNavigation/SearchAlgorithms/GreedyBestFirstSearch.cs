using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class GreedyBestFirstSearch : SearchMethod
    {
        public GreedyBestFirstSearch() 
        {
            Code = "GBFS";
            Description = "Greedy Best-First Search";
        }

        // Add list to frontier in the appropriate order
        internal override void AddListToFrontier(List<State> aList)
        {
            int newStateCost;
            int lStateInListCost;

            _discovered += aList.Count;

            foreach (State newState in aList)
            {
                // set the heuristic value for the new location
                newState.CurrentNode.Heuristic = CalculateCost(newState);

                newStateCost = newState.CurrentNode.Heuristic;

                bool inserted = false;
                for (int i = 0; i < Frontier.Count; i++)
                {
                    lStateInListCost = Frontier.ElementAt(i).CurrentNode.Heuristic;
                    if (lStateInListCost > newStateCost)
                    {
                        Frontier.Insert(i, newState);
                        inserted = true;
                        break;
                    }
                }
                // if it reaches the end, it's got the highest cost.
                if (!inserted)
                {
                    Frontier.Add(newState);
                }
            }
        }

        private int CalculateCost(State aState)
        {
            int lLowestCost = aState.GetMap.Width + aState.GetMap.Height;
            foreach(Cell endCell in aState.GetMap.Ends)
            {
                int lCost = Math.Abs(aState.CurrentNode.X - endCell.X) + Math.Abs(aState.CurrentNode.Y - endCell.Y);
                if (lCost < lLowestCost)
                {
                    lLowestCost = lCost;
                }
            }

            // return cost distance to closest end cell
            return lLowestCost;
        }
    }
}
