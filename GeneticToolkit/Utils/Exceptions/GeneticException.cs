using System;

namespace GeneticToolkit.Utils.Exceptions
{
    public abstract class GeneticException : Exception
    {
        protected GeneticException(string message) : base(message)
        {
            
        }
    }
}