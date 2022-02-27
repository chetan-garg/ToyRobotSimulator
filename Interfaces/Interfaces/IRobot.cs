using System;

namespace Contracts.Interfaces
{
    public interface IRobot
    {
        IRobotPosition CurrentPosition { get; set; }
        void Place(IRobotPosition position);
        IRobotPosition GetNextPosition(int movePlaces);
        void RotateRobot(int rotations);
        void RotateLeft(int rotations);
        void RotateRight(int rotations);
        string ReportCurrentPosition();
    }
}
