using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CellGame.Germs;
using CellGame.ListShuffle;

namespace CellGame.Tissue
{
    internal sealed class Tissue2DFactory
    {
        private ImmutableDictionary<Location, Cell> _tissue;
        private readonly ICellFactory _cellFactory;
        private readonly IGermFactory _germFactory;
        private readonly IShuffle _shuffler;

        public ImmutableDictionary<Location, Cell> Tissue => _tissue;

        public Tissue2DFactory(ICellFactory cellFactory, IGermFactory germFactory, IShuffle shuffler)
        {
            _tissue = ImmutableDictionary<Location, Cell>.Empty;
            _cellFactory = cellFactory ?? throw new ArgumentNullException(nameof(cellFactory));
            _germFactory = germFactory ?? throw new ArgumentNullException(nameof(germFactory));
            _shuffler = shuffler ?? throw new ArgumentNullException(nameof(shuffler));
        }
        
        public Tissue2D Create(int maxX, int maxY, float ratioHealthyCells, float ratioInfectedCells)
        {
            var totalCount = (maxY + 1) * (maxX + 1);
            
            if(ratioHealthyCells + ratioInfectedCells > 1)
                throw new InvalidOperationException("Cannot create more then 100% cells.");

            int countHealthyCells = (int)Math.Floor(ratioHealthyCells * totalCount);
            int countInfectedCells = (int)Math.Floor(ratioInfectedCells * totalCount);

            var xList = _shuffler.Shuffle(Enumerable.Range(0, maxX+1));
            var yList = _shuffler.Shuffle(Enumerable.Range(0, maxY+1));
            var cells = new List<Cell>();
            cells.AddRange(CreateHealthyCells(countHealthyCells));
            cells.AddRange(CreateInfectedCells(countInfectedCells));
            cells.AddRange(CreateEmptyPlaces(totalCount - countHealthyCells - countInfectedCells));
            var cellList = _shuffler.Shuffle(cells);

            for(int y = 0; y <= maxY; y++)
            for (int x = 0; x <= maxX; x++)
            {
                var currentCell = (y + 1) * (x + 1);
                _tissue = _tissue.Add(
                    new Location(xList.ElementAt(x),yList.ElementAt(y)),
                    cellList.ElementAt(currentCell-1));                    
            }

            return new Tissue2D(_tissue);
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
