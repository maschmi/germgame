using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using CellGame.Germs;
using CellGame.Helper;
using CellGame.ListShuffle;
using CellGame.Tissue;
using CellGameTest.Helper;
using CellGameTest.TestAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace CellGameTest.Tissue
{
    public class Tissue2DFactoryTest
    {
        [Theory]
        [AutoMoqData]
        public void Create_Tissue2D_WithCorrectNumberOfCells(int x, int y,
            [Ratio]float living,
            [Frozen(Matching.ImplementedInterfaces)]NullShuffle shuffler,
            Tissue2DFactory sut)
        {
            Tissue2D result = sut.Create(x, y, living, 1-living);
            result.Tissue.Count.Should().Be(x*y);
        }
        
        [Theory]
        [AutoMoqData]
        public void Create_Tissue2D_WithCorrectLocationOrigin(int x, int y,
            [Ratio]float living,
            [Frozen(Matching.ImplementedInterfaces)]ReverseTestShuffler shuffler, //we need to make sure max Location is in but do not want to use a complex shuffler
            Tissue2DFactory sut)
        {
            Tissue2D result = sut.Create(x, y, living, 1-living);
            VerifyLocationBounds(x, y, result);
        }

        private static void VerifyLocationBounds(int x, int y, Tissue2D result)
        {
            result.Tissue.TryGetValue(new Location(0, 0), out _).Should().BeTrue();
            result.Tissue.TryGetValue(new Location(x - 1, y - 1), out _).Should().BeTrue();
            result.Tissue.TryGetValue(new Location(x, y), out _).Should().BeFalse();
        }

        [Theory]
        [AutoMoqData]
        public void Create_Tissue2D_CorrectCellsRatiosAreCreated(int x, int y,
            [Ratio] float variableRatio,
            [Frozen(Matching.ImplementedInterfaces)] NullShuffle shuffler, //do not shuffle but inject when generating sut
            [Frozen] ICellFactory cellFactory,
            Tissue2DFactory sut)
        {
            var infected = variableRatio;
            var healthy = 1 - variableRatio;
            var (expectedHealthyCells, expectedInfectedCells, expectedEmptyPlaces) 
                = CalculateExpectations(x, y, healthy, infected);

            _ = sut.Create(x, y, healthy, infected);

            var cellFactoryMock = Mock.Get(cellFactory);
            
            VerifyCorrectCellCounts(cellFactoryMock, expectedHealthyCells, expectedInfectedCells, expectedEmptyPlaces);
        }
        
        [Theory]
        [AutoMoqData]
        public void Create_Tissue2D_AllPositionsAreCreated(
            [MinMaxInt(0,50)] int x, 
            [MinMaxInt(0,50)] int y,
            [Ratio] float variableRatio,
            [Frozen(Matching.ImplementedInterfaces)] NullShuffle shuffler, //do not shuffle but inject when generating sut
            Tissue2DFactory sut)
        {
            var infected = variableRatio;
            var healthy = 1 - variableRatio;
            
            var result = sut.Create(x, y, healthy, infected);

            VerifyAllLocationAreCreated(x, y, result);
        }

        private void VerifyAllLocationAreCreated(in int maxX, in int maxY, Tissue2D result)
        {
            var locations = result.Tissue.Keys.ToArray();
            var expectedLocations = new List<Location>();
            for(int y = 0; y < maxY; y++)
                for(int x = 0; x < maxX; x++)
                    expectedLocations.Add(new Location(x,y));

            locations.Should().BeEquivalentTo(expectedLocations);
        }

        private static void VerifyCorrectCellCounts(Mock<ICellFactory> cellFactoryMock, int expectedHealthyCells, int expectedInfectedCells,
            int expectedEmptyPlaces)
        {
            cellFactoryMock
                .Verify(cf =>
                    cf.CreateHealthyCell(), Times.Exactly(expectedHealthyCells));
            cellFactoryMock
                .Verify(cf =>
                    cf.CreateInfectedCell(It.IsAny<IGerm>()), Times.Exactly(expectedInfectedCells));
            cellFactoryMock
                .Verify(cf =>
                    cf.CreateNullCell(), Times.Exactly(expectedEmptyPlaces));
        }

        private static (int expectedHealthyCells, int expectedInfectedCells, int expectedEmptyPlaces) CalculateExpectations(
            int x, int y, float healthy, float infected)
        {
            var totalCount = x * y;
            var expectedHealthyCells = (int) Math.Floor(totalCount * healthy);
            var expectedInfectedCells = (int) Math.Floor(totalCount * infected);
            var expectedEmptyPlaces = totalCount - expectedHealthyCells - expectedInfectedCells;
            return (expectedHealthyCells, expectedInfectedCells, expectedEmptyPlaces);
        }
    }
}
