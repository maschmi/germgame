using CellGame.Tissue;

namespace CellGame.Germs
{
    internal interface IGerm : ICellVisitor
    {
        Cell InfectCell(Cell cellToInfect);
    }
}
