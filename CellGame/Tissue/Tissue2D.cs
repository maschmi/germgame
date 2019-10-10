using System.Collections.Generic;

namespace CellGame.Tissue
{
    public class Tissue2D
    {
        private Dictionary<Location, Cell> _tissue;
        private int _maxX;
        private int _maxY;

        public Tissue2D(int x, int y)
        {
            _maxX = x - 1;
            _maxY = y - 1;
            _tissue = new Dictionary<Location, Cell>();
        }
    }
}