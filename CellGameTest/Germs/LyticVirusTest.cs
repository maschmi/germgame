using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using CellGame.Germs;
using CellGame.Germs.Messages;
using CellGame.Helper;
using CellGame.Tissue;
using CellGameTest.TestAttributes;
using CellGameTest.TestHelper;
using FluentAssertions;
using Moq;
using Xunit;
using static System.Math;

namespace CellGameTest.Germs
{
    public class LyticVirusTest
    {
        private const int GenerationToMature = 2;
        private const int ReplicationMultiplier = 100;
        private const double InfectionFailureRate = 0.9;

        [Theory]
        [AutoMoqData]
        internal void NewlyConstructedVirusTellsVisitorItIsLyticButNotMature(
            IGermVistor visitor,
            LyticVirus sut)
        {
            sut.Accept(visitor);
            bool isNotMature = false;
            VerifyGerm(isNotMature, visitor);
        }
        
        [Theory]
        [AutoMoqData]
        internal void VirusReplicationReturnsNewVirus(
            LyticVirus sut)
        {
            var result = sut.Replicate();
            result.Should().NotBeSameAs(sut);
        }
        
        [Theory]
        [AutoMoqData]
        internal void VirusCanInfectHealthyCellsInOneOutOfTenTimes(
            HealthyCell cellToInfect,
            LyticVirus sut)
        {
            var result = new List<ICell>();
            var numberOfTries = (int)Ceiling(InfectionFailureRate * 3 * 10);
            for (int currentTry = 0; currentTry < numberOfTries; currentTry++)
            {
                result.Add(sut.InfectCell(cellToInfect).Clone());
            }

            VerifyAtLeastOneCellGotInfected(result);
        }

        private static void VerifyAtLeastOneCellGotInfected(List<ICell> result)
        {
            result.Should().Contain(c => c is InfectedCell);
        }

        [Theory]
        [AutoMoqData]
        internal void VirusDoesNotInfectAlreadyInfectedCellsOrNullCells(
            InfectedCell infectedCellToInfect,
            NullCell nullCellToInfect,
            LyticVirus sut)
        {
            var result = new List<ICell>();
            var numberOfTries = (int)Ceiling(InfectionFailureRate * 3 * 10);
            for (int currentTry = 0; currentTry < numberOfTries; currentTry++)
            {
                result.Add(sut.InfectCell(infectedCellToInfect).Clone());
                result.Add(sut.InfectCell(nullCellToInfect).Clone());
            }

            VerifyNoNewlyInfectedCellsArePresent(result, numberOfTries);
        }

        [Theory]
        [AutoMoqData]
        internal void VirusMaturesAfterTwoGenerationsSendsMessageAndTellsVisitorItIsMature(
            [Frozen] EventAggregator eventAggregator,
            IListenOn<GermGrowthMessage> listener,
            IGermVistor visitor,
            LyticVirus sut)
        {
            eventAggregator.Subscribe(listener);
            
            IGerm matureVirus = sut;
            for (int gen = 0; gen <= GenerationToMature; gen++)
            {
                matureVirus = matureVirus.Replicate();
            }
            
            VerifyGermIsMature(visitor, matureVirus);
            VerifyCorrectMessageIsSent(listener, matureVirus);
        }

        private static void VerifyCorrectMessageIsSent(IListenOn<GermGrowthMessage> listener, IGerm matureVirus)
        {
            var expectedMessaged = new GermGrowthMessage(true, false, ReplicationMultiplier, matureVirus);
            
            Mock.Get(listener).Verify(
                v => v.ProcessMessageAsync(
                    It.Is<GermGrowthMessage>(m => m.IsEquivalentTo(expectedMessaged))),
                Times.Once);
        }

        private void VerifyGermIsMature(IGermVistor vistor, IGerm matureVirus)
        {
            matureVirus.Accept(vistor);
            bool isMature = true;
            VerifyGerm(isMature, vistor);
        }

        private static void VerifyNoNewlyInfectedCellsArePresent(List<ICell> result, int numberOfTries)
        {
            result.OfType<InfectedCell>().Should().HaveCount(numberOfTries);
        }

        private void VerifyGerm(bool isMature, IGermVistor visitor)
        {
            Mock.Get(visitor)
                .Verify(m => m.Visit(
                    It.Is<bool>(mature => mature == isMature),
                    It.Is<bool>(lytic => lytic),
                    It.Is<bool>(budding => !budding)),
                Times.Once());
        }
    }
}