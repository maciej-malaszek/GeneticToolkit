using System;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Stop
{
    public class TimeSpanCondition<TFitness> : IStopCondition<TFitness> where TFitness:IComparable
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

        public bool Satisfied(IPopulation<TFitness> population)
        {
            if(_notStarted)
                Start();
            return EndTime >= DateTime.Now;
        }
    }
}
