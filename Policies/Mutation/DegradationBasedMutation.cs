﻿using GeneticToolkit.Interfaces;


namespace GeneticToolkit.Policies.Mutation
{
    public class DegradationBasedMutation : IMutationPolicy
    {
        private uint _lastGeneration = 1000;
        private float _mutationChance;        
        public float GetMutationChance(IPopulation population)
        {
            if (_lastGeneration != population.Generation)
            {
                _mutationChance = population.Homogeneity * MutationChanceFactor;
                _lastGeneration = population.Generation;
            }

            return _mutationChance;
        }

        public float MutationChanceFactor { get; set; }

        public float MutatedGenesPercent { get; set; }

        public DegradationBasedMutation(float mutationChanceFactor, float mutatedGenesPercent = 0.5f)
        {
            MutationChanceFactor = mutationChanceFactor;
            MutatedGenesPercent = mutatedGenesPercent;
        }
    }
}
