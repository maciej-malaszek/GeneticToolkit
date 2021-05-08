using System;
using System.Runtime.Serialization;

namespace GeneticToolkit.Utils.Exceptions
{
    [Serializable]
    public class CrossoverInvalidParamException : GeneticException
    {
        public CrossoverInvalidParamException(string paramName) : base(
            $"Crossover parameter {paramName} had incorrect value"){}

        protected CrossoverInvalidParamException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(
            serializationInfo, streamingContext)
        {
        }
    }
}