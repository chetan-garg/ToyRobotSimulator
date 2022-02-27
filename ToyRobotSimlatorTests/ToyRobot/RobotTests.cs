using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobotSimlator.ToyRobot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Contracts.Interfaces;
using Contracts.Enums;

namespace ToyRobotSimlator.ToyRobot.Tests
{
    [TestClass()]
    public class RobotTests
    {
        [DataTestMethod()]
        [DataRow(4,5,2,"north")]
        [DataRow(4, 5, 2, "south")]
        [DataRow(4, 5, 2, "east")]
        [DataRow(4, 5, 2, "west")]
        public void GetNextPositionTest(int X, int Y, int movePlaces, string direction)
        {
            var mockPosition = new Mock<IRobotPosition>();
            mockPosition.SetupGet(x => x.X).Returns(X);
            mockPosition.SetupGet(x => x.Y).Returns(Y);
            mockPosition.SetupGet(x => x.Direction).Returns((RobotDirection)Enum.Parse(typeof(RobotDirection), direction, true));

            Robot robot = new Robot();
            robot.Place(mockPosition.Object);

            var newPos = robot.GetNextPosition(movePlaces);

            Assert.IsNotNull(newPos);
            switch (mockPosition.Object.Direction)
            {
                case RobotDirection.North:
                    Assert.AreEqual(newPos.Y, Y + movePlaces); 
                    break;
                case RobotDirection.South:
                    Assert.AreEqual(newPos.Y, Y - movePlaces);
                    break;
                case RobotDirection.East:
                    Assert.AreEqual(newPos.X, X + movePlaces);
                    break;
                case RobotDirection.West:
                    Assert.AreEqual(newPos.X, X - movePlaces);
                    break;
            }
            
        }

        [TestMethod()]
        [DataRow(4, 5, "north")]
        [DataRow(4, 5, "south")]
        [DataRow(4, 5, "east")]
        [DataRow(4, 5, "west")]
        [DataRow(0, 1, "north")]
        [DataRow(4, 6, "south")]
        [DataRow(1, 9, "east")]
        [DataRow(40, 15, "west")]
        public void PlaceTest(int X, int Y, string direction)
        {
            Robot robot = new Robot();
            var mockPosition = new Mock<IRobotPosition>();
            mockPosition.SetupGet(x => x.X).Returns(X);
            mockPosition.SetupGet(x => x.Y).Returns(Y);
            mockPosition.SetupGet(x => x.Direction).Returns((RobotDirection)Enum.Parse(typeof(RobotDirection), direction, true));

            robot.Place(mockPosition.Object);

            Assert.IsNotNull(robot.CurrentPosition);
            Assert.AreEqual(robot.CurrentPosition.X, mockPosition.Object.X);
            Assert.AreEqual(robot.CurrentPosition.Y, mockPosition.Object.Y);
            Assert.AreEqual(robot.CurrentPosition.Direction, mockPosition.Object.Direction);
        }

        [TestMethod()]
        [DataRow(1, 9, "north", 2, RobotDirection.South)]
        [DataRow(1, 9, "north", 1, RobotDirection.West)]
        [DataRow(1, 9, "north", 3, RobotDirection.East)]
        [DataRow(1, 9, "north", 7, RobotDirection.East)]
        [DataRow(1, 9, "north", 5, RobotDirection.West)]
        [DataRow(1, 9, "north", 0, RobotDirection.North)]
        public void RotateLeftTest(int X, int Y, string direction, int leftRotations, RobotDirection expectedOutput)
        {
            Robot robot = new Robot();
            RobotPosition mockPosition = new RobotPosition(X, Y, (RobotDirection)Enum.Parse(typeof(RobotDirection), direction, true));
            
            robot.Place(mockPosition);

            robot.RotateLeft(leftRotations);

            Assert.IsNotNull(robot.CurrentPosition);
            Assert.IsNotNull(robot.CurrentPosition.Direction);
            Assert.AreEqual(robot.CurrentPosition.Direction, expectedOutput);
        }

        [TestMethod()]
        [DataRow(1, 9, "north", 2, RobotDirection.South)]
        [DataRow(1, 9, "north", 1, RobotDirection.East)]
        [DataRow(1, 9, "north", 3, RobotDirection.West)]
        [DataRow(1, 9, "north", 7, RobotDirection.West)]
        [DataRow(1, 9, "north", 5, RobotDirection.East)]
        [DataRow(1, 9, "north", 0, RobotDirection.North)]
        public void RotateRightTest(int X, int Y, string direction, int rightRotations, RobotDirection expectedOutput)
        {
            Robot robot = new Robot();
            RobotPosition mockPosition = new RobotPosition(X, Y, (RobotDirection)Enum.Parse(typeof(RobotDirection), direction, true));

            robot.Place(mockPosition);

            robot.RotateRight(rightRotations);

            Assert.IsNotNull(robot.CurrentPosition);
            Assert.IsNotNull(robot.CurrentPosition.Direction);
            Assert.AreEqual(robot.CurrentPosition.Direction, expectedOutput);
        }

        [TestMethod()]
        [DataRow(4, 5, "north")]
        [DataRow(4, 5, "south")]
        [DataRow(4, 5, "east")]
        [DataRow(4, 5, "west")]
        [DataRow(0, 1, "north")]
        [DataRow(4, 6, "south")]
        [DataRow(1, 9, "east")]
        [DataRow(40, 15, "west")]
        public void ReportCurrentPositionTest(int X, int Y, string direction)
        {
            Robot robot = new Robot();
            var mockPosition = new Mock<IRobotPosition>();
            mockPosition.SetupGet(x => x.X).Returns(X);
            mockPosition.SetupGet(x => x.Y).Returns(Y);
            mockPosition.SetupGet(x => x.Direction).Returns((RobotDirection)Enum.Parse(typeof(RobotDirection), direction, true));

            robot.Place(mockPosition.Object);

            var report = robot.ReportCurrentPosition();
            
            Assert.IsNotNull(report);
            Assert.AreEqual(report.ToLower(), $"current position for the robot : x: {X}, y: {Y}, direction: {direction.ToLower()}");
        }
    }
}