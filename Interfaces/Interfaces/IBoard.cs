using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IBoard
    {
        int Rows { get; }
        int Cols { get; }

        List<ObstructedCells> ObstructedCells { get; set; }
        bool ValidatePosition(IRobotPosition robotPosition, out string validationMessage);
    }
}
