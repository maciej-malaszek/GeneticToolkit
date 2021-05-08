using System;
using System.Runtime.Serialization;

namespace GeneticToolkit.Utils.Exceptions
{
    [Serializable]
    public class PopulationTooSmallException : GeneticException
    {
        public PopulationTooSmallException(int size, int minimalSize) : base(
            $"Population is too small. It has {size} individuals when at least {minimalSize} expected")
        { }
        protected PopulationTooSmallException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
            serializationInfo, streamingContext)
        { }
    }
}