namespace CellGame.Tissue
{
    public interface ICellVisitor
    {
        void VisitCell(bool isAlive, ushort selfSignal, ushort alertSignal, bool isInfected);
    }
}
