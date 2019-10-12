namespace CellGame.Tissue
{
    public interface ICell
    {
        ICell Clone();
        void Accept(ICellVisitor cellVisitor);
    }
}