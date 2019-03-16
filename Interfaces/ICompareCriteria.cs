using System;

namespace GeneticToolkit.Interfaces
{
    public interface ICompareCriteria<TFitness> where TFitness:IComparable
    {
        IFitnessFunction<TFitness> FitnessFunction { get; set; }
         IIndividual<TFitness>  GetBetter( IIndividual<TFitness>  x1,  IIndividual<TFitness>  x2);
        int Compare( IIndividual<TFitness>  x1,  IIndividual<TFitness>  x2);
    }
}
