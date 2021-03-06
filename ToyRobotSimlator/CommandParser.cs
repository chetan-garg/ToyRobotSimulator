using Contracts;
using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotSimlator.ToyRobot;

namespace ToyRobotSimlator
{
    public class CommandParser : ICommandParser
    {
        public RobotCommandType ParseCommand(string commandInput)
        {
            if (string.IsNullOrEmpty(commandInput))
            {
                throw new InvalidOperationException(Constants.InvalidCommandErrorMessage);
            }

            RobotCommandType command;
            if (Enum.TryParse(commandInput, true, out command))
            {
                return command;
            }
            else
                throw new InvalidOperationException(Constants.InvalidCommandErrorMessage);
        }

        public IRobotPosition ParsePlacementParametes(string[] inputs, IRobot toyRobot)
        {
            if (inputs == null || ((toyRobot == null || toyRobot.CurrentPosition == null) && inputs.Length < Constants.ParametersMinLength))
            {
                throw new ArgumentException(Constants.InvalidParametersError);
            }
            else if(inputs.Length < Constants.SubsPlaceParamsMinLength)
            {
                throw new ArgumentException(Constants.InvalidParametersError);
            }

            int x = 0;
            int y = 0;
            if (!int.TryParse(inputs[0], out x))
            {
                throw new ArgumentException(Constants.InvalidParametersError);
            }
            if (!int.TryParse(inputs[1], out y))
            {
                throw new ArgumentException(Constants.InvalidParametersError);
            }

            RobotDirection direction;

            if (toyRobot.CurrentPosition == null || (inputs.Length == Constants.ParametersMinLength && !string.IsNullOrEmpty(inputs[2])))
            {
                if (!Enum.TryParse(inputs[2], true, out direction))
                {
                    throw new ArgumentException(Constants.InvalidDirectionError);
                }
            }
            else
            {
                direction = toyRobot.CurrentPosition.Direction;
            }

            return new RobotPosition(x, y, direction);
        }

        public ObstructedCells ParseAvoidCommandParameters(string[] inputs, IBoard board)
        {
            if (inputs == null || inputs.Length < Constants.AvoidCommandLength)
            {
                throw new ArgumentException(Constants.InvalidAvoidParametersError);
            }

            int x = 0;
            int y = 0;

            if (!int.TryParse(inputs[0], out x))
            {
                throw new ArgumentException(Constants.InvalidAvoidParametersError);
            }

            if (!int.TryParse(inputs[1], out y))
            {
                throw new ArgumentException(Constants.InvalidAvoidParametersError);
            }

            if (x >= board.Cols || x < 0 || y >= board.Rows || y < 0)
            {
                throw new ArgumentException(string.Format(Constants.InvalidCoordinatesError, board.Rows, board.Cols));
            }

            return new ObstructedCells(x, y);

        }


    }
}
