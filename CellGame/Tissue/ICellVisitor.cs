namespace CellGame.Tissue
{
    public interface ICellVisitor
    {
        void Visit(bool isAlive, ushort selfSignal, ushort alertSignal, bool isInfected);
    }
}