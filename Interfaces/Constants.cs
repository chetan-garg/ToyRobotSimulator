﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public static class Constants
    {
        public const string InvalidPostionErrorMessage = "Position is invalid. Cannot place robot.";
        public const string NullPostionErrorMessage = "The position object is null. Cannot place robot.";
        public const string IgnoringCommandMessage = "Ignoring current command as the Robot has not been placed on board yet.";
        public const string InvalidCommandErrorMessage = "Command entered is either null or invalid.";
        public const int CommandMinLength = 2;
        public const string InvalidParametersError = "It seems place command does not have correct parameters. \r\n The place command format is \"PLACE X,Y,Direction\" where X and Y has to be valid integers.";
        public const int ParametersMinLength = 3;
        public const string InvalidDirectionError = "The direction value provided is not valid. Valid values are NORTH, EAST, SOUTH OR WEST.";
        public const string SuccesfulOperation = "The command was successfully executed. You can run \"REPORT\" command to see current position.";
    }
}