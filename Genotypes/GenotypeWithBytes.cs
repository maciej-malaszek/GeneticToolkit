using System;
using System.Collections;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Genotypes
{
    public class GenotypeWithBytes : GenotypeWithWatcher
    {
        protected bool Deprecated = true;

        protected static ushort ByteSize = 8;

        private readonly byte[] _bytes;

        public byte[] Bytes
        {
            get {
                if(!Deprecated) return _bytes;

                if(Genes.Length % 8 != 0)
                    throw new Exception("BitArray length not compatible with bytes!");

                byte[] buffer = new byte[(int)Math.Ceiling(Genes.Length / 8.0)];
                Genes.CopyTo(_bytes, 0);
                Deprecated = false;

                return _bytes;
            }
        }

        public override IGenotype ShallowCopy()
        {
            return new GenotypeWithBytes(_bytes) { Genes = Genes.Clone() as BitArray };
        }

        public GenotypeWithBytes(int size) : base(size * ByteSize)
        {
            ValueChanged += Deprecate;
            _bytes = new byte[size];
        }

        public GenotypeWithBytes(byte[] bytes) : base(bytes.Length * ByteSize)
        {
            _bytes = bytes;
        }

        private void Deprecate(object sender, EventArgs e)
        {
            Deprecated = true;
        }


    }
}
