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

        public IRobotPosition ParsePlacementParametes(string[] inputs)
        {
            if (inputs == null || inputs.Length < Constants.ParametersMinLength)
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
            if (!Enum.TryParse(inputs[2], true, out direction))
            {
                throw new ArgumentException(Constants.InvalidDirectionError);
            }

            return new RobotPosition(x, y, direction);
        }
    }
}
