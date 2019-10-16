using System;
using System.Collections.Immutable;
using CellGame.Tissue;

namespace CellGame.RunGame
{
    internal sealed class RoundBasedGame : IRunGame
    {
        private readonly IGrowTissue _growthMechanism;
        private readonly IPropagateInfection _infectionPropagation;

        public RoundBasedGame(IGrowTissue growthMechanism, IPropagateInfection infectionPropagation)
        {
            _growthMechanism = growthMechanism ?? throw new ArgumentException(nameof(growthMechanism)); 
            _infectionPropagation = infectionPropagation ?? throw new ArgumentException(nameof(infectionPropagation));
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

    }
}
