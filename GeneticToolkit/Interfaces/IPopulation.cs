using JetBrains.Annotations;

namespace GeneticToolkit.Interfaces
{
    [PublicAPI]
    public interface IPopulation : IEvolutionaryPopulation
    {
        ICrossover Crossover { get; set; }

        /// <summary>
        /// Defines probability of mutation
        /// </summary>
        IMutationPolicy MutationPolicy { set; get; }

        /// <summary>
        /// Defines how mutation is performed on genes
        /// </summary>
        IMutation Mutation { get; set; }
  
        IIncompatibilityPolicy IncompatibilityPolicy { get; set; }        

        IPopulationResizePolicy ResizePolicy { set; get; }

        ISelectionMethod SelectionMethod { get; set; }
    }
}
