using CellGame.Tissue;

namespace CellGame.Germs
{
    public interface IGerm : ICellVisitor
    {
        ICell InfectCell(ICell cellToInfect);
        void Accept(IGermVisitor visitor);
        IGerm Clone();
    }
}
