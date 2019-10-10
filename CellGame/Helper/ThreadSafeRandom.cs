using System;
using System.Threading;

namespace CellGame.ListShuffle
{
    internal static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random _rnd;

        public static Random GetRandomGenerator
            => _rnd ??= new Random(unchecked(Environment.TickCount * 21 * Thread.CurrentThread.ManagedThreadId));
    }
}
