using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Policies.Stop
{
    public class DateTimeCondition : IStopCondition
    {
        public bool UseUtc { get; set; }
        public DateTime StopDateTime { get; set; }

        public bool Satisfied(IEvolutionaryPopulation population) =>
            StopDateTime >= (UseUtc ? DateTime.UtcNow : DateTime.Now);
        public DateTimeCondition(DateTime stopDateTime, bool useUtc = false)
        {
            StopDateTime = stopDateTime;
            UseUtc = useUtc;
        }
    }
}
