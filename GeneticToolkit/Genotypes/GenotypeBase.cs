using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Genotypes
{
    [PublicAPI]
    public class GenotypeBase : IGenotype
    {
        public GenotypeBase() { }
        public GenotypeBase(int size)
        {
            Genes = new byte[size];
        }

        public byte[] Genes { get; set; }

        public bool GetBit(int index)
        {
            return (Genes[index / 8] & (1u << (index % 8))) == 1;
        }

        public void SetBit(int index, bool value)
        {
            Genes[index / 8] =
                (byte)(value ? Genes[index / 8] & (1u << (index % 8)) : Genes[index / 8] | ~(1u << (index % 8)));
        }

        public virtual byte this[int indexer]
        {
            get => Genes[indexer];
            set => Genes[indexer] = value;
        }

        public static int BitsPerGene => 8;
        public int Length => Genes.Length;

        public virtual IGenotype ShallowCopy()
        {
            return new GenotypeBase(Genes.Length) { Genes = Genes.Clone() as byte[] };
        }

        public virtual TGenotype ShallowCopy<TGenotype>() where TGenotype : class, IGenotype
        {
            return ShallowCopy() as TGenotype;
        }

        public virtual IGenotype EmptyCopy()
        {
            return new GenotypeBase(Genes.Length);
        }

        public virtual T EmptyCopy<T>()
        {
            return (T)EmptyCopy();
        }

        public virtual void Randomize()
        {
            var rng = new Random();
            Genes = new byte[Length];
            rng.NextBytes(Genes);
        }

        public virtual IGenotype Randomized()
        {
            Randomize();
            return this;
        }

        public virtual double SimilarityCheck(IGenotype other)
        {
            if (other.Length != Length)
                return 0;
            long sum = 0;

            for (var i = 0; i < Length; i++)
            {
                byte diff = (byte)(Genes[i] ^ other.Genes[i]);
                for (var j = 0; j < 8; j++)
                    sum += (diff & (1u << j)) > 0 ? 1 : 0;
            }

            return (8.0 * Length - sum) / (Length * 8.0);
        }

        public virtual int CompareTo(IGenotype other)
        {
            var i = -1;
            bool identical;
            do
            {
                i++;
                identical = Genes[i] == other.Genes[i];
            } while (identical && i < Length - 1);

            if (identical)
            {
                return 0;
            }

            return other.Genes[i] > Genes[i] ? -1 : 1;
        }
    }
}