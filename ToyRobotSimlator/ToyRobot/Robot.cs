using Contracts;
using Contracts.Enums;
using Contracts.Interfaces;
using System;

namespace ToyRobotSimlator.ToyRobot
{
    public class Robot : IRobot
    {
        public IRobotPosition CurrentPosition { get; set; }

        /// <summary>
        /// This method get the position of the robot based on their current position and direction and 
        /// how many places the user want to move the robot.
        /// </summary>
        /// <param name="movePlaces">Number of places the robot need to be moved.</param>
        /// <returns>The new position the robot will be if the move is finalised.</returns>
        public IRobotPosition GetNextPosition(int movePlaces = 1)
        {
            var newPosition = new RobotPosition(CurrentPosition.X, CurrentPosition.Y, CurrentPosition.Direction);
            switch (CurrentPosition.Direction)
            {
                case RobotDirection.North:
                    newPosition.Y += movePlaces;
                    break;
                case RobotDirection.South:
                    newPosition.Y -= movePlaces;
                    break;
                case RobotDirection.East:
                    newPosition.X += movePlaces;
                    break;
                case RobotDirection.West:
                    newPosition.X -= movePlaces;
                    break;
            }

            return newPosition;
        }

        public void Place(IRobotPosition position)
        {
            this.CurrentPosition = position;
        }

        public void RotateLeft(int positions)
        {
            RotateRobot(0 - positions);
        }
        public void RotateRight(int positions)
        {
            RotateRobot(positions);
        }

        public void RotateRobot(int rotations)
        {
            var directions = (RobotDirection[])Enum.GetValues(typeof(RobotDirection));
            var index = ((int)(CurrentPosition.Direction + rotations)) % directions.Length;

            if ((CurrentPosition.Direction + rotations) < 0)
            {
                CurrentPosition.Direction = directions[directions.Length + index];
            }
            else
            {
                CurrentPosition.Direction = directions[index];
            }
        }

        public string ReportCurrentPosition()
        {
            if (CurrentPosition == null)
            {
                return Constants.NullPostionErrorMessage;
            }
            return $"Current position for the robot : X: {CurrentPosition.X}, Y: {CurrentPosition.Y}, Direction: {CurrentPosition.Direction}";
        }
    }
}
