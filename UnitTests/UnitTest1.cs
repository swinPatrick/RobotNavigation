using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotNavigation;
using System;

namespace UnitTests
{
    [TestClass]
    public class RobotScenarioTests
    {
        Map lMap = new Map(3, 3);
        Robot lRobot = new Robot(0, 0);

        [TestMethod()]
        public void BasicIsSolved()
        {
            lMap.setCell(0, 0, cellType.END);
            RobotScenario lScenario = new RobotScenario(lMap, lRobot);
            Assert.AreEqual(expected: true, actual: lScenario.IsSolved());
        }

        [TestMethod()]
        public void MovedIsSolved()
        {
            lMap.setCell(0, 0, cellType.START);
            lMap.setCell(0, 1, cellType.END);
            RobotScenario lScenario = new RobotScenario(lMap, lRobot);
            DepthFirstSearch DFS = new DepthFirstSearch(lMap);
            //DFS.findPath();
            Assert.AreEqual(expected: true, lScenario.IsSolved());
        }
    }
}
