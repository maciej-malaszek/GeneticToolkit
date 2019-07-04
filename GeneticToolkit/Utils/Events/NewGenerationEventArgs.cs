using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils.Events
{
    [PublicAPI]
    public class NewGenerationEventArgs : EventArgs
    {
        public IEvolutionaryPopulation Population { get; set; }
        public uint Generation { get; set; }

        public NewGenerationEventArgs(IEvolutionaryPopulation population, uint generation)
        {
            Population = population;
            Generation = generation;
        }
    }
}