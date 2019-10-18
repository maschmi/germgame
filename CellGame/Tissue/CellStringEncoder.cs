using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CellGame.Tissue;

namespace CellGame
{
    public sealed class CellStringEncoder : ICellVisitor
    {
        private string _currentCellCode = string.Empty;
        private ConsoleColor _colorCode = ConsoleColor.White;

        public void VisitCell(bool isAlive, ushort selfSignal, ushort alertSignal)
        {
            if (isAlive) 
                return;
            
            _currentCellCode = "X";
            _colorCode = ConsoleColor.DarkBlue;
        }

        public string GetEncodedCell(ICell cell)
        {
            _currentCellCode = cell switch
            {
                InfectedCell _ => "I",
                HealthyCell _ => "C",
                _ => string.Empty
            };
            
            cell?.Accept(this);

            return _currentCellCode;
        }
        
        public  (ConsoleColor, string) GetColorEncodedCell(ICell cell)
        {
            (_colorCode, _currentCellCode) = cell switch
            {
                InfectedCell _ => (ConsoleColor.Red, "I"),
                HealthyCell _ => (ConsoleColor.Green, "C"),
                _ => (ConsoleColor.DarkYellow, string.Empty)
            };

            cell?.Accept(this);
            
            return (_colorCode, _currentCellCode);
        }
    }
}
