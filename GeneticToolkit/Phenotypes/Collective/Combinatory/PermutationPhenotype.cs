using GeneticToolkit.Genotypes.Collective.Combinatory;
using GeneticToolkit.Interfaces;
using JetBrains.Annotations;

namespace GeneticToolkit.Phenotypes.Collective.Combinatory
{
    [PublicAPI]
    public class PermutationPhenotype : IGenericPhenotype<short[]>
    {
        private CombinatoryGenotype _genotype;

        /// <summary>
        /// Returns list of indexes for combinatory problem.
        /// </summary>
        public short[] GetValue()
        {
            return _genotype.GetDecoded();
        }

        public IGenotype Genotype 
        {
            get => _genotype; 
            set => _genotype = value as CombinatoryGenotype;
        }

        public IPhenotype ShallowCopy()
        {
            return new PermutationPhenotype()
            {
                Genotype = Genotype
            };
        }
    }
}
