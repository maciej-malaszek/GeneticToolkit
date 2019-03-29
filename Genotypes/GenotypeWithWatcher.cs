using System;
using System.Collections;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Genotypes
{
    public class GenotypeWithWatcher : SimpleGenotype
    {

        // ReSharper disable once InconsistentNaming
        protected BitArray _Genes;

        public event EventHandler ValueChanged;

        public override bool this[int indexer]
        {
            get => _Genes[indexer];
            set
            {
                _Genes[indexer] = value;
                OnValueChanged(new EventArgs());
            }
        }

        public override BitArray Genes
        {
            get => _Genes;
            set
            {
                _Genes = value;
                OnValueChanged(new EventArgs());
            }
        }

        public override IGenotype ShallowCopy()
        {
            return new GenotypeWithWatcher(Length/8) { Genes = Genes.Clone() as BitArray };
        }

        public override IGenotype EmptyCopy()
        {
            return new GenotypeWithWatcher(Length/8);
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        public GenotypeWithWatcher(int size) : base(size)
        {
        }
    }
}
