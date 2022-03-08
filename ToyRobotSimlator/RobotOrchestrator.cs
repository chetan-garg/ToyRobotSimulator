using Contracts;
using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotSimlator
{
    public class RobotOrchestrator
    {
        public IRobot ToyRobot { get; set; }
        public IBoard ToyBoard { get; set; }

        public ICommandParser CommandParser { get; set; }

        public RobotOrchestrator(IRobot robot, IBoard board, ICommandParser parser)
        {
            ToyRobot = robot;
            ToyBoard = board;
            CommandParser = parser;
        }

        public string Process(string input)
        {
            if (input == string.Empty)
            {
                return Constants.InvalidCommandErrorMessage;
            }

            var commandValues = input.Split(' ');
            var command = CommandParser.ParseCommand(commandValues[0]);
            string validationMessage;

            switch (command)
            {
                case RobotCommandType.Avoid:
                    if (commandValues.Count() < Constants.CommandMinLength)
                    {
                        return Constants.InvalidAvoidParametersError;
                    }
                    if (ToyRobot.CurrentPosition == null)
                    {
                        return Constants.IgnoringCommandMessage;
                    }


                    var successfulAddToObstructedCells = CommandParser.ParseAvoidCommandParameters(commandValues[1].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries), ToyBoard);

                    if (successfulAddToObstructedCells != null)
                    {
                        validationMessage = string.Empty;
                        if (ToyRobot.CurrentPosition.X == successfulAddToObstructedCells.X && ToyRobot.CurrentPosition.Y == successfulAddToObstructedCells.Y)
                        {
                            return Constants.ObstructedCellErrorMessage;
                        }

                        if (ToyRobot.CurrentPosition != null && ToyBoard.ValidatePosition(ToyRobot.CurrentPosition, out validationMessage))
                        {
                            ToyBoard.ObstructedCells.Add(successfulAddToObstructedCells);
                            return Constants.SuccesfulOperation;
                        }
                        else
                        {
                            return validationMessage;
                        }
                    }

                    return string.Empty;

                case RobotCommandType.Place:
                    if (commandValues.Count() < Constants.CommandMinLength)
                    {
                        return Constants.InvalidParametersError;
                    }
                    var placementPosition = CommandParser.ParsePlacementParametes(commandValues[1].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries), ToyRobot);
                    if (placementPosition != null)
                    {
                        if (ToyBoard.ValidatePosition(placementPosition, out validationMessage))
                        {
                            ToyRobot.Place(placementPosition);
                            return Constants.SuccesfulOperation;
                        }
                        else
                        {
                            return validationMessage;
                        }
                    }
                    else
                    {
                        return Constants.InvalidParametersError;
                    }
                case RobotCommandType.Move:
                    if (ToyRobot.CurrentPosition == null)
                    {
                        return Constants.IgnoringCommandMessage;
                    }
                    IRobotPosition position = ToyRobot.GetNextPosition(1);
                    validationMessage = string.Empty;
                    if (position != null && ToyBoard.ValidatePosition(position, out validationMessage))
                    {
                        ToyRobot.Place(position);
                        return Constants.SuccesfulOperation;
                    }
                    else
                    {
                        return validationMessage;
                    }
                case RobotCommandType.Left:
                    if (ToyRobot.CurrentPosition == null)
                    {
                        return Constants.IgnoringCommandMessage;
                    }
                    else
                    {
                        ToyRobot.RotateLeft(1);
                        return Constants.SuccesfulOperation;
                    }
                case RobotCommandType.Right:
                    if (ToyRobot.CurrentPosition == null)
                    {
                        return Constants.IgnoringCommandMessage;
                    }
                    else
                    {
                        ToyRobot.RotateRight(1);
                        return Constants.SuccesfulOperation;
                    }
                case RobotCommandType.Report:
                    if (ToyRobot.CurrentPosition == null)
                    {
                        return Constants.IgnoringCommandMessage;
                    }
                    else
                    {
                        return ToyRobot.ReportCurrentPosition();
                    }
                default:
                    return Constants.InvalidCommandErrorMessage;
            }

        }
    }
}
