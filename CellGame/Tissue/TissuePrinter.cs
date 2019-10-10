using System;
using System.Collections.Immutable;
using System.Text;

namespace CellGame.Tissue
{
    internal sealed class TissuePrinter : ITissueVisitor
    {
        private readonly CellPrinter _cellPrinter;
        private readonly Tissue2D _tissue;
        private readonly int _maxX;
        private readonly int _maxY;
        private readonly StringBuilder _buffer;

        public TissuePrinter(int maxX, int maxY, Tissue2D tissue, CellPrinter cellPrinter)
        {
            _cellPrinter = cellPrinter;
            _tissue = tissue;
            _buffer = new StringBuilder();
            _maxX = maxX;
            _maxY = maxY;
        }

        public void PrintTissue()
        {
            _tissue.Accept(this);
            Console.WriteLine(_buffer.ToString());
        }
        
        public void Visit(ImmutableDictionary<Location, Cell> tissue)
        {
            for (int y = 0; y < _maxY; y++)
            {
                for (int x = 0; x < _maxX; x++)
                {
                    var currentLocation = new Location(y,x);
                    var cell = tissue[currentLocation];
                    _buffer.Append(_cellPrinter.GetStringRepresentaionOf(cell));
                }

                _buffer.AppendLine();
            }
            
        }
    }
}
