using System;
using AutoFixture;
using CellGame.Helper.Shuffle;
using CellGame.Tissue;

namespace CellGameTest.TestAttributes
{
    public class TissueFromFactoryCustomization : ICustomization
    {
        private readonly int _max;
        private readonly Random _rnd = new Random();

        public TissueFromFactoryCustomization(in int max)
        {
            _max = max + 1;
        }

        public void Customize(IFixture fixture)
        {
            fixture.Customize<Tissue2D>(c =>
                c.FromFactory(() =>
                {
                    fixture.Inject<ICellFactory>(new DefaultCellFactory());
                    fixture.Inject<IShuffle>(item: new NullShuffle());
                    var ratioSeed = (float) _rnd.NextDouble();
                    var nullCells = ratioSeed / 5;
                    var infectedCells = ratioSeed / 3;
                    var healthyCells = 1 - nullCells - infectedCells;
                    var maxX = _rnd.Next(1, _max);
                    var maxY = _rnd.Next(1, _max);
                    var tissueFactory = fixture.Create<Tissue2DFactory>();

                    return tissueFactory
                        .Create(maxX, maxY, healthyCells, infectedCells);
                }));
        }
    }
}
