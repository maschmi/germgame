using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CellGame.Germs.Messages;
using CellGame.Helper;
using CellGame.Tissue;

namespace CellGame.Germs
{
    internal class LyticVirus : IGerm, ICellVisitor
    {
        private readonly Random _random = new Random();
        
        private const bool IsLytic = true;
        private const bool IsBudding = false;
        private const int GenToReplicateIn = 2;
        private const int ReplicationMultiplier = 100;
        private const double InfectionFailureRate = 0.9;

        private readonly int _generation;
        private readonly bool _isMature;

        private bool _canInfectCell = false;
        private (ushort selfSignal, ushort alertSignal) _originCellSignals;
        private readonly EventAggregator _eventAggregator;

        public LyticVirus(EventAggregator eventAggregator)
        {
            _generation = 0;
            _isMature = false;
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        private LyticVirus(int currentGeneration, EventAggregator eventAggregator)
        {
            _generation = currentGeneration;
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _isMature = _generation >= GenToReplicateIn;
            if (_isMature) 
                PublishGermGowthAsync();
        }

        private async void PublishGermGowthAsync()
        {
            await _eventAggregator.Publish(
                new GermGrowthMessage(IsLytic, IsBudding, ReplicationMultiplier, this));
        }

        public void VisitCell(bool isAlive, ushort selfSignal, ushort alertSignal)
        {
            _canInfectCell = isAlive;
            _originCellSignals = (selfSignal, alertSignal);
        }

        public ICell InfectCell(ICell cellToInfect)
        {
            cellToInfect.Accept(this);
            var isSuccessfull = _random.NextDouble() >= InfectionFailureRate;
            if (isSuccessfull && cellToInfect is HealthyCell cell && _canInfectCell)
                return new InfectedCell(true,
                    (ushort)Math.Floor(_originCellSignals.selfSignal * 0.7),
                    (ushort)(_originCellSignals.alertSignal +
                             (ushort)Math.Floor((ushort.MaxValue - _originCellSignals.alertSignal) * 0.2)),
                    NewInfection());

            return cellToInfect;
        }

        public void Accept(IGermVistor visitor)
        {
            visitor?.Visit(_isMature, IsLytic, IsBudding);
        }

        public IGerm Replicate()
        {
            return new LyticVirus(_generation + 1, _eventAggregator);
        }

        private IGerm NewInfection()
        {
            return new LyticVirus(_eventAggregator);
        }
    }
}
