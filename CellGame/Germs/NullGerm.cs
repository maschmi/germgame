using System.ComponentModel.DataAnnotations;
using CellGame.Tissue;

namespace CellGame.Germs
{
    internal sealed class NullGerm : IGerm
    {
        private ICell _healthyCellToReturn;
        
        public void Visit(bool isAlive, ushort selfSignal, ushort alertSignal, bool isInfected)
        {
            _healthyCellToReturn = new HealthyCell();
        }

        public ICell InfectCell(ICell healthyCellToInfect)
        {
            healthyCellToInfect.Accept(this);
            return _healthyCellToReturn;
        }
    }
}
