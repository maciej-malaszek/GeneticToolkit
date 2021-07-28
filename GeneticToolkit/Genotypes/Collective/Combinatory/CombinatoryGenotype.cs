using GeneticToolkit.Interfaces;
using System;

namespace GeneticToolkit.Genotypes.Collective.Combinatory
{
    public abstract class CombinatoryGenotype : GenotypeWithWatcher
    {
        protected bool Deprecated = true;

        private short[] _value;

        private int[] _reverseIndexValue;
        public int Count => _value.Length;
        
        protected CombinatoryGenotype(int size) : base(size)
        {
            _value = new short[size];
            _reverseIndexValue = new int[size];
        }

        protected CombinatoryGenotype(byte[] bytes) : base(bytes)
        {
        }

        public short[] Value
        {
            get
            {
                if (!Deprecated)
                {
                    return _value;
                }

                _value = new short[Count];
                _reverseIndexValue = new int[Count];
                for (var i = 0; i < Count; i++)
                {
                    _value[i] = BitConverter.ToInt16(Genes, i * sizeof(short));
                    _reverseIndexValue[_value[i]] = i;
                }
                return _value;
            }
            set
            {
                _value = value;
                _reverseIndexValue = new int[_value.Length];
                for (var i = 0; i < _value.Length; i++)
                {
                    _reverseIndexValue[_value[i]] = i;
                }

                UpdateBits();
            }
        }

        public void SetValue(int index, short value)
        {
            Value[index] = value;
            _reverseIndexValue[value] = index;
            BitConverter.GetBytes(value).CopyTo(Genes, index * sizeof(short));
        }

        public void UpdateBits()
        {
            Genes = new byte[Count * sizeof(short)];
            for (var i = 0; i < Count; i++)
            {
                BitConverter.GetBytes(_value[i]).CopyTo(Genes, i * sizeof(short));
            }

            Deprecated = false;
        }

        public short GetPrevious(short value)
        {
            var index = _reverseIndexValue[value] <= 0 ? Count - 1 : _reverseIndexValue[value];
            return Value[index - 1];
        }

        public short GetNext(short value)
        {
            var index = _reverseIndexValue[value] >= Count - 1 ? 0 : _reverseIndexValue[value];
            return Value[index + 1];
        }

        public int GetIndex(short value)
        {
            return _reverseIndexValue[value];
        }

        public override double SimilarityCheck(IGenotype other)
        {
            double sum = 0;
            var otherCombinatoryGenotype = other as CombinatoryGenotype;

            if (otherCombinatoryGenotype == null)
            {
                return 0;
            }

            for (var testedValue = 0; testedValue < Count; testedValue++)
            {
                var testedIndex = _reverseIndexValue[testedValue];
                var otherTestedIndex = otherCombinatoryGenotype._reverseIndexValue[testedValue];

                testedIndex = testedIndex >= Count - 2 ? 0 : testedIndex + 1;
                otherTestedIndex = otherTestedIndex >= Count - 2 ? 0 : otherTestedIndex + 1;
                if (Value[testedIndex] == otherCombinatoryGenotype.Value[otherTestedIndex])
                {
                    sum++;
                }
            }

            return sum / Count;
        }

        public abstract short[] GetDecoded();
    }
}