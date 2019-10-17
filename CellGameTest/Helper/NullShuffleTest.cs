using System.Collections.Generic;
using AutoFixture.Xunit2;
using CellGame.Helper.Shuffle;
using CellGame.Tissue;
using FluentAssertions;
using Xunit;

namespace CellGameTest.Helper
{
    public class NullShuffleTest
    {
        public static IEnumerable<object[]> IntTestList = ShuffleTestData.IntTestList;
        public static IEnumerable<object[]> LocationTestList = ShuffleTestData.LocationTestList;
        
        [Theory]
        [MemberAutoData(nameof(IntTestList))]
        public void NullShuffle_Int_DoesNotShuffle(
            IEnumerable<int> input, NullShuffle sut)
        {
            var result = sut.Shuffle(input);
            VerifyResultIsNotShuffled(input, result);
        }
        
        [Theory]
        [MemberAutoData(nameof(LocationTestList))]
        public void NullShuffle_Location_DoesNotShuffle(
            IEnumerable<Location> input, NullShuffle sut)
        {
            var result = sut.Shuffle(input);
            VerifyResultIsNotShuffled(input, result);
        }
        
        private static void VerifyResultIsNotShuffled<T>(IEnumerable<T> input, IEnumerable<T> result)
        {
            result.Should().BeEquivalentTo(input, o => o.WithStrictOrdering());
            result.Should().NotBeSameAs(input);
        }
    }
}
