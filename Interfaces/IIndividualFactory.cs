using System;

namespace GeneticToolkit.Interfaces
{
    public abstract class IIndividualFactory<TFitness> where TFitness:IComparable
    {
        public abstract IIndividual<TFitness>  CreateRandomIndividual();

        public virtual IIndividual<TFitness> [] CreateRandomPopulation(int size)
        {
            var population = new  IIndividual<TFitness> [size];
            for(int i = 0; i < size; i++)
                population[i] = CreateRandomIndividual();
            return population;
        }

    }
}
