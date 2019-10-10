namespace CellGame.Tissue
{
    internal interface ICellVisitor
    {
        void Visit(bool isAlive, ushort selfSignal, ushort alertSignal, bool isInfected);
    }
}