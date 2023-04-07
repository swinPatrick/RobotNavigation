using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class Robot
    {
        private int _x;
        private int _y;
        private List<Instruction> _path;

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public List<Instruction> Path { get { return _path; } }

        public Robot(int aX, int aY, List<Instruction> aInstruction)
        {
            _x = aX;
            _y = aY;
            _path = new List<Instruction>(aInstruction);
        }
        public Robot(int aX, int aY) : this(aX, aY, new List<Instruction>())
        { }

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
            _path.Add(instruction);
        }

    }
}
