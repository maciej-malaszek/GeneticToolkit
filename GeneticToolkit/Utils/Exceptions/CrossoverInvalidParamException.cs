namespace GeneticToolkit.Utils.Exceptions
{
    public class CrossoverInvalidParamException : GeneticException
    {
        public CrossoverInvalidParamException(string paramName) : base(
            $"Crossover parameter {paramName} had incorrect value"){}
        
    }
}