using System;

namespace GeneticToolkit.Interfaces
{
    public interface IIncompatibilityPolicy<TFitness> where TFitness:IComparable
    {
        Func<IPopulation<TFitness>,  IIndividual<TFitness> , bool> IsCompatible { get; set; }

         IIndividual<TFitness>  GetReplacement(IPopulation<TFitness> population,  IIndividual<TFitness>  incompatibleIndividual,  IIndividual<TFitness> [] parents);
    }
}
