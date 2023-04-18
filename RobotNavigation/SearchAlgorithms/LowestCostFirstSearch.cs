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
            _code = "LCF";
            _description = "Lowest Cost First Search";
        }

        // Add list to frontier in the appropriate order
        internal override void AddListToFrontier(List<RobotScenario> aList)
        {
            int newScenarioCost;
            int listScenarioCost;

            _discovered += aList.Count;

            List<RobotScenario> lList = new List<RobotScenario>(_frontier);

            foreach (RobotScenario aScenario in aList)
            {
                // insert the scenario into the frontier in the correct position
                // based on the cost of the path travelled so far + the distance to the closest end cell
                newScenarioCost = aScenario.CalculatePathCost();
                bool inserted = false;
                for (int i = 0; i < lList.Count; i++)
                {
                    RobotScenario lElement = lList.ElementAt(i);
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

            _frontier = null;
            _frontier = new LinkedList<RobotScenario>(lList);
        }

        private int CalculateCost(RobotScenario aScenario)
        {
            // return cost of path travelled so far
            return aScenario.CalculatePathCost();
        }
    }
}
