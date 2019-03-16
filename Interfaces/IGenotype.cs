using System.Collections;

namespace GeneticToolkit.Interfaces
{
    public interface IGenotype
    {
        BitArray Genes { get; set; }

        bool this[int indexer] { get; set; }

        int Length { get; }

        IGenotype ShallowCopy();

        void Randomize();

        IGenotype Randomized();
    }
}
