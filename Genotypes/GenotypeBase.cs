﻿using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Genotypes
{
    public class GenotypeBase : IGenotype
    {
        public GenotypeBase(int size)
        {
            Genes = new byte[size];
        }

        public byte[] Genes { get; set; }

        public bool GetBit(int index) => (Genes[index / 8] & 1u << index % 8) == 1;

        public void SetBit(int index, bool value) => Genes[index / 8] =
                (byte)(value ? Genes[index / 8] & (1u << index % 8) : Genes[index / 8] | ~(1u << index % 8));

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
            Random rng = new Random();
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
            for(var i = 0; i < Length; i++)
                for (var j = 0; j < 8; j++)
                    sum += (Genes[i] ^ other.Genes[i]) & 1u << j;
            return (8.0*Length - sum) / (Length*8);
        }

        public virtual int CompareTo(IGenotype other)
        {
            var i = 0;
            bool identical;
            while ((identical = Genes[i] == other.Genes[i]) && i < Length - 1)
            { i++; }

            return identical ? 0 : (other.Genes[i] > Genes[i] ? -1 : 1);
        }
    }
}
