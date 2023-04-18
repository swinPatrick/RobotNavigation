using System;
using System.Collections.Generic;

namespace RobotNavigation
{
    public class RobotScenario
    {
        private Map _map;
        private Robot _robot;

        public Robot Robot
        { get { return _robot; } }

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

        public List<RobotScenario> DetermineMoveSet(bool excludeVisited = true, bool withJumping = false)
        {
            List<RobotScenario> moveScenarios = new List<RobotScenario>();

            Robot lRobot;

            // iterate first 4 instructions in enum instructions
            for (int i = 0; i < 4; i++)
            {
                Instruction instruction = (Instruction)i;
                if ((instruction == Instruction.UP) && !(_robot.Y > 0 && _map.Cells[_robot.X, _robot.Y - 1].Type != cellType.WALL))
                    continue;
                else if ((instruction == Instruction.LEFT) && !(_robot.X > 0 && _map.Cells[_robot.X - 1, _robot.Y].Type != cellType.WALL))
                    continue;
                else if ((instruction == Instruction.DOWN) && !(_robot.Y < _map.Height - 1 && _map.Cells[_robot.X, _robot.Y + 1].Type != cellType.WALL))
                    continue;
                else if ((instruction == Instruction.RIGHT) && !(_robot.X < _map.Width - 1 && _map.Cells[_robot.X + 1, _robot.Y].Type != cellType.WALL))
                    continue;
                lRobot = new Robot(_robot.X, _robot.Y, _robot.Path);
                lRobot.Move(instruction);
                if (excludeVisited && _map.Cells[lRobot.X, lRobot.Y].wasVisited)
                    continue;
                moveScenarios.Add(new RobotScenario(_map, new Robot(lRobot.X, lRobot.Y, lRobot.Path)));
            }

            if (withJumping == true)
            {
                // iterate remaining instructions in enum instructions
                for (int i = 4; i < Enum.GetValues(typeof(Instruction)).Length; i++)
                {
                    int StartDistance = 2;
                    Instruction instruction = (Instruction)i;
                    if (instruction == Instruction.JUMP_UP)
                    {
                        for (int d = StartDistance; d < _robot.Y; d++)
                        {
                            if (_map.Cells[_robot.X, _robot.Y - d].Type != cellType.WALL)
                            {
                                moveScenarios.Add(generateScenario(instruction, d));
                            }
                        }
                    }
                    else if (instruction == Instruction.JUMP_LEFT)
                    {
                        for (int d = StartDistance; d < _robot.X; d++)
                        {
                            if (_map.Cells[_robot.X - d, _robot.Y].Type != cellType.WALL)
                            {
                                moveScenarios.Add(generateScenario(instruction, d));
                            }
                        }
                    }
                    else if (instruction == Instruction.JUMP_DOWN)
                    {
                        for (int d = StartDistance; d < _map.Height - _robot.Y; d++)
                        {
                            if (_map.Cells[_robot.X, _robot.Y + d].Type != cellType.WALL)
                            {
                                moveScenarios.Add(generateScenario(instruction, d));
                            }
                        }
                    }
                    else if (instruction == Instruction.JUMP_RIGHT)
                    {
                        for (int d = StartDistance; d < _map.Width - _robot.X; d++)
                        {
                            if (_map.Cells[_robot.X + d, _robot.Y].Type != cellType.WALL)
                            {
                                moveScenarios.Add(generateScenario(instruction, d));
                            }
                        }
                    }
                }
            }

            return moveScenarios;
        }

        private RobotScenario generateScenario(Instruction aInstruction, int aDistance = 1)
        {
            Robot lRobot = new Robot(_robot.X, _robot.Y, _robot.Path);
            lRobot.Move(aInstruction, aDistance);
            return new RobotScenario(_map, lRobot);
        }

        public int CalculatePathCost()
        {
            int cost = 0;
            foreach(Path link in _robot.Path)
            {
                cost += link.Cost;
            }
            return cost;
        }
    }
}