using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using CellGame.Germs.Messages;
using CellGame.Helper;
using CellGame.RunGame.Messages;
using CellGame.Tissue;

namespace CellGame.RunGame
{
    internal sealed class RoundBasedGame : IRunGame, IListenOn<GermGrowthMessage>, IListenOn<CellInfectionMessage>
    {
        private readonly IGrowTissue _growthMechanism;
        private readonly IPropagateInfection _infectionPropagation;


        public RoundBasedGame(IGrowTissue growthMechanism, IPropagateInfection infectionPropagation,
            EventAggregator eventAggregator, bool logMessages = true)
        {
            _growthMechanism = growthMechanism ?? throw new ArgumentException(nameof(growthMechanism));
            _infectionPropagation = infectionPropagation ?? throw new ArgumentException(nameof(infectionPropagation));
 
            SubscribeToLogMessages(eventAggregator, logMessages);
        }

        private void SubscribeToLogMessages(EventAggregator eventAggregator, bool logMessages)
        {
            if (!logMessages) 
                return;
            
            eventAggregator.Subscribe<GermGrowthMessage>(this);
            eventAggregator.Subscribe<CellInfectionMessage>(this);
        }

        public Tissue2D Advance(Tissue2D input)
        {
            var result = ImmutableDictionary<Location, ICell>.Empty;
            foreach ((Location location, ICell cell) in input.Tissue)
                result = cell switch
                {
                    NullCell _ => result.Add(location, GrowTissue(input, location)),
                    InfectedCell currentCell => result.Add(location, PropagateInfection(location, currentCell)),
                    HealthyCell currentCell => result.Add(location, PropagateInfection(location, currentCell)),
                    _ => result.Add(location, cell.Clone())
                };

            return new Tissue2D(result);
        }

        private ICell GrowTissue(Tissue2D input, Location location)
            => _growthMechanism.GrowTissue(input, location);

        private ICell PropagateInfection(Location location, ICell infectedCell)
            => _infectionPropagation.PropagateInfection(infectedCell);

        public async Task ProcessMessageAsync(GermGrowthMessage message)
        {
            Console.WriteLine(
                $"{message.Germ.GetType().Name} has produced {message.ReplicationMultiplier.ToString()} young ones");
            await Task.CompletedTask;
        }

        public async Task ProcessMessageAsync(CellInfectionMessage message)
        {
            Console.WriteLine("Infected new cell!");
            await Task.CompletedTask;
        }
    }
}
