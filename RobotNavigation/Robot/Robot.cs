using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    internal class Robot
    {
        private int _x;
        private int _y;

        public int X { get { return _x; } }
        public int Y { get { return _y; } }

        public Robot(int aX, int aY)
        {
            _x = aX;
            _y = aY;
        }

        public Robot(Robot aRobot)
        {
            _x = aRobot.X;
            _y = aRobot.Y;
        }

        public void Move(Instruction instruction)
        {
            switch (instruction)
            {
                case Instruction.UP:
                    _y--;
                    break;
                case Instruction.LEFT:
                    _x--;
                    break;
                case Instruction.DOWN:
                    _y++;
                    break;
                case Instruction.RIGHT:
                    _x++;
                    break;
            }
        }

    }
}
