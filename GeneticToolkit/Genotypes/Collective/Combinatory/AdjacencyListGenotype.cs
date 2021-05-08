using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Genotypes.Collective.Combinatory
{
    [PublicAPI]
    public class AdjacencyListGenotype : CombinatoryGenotype
    {
        private readonly Random _random = new Random();

        public AdjacencyListGenotype() : base(0)
        {
        }
        public AdjacencyListGenotype(byte[] bytes) : base(bytes)
        {
        }
        public AdjacencyListGenotype(short[] decoded) : base(decoded.Length)
        {
            Value = Encoded(decoded);
        }
        public AdjacencyListGenotype(int size) : base(size)
        {
            Value = new short[Count];
        }

        public short[] Encoded(short[] decoded)
        {
            var value = new short[Count];
            for (var i = 0; i < Count - 1; i++)
                value[decoded[i]] = decoded[i + 1];
            value[decoded[Count - 1]] = decoded[0];

            return value;
        }

        public void Encode(short[] decoded)
        {
            Value = Encoded(decoded);
        }
        
        public override IGenotype EmptyCopy()
        {
            return new AdjacencyListGenotype(Count);
        }

        public override T EmptyCopy<T>()
        {
            return (T) EmptyCopy();
        }

        public override IGenotype ShallowCopy()
        {
            return new AdjacencyListGenotype(Count)
            {
                Value = (short[]) Value.Clone(),
                Genes = Genes.Clone() as byte[]
            };
        }

        public override TGenotype ShallowCopy<TGenotype>()
        {
            return new AdjacencyListGenotype(Count)
            {
                Value = (short[]) Value.Clone(),
                Genes = Genes.Clone() as byte[]
            } as TGenotype;
        }

        public override short[] GetDecoded()
        {
            int count = Value.Length;
            var decoded = new short[count];
            short index = 0;
            short value = Value[index];

            for (var i = 0; i < count; i++)
            {
                decoded[i] = value;
                index = value;
                value = Value[index];
            }

            return decoded;
        }

        public override void Randomize()
        {
            var value = new short[Count];
            for (var i = 0; i < Count; i++)
                value[i] = (short) i;

            for (var i = 0; i < Count; i++)
            {
                int index0 = _random.Next(0, Count);
                int index1 = _random.Next(0, Count);
                short t = value[index0];
                value[index0] = value[index1];
                value[index1] = t;
            }

            Value = Encoded(value);
            UpdateBits();
        }

        public override IGenotype Randomized()
        {
            Randomize();
            return this;
        }

        public override double SimilarityCheck(IGenotype other)
        {
            if (!(other is AdjacencyListGenotype adjacencyListGenotype))
                return 0;

            double sum = 0;
            for (var i = 0; i < Count; i++)
                sum += Value[i] == adjacencyListGenotype.Value[i] ? 1 : 0;

            return sum / Count;
        }
    }
}