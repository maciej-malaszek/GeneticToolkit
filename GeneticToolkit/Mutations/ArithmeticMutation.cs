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
            Byte,
            Single,
            Double,
            Short,
            Integer,
            Long
        };
        public Range<float>[] MutationRanges { get; set; }
        public EMode[] Modes { get; set; }
        private Random random = new Random();

        public ArithmeticMutation(Range<float>[] mutationRanges, EMode[] modes)
        {
            MutationRanges = mutationRanges;
            Modes = modes;
        }

        public void Mutate(IGenotype genotype, IMutationPolicy mutationPolicy, IPopulation population)
        {
            if (random.NextDouble() > mutationPolicy.GetMutationChance(population))
                return;

            int offset = 0;
            for (var i = 0; i < Modes.Length; i++)
            {
                EMode mode = Modes[i];
                byte[] bytes;
                switch (mode)
                {
                    case EMode.Byte:
                        genotype.Genes[offset] =
                            (byte)(genotype.Genes[offset] + (sbyte) (random.NextDouble() * (MutationRanges[i].High - MutationRanges[i].Low) +
                                                          MutationRanges[0].Low));
                        offset += sizeof(byte);
                        break;
                    case EMode.Single:
                        float floats = BitConverterX.ToValue<float>(genotype.Genes, offset);
                        floats += (float)(random.NextDouble() * (MutationRanges[i].High - MutationRanges[i].Low) +
                                  MutationRanges[0].Low);
                        bytes = BitConverter.GetBytes(floats);
                        bytes.CopyTo(genotype.Genes, offset);
                        offset += sizeof(float);
                        break;
                    case EMode.Double:
                        double doubles = BitConverterX.ToValue<double>(genotype.Genes, offset);
                        doubles += random.NextDouble() * (MutationRanges[i].High - MutationRanges[i].Low) +
                                   MutationRanges[0].Low;
                        bytes = BitConverter.GetBytes(doubles);
                        bytes.CopyTo(genotype.Genes, offset);
                        offset += sizeof(double);
                        break;
                    case EMode.Integer:
                        int ints = BitConverterX.ToValue<int>(genotype.Genes, offset);
                        ints += (int)(random.NextDouble() * (MutationRanges[i].High - MutationRanges[i].Low) +
                                   MutationRanges[0].Low);
                        bytes = BitConverter.GetBytes(ints);
                        bytes.CopyTo(genotype.Genes, offset);
                        offset += sizeof(int);
                        break;
                    case EMode.Short:
                        short shorts = BitConverterX.ToValue<short>(genotype.Genes, offset);
                        shorts += (short)(random.NextDouble() * (MutationRanges[i].High - MutationRanges[i].Low) +
                                   MutationRanges[0].Low);
                        bytes = BitConverter.GetBytes(shorts);
                        bytes.CopyTo(genotype.Genes, offset);
                        offset += sizeof(short);
                        break;
                    case EMode.Long:
                        long longs = BitConverterX.ToValue<long>(genotype.Genes, offset);
                        longs += (long)(random.NextDouble() * (MutationRanges[i].High - MutationRanges[i].Low) +
                                   MutationRanges[0].Low);
                        bytes = BitConverter.GetBytes(longs);
                        bytes.CopyTo(genotype.Genes, offset);
                        offset += sizeof(long);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode), mode, null);

                }
            }
        }

      
    }
}
