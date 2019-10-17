using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CellGame.Tissue;

namespace CellGameTest.Helper
{
    public class ShuffleTestData
    {
        private static readonly IFixture _fixture = new Fixture().Customize(new OnlyUnsignedIntegersCustomization());

        public static IEnumerable<object[]> IntTestList =>
            new List<object[]> {new object[] {_fixture.CreateMany<int>(50)}};

        public static IEnumerable<object[]> LocationTestList =>
            new List<object[]> {new object[] {_fixture.CreateMany<Location>(50)}};
    }

    internal class OnlyUnsignedIntegersCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<int>(c => c.FromFactory<ushort>((i) => i));
        }
    }
}
