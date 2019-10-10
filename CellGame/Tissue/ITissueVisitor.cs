using System.Collections.Immutable;

namespace CellGame.Tissue
{
    internal interface ITissueVisitor
    {
        void Visit(ImmutableDictionary<Location, Cell> tissue);
    }
}
