namespace CellGame.Tissue
{
    internal interface ICellVisitor
    {
        Cell Visit(bool isAlive, ushort selfSignal, ushort alertSignal);
    }
}