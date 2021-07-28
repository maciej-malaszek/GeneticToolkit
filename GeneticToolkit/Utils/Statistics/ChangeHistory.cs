﻿using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Extensions;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Utils.Statistics
{
    [PublicAPI]
    public class ChangeHistory : IStatisticUtility
    {
        protected int Index;
        protected double[] ValueHistory = new double[4];
        public double[] History { get; set; } = new double[4];

        public int Length => Index - 1;

        public double GetValue(int generation)
        {
            return generation <= Length ? History[generation] : 0;
        }

        public virtual void UpdateData(IEvolutionaryPopulation population)
        {
            var f = population.FitnessFunction;
            if (Index == History.Length)
            {
                History = History.Resize();
                ValueHistory = ValueHistory.Resize();
            }

            ValueHistory[Index] = f.GetValue(population.Best);
            History[Index] = Length + 1 == 0
                ? ValueHistory[Index]
                : ValueHistory[Index] - ValueHistory[Index - 1];
            Index++;
        }

        public virtual void Reset()
        {
            History = new double[4];
            ValueHistory = new double[4];
            Index = 0;
        }

        public virtual double GetAverageImprovement(int startIndex, uint generations,
            EOptimizationMode optimizationMode = EOptimizationMode.Maximize)
        {
            if (startIndex + generations > History.Length)
            {
                throw new ArgumentException("History too short to calculate such statistic");
            }

            double sum = 0;
            for (var i = startIndex; i < startIndex + generations; i++)
            {
                sum += History[i] * (optimizationMode == EOptimizationMode.Minimize ? -1 : 1);
            }

            sum /= generations;
            return sum;
        }
    }
}