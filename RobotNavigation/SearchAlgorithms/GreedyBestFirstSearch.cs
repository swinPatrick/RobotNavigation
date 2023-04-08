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
            _code = "GBFS";
            _description = "Greedy Best-First Search";
        }

        // Add list to frontier in the appropriate order
        internal override void AddListToFrontier(List<RobotScenario> aList)
        {
            int totalCostI;
            int totalCostJ;

            _discovered += aList.Count;

            aList.AddRange(_frontier);
            
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
                        RobotScenario temp = aList.ElementAt(i);
                        aList[i] = aList.ElementAt(j);
                        aList[j] = temp;
                    }
                }
            }

            _frontier.Clear();
            foreach(RobotScenario temp in aList)
            { 
                _frontier.AddLast(temp);
            }
        }

        private int CalculateCost(RobotScenario aScenario)
        {
            int lLowestCost = _map.Width + _map.Height;
            foreach(Cell endCell in _map.Ends)
            {
                int lCost = Math.Abs(aScenario.Robot.X - endCell.X) + Math.Abs(aScenario.Robot.Y - endCell.Y);
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
