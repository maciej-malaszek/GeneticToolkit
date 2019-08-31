using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using GeneticToolkit.Crossovers;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils;
using GeneticToolkit.Utils.Extensions;

namespace GeneticToolkit.Mutations
{
    public class ArithmeticMutation : IMutation
    {
        public enum EMode
        {
            Single,
            Double,
            Short,
            Integer,
            Long
        };
        public Range<float> MutationRange { get; set; }
        public EMode Mode { get; set; }
        private Random random = new Random();

        public ArithmeticMutation(Range<float> mutationRange, EMode mode)
        {
            MutationRange = mutationRange;
            Mode = mode;
        }

        public void Mutate(IGenotype genotype, IMutationPolicy mutationPolicy, IPopulation population)
        {
            if(random.NextDouble() > mutationPolicy.GetMutationChance(population))
                return;
            switch (Mode)
            {
                case ArithmeticMutation.EMode.Single:
                    genotype.Genes = GetNumbers<float>(genotype)
                        .Select(x => (float)(x+(random.NextDouble()*(MutationRange.High-MutationRange.Low) + MutationRange.Low)))
                        .SelectMany(BitConverter.GetBytes)
                        .ToArray();
                    break;
                case ArithmeticMutation.EMode.Double:
                    genotype.Genes = GetNumbers<double>(genotype)
                        .Select(x => x+(random.NextDouble()*(MutationRange.High-MutationRange.Low) + MutationRange.Low))
                        .SelectMany(BitConverter.GetBytes)
                        .ToArray();
                    break;
                case ArithmeticMutation.EMode.Integer:
                    genotype.Genes = GetNumbers<int>(genotype)
                        .Select(x => (int)(x+(random.NextDouble()*(MutationRange.High-MutationRange.Low) + MutationRange.Low)))
                        .SelectMany(BitConverter.GetBytes)
                        .ToArray();
                    break;
                case ArithmeticMutation.EMode.Short:
                    genotype.Genes = GetNumbers<short>(genotype)
                        .Select(x => (short)(x+(random.NextDouble()*(MutationRange.High-MutationRange.Low) + MutationRange.Low)))
                        .SelectMany(BitConverter.GetBytes)
                        .ToArray();
                    break;
                case ArithmeticMutation.EMode.Long:
                    genotype.Genes = GetNumbers<long>(genotype)
                        .Select(x => (long)(x+(random.NextDouble()*(MutationRange.High-MutationRange.Low) + MutationRange.Low)))
                        .SelectMany(BitConverter.GetBytes)
                        .ToArray();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Mode), Mode, null);
            }
        }

        private T[] GetNumbers<T>(IGenotype genotype)
        {
            int sizeOfType = Marshal.SizeOf<T>();

            if (genotype.Length % sizeOfType != 0)
                return null;

            var result = new T[genotype.Length / sizeOfType];
            
            for (int i = 0; i < result.Length; i++)
                result[i] = BitConverterX.ToValue<T>(genotype.Genes, i * sizeOfType);

            return result;
        }
    }
}
