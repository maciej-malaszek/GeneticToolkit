using System.Reflection.Emit;

namespace GeneticToolkit.Utils.Exceptions
{
    public class FitnessFunctionNotInitializedException: GeneticException
    {
        public FitnessFunctionNotInitializedException() : base("Fitness function not initialized!") {}
    }
}