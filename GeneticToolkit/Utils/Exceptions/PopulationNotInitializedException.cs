using System;
using System.Runtime.Serialization;

namespace GeneticToolkit.Utils.Exceptions
{
    [Serializable]
    public class PopulationNotInitializedException : GeneticException
    {
        public PopulationNotInitializedException() : base("Population has not been initialized!")
        { }
        protected PopulationNotInitializedException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
            serializationInfo, streamingContext)
        { }
    }
}