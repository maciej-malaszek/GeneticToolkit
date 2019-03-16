using System;

namespace GeneticToolkit.Interfaces
{
    public interface IStatisticUtility<TFitness> where TFitness : IComparable
    {
        void UpdateData(IPopulation<TFitness> population);
    }
}
