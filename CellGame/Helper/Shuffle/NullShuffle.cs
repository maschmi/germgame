using System.Collections.Generic;
using System.Linq;

namespace CellGame.Helper.Shuffle
{
    public class NullShuffle : IShuffle
    {
        public IEnumerable<T> Shuffle<T>(in IEnumerable<T> input)
        {
            return input.Select(p => p);
        }
    }
}
