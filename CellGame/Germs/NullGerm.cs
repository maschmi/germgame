using System;
using System.ComponentModel.DataAnnotations;
using CellGame.Tissue;

namespace CellGame.Germs
{
    internal sealed class NullGerm : IGerm
    {
        
        private ICell _healthyCellToReturn;
        
        public void VisitCell(bool isAlive, ushort selfSignal, ushort alertSignal, bool isInfected)
        {
            _healthyCellToReturn = new HealthyCell();
        }

        public ICell InfectCell(ICell cellToInfect)
        {
            cellToInfect.Accept(this);
            return _healthyCellToReturn;
        }

        public IGerm Replicate()
        {
            return new NullGerm();
        }
    }
}
