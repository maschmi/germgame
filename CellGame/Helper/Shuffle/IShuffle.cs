using System.Collections.Generic;

namespace CellGame.Helper.Shuffle
{
    public interface IShuffle
    {
        IEnumerable<T> Shuffle<T>(in IEnumerable<T> input);
    }
}
