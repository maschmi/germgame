namespace CellGame.Tissue
{
    public class NullCell : ICell
    {
        public ICell Clone()
        {
            return new NullCell();
        }

        public void Accept(ICellVisitor cellVisitor)
        {
            cellVisitor.Visit(false,0,0,false);
        }
    }
}
