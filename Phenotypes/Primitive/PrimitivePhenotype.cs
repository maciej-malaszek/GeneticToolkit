using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Extensions;

namespace GeneticToolkit.Phenotypes.Primitive
{
    public class PrimitivePhenotype<T> : IGenericPhenotype<T>
    {
        public virtual T GetValue()
        {
            return BitConverterX.ToValue<T>(Genotype.Genes);
        }

        public IGenotype Genotype { get; set; }
        public virtual IPhenotype ShallowCopy()
        {
            return new PrimitivePhenotype<T>()
            {
                Genotype = Genotype.EmptyCopy()
            };
        }
    }
}
