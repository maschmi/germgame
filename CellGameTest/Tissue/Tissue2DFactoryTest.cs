using System;
using System.Collections;
using System.Collections.Immutable;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using CellGame.Germs;
using CellGame.ListShuffle;
using CellGame.Tissue;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Sdk;

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
        public void Create_Tissue2D_CorrectCellsRatiosAreCreated(int x, int y,
            [Ratio] float variableRatio,
            [Frozen(Matching.ImplementedInterfaces)] NullShuffle shuffler, //do not shuffle but inject when generating sut
            [Frozen] ICellFactory cellFactory,
            Tissue2DFactory sut)
        {
            var infected = variableRatio;
            var healthy = 1 - variableRatio;
            var totalCount = x * y;
            var expectedHealthyCells = (int)Math.Floor(totalCount * healthy);
            var expectedInfectedCells = (int)Math.Floor(totalCount * infected);
            var expectedEmptyPlaces = totalCount - expectedHealthyCells - expectedInfectedCells;

            _ = sut.Create(x, y, healthy, infected);

            var cellFactoryMock = Mock.Get(cellFactory);
            
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
                
    }
}