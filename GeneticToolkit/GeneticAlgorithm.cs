﻿using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Events;
using GeneticToolkit.Utils.Extensions;

using System;
using JetBrains.Annotations;

namespace GeneticToolkit
{
    public enum EStopConditionMode { Any, All }

    [PublicAPI]
    public class GeneticAlgorithm
    {
        public IEvolutionaryPopulation Population { get; set; }

        public event EventHandler<NewGenerationEventArgs> CreatedNextGeneration;

        public IStopCondition[] StopConditions { get; set; }

        public EStopConditionMode StopConditionMode { get; set; } = EStopConditionMode.Any;

        public void Run()
        {
            Population.Initialize();
            switch (StopConditionMode)
            {
                case EStopConditionMode.Any:
                    while (StopConditions.Any(x => x.Satisfied(Population)) == false)
                    {
                        Population.NextGeneration();
                        CreatedNextGeneration?.Invoke(this, new NewGenerationEventArgs(Population, Population.Generation) );
                    }

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