using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Stop
{
    public class PopulationDegradation<TFitness> : IStopCondition<TFitness> where TFitness : IComparable
    {
        public bool Satisfied(IPopulation<TFitness> population)
        {
            throw new NotImplementedException();
        }
    }
}
