using RobotNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public abstract class SearchMethod
    {
        internal string code;
        internal string description;
        public string Code { get { return code; } }
        public string Description { get { return description; } }

        protected RobotScenario _scenario;
        protected Map _map;
        protected LinkedList<RobotScenario> _frontier;

        public LinkedList<RobotScenario> Frontier { get { return _frontier; } }

        public void Initialise(Map aMap)
        {
            _map = aMap;
            cell Start = _map.Start;

            _frontier = new LinkedList<RobotScenario>();
            _frontier.AddFirst(new RobotScenario(_map, new Robot(Start.X, Start.Y)));
            _map.Cells[Start.X, Start.Y].wasVisited = true;
        }

        public abstract List<Instruction> FindPath();
    }
}
