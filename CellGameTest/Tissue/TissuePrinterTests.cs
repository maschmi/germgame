using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using CellGame;
using CellGame.Tissue;
using CellGameTest.TestAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace CellGameTest.Tissue
{
    public class TissuePrinterTests
    {
        [Fact]
        public void PrintTissue_CellPrintIsCalled_CorrectNumberOfTimes()
        {
            var random = new Random();
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var maxX = random.Next(1,200);
            var maxY = random.Next(1,200);

            var locations = new List<Location>();
            for(int y = 0; y < maxY; y++)
                for (int x = 0; x < maxX; x++)
                    locations.Add(new Location(x, y));
            
            var cell = fixture.Freeze<ICell>();
            locations.Count().Should().Be(maxX * maxY);
            ImmutableDictionary<Location,ICell> tissue = locations
                .Aggregate(
                    ImmutableDictionary<Location,ICell>.Empty,
                    (result, next) =>
                        result.Add(next, cell)
                );

            fixture.Register(() => tissue);
            
            var testTissue = fixture.Create<Tissue2D>();
            fixture.Register(() => testTissue);

            var sut = fixture.Create<TissuePrinter>();
            sut.PrintTissue();
            
            Mock.Get(cell).Verify(c => c.Accept(It.IsAny<CellStringEncoder>()) 
                , Times.Exactly(testTissue.Tissue.Count));
        }

        [Theory]
        [AutoMoqData]
        public void PrintTissue_CellPrintIsCalled_CorrectNumberOfTimes2(
            [Frozen] ICell cell,
            //be sure to freeze it, otherwise a new tissue is created for parameters and stuff
            [Frozen, TissueCreation] Tissue2D tissue, 
            TissuePrinter sut)
        {
            sut.PrintTissue();
            VerifyCellStringencoderIsCalledForTotalCountTimes(cell, tissue);
        }
        
        [Theory]
        [AutoMoqData]
        public void PrintTissue_CellPrintIsCalled_ForEachCell(
            [Frozen, TissueCreation] Tissue2D tissue,
            TissuePrinter sut)
        {
            sut.PrintTissue();
            VerifyCellStringencoderIsCalledForEachCell(tissue);
        }

        private void VerifyCellStringencoderIsCalledForEachCell(Tissue2D tissue)
        {
            var cells = tissue.Tissue.Values;
            var currentCount = 0;
            foreach (var cell in cells)
            {
                Mock.Get(cell)
                    .Verify(c => c.Accept(It.IsAny<CellStringEncoder>()), 
                        Times.Once,
                        $"because invocation must not happen only for the first {currentCount} times but for all {cells.Count()}");
                currentCount++;
            }
        }

        private static void VerifyCellStringencoderIsCalledForTotalCountTimes(ICell cell, Tissue2D tissue)
        {
            Mock.Get(cell).Verify(c => c.Accept(It.IsAny<CellStringEncoder>())
                , Times.Exactly(tissue.Tissue.Count()));
        }
    }
}
