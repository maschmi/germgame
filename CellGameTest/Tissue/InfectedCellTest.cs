using System;
using AutoFixture.Xunit2;
using CellGame.Germs;
using CellGame.Tissue;
using CellGameTest.TestAttributes;
using FluentAssertions;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace CellGameTest.Tissue
{
    public class InfectedCellTest
    {
        [Theory]
        [AutoMoqData]
        internal void InfectedCellIsConstructedWithCorrectAttributes(
            [Frozen] IGerm germ,
            [Frozen] ushort signals,
            [Frozen] bool isAlive,
            ICellVisitor visitor,
            InfectedCell sut)
        {
            sut.Accept(visitor);
            VerifyCellExposesCorrectParametersToVisitor(signals, isAlive, visitor);
        }
        
        [Theory]
        [AutoMoqData]
        internal void CloningADeadCellDoesNotReplicateTheGermAndReturnsNullCellButAL(
            [Frozen] IGerm germ,
            DeadInfectedCell sut)
        {
            var result = sut.Clone();
            result.Should().BeOfType<NullCell>();
            VerifyTimesGermReplicatIsCalled(0, germ);
        }
        
        [Theory]
        [AutoMoqData]
        internal void CloningALivingCellDoesReplicatesTheGermAndReturnsInfectedCell(
            [Frozen] IGerm germ,
            [Frozen] ushort signals,
            [Frozen] ICellVisitor visitor,
            LivingInfectedCell sut)
        {
            var result = sut.Clone();
            result.Should().BeOfType<InfectedCell>();
            VerifyTimesGermReplicatIsCalled(1, germ);
            VerifySignalsAreCloned(sut, result, signals, visitor);
        }

        [Theory]
        [AutoMoqData]
        internal void MatureLyticGermKillsCellWhileCloning(
            [Frozen(Matching.ImplementedInterfaces)] MatureLyticGerm lyticGerm,
            [Frozen] ICellVisitor visitor,
            LivingInfectedCell sut)
        {
            bool isAlive = true;
            bool isNotAlive = false;
            sut.Accept(visitor);
            
            VerifyCellIsAlive(visitor, isAlive);
            
            var result = sut.Clone();
            result.Accept(visitor);
            result.Should().BeOfType<InfectedCell>();
            VerifyCellIsAlive(visitor, isNotAlive);
        }

        private static void VerifyCellIsAlive(ICellVisitor visitor, bool isAlive)
        {
            Mock.Get(visitor)
                .Verify(v => v.VisitCell(
                        It.Is<bool>(alive => alive == isAlive),
                        It.IsAny<ushort>(),
                        It.IsAny<ushort>()),
                    Times.Once);
            Mock.Get(visitor).Reset();
        }

        private void VerifySignalsAreCloned(LivingInfectedCell sut, ICell result, ushort signals, ICellVisitor visitor)
        {
            sut.Accept(visitor);
            result.Accept(visitor);
            Mock.Get(visitor)
                .Verify(v => v.VisitCell(
                    It.IsAny<bool>(),
                    It.Is<ushort>(self => self == signals),
                    It.Is<ushort>(alert => alert == signals)),
                    Times.Exactly(2));
        }

        private static void VerifyTimesGermReplicatIsCalled(int count, IGerm germ)
        {
            Mock.Get(germ)
                .Verify(g => g.Replicate(), Times.Exactly(count));
        }

        private static void VerifyIsNullCell(ICell result)
        {
            result.Should().BeOfType<NullCell>();
        }

        private static void VerifyCellExposesCorrectParametersToVisitor(ushort signals, bool isAlive, ICellVisitor visitor)
        {
            Mock.Get(visitor)
                .Verify(v => v.VisitCell(
                        It.Is<bool>(alive => alive == isAlive),
                        It.Is<ushort>(self => self == signals),
                        It.Is<ushort>(alert => alert == signals)),
                    Times.Once);
        }
    }

    internal class MatureLyticGerm : IGerm
    {
        public ICell InfectCell(ICell cellToInfect)
        {
            throw new NotImplementedException();
        }

        public void Accept(IGermVistor visitor)
        {
            visitor.Visit(true, true, false);
        }

        public IGerm Replicate()
        {
            return this;
        }
    }

    internal class LivingInfectedCell : ICell
    {
        private InfectedCell _cell;

        public LivingInfectedCell(ushort selfSignal, ushort alertSignal, IGerm germ)
        {
            _cell = new InfectedCell(true, selfSignal, alertSignal, germ);            
        }

        public ICell Clone()
            => _cell.Clone();

        public void Accept(ICellVisitor cellVisitor)
            => _cell.Accept(cellVisitor);
    }

    internal class DeadInfectedCell : ICell
    {
        private InfectedCell _cell;

        public DeadInfectedCell(ushort selfSignal, ushort alertSignal, IGerm germ)
        {
            _cell = new InfectedCell(false, selfSignal, alertSignal, germ);            
        }

        public ICell Clone()
            => _cell.Clone();

        public void Accept(ICellVisitor cellVisitor)
            => _cell.Accept(cellVisitor);
    }
}