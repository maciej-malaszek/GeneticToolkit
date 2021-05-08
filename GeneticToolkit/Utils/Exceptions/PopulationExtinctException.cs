using System;
using System.Runtime.Serialization;

namespace GeneticToolkit.Utils.Exceptions
{
    [Serializable]
    public class PopulationExtinctException : GeneticException
    {
        public PopulationExtinctException() : base("Population is extinct! There are no individuals left."){}
        protected PopulationExtinctException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
            serializationInfo, streamingContext)
        {
        }
    }
}