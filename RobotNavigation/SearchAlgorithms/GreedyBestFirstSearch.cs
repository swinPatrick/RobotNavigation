﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class GreedyBestFirstSearch : SearchMethod
    {

        // the count of how many have been discovered while searching, wand how many have actually been explored
        private int _discovered;
        private int _searched;


        public GreedyBestFirstSearch() 
        {
            code = "GBFS";
            description = "Greedy Best-First Search";
        }

        // Add list to frontier in the appropriate order
        private void addListToFrontier(List<RobotScenario> _list)
        {
            int totalCostI;
            int totalCostJ;

            _discovered += _list.Count;

            _list.AddRange(_frontier);
            
            // sort the list by total cost
            for(int i = 0; i < _list.Count; i++)
            {
                for (int j = i + 1; j < _list.Count; j++)
                {
                    // calculate cost of element at i
                    totalCostI = CalculateCost(_list.ElementAt(i));
                    // calculate cost of element at j
                    totalCostJ = CalculateCost(_list.ElementAt(j));
                    if (totalCostJ < totalCostI)
                    {
                        RobotScenario temp = _list.ElementAt(i);
                        _list[i] = _list.ElementAt(j);
                        _list[j] = temp;
                    }
                }
            }

            _frontier.Clear();
            foreach(RobotScenario temp in _list)
            { 
                _frontier.AddLast(temp);
            }
        }

        public override List<Instruction> FindPath()
        {
            RobotScenario lState = null;

            // while the frontier is not empty
            while (_frontier.Count > 0)
            {
                lState = _frontier.First();
                _frontier.RemoveFirst();

                _map.Cells[lState.Robot.X, lState.Robot.Y].wasVisited = true;

                // increment searched cells
                _searched++;

                // check to see if robot is at an end
                if (lState.IsSolved())
                {
                    _frontier.Clear();
                    return lState.Robot.Path;
                }

                // determine what moves can be done
                addListToFrontier(lState.DetermineMoveSet());
            }

            // no solution was found
            return null;
        }

        private int CalculateCost(RobotScenario scenario)
        {
            int lowestCost = _map.Width + _map.Height;
            foreach(cell endCell in _map.Ends)
            {
                int cost = Math.Abs(scenario.Robot.X - endCell.X) + Math.Abs(scenario.Robot.Y - endCell.Y);
                if (cost < lowestCost)
                {
                    lowestCost = cost;
                }
            }

            // return cost distance to closest end cell
            return lowestCost;
        }
    }
}
