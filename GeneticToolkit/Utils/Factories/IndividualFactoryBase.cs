using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils.Factories
{
    [PublicAPI]
    public abstract class IndividualFactoryBase
    {
        public abstract IIndividual CreateFromGenotype(IGenotype genotype);
        public abstract IIndividual CreateRandomIndividual();

        public virtual IIndividual[] CreateRandomPopulation(int size)
        {
            var population = new IIndividual[size];
            for (var i = 0; i < size; i++)
                population[i] = CreateRandomIndividual();
            return population;
        }
    }
}