using System;

namespace GeneticToolkit.Utils.Extensions
{
    public static class RandomExtensions
    {
        public static int Next(this Random random, int maxValue, int[] excluding)
        {
            int r;
            do
            {
                r = random.Next(maxValue);
            } while (excluding.Contains(r));
            return r;
        }
    }
}
