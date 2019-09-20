using System.Reflection.Emit;

namespace GeneticToolkit.Utils.Exceptions
{
    public class FitnessFunctionNotInitialized : GeneticException
    {
        public FitnessFunctionNotInitialized() : base("Fitness function not initialized!") {}
    }
}