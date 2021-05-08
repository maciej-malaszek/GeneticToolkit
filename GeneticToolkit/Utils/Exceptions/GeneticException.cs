using System;

namespace GeneticToolkit.Utils.Exceptions
{
    [Serializable]
    public abstract class GeneticException : Exception
    {
        protected GeneticException(string message) : base(message)
        {
            
        }
    }
}