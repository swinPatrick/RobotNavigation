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
            int newStateCost;
            int lStateInListCost;

            _discovered += aList.Count;

            foreach (State newState in aList)
            {
                // set the heuristic value for the new location
                newState.CurrentNode.Heuristic = CalculateHeuristic(newState);

                newStateCost = newState.CurrentNode.Heuristic;

                // if newState is already in Frontier, and came from same direction
                // keep the state which has the lowest heuristic from that direction only
                State lStateToRemove = null;
                foreach(State lState in Frontier.Where(s =>
                    newState.CurrentNode.X == s.CurrentNode.X && 
                    newState.CurrentNode.Y == s.CurrentNode.Y &&
                    (int)newState.CurrentNode.Connection.Direction % 4 == (int)s.CurrentNode.Connection.Direction % 4))
                {
                    // if new state is cheapest way to reach cell from that direction, delete the old state.
                    if (newState.CurrentNode.Heuristic < lState.CurrentNode.Heuristic)
                        lStateToRemove = lState;
                }
                if(lStateToRemove != null)
                {
                    Frontier.Remove(lStateToRemove);
                }

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

        private int CalculateCost(State aState)
        {
            // return cost of path travelled so far + distance to closest end cell
            return aState.CalculatePathCost() + aState.CurrentNode.Heuristic;
        }
    }
}