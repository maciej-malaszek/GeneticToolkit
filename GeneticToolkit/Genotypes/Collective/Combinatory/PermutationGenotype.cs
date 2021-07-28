using GeneticToolkit.Interfaces;
using System;
using JetBrains.Annotations;

namespace GeneticToolkit.Genotypes.Collective.Combinatory
{
    /// <summary>
    /// Genotype dedicated for Traveling Salesman Problem (TSP) with Route-Based Representation.
    /// </summary>
    [PublicAPI]
    public class PermutationGenotype : CombinatoryGenotype
    {
        private readonly Random _random = new();

        public PermutationGenotype() : base(0)
        {
        }

        public PermutationGenotype(int size) : base(size)
        {
            Value = new short[Count];
        }

        public PermutationGenotype(byte[] bytes) : base(bytes)
        {
        }

        public override IGenotype EmptyCopy()
        {
            return new PermutationGenotype(Count);
        }

        public override T EmptyCopy<T>()
        {
            return (T) EmptyCopy();
        }

        public override IGenotype ShallowCopy()
        {
            return new PermutationGenotype(Count)
            {
                Value = (short[]) Value.Clone(),
                Genes = Genes.Clone() as byte[]
            };
        }

        public override TGenotype ShallowCopy<TGenotype>()
        {
            return new PermutationGenotype(Count)
            {
                Value = (short[]) Value.Clone(),
                Genes = Genes.Clone() as byte[]
            } as TGenotype;
        }

        protected void SwapElements(int index0, int index1)
        {
            var t = Value[index0];
            SetValue(index0, Value[index1]);
            SetValue(index1, t);
        }

        public override void Randomize()
        {
            for (var i = 0; i < Count; i++)
            {
                SetValue(i, (short) i);
            }

            UpdateBits();
            for (var i = 0; i < Count; i++)
            {
                SwapElements(_random.Next(0, Count), _random.Next(0, Count));
            }
        }

        public override IGenotype Randomized()
        {
            Randomize();
            return this;
        }

        public override short[] GetDecoded()
        {
            return Value;
        }
    }
}