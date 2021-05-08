using System;

namespace GeneticToolkit.Utils.Exceptions
{
    [Serializable]
    public class PopulationExtinctException : GeneticException
    {
        public PopulationExtinctException() : base("Population is extinct! There are no individuals left."){}
    }
}