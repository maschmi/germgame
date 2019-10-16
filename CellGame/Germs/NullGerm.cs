using System;
using System.ComponentModel.DataAnnotations;
using CellGame.Tissue;

namespace CellGame.Germs
{
    internal sealed class NullGerm : IGerm
    {
        public event EventHandler<GermGrowthEventArgs> GermGrowth;
        
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

        public void Accept(IGermVisitor visitor)
        {
            //do nothing. this is a null implementation
        }

        public IGerm Replicate()
        {
            return new  NullGerm();
        }
    }
}
