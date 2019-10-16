using System.Linq;
using AutoFixture;
using AutoFixture.Xunit2;
using CellGame.RunGame;
using CellGame.Tissue;
using CellGameTest.TestAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace CellGameTest.RunGame
{
    public class RoundBasedGameTest
    {
        [Theory]
        [AutoMoqData]
        internal void Advance_DoesNotReturnSameTissue2DInstanceAndCellsAreCloned(
            [MockCellTissueCreation(10)]Tissue2D input,
            RoundBasedGame sut)
        {
            var result = sut.Advance(input);
            
            VerifyNotSameInstance(input, result);
            VerifyInputCellsAreCloned(input);
        }
        
        [Theory]
        [AutoMoqData]
        internal void Advance_TissueGrows_NullCellsGetReduced(
            [TissueCreationFromFactory(10)]Tissue2D input,
            [Frozen(Matching.ImplementedInterfaces)]TissueGrowthMechanism growth,
            RoundBasedGame sut)
        {
            var result = sut.Advance(input);
            
            VerifyNotSameInstance(input, result);
            VerifyLessNullCellsPresent(input, result);
        }

        private void VerifyLessNullCellsPresent(Tissue2D input, Tissue2D result)
        {
            var resultNullCells = result.Tissue.Values.Count(c => c is NullCell);
            var inputNullCells = input.Tissue.Values.Count(c => c is NullCell);
            resultNullCells.Should().BeLessThan(inputNullCells);
        }

        private static void VerifyInputCellsAreCloned(Tissue2D input)
        {
            foreach (var cell in input.Tissue.Values)
            {
                Mock.Get(cell).Verify(c => c.Clone(), Times.Once);
            }
        }

        private static void VerifyNotSameInstance(Tissue2D input, Tissue2D result)
        {
            result.Should().NotBe(input);
        }
    }
}
