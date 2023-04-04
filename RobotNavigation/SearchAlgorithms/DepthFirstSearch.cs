using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation.SearchAlgorithms
{
    internal class DepthFirstSearch : SearchMethod
    {
        // current frontier
        private RobotScenario _front;
        // the object thhat defines how to interact with the data we are searching


        // the count of how many have been discovered while searching, wand how many have actually been explored
        private int _discovered;
        private int _searched;

        public DepthFirstSearch(Map aMap) : base(aMap)
        {
            _discovered = 0;
            _searched = 0;
        }

        public override void findPath()
        {
            // Direct a robot to the goal cell using a depth-first search algorithm.
            // The robot can move in four directions: up, left, down, and right.
            // The robot can only move to a cell that is not a wall.
            // The robot can only move to a cell that has not been visited before.
            // The robot can only move to a cell that is adjacent to the current cell.

            _front = _frontier.First.Value;
            _frontier.RemoveFirst();

            LinkedList<RobotScenario> moves = _front.DetermineMoveSet();
            foreach(RobotScenario scenario in moves)
            {
                _frontier.AddFirst(scenario);
            }

            _searched++;

            if (_front.IsSolved())
            {
                _frontier.Clear();
                _frontier.AddFirst(_front);
            }
            else
            {
                findPath();
            }
        }
    }
}
