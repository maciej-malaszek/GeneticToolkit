﻿using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Policies.Resize
{
    public class ConstantResizePolicy : IPopulationResizePolicy
    {
        public int NextGenSize(IPopulation population)
        {
            return population.Size;
        }
    }
}
