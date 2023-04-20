using System.Collections.Generic;
using System.Linq;

namespace RobotNavigation
{
    public class State
    {
        public Node CurrentNode { get; private set; }

        public Map GetMap { get; private set; }

        //public List<Node> Path { get; private set; }

        public State(Node aRobot, List<Node> aPath, Map aMap)
        {
            CurrentNode = aRobot;
            //Path = aPath;
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

        public void Visit()
        {
            // if map contains cell in ends, mark is as visited
            if (GetMap.Ends.Any(c => c.X == CurrentNode.Cell.X && c.Y == CurrentNode.Cell.Y))
            {
                GetMap.Ends.First(c => c.X == CurrentNode.Cell.X && c.Y == CurrentNode.Cell.Y).Visited = true;
            }
        }

        public bool IsSolved(bool isCompletionist)
        {
            if (isCompletionist)
            {
                return GetMap.Ends.All(end => end.Visited);
            }
            else return GetMap.Ends.Any(end => end.Visited);
        }

        public List<Node> DetermineMoveSet(bool useJumping)
        {
            List<Node> result = new List<Node>();
            // this is the parent Node
            Node parent = new Node(CurrentNode.Cell, CurrentNode.Heuristic, CurrentNode.Connection);

            for (int i = 0; i < 4; i++)
            {
                Instruction instruction = (Instruction)i;

                // check if the instruction is invalid, not(Within bounds) OR is a wall)
                if ((instruction == Instruction.UP))
                {
                    if (!(CurrentNode.Y > 0) || GetMap.Ends.Any(c => c.X == CurrentNode.X && c.Y == CurrentNode.Y - 1))
                        continue;
                }
                else if (instruction == Instruction.LEFT)
                {
                    if (!(CurrentNode.X > 0) || GetMap.Ends.Any(c => c.X == CurrentNode.X - 1 && c.Y == CurrentNode.Y))
                        continue;
                }
                else if (instruction == Instruction.DOWN)
                {
                    if ((CurrentNode.Y < GetMap.Height - 1) || GetMap.Ends.Any(c => c.X == CurrentNode.X && c.Y == CurrentNode.Y + 1))
                        continue;
                }
                else if (instruction == Instruction.RIGHT)
                {
                    if (!(CurrentNode.X < GetMap.Width - 1) || GetMap.Ends.Any(c => c.X == CurrentNode.X + 1 && c.Y == CurrentNode.Y))
                        continue;
                }

                // if the instruction is valid, create a new node and add it to the result
                // new node needs Connection with (parent, instruction). Current cell with instruction applied is the new cell
                Cell lCell = ApplyInstruction(instruction: instruction);
                // check if lCell is the same cell as any of the parents
                // iterate through Node->Connection->Parent to see if new Cell matches any Parent.Cell
                if (lCell != null)
                {
                    Node currentNode = CurrentNode;
                    while (currentNode.Connection != null)
                    {
                        if (currentNode.X == lCell.X && currentNode.Y == lCell.Y)
                        {
                            lCell = null;
                            break;
                        }
                        currentNode = currentNode.Connection.Parent;
                    }
                }
                // if not, add the new node to the result
                Node lNode = new Node(lCell, new Connection(parent, instruction));
                // create a new state where lNode is Robot, current Node is added
                result.Add(lNode);
            }

            return result;
        }

        private Cell ApplyInstruction(Instruction instruction)
        {
            int lX = CurrentNode.X;
            int lY = CurrentNode.Y;
            switch (instruction)
            {
                case Instruction.UP:
                    lY--;
                    break;

                case Instruction.LEFT:
                    lX--;
                    break;

                case Instruction.DOWN:
                    lY++;
                    break;

                case Instruction.RIGHT:
                    lX++;
                    break;

                default:
                    return null;
            }
            CellType cellType = GetMap.Ends.Any(c => c.X == lX && c.Y == lY) ? CellType.END : CellType.EMPTY;

            return new Cell(new Coordinate(lX, lY, cellType));
        }

        internal int CalculatePathCost()
        {
            int result = 0;
            // for each node, add Connection.Cost. next cost is connection.parent.cost. repeat until connection.parent is null
            // start at the end node
            Node currentNode = CurrentNode;
            while (currentNode != null)
            {
                result += currentNode.Connection.Cost;
                currentNode = currentNode.Connection.Parent;
            }
            return result;
        }
    }
}