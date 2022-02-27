using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobotSimlator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Enums;

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
        public void ParseCommandTest(string command, RobotCommandType expectedOutput)
        {
            CommandParser parser = new CommandParser();
            var outputCommand = parser.ParseCommand(command);

            Assert.AreEqual(expectedOutput, outputCommand);
        }
    }
}