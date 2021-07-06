using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Extensions;

using System;
using System.Linq;
using System.Runtime.InteropServices;
using GeneticToolkit.Utils.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GeneticToolkit.Crossovers
{
    public class ArithmeticCrossover : ICrossover
    {
        [JsonConverter(typeof(StringEnumConverter))]
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
        
        public ArithmeticCrossover() {}

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
                        break;
                    case EMode.Double:
                        double[] doubles = GetNumbersSingle<double>(parents, offset);
                        bytes = BitConverter.GetBytes(doubles.Average());
                        bytes.CopyTo(child.Genes, offset);
                        offset += sizeof(double);
                        break;
                    case EMode.Integer:
                        uint[] ints = GetNumbersSingle<uint>(parents, offset);
                        bytes = BitConverter.GetBytes((uint) (ints.Select(x => (double) x).Average()));
                        bytes.CopyTo(child.Genes, offset);
                        offset += sizeof(uint);
                        break;
                    case EMode.Short:
                        ushort[] shorts = GetNumbersSingle<ushort>(parents, offset);
                        bytes = BitConverter.GetBytes((ushort) (shorts.Select(x => ((double) x)).Average()));
                        bytes.CopyTo(child.Genes, offset);
                        offset += sizeof(ushort);
                        break;
                    case EMode.Long:
                        ulong[] longs = GetNumbersSingle<ulong>(parents, offset);
                        bytes = BitConverter.GetBytes((ulong) (longs.Select(x => ((double) x)).Average()));
                        bytes.CopyTo(child.Genes, offset);
                        offset += sizeof(ulong);
                        break;
                    default:
                        throw new CrossoverInvalidParamException(nameof(Mode));
                }
            }
            return new[] { child };
        }


        private static T[] GetNumbersSingle<T>(IGenotype[] parents, int offset)
        {
            var result = new T[parents.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = BitConverterX.ToValue<T>(parents[i].Genes, offset);
            }

            return result;
        }

    }
}
