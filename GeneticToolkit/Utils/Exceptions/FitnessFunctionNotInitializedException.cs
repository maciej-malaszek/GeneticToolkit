using System;

namespace GeneticToolkit.Utils.Exceptions
{
    [Serializable]
    public class FitnessFunctionNotInitializedException: GeneticException
    {
        public FitnessFunctionNotInitializedException() : base("Fitness function not initialized!") {}
    }
}