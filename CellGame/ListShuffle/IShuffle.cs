using System.Collections.Generic;

namespace CellGame.ListShuffle
{
    internal interface IShuffle
    {
        IEnumerable<T> Shuffle<T>(in IEnumerable<T> input);
    }
}