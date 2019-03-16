using System;
using GeneticToolkit.Genotypes;
using GeneticToolkit.Interfaces;

namespace GeneticToolkit.Phenotypes
{
    public class F64Phenotype : IGenericPhenotype<double>
    {
        protected GenotypeWithBytes _genotype;

        public IGenotype Genotype
        {
            get => _genotype; 
            set => _genotype = value as GenotypeWithBytes;
        }

        protected byte[] Bytes => _genotype.Bytes;

        public virtual double GetValue()
        {
           return BitConverter.ToDouble(_genotype.Bytes);
        }

        public virtual IPhenotype ShallowCopy()
        {
            return new F64Phenotype()
            {
                Genotype = _genotype.ShallowCopy() 
            };
        }
    }
}
