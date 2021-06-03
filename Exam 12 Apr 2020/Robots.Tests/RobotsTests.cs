namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    public class RobotsTests
    {
        RobotManager robotManager;
        Robot robot;

        [SetUp]
        public void Initialize()
        {
            robotManager = new RobotManager(100);
            robot = new Robot("Anka", 150);
        }

        [Test]
        public void CapacityShoulReturnCorrectResult()
        {
            //Invalid Capacity
            Assert.Throws<ArgumentException>(() => new RobotManager(-15));

            //Test Capacity Count
            int expectedCapacity = 100;
            int actualCapacity = robotManager.Capacity;

            Assert.AreEqual(expectedCapacity, actualCapacity);

            //Border Capacity
            robotManager = new RobotManager(1);
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() => robotManager.Add(new Robot("Test", 173)));
        }

        [Test]
        public void RobotsCountShouldBeAsExpectd()
        {
            robotManager.Add(robot);
            robotManager.Add(new Robot("Anaka2", 100));
            int actualCount = robotManager.Count;
            int expectedCount = 2;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddingTheSameRobotShouldThrowException()
        {
            robotManager.Add(robot);

            Assert.Throws<InvalidOperationException>(() => robotManager.Add(robot));
        }


        [Test]
        public void RemovingRobotShouldWorkCorrect()
        {
            //Removing NotExisting Robot
            Assert.Throws<InvalidOperationException>(() => robotManager.Remove("G"));

            //Remove Robot
            robotManager.Add(robot);
            robotManager.Remove(robot.Name);
            Assert.AreEqual(robotManager.Count, 0);

        }

        [Test]
        public void WorkMethodShouldReduceRobotsBattery()
        {
            //Inexisting robot
            Assert.Throws<InvalidOperationException>(()=> robotManager.Work("Test", " ", 10));

            //CurrentBattery < BatteryUsage
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() => robotManager.Work(robot.Name, " ", 155));

            robotManager.Work(robot.Name, "Programmer", 140);
            int expectedBattLeft = 10;
            int actualBattLeft = robot.Battery;

            Assert.AreEqual(expectedBattLeft, actualBattLeft);
        }

        [Test]
        public void ChargeShouldIncreaseBatteryLevelToMax()
        {
            robotManager.Add(robot);
            robotManager.Work(robot.Name, "Programmer", 140);
            robotManager.Charge(robot.Name);
            int expectedBattLeft = 150;
            int actualBattLeft = robot.Battery;
            Assert.AreEqual(expectedBattLeft, actualBattLeft);


            Assert.Throws<InvalidOperationException>(() => robotManager.Charge(" "));
        }

    }
}
