using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class RobotScenario
    {
        private Map _map;
        private Robot _robot;

        public Robot Robot { get { return _robot; } }

        public RobotScenario(Map aMap, Robot aRobot)
        {
            _map = aMap;
            _robot = aRobot;
        }

        public RobotScenario(RobotScenario aScenario)
        {
            _map = aScenario._map;
            _robot = aScenario._robot;
        }

        public bool IsSolved()
        {
            return _map.Cells[_robot.X, _robot.Y].Type == cellType.END;
        }

        public List<RobotScenario> DetermineMoveSet()
        {
            List<RobotScenario> moveScenarios = new List<RobotScenario>();

            Robot lRobot;

            // iterate through instructions in enum instructions
            foreach (Instruction instruction in Enum.GetValues(typeof(Instruction)))
            {
                if ((instruction == Instruction.UP) && !(_robot.Y > 0 && _map.Cells[_robot.X, _robot.Y - 1].Type != cellType.WALL))
                    continue;
                else if ((instruction == Instruction.LEFT) && !(_robot.X > 0 && _map.Cells[_robot.X - 1, _robot.Y].Type != cellType.WALL))
                    continue;
                else if((instruction == Instruction.DOWN) && !(_robot.Y < _map.Height - 1 && _map.Cells[_robot.X, _robot.Y + 1].Type != cellType.WALL))
                    continue;
                else if((instruction == Instruction.RIGHT) && !(_robot.X < _map.Width - 1 && _map.Cells[_robot.X + 1, _robot.Y].Type != cellType.WALL))
                    continue;
                lRobot = new Robot(_robot.X, _robot.Y, _robot.Path);
                lRobot.Move(instruction);

                if (_map.Cells[lRobot.X, lRobot.Y].wasVisited)
                    continue;

                moveScenarios.Add(new RobotScenario(_map, new Robot(lRobot.X, lRobot.Y, lRobot.Path)));
            }

            return moveScenarios;
        }
    }
}
