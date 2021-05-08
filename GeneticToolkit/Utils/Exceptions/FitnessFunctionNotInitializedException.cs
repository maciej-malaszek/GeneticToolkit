using System;
using System.Runtime.Serialization;

namespace GeneticToolkit.Utils.Exceptions
{
    [Serializable]
    public class FitnessFunctionNotInitializedException: GeneticException
    {
        public FitnessFunctionNotInitializedException() : base("Fitness function not initialized!") {}
        protected FitnessFunctionNotInitializedException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
            serializationInfo, streamingContext)
        {
        }
    }
}