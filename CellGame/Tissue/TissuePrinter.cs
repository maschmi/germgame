using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Xml;

namespace CellGame.Tissue
{
    internal sealed class TissuePrinter
    {
        private readonly CellPrinter _cellPrinter;
        private readonly Tissue2D _tissue;
        private readonly int _maxX;
        private readonly int _maxY;
        private readonly StringBuilder _buffer;

        public TissuePrinter(Tissue2D tissue, CellPrinter cellPrinter)
        {
            _cellPrinter = cellPrinter;
            _tissue = tissue;
            _buffer = new StringBuilder();

            var locations = _tissue.Tissue.Keys;

            _maxX = locations.Select(l => l.X).Max();
            _maxY = locations.Select(l => l.Y).Max();
        }

        public void PrintTissue()
        {
            CreateOutput(_tissue.Tissue);
            Console.WriteLine(_buffer.ToString());
        }
        
        public void CreateOutput(ImmutableDictionary<Location, ICell> tissue)
        {
            for (int y = 0; y < _maxY; y++)
            {
                for (int x = 0; x < _maxX; x++)
                {
                    var currentLocation = new Location(x,y);
                    var cell = tissue[currentLocation];
                    _buffer.Append(_cellPrinter.GetStringRepresentationOf(cell));
                }

                _buffer.AppendLine();
            }
            
        }
    }
}
