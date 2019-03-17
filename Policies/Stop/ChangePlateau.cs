using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Statistics;

namespace GeneticToolkit.Policies.Stop
{
    public class ChangePlateau : IStopCondition
    {
        public double MinimalImprovement { get; set; }
        public ChangeHistory ChangeHistory { get; set; }
        public uint Generations { get; set; }

        public bool Satisfied(IPopulation population)
        {
            if(population.Generation <= Generations + 1)
                return false;
            double newerValue = ChangeHistory.GetAverageImprovement((int)(population.Generation - (int)Generations), Generations);
            double olderValue = ChangeHistory.GetAverageImprovement((int)(population.Generation - (int)Generations) - 1, Generations);

            return newerValue - olderValue >= MinimalImprovement;

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
