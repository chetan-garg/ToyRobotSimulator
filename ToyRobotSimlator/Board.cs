using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Interfaces;
using Contracts;

namespace ToyRobotSimlator
{
    public class Board : IBoard
    {
        public int Rows { get;}
        public int Cols { get;}

        public Board(int rows, int cols)
        {
            Rows = rows; Cols = cols;
            ObstructedCells = new List<ObstructedCells>();
        }

        public List<ObstructedCells> ObstructedCells { get; set; }

        /// <summary>
        /// Validates whether the new position being set for the robot is a valid position on the board.
        /// </summary>
        /// <param name="robotPosition">New position to be set to the robot.</param>
        /// <param name="validationMessage">Any validation message.</param>
        /// <returns>Position valid or not</returns>
        public bool ValidatePosition(IRobotPosition robotPosition, out string validationMessage)
        {
            if (robotPosition == null)
            {
                validationMessage = Constants.NullPostionErrorMessage;
                return false;
            }

            if (robotPosition.X >= Cols || robotPosition.Y >= Rows || robotPosition.X < 0 || robotPosition.Y < 0)
            {
                validationMessage = Constants.InvalidPostionErrorMessage;
                return false;
            }

            if (ObstructedCells.Exists(x => x.X == robotPosition.X && x.Y == robotPosition.Y))
            {
                validationMessage = Constants.ObstructedCellErrorMessage;
                return false;
            }

            validationMessage = "This is a valid position.";
            return true;
        }

    }
}
