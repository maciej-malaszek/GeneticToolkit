using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils
{
    [PublicAPI]
    public class Range<T>
    {
        public T Low { get; set; }
        public T High { get; set; }

        public Range()
        {
            
        }

        public Range(T low, T high)
        {
            Low = low;
            High = high;
        }
    }
}