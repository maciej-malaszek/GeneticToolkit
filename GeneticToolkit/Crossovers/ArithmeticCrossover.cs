using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Extensions;

using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace GeneticToolkit.Crossovers
{
    public class ArithmeticCrossover : ICrossover
    {
        public enum EMode
        {
            Single,
            Double,
            Short,
            Integer,
            Long
        };
        public int ParentsCount { get; }
        public int ChildrenCount => 1;
        public int BitAlign { get; set; } = -1;
        public EMode Mode { get; set; }

        public ArithmeticCrossover(EMode mode)
        {
            Mode = mode;
        }
        public IGenotype[] Cross(IGenotype[] parents)
        {
            IGenotype child = parents[0].ShallowCopy();

            switch (Mode)
            {
                case EMode.Single:
                    child.Genes = GetNumbers<float>(parents)
                        .Select(x => x.Average(y => y))
                        .SelectMany(BitConverter.GetBytes)
                        .ToArray();
                    break;
                case EMode.Double:
                    child.Genes = GetNumbers<double>(parents)
                        .Select(x => x.Average(y => y))
                        .SelectMany(BitConverter.GetBytes)
                        .ToArray();
                    break;
                case EMode.Integer:
                    child.Genes = GetNumbers<int>(parents)
                        .Select(x => x.Average(y => y))
                        .SelectMany(BitConverter.GetBytes)
                        .ToArray();
                    break;
                case EMode.Short:
                    child.Genes = GetNumbers<short>(parents)
                        .Select(x => x.Average(y => y))
                        .SelectMany(BitConverter.GetBytes)
                        .ToArray();
                    break;
                case EMode.Long:
                    child.Genes = GetNumbers<long>(parents)
                        .Select(x => x.Average(y => y))
                        .SelectMany(BitConverter.GetBytes)
                        .ToArray();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Mode), Mode, null);
            }

            return new[] {child};
        }

        private T[][] GetNumbers<T>(IGenotype[] parents)
        {
            int sizeOfType = Marshal.SizeOf<T>();

            if (parents[0].Length % sizeOfType != 0)
                return null;

            T[][] result = new T[parents[0].Length / sizeOfType][];
            
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new T[parents.Length];
                for (int j = 0; j < result[i].Length; j++)
                    result[i][j] = BitConverterX.ToValue<T>(parents[j].Genes, i * sizeOfType);
            }

            return result;
        }

    }
}
