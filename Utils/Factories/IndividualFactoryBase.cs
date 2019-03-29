using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Utils.Factories
{
    public abstract class IndividualFactoryBase
    {
        public abstract IIndividual CreateFromGenotype(IGenotype genotype, IPhenotype phenotype);
        public abstract IIndividual CreateRandomIndividual();
        public virtual IIndividual[] CreateRandomPopulation(int size)
        {
            IIndividual[] population = new IIndividual[size];
            for(int i = 0; i < size; i++)
                population[i] = CreateRandomIndividual();
            return population;
        }

    }
}
