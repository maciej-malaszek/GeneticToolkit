using System;
using System.Collections.Generic;
using System.Text;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Stop
{
    public class ChangePlateau<TFitness> : IStopCondition<TFitness> where TFitness:IComparable
    {

        public bool Satisfied(IPopulation<TFitness> population)
        {
            throw new NotImplementedException();
        }
    }
}
