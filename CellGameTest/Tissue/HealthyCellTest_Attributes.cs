using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using AutoFixture.AutoMoq;
using CellGame.Tissue;
using CellGameTest.TestAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace CellGameTest.Tissue
{
    [ExcludeFromCodeCoverage]
    public class HealthyCellTestAttr
    {
        [Theory]
        [AutoMoqData]
        public void HealthyCellConstructorSetsDefaultParameters(
            ICellVisitor visitor,
            HealthyCell sut)
        {
            sut.Accept(visitor);
            VerifyHealhyCellWithDefault(visitor);
        }
        
        [Theory]
        [AutoMoqData]
        public void HealthyCellCloneCreatesNewCellWithSameParametersAsParentCell(
            ICellVisitor visitsParent,
            ICellVisitor visitsClone,
            HealthyCell sut)
        {
            sut.Accept(visitsParent);
            var newCell = sut.Clone();
            newCell.Accept(visitsClone);
            
            newCell.Should().NotBeSameAs(sut);
            VerifyHealhyCellWithDefault(visitsParent);
            VerifyHealhyCellWithDefault(visitsClone);
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