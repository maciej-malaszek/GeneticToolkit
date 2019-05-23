using System.Collections.Generic;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Data;

namespace GeneticToolkit.Utils
{

    public class Range<T> : IGeneticSerializable
    {
        public T Low { get; set; }
        public T High { get; set; }

        public Range(IDictionary<string, object> parameters)
        {
            Low = (T) parameters["Low"];
            High = (T) parameters["High"];
        }

        public Range(T low, T high)
        {
            Low = low;
            High = high;
        }

        public GeneticAlgorithmParameter Serialize()
        {
            return new GeneticAlgorithmParameter(this)
            {
                Params = new Dictionary<string, object>()
                {
                    {"Low", Low},
                    {"High", High}
                }
            };
        }
    }
}
