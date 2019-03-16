using System;
using System.Collections;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Genotypes
{
    public class SimpleGenotype : IGenotype
    {

        public SimpleGenotype(int size)
        {
            Genes = new BitArray(size);

        }

        public virtual BitArray Genes { get; set; }

        public virtual bool this[int indexer]
        {
            get => Genes[indexer];
            set => Genes[indexer] = value;
        }

        public int Length => Genes.Count;

        public virtual IGenotype ShallowCopy()
        {
            return new SimpleGenotype(Genes.Length) { Genes = Genes.Clone() as BitArray };
        }

        public void Randomize()
        {
            int length = Length;
            int remainder = Length % 8;

            Random rng = new Random();
            byte[] buffer = new byte[Length / 8];
            rng.NextBytes(buffer);
            Genes = new BitArray(buffer);

            if(remainder <= 0)
                return;


            int l = Genes.Length;
            Genes.Length = length;
            for(int i = 0; i < remainder; i++)
                Genes[i + l] = rng.Next(0,1) == 1;

        }

        public IGenotype Randomized()
        {
            Randomize();
            return this;
        }
    }
}
