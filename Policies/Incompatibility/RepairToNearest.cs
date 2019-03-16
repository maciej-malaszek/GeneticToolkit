using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Incompatibility
{
    public class RepairToNearest<TFitness> : IIncompatibilityPolicy<TFitness> where TFitness:IComparable
    {
        public Func<IPopulation<TFitness>,  IIndividual<TFitness> , bool> IsCompatible { get; set; }

        public Func<IGenotype, IGenotype> GetNearest { get; set; }

        public RepairToNearest(Func<IGenotype, IGenotype> repairFunction)
        {
            GetNearest = repairFunction;
        }

        public  IIndividual<TFitness>  GetReplacement(IPopulation<TFitness> population,  IIndividual<TFitness>  incompatibleIndividual,  IIndividual<TFitness> [] parents)
        {
            incompatibleIndividual.Genotype = GetNearest(incompatibleIndividual.Genotype);
            return incompatibleIndividual;
        }
    }
}
