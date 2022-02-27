using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotSimlator.ToyRobot
{
    public class RobotPosition : IRobotPosition
    {
        public RobotPosition(int x, int y, RobotDirection Direction)
        {
            X = x;
            Y = y;
            this.Direction = Direction;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public RobotDirection Direction { get; set; }
    }
}
