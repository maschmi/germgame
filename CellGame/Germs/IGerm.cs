using CellGame.Tissue;

namespace CellGame.Germs
{
    public interface IGerm : ICellVisitor
    {
        ICell InfectCell(ICell healthyCellToInfect);
    }
}
