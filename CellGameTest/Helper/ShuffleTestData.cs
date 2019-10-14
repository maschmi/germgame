using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using CellGame.Tissue;

namespace CellGameTest.Helper
{
    public class ShuffleTestData
    {
        private static IFixture _fixture = new Fixture();
        
        public static IEnumerable<object[]> IntTestList =>
            new List<object[]> {new object[] {_fixture.CreateMany<int>(250)}};
        public static IEnumerable<object[]> LocationTestList =>
            new List<object[]> {new object[] {_fixture.CreateMany<Location>(250)}};
    }
}
