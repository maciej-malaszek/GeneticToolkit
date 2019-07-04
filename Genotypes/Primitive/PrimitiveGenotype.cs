using GeneticToolkit.Utils.Extensions;

namespace GeneticToolkit.Genotypes.Primitive
{
    public abstract class PrimitiveGenotype : GenotypeWithWatcher
    {
        protected PrimitiveGenotype(int size) : base(size)
        {
        }

        protected PrimitiveGenotype(byte[] bytes) : base(bytes)
        {
        }
    }

    public abstract class GenericPrimitiveGenotype<T> : PrimitiveGenotype where T:struct
    {
        protected bool Deprecated;
        private T _value;
        protected GenericPrimitiveGenotype(int size) : base(size)
        {
        }


        protected GenericPrimitiveGenotype(byte[] bytes) : base(bytes)
        {
        }

        public T Value
        {
            get {
                if (Deprecated)
                    _value = BitConverterX.ToValue<T>(Genes);
                return _value;
            }
            set {
                _value = value;
                Genes = BitConverterX.GetBytes<T>(value);
            }
        }

    }
}
