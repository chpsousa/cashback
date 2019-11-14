using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Domain.Util
{
    public static class StaticRandom
    {
        static int seed = Environment.TickCount;
        static readonly System.Threading.ThreadLocal<Random> random = new System.Threading.ThreadLocal<Random>(() => new Random(System.Threading.Interlocked.Increment(ref seed)));

        public static int Next(int min, int max)
        {
            return random.Value.Next(min, max);
        }
        public static int Next(int max)
        {
            return random.Value.Next(max);
        }
        public static int Next()
        {
            return random.Value.Next();
        }
    }
}
