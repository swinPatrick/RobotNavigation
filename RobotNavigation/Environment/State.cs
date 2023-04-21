using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotNavigation
{
    public enum Instruction
    {
        UP,
        LEFT,
        DOWN,
        RIGHT,
        JUMP_UP,
        JUMP_LEFT,
        JUMP_DOWN,
        JUMP_RIGHT
    }

    public class State
    {
        public Node CurrentNode { get; private set; }

        public Map GetMap { get; private set; }

        //public List<Node> Path { get; private set; }

        public State(Node aNode, Map aMap)
        {
            CurrentNode = aNode;
            GetMap = new Map(aMap);
        }

        /// <summary>
        /// Robot is set to the start cell
        /// </summary>
        /// <param name="start"></param>
        public State(Map aMap)
        {
            GetMap = new Map(aMap);
            CurrentNode = new Node(GetMap.Start, 0, null);
        }

        public bool IsSolved(bool isCompletionist)
        {
            int lEndsVisited = 0;
            Node lNode = CurrentNode;

            // loop through all nodes and node parents to check if the current cell is the same as any of the end cells
            while (lNode.Connection != null)
            {
                if (GetMap.Ends.Any(c => c.X == lNode.X && c.Y == lNode.Y))
                    lEndsVisited++;
                lNode = lNode.Connection.Parent;
            }

            // if completionist, return true if all ends are visited
            if (isCompletionist)
                return lEndsVisited == GetMap.Ends.Count;
            else
                return lEndsVisited > 0;
        }

        public List<State> DetermineMoveSet(bool useJumping)
        {
            List<State> result = new List<State>();
            // this is the parent Node
            Node parent = CurrentNode;
            //Node parent = new Node(CurrentNode.Cell, CurrentNode.Heuristic, CurrentNode.Connection);

            // range of insturctions to loop through.
            int range = (useJumping ? 8 : 4);

            for (int i = 0; i < range; i++)
            {
                int moveDistance = 0;
                Instruction lInstruction = (Instruction)i;
                do
                {
                    moveDistance++;
                    // if the instruction is valid, create a new node and add it to the result
                    // new node needs Connection with (parent, instruction). Current cell with instruction applied is the new cell

                    Cell lCell = ApplyInstruction(aInstruction: lInstruction, aDistance: moveDistance);
                    if (lCell == null) // will return null if new cell is not valid
                        continue;
                    // need seperate check for bounds as they apply to Jumping instructions. if the cell is a wall, it can continue to jump over it, but it cannot jump out of bounds
                    if (!InBounds(lCell))
                        break;

                    // check if the new cell is at the same location of any of its parent nodes
                    for (Node n = parent; n.Connection != null; n = n.Connection.Parent)
                    {
                        if (lCell.X == n.X && lCell.Y == n.Y)
                        {
                            lCell = null;
                            break;
                        }
                    }

                    if (lCell == null)
                        break;

                    // if not, add the new node to the result
                    int cost = (int)Math.Pow(2, moveDistance - 1);
                    Node lNode = new Node(lCell, new Connection(parent, lInstruction, cost));

                    if (!InstructionSensible(lNode))
                        continue;
                    // create a new state where lNode is Robot, current Node is added
                    State lState = new State(lNode, GetMap);
                    result.Add(lState);
                } while (i >= 4 && moveDistance < Math.Max(GetMap.Width, GetMap.Height));
            }

            return result;
        }

        internal int CalculatePathCost()
        {
            int result = 0;
            // for each node, add Connection.Cost. next cost is connection.parent.cost.
            // repeat until connection is null
            // start at the end node
            for(Node n = CurrentNode; n.Connection != null; n = n.Connection.Parent)
            {
                result += n.Connection.Cost;
            }
            return result;
        }

        private Cell ApplyInstruction(Instruction aInstruction, int aDistance = 1)
        {
            int lX = CurrentNode.X;
            int lY = CurrentNode.Y;

            // update coordinate based on instruction
            switch (aInstruction)
            {
                case Instruction.UP:
                case Instruction.JUMP_UP:
                    lY -= aDistance;
                    break;

                case Instruction.LEFT:
                case Instruction.JUMP_LEFT:
                    lX -= aDistance;
                    break;

                case Instruction.DOWN:
                case Instruction.JUMP_DOWN:
                    lY += aDistance;
                    break;

                case Instruction.RIGHT:
                case Instruction.JUMP_RIGHT:
                    lX += aDistance;
                    break;

                default:
                    return null;
            }
            // check new coordinate is valid
            if ((lX == GetMap.Start.X && lY == GetMap.Start.Y) // it's the start
                || GetMap.Walls.Any(c => c.X == lX && c.Y == lY)) // if it's a wall
                return null;

            // check if new coordinate makes sense


            // cell type can't be start, and walls have already been checked. so it is either an end or empty.
            CellType cellType = GetMap.Ends.Any(c => c.X == lX && c.Y == lY) ? CellType.END : CellType.EMPTY;

            return new Cell(lX, lY, cellType);
        }

        private bool InBounds(Cell aCell)
        {
            return ((aCell.X >= 0 && aCell.X < GetMap.Width)    // out of x bounds
                && (aCell.Y >= 0 && aCell.Y < GetMap.Height));    // out of y bounds
        }

        private bool InstructionSensible(Node n)
        {
            bool sensible = true;
            if(n.Connection == null || n.Connection.Parent.Connection == null)
                return sensible;
            Instruction currentInstruction = n.Connection.Direction;
            Instruction parentInstruction = n.Connection.Parent.Connection.Direction;
            if (
                (int)currentInstruction > 3 && currentInstruction == parentInstruction || // if the directions are both jump and same instruction
                (int)currentInstruction + 2 % 4 == (int)parentInstruction % 4          // if the directions are opposite
                )
                sensible = false;
            return sensible;
        }
    }

    public class StateComparer : IComparer<State>
    {
        public int Compare(State x, State y)
        {
            return x.CurrentNode.Heuristic.CompareTo(y.CurrentNode.Heuristic);
        }
    }
}