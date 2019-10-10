using System.ComponentModel.DataAnnotations;
using CellGame.Tissue;

namespace CellGame.Germs
{
    internal sealed class NullGerm : IGerm
    {
        private Cell _cellToReturn;
        
        public void Visit(bool isAlive, ushort selfSignal, ushort alertSignal, bool isInfected)
        {
            _cellToReturn = new Cell(isAlive,
                isInfected,
                selfSignal,
                alertSignal,
                this);
        }

        public Cell InfectCell(Cell cellToInfect)
        {
            cellToInfect.Accept(this);
            return _cellToReturn;
        }
    }
}
