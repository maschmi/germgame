using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CellGame.ListShuffle
{
    internal class FisherYatesShuffle : IShuffle
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

        private static class ThreadSafeRandom
        {
            [ThreadStatic] private static Random _rnd;

            public static Random GetRandomGenerator
                 => _rnd ??= new Random(unchecked(Environment.TickCount * 21 * Thread.CurrentThread.ManagedThreadId));
        }
        
        
    }
}