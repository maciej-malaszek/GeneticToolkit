using System;
using System.Collections.Generic;
using System.Text;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Incompatibility
{
    /// <summary>
    /// Special type of policy that does nothing. It should be used when there is no limit or genotype is immune to incompatibilities.
    /// </summary>
    /// <typeparam name="TFitness"></typeparam>
    public class AllowAll<TFitness> : IIncompatibilityPolicy<TFitness> where TFitness:IComparable
    {
        public Func<IPopulation<TFitness>, IIndividual<TFitness>, bool> IsCompatible { get; set; }
        public IIndividual<TFitness> GetReplacement(IPopulation<TFitness> population, IIndividual<TFitness> incompatibleIndividual, IIndividual<TFitness>[] parents)
        {
            return incompatibleIndividual;
        }

        public AllowAll()
        {
            IsCompatible = (population, individual) => true;
        }
    }
}
