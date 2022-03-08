using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobotSimlator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Interfaces;
using Moq;
using Contracts.Enums;

namespace ToyRobotSimlator.Tests
{
    [TestClass()]
    public class BoardTests
    {
        [DataTestMethod()]
        [DataRow(5,1,2, true)]
        [DataRow(5,0, 2, true)]
        [DataRow(8,1, 4, true)]
        [DataRow(2,7, 6, false)]
        [DataRow(6, 9, 2, false)]
        [DataRow(8, 5, 2, true)]
        [DataRow(8, 7, 7, true)]
        [DataRow(7, 8, 2, false)]
        [DataRow(5, 1, 8, false)]
        [DataRow(2, 1, 1, true)]
        [DataRow(8, 10, 7, false)]
        [DataRow(8, 7, 17, false)]
        [DataRow(8, 10, 10, false)]
        public void ValidatePositionValidTest(int boardLength, int positionX, int positionY, bool expectedOutcome)
        {
            Board newBoard = new Board(boardLength, boardLength);
            var mockPosition = new Mock<IRobotPosition>();
            mockPosition.Setup(x => x.X).Returns(positionX);
            mockPosition.Setup(x => x.Y).Returns(positionY);
            string validationMessage;
            var valid = newBoard.ValidatePosition(mockPosition.Object, out validationMessage);
            Assert.IsNotNull(valid);
            Assert.AreEqual(expectedOutcome, valid);
        }
    }
}