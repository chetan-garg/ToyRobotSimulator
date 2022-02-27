using Contracts.Interfaces;

namespace Contracts
{
    public class PlacementParameters
    {
        public IRobotPosition Position { get; set; }

        public PlacementParameters(IRobotPosition position) { this.Position = position; }
    }
}