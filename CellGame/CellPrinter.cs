using System;
using System.Collections.Immutable;
using System.Text;
using CellGame.Tissue;

namespace CellGame
{
    internal class CellPrinter : ICellVisitor
    {

        private string _currentCellCode = string.Empty; 
        public void Visit(bool isAlive, ushort selfSignal, ushort alertSignal, bool isInfected)
        {
            if (isInfected)
                _currentCellCode = "I";
            else
                _currentCellCode = "C";
        }

        public string GetStringRepresentationOf(ICell cell)
        {
            _currentCellCode = string.Empty;

            if (cell != null)
                cell.Accept(this);
            else
                return ("X");

            return _currentCellCode;
        }
    }
}
