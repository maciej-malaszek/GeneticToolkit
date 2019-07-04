using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Extensions;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils.Statistics
{
    [PublicAPI]
    public class HeavenChangeHistory : ChangeHistory
    {
        public override void UpdateData(IEvolutionaryPopulation population)
        {
            var f = population.FitnessFunction;
            var individual = population.HeavenPolicy.Memory.Last();
            if (Index == History.Length)
            {
                History = History.Resize();
                ValueHistory = ValueHistory.Resize();
            }

            ValueHistory[Index] = f.GetValue(individual);
            History[Index] = Length + 1 == 0
                ? ValueHistory[Index]
                : ValueHistory[Index] - ValueHistory[Index - 1];
            Index++;
        }
    }
}