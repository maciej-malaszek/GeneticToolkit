using System;
using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils;
using GeneticToolkit.Utils.Exceptions;
using GeneticToolkit.Utils.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GeneticToolkit.Mutations
{
    public class ArithmeticMutation : IMutation
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
        public Range<float>[] MutationRanges { get; set; }
        public EMode[] Modes { get; set; }
        private readonly Random _random = new();

        public ArithmeticMutation() {}
        public ArithmeticMutation(Range<float>[] mutationRanges, EMode[] modes)
        {
            MutationRanges = mutationRanges;
            Modes = modes;
        }

        public void Mutate(IGenotype genotype, IMutationPolicy mutationPolicy, IPopulation population)
        {
            if (_random.NextDouble() > mutationPolicy.GetMutationChance(population))
            {
                return;
            }

            var offset = 0;
            for (var i = 0; i < Modes.Length; i++)
            {
                var mode = Modes[i];
                byte[] bytes;
                switch (mode)
                {
                    case EMode.Byte:
                        genotype.Genes[offset] =
                            (byte) (genotype.Genes[offset] + (sbyte) (_random.NextDouble() * (MutationRanges[i].High - MutationRanges[i].Low) +
                                                                      MutationRanges[0].Low));
                        offset += sizeof(byte);
                        break;
                    case EMode.Single:
                        var floats = BitConverterX.ToValue<float>(genotype.Genes, offset);
                        floats += (float) (_random.NextDouble() * (MutationRanges[i].High - MutationRanges[i].Low) +
                                           MutationRanges[0].Low);
                        bytes = BitConverter.GetBytes(floats);
                        bytes.CopyTo(genotype.Genes, offset);
                        offset += sizeof(float);
                        break;
                    case EMode.Double:
                        var doubles = BitConverterX.ToValue<double>(genotype.Genes, offset);
                        doubles += _random.NextDouble() * (MutationRanges[i].High - MutationRanges[i].Low) +
                                   MutationRanges[0].Low;
                        bytes = BitConverter.GetBytes(doubles);
                        bytes.CopyTo(genotype.Genes, offset);
                        offset += sizeof(double);
                        break;
                    case EMode.Integer:
                        var integers = BitConverterX.ToValue<int>(genotype.Genes, offset);
                        integers += (int) (_random.NextDouble() * (MutationRanges[i].High - MutationRanges[i].Low) +
                                           MutationRanges[0].Low);
                        bytes = BitConverter.GetBytes(integers);
                        bytes.CopyTo(genotype.Genes, offset);
                        offset += sizeof(int);
                        break;
                    case EMode.Short:
                        var shorts = BitConverterX.ToValue<short>(genotype.Genes, offset);
                        shorts += (short) (_random.NextDouble() * (MutationRanges[i].High - MutationRanges[i].Low) +
                                           MutationRanges[0].Low);
                        bytes = BitConverter.GetBytes(shorts);
                        bytes.CopyTo(genotype.Genes, offset);
                        offset += sizeof(short);
                        break;
                    case EMode.Long:
                        var longs = BitConverterX.ToValue<long>(genotype.Genes, offset);
                        longs += (long) (_random.NextDouble() * (MutationRanges[i].High - MutationRanges[i].Low) +
                                         MutationRanges[0].Low);
                        bytes = BitConverter.GetBytes(longs);
                        bytes.CopyTo(genotype.Genes, offset);
                        offset += sizeof(long);
                        break;
                    default:
                        throw new CrossoverInvalidParamException(nameof(mode));

                }
            }
        }
    }
}
