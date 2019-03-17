using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Incompatibility
{
    public class RepairToNearest : IIncompatibilityPolicy
    {
        public Func<IPopulation, IIndividual, bool> IsCompatible { get; set; }

        public Action<IGenotype> RepairFunction { get; set; }


        public IIndividual GetReplacement(IPopulation population, IIndividual incompatibleIndividual, IIndividual[] parents)
        {
            RepairFunction(incompatibleIndividual.Genotype);
            return incompatibleIndividual;
        }
    }
}
