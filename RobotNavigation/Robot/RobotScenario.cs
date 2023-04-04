using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    internal class RobotScenario
    {
        private Map _map;
        private Robot _robot;
        private List<Instruction> _path;

        public RobotScenario(Map aMap, Robot aRobot)
        {
            _map = aMap;
            _robot = aRobot;
            _path = new List<Instruction>();
        }

        public RobotScenario(RobotScenario aScenario, Instruction aInstruction)
        {
            _map = aScenario._map;
            _robot = new Robot(aScenario._robot);
            _path = new List<Instruction>(aScenario._path);
            _path.Add(aInstruction);
        }

        public bool IsSolved()
        {
            return _map.Cells[_robot.X, _robot.Y].Type == cellType.END;
        }

        public LinkedList<RobotScenario> DetermineMoveSet()
        {
            LinkedList<RobotScenario> moveScenarios = new LinkedList<RobotScenario>();

            // TODO: check where robot can move, add scenario for each to the list.

            // for each instruction in instructions
            foreach (Instruction instruction in (Instruction[])Enum.GetValues(typeof(Instruction)))
            {
                try
                {
                    // create new scenario
                    RobotScenario lScenario = new RobotScenario(this, instruction);
                    // check if valid
                    // try would fail if out of bounds
                    if (_map.Cells[_robot.X, _robot.Y].Type == cellType.WALL)
                        lScenario = null;

                    // add to list or delete
                    if (lScenario != null)
                        moveScenarios.AddLast(lScenario);
                }
                catch { }
            }


            return moveScenarios;
        }

        public string PrintPath()
        {
            string s = "";
            // for each instruction in list, add instruction to string. "instruction1, unstruction2, etc."
            foreach(Instruction instruction in _path)
            {
                // add instruction to end of s
                s += instruction.ToString();
            }
            s.Trim(',');

            return s;
        }
    }
}
