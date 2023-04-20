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
            Code = "LCF";
            Description = "Lowest Cost First Search";
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
                newScenarioCost = aScenario.CalculatePathCost();
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

        private int CalculateCost(State aScenario)
        {
            // return cost of path travelled so far
            return aScenario.CalculatePathCost();
        }
    }
}
