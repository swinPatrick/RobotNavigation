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

            foreach (State aScenario in aList)
            {
                // insert the scenario into the frontier in the correct position
                // based on the cost of the path travelled so far + the distance to the closest end cell
                newScenarioCost = CalculateCost(aScenario);
                bool inserted = false;
                for (int i = 0; i < Frontier.Count; i++)
                {
                    listScenarioCost = CalculateCost(Frontier.ElementAt(i));
                    if (listScenarioCost > newScenarioCost)
                    {
                        Frontier.Insert(i, aScenario);
                        inserted = true;
                        break;
                    }
                }
                if (!inserted)
                {
                    Frontier.Add(aScenario);
                }
            }
        }

        private int CalculateCost(State aScenario)
        {
            // return cost of path travelled so far
            return aScenario.CalculatePathCost();
        }
    }
}
