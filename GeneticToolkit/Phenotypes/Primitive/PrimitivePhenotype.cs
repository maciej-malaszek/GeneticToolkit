using GeneticToolkit.Interfaces;
using GeneticToolkit.Utils.Extensions;
using JetBrains.Annotations;

namespace GeneticToolkit.Phenotypes.Primitive
{
    [PublicAPI]
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
