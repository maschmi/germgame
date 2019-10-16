using CellGame.Tissue;

namespace CellGame.RunGame
{
    public interface IGrowTissue
    {
        ICell GrowTissue(Tissue2D input, Location location);
    }
}
