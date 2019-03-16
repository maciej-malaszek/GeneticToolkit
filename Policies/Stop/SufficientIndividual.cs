using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Stop
{
    public class SufficientIndividual<TFitness> : IStopCondition<TFitness> where TFitness : IComparable
    {
        public IFitnessFunction<TFitness> FitnessFunction { get; set; }

        public TFitness SufficientResult { get; set; }

        public bool Satisfied(IPopulation<TFitness> population)
        {
            if(population == null)
                throw new NullReferenceException("Population has not been initialized!");
            if(FitnessFunction == null)
                throw new NullReferenceException("Fitness function has not been initialized!");
            return FitnessFunction.GetValue(population.GetBest()).CompareTo(SufficientResult) <= 0;
        }

        public SufficientIndividual(IFitnessFunction<TFitness> fitnessFunction, TFitness sufficientResult)
        {
            FitnessFunction = fitnessFunction;
            SufficientResult = sufficientResult;
        }
    }
}
