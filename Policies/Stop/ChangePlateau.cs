using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Statistics;

namespace GeneticToolkit.Policies.Stop
{
    public class ChangePlateau : IStopCondition
    {
        public double MinimalImprovement { get; set; }
        public ChangeHistory ChangeHistory { get; set; }
        public uint Generations { get; set; }

        public bool Satisfied(IEvolutionaryPopulation population)
        {
            if(population.Generation <= Generations + 1)
                return false;
            double improvement = ChangeHistory.GetAverageImprovement((int)(population.Generation - (int)Generations), Generations, population.CompareCriteria.OptimizationMode);

            return improvement <= MinimalImprovement;

        }

        public void Reset()
        {
            ChangeHistory.Reset();
        }

        public ChangePlateau(ChangeHistory changeHistory, double minimalImprovement, uint generations)
        {
            MinimalImprovement = minimalImprovement;
            ChangeHistory = changeHistory;
            Generations = generations;

        }
    }
}
