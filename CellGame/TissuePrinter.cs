using System;
using System.Collections.Immutable;
using System.Text;
using CellGame.Tissue;

namespace CellGame
{
    internal class TissuePrinter : ICellVisitor
    {
        private IImmutableDictionary<Location, Cell> _tissueMap;
        private StringBuilder _buffer;

        public TissuePrinter(IImmutableDictionary<Location, Cell> tissue)
        {
            _tissueMap = tissue;
            _buffer = new StringBuilder();
            
        }

        public void PrintTissue(int maxX, int maxY)
        {
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    var currentLocation = new Location(y,x);
                    var cell = _tissueMap[currentLocation];
                    if (cell != null)
                        cell.Accept(this);
                    else
                        _buffer.Append(" ");
                }

                _buffer.AppendLine();
            }

            Console.WriteLine(_buffer.ToString());
        }
        
        public void Visit(bool isAlive, ushort selfSignal, ushort alertSignal, bool isInfected)
        {
            if (isInfected)
                _buffer.Append("I");
            else
                _buffer.Append("C");
        }
    }
}
