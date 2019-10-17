using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using CellGame.Germs;
using CellGame.Helper;
using CellGame.Tissue;

namespace CellGame.RunGame
{
    internal sealed class RoundBasedGame : IRunGame, IListener<GermGrowthMessage>
    {
        private readonly IGrowTissue _growthMechanism;
        private readonly IPropagateInfection _infectionPropagation;
        

        public RoundBasedGame(IGrowTissue growthMechanism, IPropagateInfection infectionPropagation, IEventAggregator eventAggregator)
        {
            _growthMechanism = growthMechanism ?? throw new ArgumentException(nameof(growthMechanism)); 
            _infectionPropagation = infectionPropagation ?? throw new ArgumentException(nameof(infectionPropagation));
            eventAggregator.Subscribe(this);
        }
        
        public Tissue2D Advance(Tissue2D input)
        {
            var result = ImmutableDictionary<Location, ICell>.Empty;
            foreach ((Location location, ICell cell) in input.Tissue)
            {
                result = cell switch
                {
                    NullCell _ => result.Add(location, GrowTissue(input,location)),
                    InfectedCell infectedCell => result.Add(location, PropagateInfection(location, infectedCell)),
                    _ => result.Add(location, cell.Clone())
                };
            }
            
            return new Tissue2D(result);
        }

        private ICell GrowTissue(Tissue2D input, Location location)
            => _growthMechanism.GrowTissue(input, location);

        private ICell PropagateInfection(Location location, InfectedCell infectedCell)
            => _infectionPropagation.PropagateInfection(infectedCell);

        public async Task ProcessMessageAsync(GermGrowthMessage message)
        {
            Console.WriteLine($"{message.Germ.GetType().Name} has produced {message.ReplicationMultiplier.ToString()} young ones");
            await Task.CompletedTask;
        }
    }
}
