using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticToolkit.Interfaces
{
    public interface IPopulationResizePolicy<TFitness> where TFitness:IComparable
    {
        int NextGenSize(IPopulation<TFitness> population);
    }
}
