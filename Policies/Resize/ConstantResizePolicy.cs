using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Resize
{
    public class ConstantResizePolicy<TFitness> : IPopulationResizePolicy<TFitness> where TFitness:IComparable
    {
        public int NextGenSize(IPopulation<TFitness> population)
        {
            return population.Size;
        }
    }
}
