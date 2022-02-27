using Contracts;
using Contracts.Enums;
using Contracts.Interfaces;
using System;

namespace ToyRobotSimlator.ToyRobot
{
    public class Robot : IRobot
    {
        public IRobotPosition CurrentPosition { get; private set; }

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

        /// <summary>
        /// This method sets the CurrentPosition to the position object sent by the caller.
        /// </summary>
        /// <param name="position">New position to be set to robot.</param>
        public void Place(IRobotPosition position)
        {
            CurrentPosition = position;
        }

        /// <summary>
        /// Rotates the robot by 90 degress for the number of <paramref name="positions"/> to the left.
        /// </summary>
        /// <param name="positions">Number of rotations to turn.</param>
        public void RotateLeft(int positions)
        {
            RotateRobot(0 - positions);
        }

        /// <summary>
        /// Rotates the robot by 90 degress for the number of <paramref name="positions"/> to the right.
        /// </summary>
        /// <param name="positions">Number of rotations to turn.</param>
        public void RotateRight(int positions)
        {
            RotateRobot(positions);
        }

        /// <summary>
        /// Rotates the robot by 90 degress for the number of <paramref name="positions"/>. The number of rotations can be +ve or -ve driving the direction of rotation.
        /// </summary>
        /// <param name="positions">Number of rotations to turn.</param>
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
