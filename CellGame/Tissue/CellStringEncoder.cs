using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using CellGame.Tissue;

namespace CellGame
{
    public sealed class CellStringEncoder : ICellVisitor
    {
        private string _currentCellCode = string.Empty;

        public void VisitCell(bool isAlive, ushort selfSignal, ushort alertSignal, bool isInfected)
        {
            if (!isAlive)
                _currentCellCode = "X";
            else if (isInfected)
                _currentCellCode = "I";
            else
                _currentCellCode = "C";
        }

        public string GetEncodedCell(ICell cell)
        {
            _currentCellCode = string.Empty;

            if (cell != null)
                cell.Accept(this);
            else
                _currentCellCode = "E";

            return _currentCellCode;
        }
    }
}
