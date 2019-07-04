﻿using GeneticToolkit.Interfaces;

using System;

namespace GeneticToolkit.Genotypes
{
    public class GenotypeWithWatcher : GenotypeBase
    {
        public event EventHandler ValueChanged;

        public GenotypeWithWatcher(byte[] bytes):base(bytes.Length)
        {
            Genes = bytes;
        }
        public GenotypeWithWatcher(int size) : base(size)
        {
        }
        public override byte this[int indexer]
        {
            get => Genes[indexer];
            set
            {
                Genes[indexer] = value;
                OnValueChanged(new EventArgs());
            }
        }
        public override IGenotype ShallowCopy()
        {
            return new GenotypeWithWatcher(Length) { Genes = Genes.Clone() as byte[] };
        }
        public override IGenotype EmptyCopy()
        {
            return new GenotypeWithWatcher(Length);
        }
        public override T EmptyCopy<T>()
        {
            return (T) EmptyCopy();
        }
        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }
    }
}
