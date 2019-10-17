using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using CellGame.Helper.Shuffle;
using CellGame.Tissue;
using CellGameTest.TestAttributes;
using FluentAssertions;
using Xunit;

namespace CellGameTest.Helper
{
    public class FisherYatesShuffleTest
    {
        public static IEnumerable<object[]> IntTestList = ShuffleTestData.IntTestList;
        public static IEnumerable<object[]> LocationTestList = ShuffleTestData.LocationTestList;

        [Theory]
        [MemberAutoData(nameof(IntTestList))]
        public void NullShuffle_Int_DoesShuffle(
            IEnumerable<int> input, FisherYatesShuffle sut)
        {
            var result = sut.Shuffle(input);
            VerifyShuffle(input, result);
        }

        [Theory]
        [MemberAutoData(nameof(LocationTestList))]
        public void NullShuffle_Location_DoesShuffle(
            IEnumerable<Location> input, FisherYatesShuffle sut)
        {
            var result = sut.Shuffle(input);
            VerifyShuffle(input, result);
        }

        private static void VerifyShuffle<T>(IEnumerable<T> input, IEnumerable<T> result)
        {
            result.Should().NotBeSameAs(input);
            result.Should().BeEquivalentTo(input, o => o.WithoutStrictOrdering());
            result.SequenceEqual(input).Should().BeFalse();
        }
    }
}
