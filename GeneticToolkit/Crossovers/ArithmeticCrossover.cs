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
            Byte,
            Single,
            Double,
            Short,
            Integer,
            Long
        };

        public int ParentsCount { get; set; } = 2;
        public int ChildrenCount => 1;
        public int BitAlign { get; set; } = -1;
        public EMode[] Mode { get; set; }

        public ArithmeticCrossover(EMode[] mode)
        {
            Mode = mode;
        }
        public IGenotype[] Cross(IGenotype[] parents)
        {
            IGenotype child = parents[0].EmptyCopy();

            int offset = 0;
            foreach (EMode mode in Mode)
            {
                byte[] bytes;
                switch (mode)
                {
                    case EMode.Byte:
                        byte b = (byte) parents.Select(x => (int)x.Genes[offset]).Average();
                        child.Genes[offset] = b;
                        offset += sizeof(byte);
                        break;
                    case EMode.Single:
                        float[] floats = GetNumbersSingle<float>(parents, offset);
                        bytes = BitConverter.GetBytes(floats.Average());
                        bytes.CopyTo(child.Genes, offset);
                        offset += sizeof(float);

                        //child.Genes = GetNumbers<float>(parents)
                        //    .Select(x => x.Average(y => y))
                        //    .SelectMany(BitConverter.GetBytes)
                        //    .ToArray();
                        break;
                    case EMode.Double:
                        double[] doubles = GetNumbersSingle<double>(parents, offset);
                        bytes = BitConverter.GetBytes(doubles.Average());
                        bytes.CopyTo(child.Genes, offset);
                        offset += sizeof(double);
                        //child.Genes = GetNumbers<double>(parents)
                        //    .Select(x => x.Average(y => y))
                        //    .SelectMany(BitConverter.GetBytes)
                        //    .ToArray();
                        break;
                    case EMode.Integer:
                        uint[] ints = GetNumbersSingle<uint>(parents, offset);
                        bytes = BitConverter.GetBytes((uint) (ints.Select(x => (double) x).Average()));
                        bytes.CopyTo(child.Genes, offset);
                        offset += sizeof(uint);

                        //child.Genes = GetNumbers<uint>(parents)
                        //    .Select(x => (uint)(x.Average(y => y)))
                        //    .SelectMany(BitConverter.GetBytes)
                        //    .ToArray();
                        break;
                    case EMode.Short:
                        ushort[] shorts = GetNumbersSingle<ushort>(parents, offset);
                        bytes = BitConverter.GetBytes((ushort) (shorts.Select(x => ((double) x)).Average()));
                        bytes.CopyTo(child.Genes, offset);
                        offset += sizeof(ushort);

                        //child.Genes = GetNumbers<ushort>(parents)
                        //    .Select(x => (ushort)(x.Average(y => y)))
                        //    .SelectMany(BitConverter.GetBytes)
                        //    .ToArray();
                        break;
                    case EMode.Long:
                        ulong[] longs = GetNumbersSingle<ulong>(parents, offset);
                        bytes = BitConverter.GetBytes((ulong) (longs.Select(x => ((double) x)).Average()));
                        bytes.CopyTo(child.Genes, offset);
                        offset += sizeof(ulong);

                        //child.Genes = GetNumbers<long>(parents)
                        //    .Select(x => (long)(x.Average(y => y)))
                        //    .SelectMany(BitConverter.GetBytes)
                        //    .ToArray();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(Mode), Mode, null);
                }
            }
            return new[] { child };
        }


        private T[] GetNumbersSingle<T>(IGenotype[] parents, int offset)
        {
            int sizeOfType = Marshal.SizeOf<T>();

            T[] result = new T[parents.Length];

            for (int i = 0; i < result.Length; i++)
                result[i] = BitConverterX.ToValue<T>(parents[i].Genes, offset);

            return result;
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
