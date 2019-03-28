using GeneticToolkit.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GeneticToolkit
{
    public enum EStopConditionMode { Any, All }

    public class GeneticAlgorithm
    {
        public IPopulation Population { get; set; }

        public ICollection<IStopCondition> StopConditions { get; set; }

        public EStopConditionMode StopConditionMode { get; set; } = EStopConditionMode.Any;

        public void Run()
        {
            switch (StopConditionMode)
            {
                case EStopConditionMode.Any:
                    while (StopConditions.Any(x => x.Satisfied(Population)) == false)
                        Population.NextGeneration();
                    break;
                case EStopConditionMode.All:
                    while (StopConditions.All(x => x.Satisfied(Population)) == false)
                        Population.NextGeneration();
                    break;
                default: return;
            }
        }

        public void Reset()
        {
            Population.Initialize();
            foreach (var stopCondition in StopConditions)
                stopCondition.Reset();
        }



    }
}
