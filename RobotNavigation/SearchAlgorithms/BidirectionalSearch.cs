using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RobotNavigation
{
    public class BidirectionalSearch : SearchMethod
    {
        // secondary frontier
        private LinkedList<RobotScenario> _backFrontier;
        private HashSet<RobotScenario> _frontVisited;
        private HashSet<RobotScenario> _backVisited;


        public BidirectionalSearch() 
        {
            _code = "BD";
            _description = "Bidirectional Search";

            // initialize the front and back frontier
            _frontVisited = new HashSet<RobotScenario>();
        }


        public override List<Instruction> FindPath()
        {
            // Initialize the back frontier with all the end cells
            var endStates = _map.Ends.Select(end => new RobotScenario(_map, new Robot(end.X, end.Y)));
            _backFrontier = new LinkedList<RobotScenario>(endStates);
            _backVisited = new HashSet<RobotScenario>(endStates);
            endStates.Select(end => _map.Cells[end.Robot.X, end.Robot.Y].WasVisited = true);

            // Initialize the frontier with the start cell
            _frontVisited = new HashSet<RobotScenario>(_frontier);

            RobotScenario lState;

            // Keep searching until both frontiers are empty
            while (_frontier.Any() && _backFrontier.Any())
            {
                // Search from the front frontier
                lState = HandleFrontier(_frontier, _frontVisited);
                AddListToFrontier(lState.DetermineMoveSet());
                if ((_frontVisited.Contains(lState) && _backVisited.Contains(lState)))
                {
                    return BuildPath(lState, _backVisited);
                }

                // Search from the back frontier
                lState = HandleFrontier(_backFrontier, _backVisited);
                AddListToBacktier(lState.DetermineMoveSet());
                // Check for a meeting point
                // if visited is true,  but it is not on the visited list on that path, then a connection has been found.
                if ((_frontVisited.Contains(lState) && _backVisited.Contains(lState)))
                {
                    return BuildPath(lState, _backVisited);
                }
            }

            // No path was found
            return null;
        }

        // Handles searching from a frontier. Returns item at the front of the list
        private RobotScenario HandleFrontier(LinkedList<RobotScenario> aFrontier, HashSet<RobotScenario> aVisited)
        {
            // Fetch the first state from the frontier and update the frontier
            RobotScenario lState = aFrontier.First();
            aFrontier.RemoveFirst();

            // Update the visited states
            aVisited.Add(lState);
            _map.Cells[lState.Robot.X, lState.Robot.Y].WasVisited = true;

            // Update the number of states searched
            _searched++;

            return lState;
        }

        // Builds the path from the start to the meeting point or end state
        private List<Instruction> BuildPath(RobotScenario aState, HashSet<RobotScenario> aVisited)
        {
            // search _Visited for robot in same location as aState
            RobotScenario complement = aVisited.Where( x=> x.Robot.X == aState.Robot.X && x.Robot.Y == aState.Robot.Y ).FirstOrDefault();
            if(complement != null)
            {
                complement.Robot.Path.Reverse();
                return (List<Instruction>)aState.Robot.Path.Concat(complement.Robot.Path);
            }
            return null;
        }

        internal override void AddListToFrontier(List<RobotScenario> aList)
        {
            // Currently excluding visited cells is trned off, so checks have to be done to make sure already visited cells aren't added to the list. but only if the cell has been visited by the current frontier.
            foreach (RobotScenario lState in aList)
            {
                // check if lState exists in _frontVisited already
                if (_frontVisited.Contains(lState) || _frontier.Contains(lState))
                    continue;
                _frontier.AddLast(lState);
                _discovered++;
            }
        }

        private void AddListToBacktier(List<RobotScenario> aList)
        {
            foreach(RobotScenario lState in aList)
            {
                if (_backVisited.Contains(lState) || _backFrontier.Contains(lState))
                    continue;
                _backFrontier.AddLast(lState);
                _discovered++;
            }
        }
    }
}
