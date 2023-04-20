using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        internal override void AddListToFrontier(List<Node> aList)
        {
            int totalCostI;
            int totalCostJ;

            _discovered += aList.Count;

            aList.AddRange(Frontier);
            
            // sort the list by total cost
            for(int i = 0; i < aList.Count; i++)
            {
                for (int j = i + 1; j < aList.Count; j++)
                {
                    // calculate cost of element at i
                    totalCostI = CalculateCost(aList.ElementAt(i));
                    // calculate cost of element at j
                    totalCostJ = CalculateCost(aList.ElementAt(j));
                    if (totalCostJ < totalCostI)
                    {
                        State temp = aList.ElementAt(i);
                        aList[i] = aList.ElementAt(j);
                        aList[j] = temp;
                    }
                }
            }

            Frontier.Clear();
            foreach(State temp in aList)
            { 
                Frontier.Add(temp);
            }
        }

        private int CalculateCost(State aState)
        {
            int lLowestCost = aState.GetMap.Width + aState.GetMap.Height;
            foreach(Cell endCell in aState.GetMap.Ends)
            {
                int lCost = Math.Abs(aState.Robot.X - endCell.X) + Math.Abs(aState.Robot.Y - endCell.Y);
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
