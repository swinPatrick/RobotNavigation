using RobotNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public abstract class SearchMethod
    {
        // Used to describe the search method
        public string Code { get; internal set; }
        public string Description { get; internal set; }

        // used to see search metrics
        internal int _discovered;
        internal int _searched;

        // Optional parameters
        public bool UseJumping { get; set; }
        public bool Completionist { get; set; }

        public List<State> Frontier { get; internal set; }

        public void Initialise(Map aMap)
        {
            // create initial state, where first node is the start node
            State initialState = new State(aMap);

            // add initial state to frontier
            Frontier = new List<State>();
            Frontier.Add(initialState);
        }

        public virtual List<Node> FindPath()
        {
            State lState;

            // while the frontier is not empty
            while (Frontier.Count > 0)
            {
                lState = Frontier.First();

                // remove first node from frontier
                Frontier.Remove(lState);

                lState.Visit();

                // increment searched cells
                _searched++;

                // check to see if robot is at an end
                if (lState.IsSolved(Completionist))
                {
                    Frontier.Clear();
                    //create a list of nodes. the final node will be robot, and the first node will be the node with the parent of null
                    List<Node> Path = new List<Node>();
                    Node currentNode = lState.CurrentNode;
                    while (currentNode != null)
                    {
                        Path.Insert(0, currentNode);
                        currentNode = currentNode.Connection.Parent;
                    }
                }

                // determine what moves can be done
                AddListToFrontier(lState.DetermineMoveSet(useJumping: UseJumping));
            }

            // no solution was found
            return null;
        }

        internal abstract void AddListToFrontier(List<State> aList);

    }
}
