using System.Collections.Immutable;

namespace CellGame.Tissue
{
    internal sealed class Tissue2D
    {
        private readonly ImmutableDictionary<Location, Cell> _tissue;

        public Tissue2D(ImmutableDictionary<Location, Cell> tissue)
        {
            _tissue = tissue ?? ImmutableDictionary<Location, Cell>.Empty;
        }

        public void Accept(ITissueVisitor visitor)
        {
            visitor.Visit(_tissue);
        }
    }
}
