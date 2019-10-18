using System;
using System.ComponentModel.DataAnnotations;
using CellGame.Tissue;

namespace CellGame.Germs
{
    internal sealed class NullGerm : IGerm
    {
        private ICell _healthyCellToReturn;

        public NullGerm()
        {
                
        }
        public void VisitCell(bool isAlive, ushort selfSignal, ushort alertSignal)
        {
            _healthyCellToReturn = new HealthyCell();
        }

        public ICell InfectCell(ICell cellToInfect)
        {
            cellToInfect.Accept(this);
            return _healthyCellToReturn;
        }

        public void Accept(IGermVistor visitor)
        {
            //do noting
        }

        public IGerm Replicate()
        {
            return new NullGerm();
        }
    }
}
