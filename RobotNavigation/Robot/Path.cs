using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class Path
    {
        private readonly Instruction _instruction;
        private readonly int _cost;
        
        public Instruction Instruction { get { return _instruction; } }
        public int Cost { get { return _cost; } }

        public Path(Instruction aInstruction, int aCost = 1)
        {
            _instruction = aInstruction;
            _cost = aCost;
        }
    }
}
