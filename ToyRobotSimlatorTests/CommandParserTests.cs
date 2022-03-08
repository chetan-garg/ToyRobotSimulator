using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobotSimlator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Enums;
using Contracts.Interfaces;
using Moq;

namespace ToyRobotSimlator.Tests
{
    [TestClass()]
    public class CommandParserTests
    {
        [DataTestMethod()]
        [DataRow("place", RobotCommandType.Place)]
        [DataRow("move", RobotCommandType.Move)]
        [DataRow("lEFT", RobotCommandType.Left)]
        [DataRow("riGHT", RobotCommandType.Right)]
        [DataRow("REPORT", RobotCommandType.Report)]
        [DataRow("exit", RobotCommandType.Exit)]
        [DataRow("avoid", RobotCommandType.Avoid)]
        [DataRow("AVOID", RobotCommandType.Avoid)]
        [DataRow("aVOID", RobotCommandType.Avoid)]
        public void ParseCommandTest(string command, RobotCommandType expectedOutput)
        {
            CommandParser parser = new CommandParser();
            var outputCommand = parser.ParseCommand(command);

            Assert.AreEqual(expectedOutput, outputCommand);
        }

        [DataTestMethod()]
        [DataRow("5", "4", 5, 4, 8)]
        public void AvoidParametersParserTest(string x, string y, int intPositionX, int intPositionY, int boardLength)
        {
            var commParser = new CommandParser();
            var mockPosition = new Mock<IBoard>();
            mockPosition.Setup(x => x.Rows).Returns(boardLength);
            mockPosition.Setup(y => y.Cols).Returns(boardLength);

            var output = commParser.ParseAvoidCommandParameters(new string[] { x, y }, mockPosition.Object);


            Assert.IsNotNull(output);
            Assert.AreEqual(output.X, intPositionX);
            Assert.AreEqual(output.Y, intPositionY);
        }
    }
}