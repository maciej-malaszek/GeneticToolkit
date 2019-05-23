namespace GeneticToolkit.Interfaces
{
    public interface IGenotype
    {
        byte[] Genes { get; set; }

        bool GetBit(int index);
        void SetBit(int index, bool value);
        
        byte this[int indexer] { get; set; }

        int Length { get; }


        /// <summary>
        /// Creates new object of same type as caller and copies values of its genes.
        /// </summary>
        /// <returns>Shallow copy of IGenotype</returns>
        IGenotype ShallowCopy();

        /// <summary>
        /// Creates new object of same type as caller and copies values of its genes.
        /// </summary>
        /// <returns>Shallow copy of IGenotype</returns>
        TGenotype ShallowCopy<TGenotype>() where TGenotype : class,IGenotype;

        /// <summary>
        /// Creates new object of same type as caller.
        /// </summary>
        /// <returns>IGenotype with default Genes</returns>
        IGenotype EmptyCopy();

        T EmptyCopy<T>();

        void Randomize();

        IGenotype Randomized();

        double SimilarityCheck(IGenotype other);

    }
}
