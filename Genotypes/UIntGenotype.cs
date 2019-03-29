using System;
using System.Collections;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Genotypes
{
    public class UIntGenotype : GenotypeWithBytes
    {
        private uint _value;

        public uint Value
        {
            get {
                if(Deprecated)
                    _value = BitConverter.ToUInt32(Bytes);
                return _value;
            }
            set {
                _value = value;
                Genes = new BitArray(BitConverter.GetBytes(value));
            }
        }

        public UIntGenotype() : base(sizeof(uint))
        {
            _value = 0;
            Genes = new BitArray(BitConverter.GetBytes(0));
        }

        public UIntGenotype(uint value):base(sizeof(uint))
        {
            _value = value;
            Genes = new BitArray(BitConverter.GetBytes(value));
        }
        public override IGenotype ShallowCopy()
        {
            return new UIntGenotype(sizeof(uint))
            {
                Genes = this.Genes.Clone() as BitArray,
                _value = this._value
            };
        }

        public override IGenotype EmptyCopy()
        {
            return new UIntGenotype(sizeof(uint));
        }

    }
}
