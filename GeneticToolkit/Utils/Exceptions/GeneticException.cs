using System;
using System.Runtime.Serialization;

namespace GeneticToolkit.Utils.Exceptions
{
    [Serializable]
    public abstract class GeneticException : Exception
    {
        protected GeneticException(string message) : base(message)
        {
            
        }

        protected GeneticException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo,
            streamingContext)
        {
            
        }
    }
}