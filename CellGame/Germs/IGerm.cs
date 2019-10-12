using CellGame.Tissue;

namespace CellGame.Germs
{
    public interface IGerm : ICellVisitor
    {
        Cell InfectCell(Cell cellToInfect);
    }
}
