namespace GeneticToolkit.Utils.Exceptions
{
    public class PopulationExtinctException : GeneticException
    {
        public PopulationExtinctException() : base("Population is extinct! There are no individuals left."){}
    }
}