namespace GeneticToolkit.Utils.Exceptions
{
    public class PopulationNotInitializedException : GeneticException
    {
        public PopulationNotInitializedException() : base("Population has not been initialized!")
        {
            
        }
    }
}