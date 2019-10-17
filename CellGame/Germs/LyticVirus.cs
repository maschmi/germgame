using System;
using System.Runtime.InteropServices;
using CellGame.Helper;
using CellGame.Tissue;

namespace CellGame.Germs
{
    internal class LyticVirus : IGerm
    {
        private const bool IsLytic = true;
        private const bool IsBudding = false;
        private const int GenerationsToMature = 2;
        private const int ReplicationMultiplier = 10;
        
        private readonly int _generation;
        private readonly bool _isMature;
        
        private bool _canInfectCell = false;
        private (ushort selfSignal, ushort alertSignal) _originCellSignals;
        private readonly IEventAggregator _eventAggregator;

        public LyticVirus(IEventAggregator eventAggregator)
        {
            _generation = 0;
            _isMature = false;
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        internal LyticVirus(int currentGeneration, IEventAggregator eventAggregator)
        {
            _generation = currentGeneration;
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _isMature = _generation >= GenerationsToMature;
        }

        private void PublishGermGowth()
        {
            _eventAggregator.Publish(
                new GermGrowthMessage(IsLytic, IsBudding, ReplicationMultiplier, this));
        }

        public void VisitCell(bool isAlive, ushort selfSignal, ushort alertSignal, bool isInfected)
        {
            _canInfectCell = isAlive && !isInfected;
            _originCellSignals = (selfSignal, alertSignal);
        }

        public ICell InfectCell(ICell cellToInfect)
        {
            cellToInfect.Accept(this);
            if (cellToInfect is HealthyCell cell && _canInfectCell)
                    return new InfectedCell(true,
                        (ushort)Math.Floor(_originCellSignals.selfSignal * 0.7),
                        (ushort)(_originCellSignals.alertSignal + (ushort) Math.Floor((ushort.MaxValue - _originCellSignals.alertSignal) * 0.2)),
                        NewInfection());
            
            return cellToInfect;
        }
        
        public IGerm Replicate()
        {
            if (_isMature)
                PublishGermGowth();
            
            return new LyticVirus(_generation + 1, _eventAggregator);
        }

        private IGerm NewInfection()
        {
            return new LyticVirus(_eventAggregator);
        }
    }
}
