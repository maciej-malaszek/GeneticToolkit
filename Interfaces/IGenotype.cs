using System;
using System.Collections;

namespace GeneticToolkit.Interfaces
{
    public interface IGenotype : IComparable<IGenotype>
    {
        BitArray Genes { get; set; }

        bool this[int indexer] { get; set; }

        int Length { get; }


        /// <summary>
        /// Creates new object of same type as caller and copies values of its genes.
        /// </summary>
        /// <returns>Shallow copy of IGenotype</returns>
        IGenotype ShallowCopy();

        /// <summary>
        /// Creates new object of same type as caller.
        /// </summary>
        /// <returns>IGenotype with default Genes</returns>
        IGenotype EmptyCopy();

        void Randomize();

        IGenotype Randomized();

        double SimilarityCheck(IGenotype other);

    }
}
