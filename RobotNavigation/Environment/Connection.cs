using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class Connection
    {
        private Instruction instruction;

        public Node Parent { get; set; }
        public Instruction Direction { get; set; }
        public int Cost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aParent">The Cell which the new cell came from</param>
        /// <param name="aDirection">What instruction was taken</param>
        /// <param name="aCost">How much does it cost to travel</param>
        public Connection(Node aParent, Instruction aDirection, int aCost)
        {
            Parent = aParent;
            Direction = aDirection;
            Cost = aCost;
        }

        public Connection(Node aParent, Instruction aInstruction): this(aParent, aInstruction, 1)
        { }
    }
}
