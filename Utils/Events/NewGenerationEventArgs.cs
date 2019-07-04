using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Utils.Events
{
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
