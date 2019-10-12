using System.Collections.Immutable;

namespace CellGame.Tissue
{
    public sealed class Tissue2D
    {
        public ImmutableDictionary<Location, ICell> Tissue { get; }
        
        public Tissue2D(ImmutableDictionary<Location, ICell> tissue)
        {
            Tissue = tissue ?? ImmutableDictionary<Location, ICell>.Empty;
        }
    }
}
