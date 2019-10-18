using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using AutoFixture.AutoMoq;
using CellGame.Tissue;
using FluentAssertions;
using Moq;
using Xunit;

namespace CellGameTest.Tissue
{
    [ExcludeFromCodeCoverage]
    public class HealthyCellTest
    {
        [Fact]
        public void HealthyCellConstructorSetsDefaultParameters()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var visitor = fixture.Create<ICellVisitor>();
            
            var sut = fixture.Create<HealthyCell>();
            
            sut.Accept(visitor);
            VerifyHealhyCellWithDefault(visitor);
        }
        
        [Fact]
        public void HealthyCellCloneCreatesNewCellWithSameParametersAsParentCell()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var visitor = fixture.Create<ICellVisitor>();
            var visitsNewCell = fixture.Create<ICellVisitor>();

            var sut = fixture.Create<HealthyCell>();
            sut.Accept(visitor);
            VerifyHealhyCellWithDefault(visitor);

            var newCell = sut.Clone();

            newCell.Should().NotBeSameAs(sut);
            newCell.Accept(visitsNewCell);
            VerifyHealhyCellWithDefault(visitsNewCell);
        }

        private static void VerifyHealhyCellWithDefault(ICellVisitor visitor)
        {
            Mock.Get(visitor)
                .Verify(v => v.VisitCell(It.Is<bool>(alive => alive),
                    It.Is<ushort>(selfSig => selfSig == ushort.MaxValue),
                    It.Is<ushort>(alertSig => alertSig == ushort.MinValue)),
                    Times.Once);
        }
    }
}