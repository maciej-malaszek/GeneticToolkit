using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Extensions;

namespace GeneticToolkit.Utils.Statistics
{
    public class HeavenChangeHistory : ChangeHistory, IStatisticUtility
    {
        public override void UpdateData(IEvolutionaryPopulation population)
        {
            IFitnessFunction f = population.FitnessFunction;
            IIndividual individual = population.HeavenPolicy.Memory.Last();
            if (Index == History.Length)
            {
                History = History.Resize();
                ValueHistory = ValueHistory.Resize();
            }

            ValueHistory[Index] = f.GetValue(individual);
            History[Index] = Length+1 == 0
                ? ValueHistory[Index]
                : ValueHistory[Index] - ValueHistory[Index-1];
            Index++;
        }
    }
}
