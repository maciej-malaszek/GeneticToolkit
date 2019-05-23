using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Stop
{
    public class TimeSpanCondition : IStopCondition
    {
        public DateTime StartTime { get; set; }

        public TimeSpan CalculationTime { get; set; }

        private DateTime EndTime { get; set; }

        private bool _notStarted = true;

        public TimeSpanCondition(TimeSpan calculationTime)
        {
            CalculationTime = calculationTime;
        }

        public void Start()
        {
            _notStarted = false;
            StartTime = DateTime.Now;
            EndTime = StartTime + CalculationTime;
        }

        public void Reset()
        {
            _notStarted = true;
        }

        public bool Satisfied(IEvolutionaryPopulation population)
        {
            if(_notStarted)
                Start();
            return  DateTime.Now >= EndTime;
        }
    }
}
