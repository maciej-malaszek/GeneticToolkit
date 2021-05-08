using System;

namespace GeneticToolkit.Utils.Exceptions
{
    [Serializable]
    public class CrossoverInvalidParamException : GeneticException
    {
        public CrossoverInvalidParamException(string paramName) : base(
            $"Crossover parameter {paramName} had incorrect value"){}
        
    }
}