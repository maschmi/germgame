using System;
using AutoFixture;

namespace CellGameTest.TestAttributes
{
    public class RatioCustomization : ICustomization
    {
        private readonly Random _rnd = new Random();
        public void Customize(IFixture fixture)
        {
            fixture.Customize<float>(c => 
                c.FromFactory(() => 
                    (float) _rnd.NextDouble()));
        }
    }
}