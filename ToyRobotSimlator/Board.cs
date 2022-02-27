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
        }

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
            validationMessage = "This is a valid position.";
            return true;
        }

    }
}
