using System;

namespace GeneticToolkit.Utils.Exceptions
{
    [Serializable]
    public class PopulationTooSmallException : GeneticException
    {
        public PopulationTooSmallException(int size, int minimalSize) : base(
            $"Population is too small. It has {size} individuals when at least {minimalSize} expected")
        {
        }
    }
}