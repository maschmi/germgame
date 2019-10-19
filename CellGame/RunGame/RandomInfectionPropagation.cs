using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using CellGame.Germs;
using CellGame.Germs.Messages;
using CellGame.Helper;
using CellGame.Helper.EvenAggregator;
using CellGame.RunGame.Messages;
using CellGame.Tissue;

namespace CellGame.RunGame
{
    internal sealed class RandomInfectionPropagation : IPropagateInfection, IListenOn<GermGrowthMessage>
    {
        private Random _random = new Random();
        private readonly ConcurrentBag<IGerm> _germReservoir = new ConcurrentBag<IGerm>();
        private readonly IGermFactory _germFactory;
        private EventAggregator _eventAggregator;

        private const double ChanceToEncounterACell = 0.9;
        
        public RandomInfectionPropagation(IGermFactory germFactory, EventAggregator eventAggregator)
        {
            _germFactory = germFactory;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }
        
        public ICell PropagateInfection(ICell cell)
        {
            if (_germReservoir.TryTake(out IGerm germ))
                return InfectCell(cell, germ);
            
            return cell.Clone();
        }

        private ICell InfectCell(ICell cell, IGerm germ)
        {
            if (_random.NextDouble() <= ChanceToEncounterACell)
            {
                PublishCellInfection();
                return germ.InfectCell(cell);
            }

            return cell.Clone();
        }

        private async void PublishCellInfection()
        {
            await _eventAggregator.Publish(new CellInfectionMessage(this));
        }

        public async Task ProcessMessageAsync(GermGrowthMessage message)
        {
            for (int i = 0; i < message.ReplicationMultiplier; i++)
                _germReservoir.Add(_germFactory.CreateDefaultGerm());

            await Task.CompletedTask;
        }
    }
}
