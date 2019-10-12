using System.Collections.Generic;

namespace CellGame.ListShuffle
{
    public interface IShuffle
    {
        IEnumerable<T> Shuffle<T>(in IEnumerable<T> input);
    }
}