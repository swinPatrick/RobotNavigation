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
        private List<Link> _path;
        private int _cost;

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public List<Link> Path { get { return _path; } }
        public int Cost { get { return _cost; } }

        public Robot(int aX, int aY, List<Link> aPath)
        {
            _x = aX;
            _y = aY;
            _path = new List<Link>(aPath);
        }
        public Robot(int aX, int aY) : this(aX, aY, new List<Link>())
        { }

        public void Move(Instruction instruction, int distance = 1)
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
                case Instruction.JUMP_UP:
                    _y -= distance;
                    break;
                case Instruction.JUMP_LEFT:
                    _x -= distance;
                    break;
                case Instruction.JUMP_DOWN:
                    _y += distance;
                    break;
                case Instruction.JUMP_RIGHT:
                    _x += distance;
                    break;

            }
            _path.Add(new Link(instruction, 2^distance));
        }

    }
}
