using System;

namespace GeneticToolkit.Utils.Exceptions
{
    [Serializable]
    public class PopulationNotInitializedException : GeneticException
    {
        public PopulationNotInitializedException() : base("Population has not been initialized!")
        {
            
        }
    }
}