using System.Collections.Generic;
using System.Linq;

namespace CellGame.Helper.Shuffle
{
    public class ReverseTestShuffler : IShuffle
    {
        public IEnumerable<T> Shuffle<T>(in IEnumerable<T> input)
        {
            var result = input.ToList();
            result.Reverse();
            return result;
        }
    }
}
