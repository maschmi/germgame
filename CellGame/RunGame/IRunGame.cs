using CellGame.Tissue;

namespace CellGame.RunGame
{
    public interface IRunGame
    {
        Tissue2D Advance(Tissue2D input);
    }
}
