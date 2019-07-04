﻿namespace GeneticToolkit.Utils
{
    public class Range<T>
    {
        public T Low { get; set; }
        public T High { get; set; }
        public Range(T low, T high)
        {
            Low = low;
            High = high;
        }
    }
}
