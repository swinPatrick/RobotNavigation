using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation.SearchAlgorithms
{
    internal class DepthFirstSearch : SearchMethod
    {
        // linked list for the queue
        LinkedList<cell> _frontier;

        // the object thhat defines how to interact with the data we are searching


        // the count of how many have been discovered while searching, wand how many have actually been explored
        private int _discovered;
        private int _searched;

        public DepthFirstSearch(Robot aRobot, map aMap) : base(aRobot, aMap)
        {
            _frontier = new LinkedList<cell>();

            _discovered = 0;
            _searched = 0;
        }

        public override void findPath()
        {
            //if( )
            // Direct a robot to the goal cell using a depth-first search algorithm.
            // The robot can move in four directions: up, left, down, and right.
            // The robot can only move to a cell that is not a wall.
            // The robot can only move to a cell that has not been visited before.
            // The robot can only move to a cell that is adjacent to the current cell.

            // The robot starts at the start cell.
            // 
            foreach( Instruction instruction in Enum.GetValues(typeof(Instruction)) )
            {
                // 
            }
        }
    }
}
