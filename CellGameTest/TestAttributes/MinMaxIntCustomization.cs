using System;
using System.Linq;
using AutoFixture;

namespace CellGameTest.TestAttributes
{
    public class MinMaxIntCustomization : ICustomization
    {
        private readonly Random _rnd = new Random();
        private readonly int _maxInt;
        private readonly int _minInt;

        public MinMaxIntCustomization(int minInt, int maxInt)
        {
            _maxInt = maxInt + 1;
            _minInt = minInt;
        }

        public void Customize(IFixture fixture)
        {
            fixture.Customize<int>(c => c.FromFactory<uint>(i => _rnd.Next(_minInt, _maxInt)));
        }
    }
}
