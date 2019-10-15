using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AutoFixture;
using CellGame.Tissue;
using FluentAssertions;

namespace CellGameTest.TestAttributes
{
    public class Tissue2DCustomization : ICustomization
    {
        private readonly Random _rnd = new Random();
        private readonly int _maxValue;

        public Tissue2DCustomization(int maxValue)
        {
            _maxValue = maxValue;
        }
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Tissue2D>(c => c.FromFactory(() =>
            {
                var maxX = _rnd.Next(1, _maxValue);
                var maxY = _rnd.Next(1, _maxValue);

                var locations = new List<Location>();
                for (int y = 0; y < maxY; y++)
                    for (int x = 0; x < maxX; x++)
                        locations.Add(new Location(x, y));
                
                ImmutableDictionary<Location, ICell> tissue = locations
                    .Aggregate(
                        ImmutableDictionary<Location, ICell>.Empty,
                        (result, next) =>
                            result.Add(next, fixture.Create<ICell>())
                    );
                tissue.Count().Should().Be(maxX * maxY);
                return new Tissue2D(tissue);
            }));
        }
    }
}
