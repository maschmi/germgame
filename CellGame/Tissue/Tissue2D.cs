using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CellGame.Germs;
using CellGame.ListShuffle;

namespace CellGame.Tissue
{
    internal sealed class Tissue2D
    {
        private ImmutableDictionary<Location, Cell> _tissue;
        private int _maxX;
        private int _maxY;
        private readonly ICellFactory _cellFactory;
        private readonly IGermFactory _germFactory;

        public ImmutableDictionary<Location, Cell> Tissue => _tissue;

        public Tissue2D(int x, int y, ICellFactory cellFactory, IGermFactory germFactory)
        {
            _maxX = x - 1;
            _maxY = y - 1;
            _tissue = ImmutableDictionary<Location, Cell>.Empty;
            _cellFactory = cellFactory ?? throw new ArgumentNullException(nameof(cellFactory));
            _germFactory = germFactory ?? throw new ArgumentNullException(nameof(germFactory));
        }
        
        public Tissue2D FillTissue(float ratioHealthyCells, float ratioInfectedCells, IShuffle shuffler)
        {
            var totalCount = (_maxY + 1) * (_maxX + 1);
            
            if(ratioHealthyCells + ratioInfectedCells > 1)
                throw new InvalidOperationException("Cannot create more then 100% cells.");

            int countHealthyCells = (int)Math.Floor(ratioHealthyCells * totalCount);
            int countInfectedCells = (int)Math.Floor(ratioInfectedCells * totalCount);

            var xList = shuffler.Shuffle(Enumerable.Range(0, _maxX+1));
            var yList = shuffler.Shuffle(Enumerable.Range(0, _maxY+1));
            var cells = new List<Cell>();
            cells.AddRange(CreateHealthyCells(countHealthyCells));
            cells.AddRange(CreateInfectedCells(countInfectedCells));
            cells.AddRange(CreateEmptyPlaces(totalCount - countHealthyCells - countInfectedCells));
            var cellList = shuffler.Shuffle(cells);

            for(int y = 0; y <= _maxY; y++)
                for (int x = 0; x <= _maxX; x++)
                {
                    var currentCell = (y + 1) * (x + 1);
                    _tissue = _tissue.Add(
                        new Location(xList.ElementAt(x),yList.ElementAt(y)),
                        cellList.ElementAt(currentCell-1));                    
                }

            return this;
        }

        private IEnumerable<Cell> CreateEmptyPlaces(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return null;
            }
        }

        private IEnumerable<Cell> CreateInfectedCells(int count)
        {
            var germ = _germFactory.CreateDefaultGerm();
            for (int i = 0; i < count; i++)
            {
                yield return _cellFactory.CreateInfectedCell(germ);
            }           

        }

        private IEnumerable<Cell> CreateHealthyCells(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return _cellFactory.CreateHealthyCell();
            }
        }
    }
}
