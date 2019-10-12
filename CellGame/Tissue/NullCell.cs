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
            //do nothing, this is empty place/cell
        }
    }
}