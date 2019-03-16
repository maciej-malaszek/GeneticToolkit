using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticToolkit.Interfaces
{
    public interface  IIndividual<TFitness>  where TFitness:IComparable
    {
        IGenotype Genotype { get; set; }

        IPhenotype Phenotype { get; }
       
        void Mutate(IMutationPolicy<TFitness> policy);

        int CompareTo( IIndividual<TFitness>  other, ICompareCriteria<TFitness> criteria);

         IIndividual<TFitness>CrossOver(ICrossOverPolicy<TFitness> policy, params  IIndividual<TFitness> [] individuals);
    }
}
