﻿using Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ICommandParser
    {
        RobotCommandType ParseCommand(string commandInput);
        IRobotPosition ParsePlacementParametes(string[] inputs);
    }
}
