using CellGame.Germs;

namespace CellGame.Tissue
{
    public interface ICellFactory
    {
        ICell CreateHealthyCell();
        ICell CreateInfectedCell(IGerm germ);
        ICell CreateNullCell();
    }
}
