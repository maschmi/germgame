using CellGame.Germs;

namespace CellGame.Tissue
{
    internal interface ICellFactory
    {
        Cell CreateHealthyCell();
        Cell CreateInfectedCell(IGerm germ);
    }
}