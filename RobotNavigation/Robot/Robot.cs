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
        private List<Path> _path;
        private int _cost;

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public List<Path> Path { get { return _path; } }
        public int Cost { get { return _cost; } }

        public Robot(int aX, int aY, List<Path> aPath)
        {
            _x = aX;
            _y = aY;
            _path = new List<Path>(aPath);
        }
        public Robot(int aX, int aY) : this(aX, aY, new List<Path>())
        { }

        public void Move(Instruction instruction, int distance = 1)
        {
            switch (instruction)
            {
                case Instruction.UP:
                case Instruction.JUMP_UP:
                    _y -= distance;
                    break;

                case Instruction.LEFT:
                case Instruction.JUMP_LEFT:
                    _x -= distance;
                    break;

                case Instruction.DOWN:
                case Instruction.JUMP_DOWN:
                    _y += distance;
                    break;

                case Instruction.RIGHT:
                case Instruction.JUMP_RIGHT:
                    _x += distance;
                    break;


            }
            int cost = (int)Math.Pow(2, distance-1);
            
            _path.Add(new Path(instruction, cost));
        }

    }
}
