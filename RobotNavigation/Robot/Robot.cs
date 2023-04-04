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

        public Robot(int x, int y)
        {
            _x = x;
            _y = y;
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
