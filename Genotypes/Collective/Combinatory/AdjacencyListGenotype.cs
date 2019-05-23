using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Genotypes.Collective.Combinatory
{
    public class AdjacencyListGenotype : CombinatoryGenotype
    {
        private readonly Random _random = new Random();
        public AdjacencyListGenotype() : base(0)
        { }

        public AdjacencyListGenotype(short[] decoded) : base(decoded.Length)
        {
            Value = Encoded(decoded);
        }

        public short[] Encoded(short[] decoded)
        {
            var value = new short[Count];
            for (int i = 0; i < Count - 1; i++)
                value[decoded[i]] = decoded[i + 1];
            value[decoded[Count - 1]] = decoded[0];

            return value;
        }

        public void Encode(short[] decoded)
        {
            Value = Encoded(decoded);
        }

        public AdjacencyListGenotype(int size) : base(size) { Value = new short[Count]; }

        public AdjacencyListGenotype(byte[] bytes) : base(bytes) { }

        public override IGenotype EmptyCopy()
        {
            return new AdjacencyListGenotype(Count);
        }

        public override T EmptyCopy<T>()
        {
            return (T)EmptyCopy();
        }

        public override IGenotype ShallowCopy()
        {
            return new AdjacencyListGenotype(Count)
            {
                Value = (short[])Value.Clone(),
                Genes = Genes.Clone() as byte[]
            };
        }

        public override TGenotype ShallowCopy<TGenotype>()
        {
            return new AdjacencyListGenotype(Count)
            {
                Value = (short[])Value.Clone(),
                Genes = Genes.Clone() as byte[]
            } as TGenotype;
        }

        public override short[] GetDecoded()
        {
            int count = Value.Length;
            var decoded = new short[count];
            //var reverseIndex = new int[count];
            //reverseIndex[0] = 0;
            //int notIndexed = count - 1;

            //for (int i = 1; i < count; i++)
            //    reverseIndex[i] = -1;

            //do
            //{
            //    for (int i = 0; i < count; i++)
            //    {
            //        var startingCity = i;
            //        var endingCity = Value[i];

            //        // Do I Know where startingCity is?
            //        if (reverseIndex[startingCity] >= 0)
            //        {
            //            var endingIndex = reverseIndex[startingCity] + 1;
            //            if (endingIndex >= count)
            //                endingIndex = 0;

            //            if (reverseIndex[endingCity] == -1)
            //                notIndexed--;
            //            reverseIndex[endingCity] = endingIndex;

            //        }

            //    }
            //} while (notIndexed > 0);

            //for (int i = 0; i < count; i++)
            //{
            //    decoded[reverseIndex[i]] = (short)i;
            //}
            
            short index = 0;
            short value = Value[index];

            for (int i = 0; i < count; i++)
            {
                decoded[i] = value;
                index = value;
                value = Value[index];
            }
            return decoded;
        }

        private void GetDecodedX()
        {
            int count = Value.Length;
            short[] decoded = new short[count];
            short index = 0;
            short value = Value[index];

            for (int i = 0; i < count; i++)
            {
                decoded[i] = value;
                index = value;
                value = Value[index];
            }
        }

        public override void Randomize()
        {
            short[] value = new short[Count];
            for (int i = 0; i < Count; i++)
                value[i] = (short)i;

            for (int i = 0; i < Count; i++)
            {
                var index0 = _random.Next(0, Count);
                var index1 = _random.Next(0, Count);
                var t = value[index0];
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
            AdjacencyListGenotype adjacencyListGenotype = other as AdjacencyListGenotype;
            if (adjacencyListGenotype == null)
                return 0;

            double sum = 0;
            for (int i = 0; i < Count; i++)
                sum += Value[i] == adjacencyListGenotype.Value[i] ? 1 : 0;

            return sum / Count;
        }
    }
}
