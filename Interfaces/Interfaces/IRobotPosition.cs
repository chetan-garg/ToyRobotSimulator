using Contracts.Enums;

namespace Contracts.Interfaces
{
    public interface IRobotPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        RobotDirection Direction { get; set; }
    }
}