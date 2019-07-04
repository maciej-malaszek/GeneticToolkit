using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Policies.Incompatibility
{
    public class RepairToNearest : IIncompatibilityPolicy
    {
        public Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }

        public Action<IGenotype> RepairFunction { get; set; }


        public IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual, IGenotype[] parents)
        {
            RepairFunction(incompatibleIndividual.Genotype);
            return incompatibleIndividual;
        }
    }
}
