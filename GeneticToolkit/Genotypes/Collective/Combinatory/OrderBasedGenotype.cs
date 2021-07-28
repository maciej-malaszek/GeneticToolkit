using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Genotypes.Collective.Combinatory
{
    [PublicAPI]
    public class OrderBasedGenotype : CombinatoryGenotype
    {
        protected OrderBasedGenotype(int size) : base(size)
        {
            Value = new short[Count];

            var indexes = new short[Count];
            for (short i = 0; i < Count; i++)
            {
                indexes[i] = i;
            }
        }

        protected OrderBasedGenotype(byte[] bytes) : base(bytes)
        {
            var indexes = new short[bytes.Length / sizeof(short)];
            for (short i = 0; i < bytes.Length / sizeof(short); i++)
            {
                indexes[i] = i;
            }
        }

        public override IGenotype EmptyCopy()
        {
            return new OrderBasedGenotype(Count);
        }

        public override T EmptyCopy<T>()
        {
            return (T) EmptyCopy();
        }

        public override IGenotype ShallowCopy()
        {
            return new OrderBasedGenotype(Count)
            {
                Value = (short[]) Value.Clone(),
                Genes = Genes.Clone() as byte[]
            };
        }

        public override TGenotype ShallowCopy<TGenotype>()
        {
            return new OrderBasedGenotype(Count)
            {
                Value = (short[]) Value.Clone(),
                Genes = Genes.Clone() as byte[]
            } as TGenotype;
        }

        public override short[] GetDecoded()
        {
            var indexIsUsed = new bool[Count];
            var decoded = new short[Count];

            // Twice as fast as using List<short> and removing items
            for (var i = 0; i < Count; i++)
            {
                var offset = 0;
                int val = Value[i];
                for (var j = 0; j <= val; j++)
                {
                    if (indexIsUsed[j + offset])
                    {
                        offset++;
                        j--;
                    }
                }

                decoded[i] = (short) (val + offset);
                indexIsUsed[decoded[i]] = true;
            }

            return decoded;
        }
    }
}