using System.Collections.Generic;
using System.Linq;

namespace CellGame.Helper.Shuffle
{
    public class FisherYatesShuffle : IShuffle
    {
        public IEnumerable<T> Shuffle<T>(in IEnumerable<T> input)
        {
            var result = input.ToArray();
            
            var n = result.Length;
            while (n > 1)
            {
                n--;
                var k = ThreadSafeRandom.GetRandomGenerator.Next(n + 1);
                var value = result[k];
                result[k] = result[n];
                result[n] = value;
            }

            return result;
        }
    }
}
