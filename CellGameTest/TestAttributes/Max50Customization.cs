using System;
using System.Linq;
using AutoFixture;

namespace CellGameTest.TestAttributes
{
    public class Max50Customization : ICustomization
    {
        private Random _rnd = new Random();
        
        public void Customize(IFixture fixture)
        {
            fixture.Customize<int>(c => c.FromFactory<uint>(i => _rnd.Next(0, 51)));
        }
    }
}