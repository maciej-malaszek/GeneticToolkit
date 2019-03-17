using System.Collections.Generic;

namespace GeneticToolkit.Interfaces
{
    public interface IHeavenPolicy
    {
        ICollection<IIndividual> Memory { get; }

        void HandleGeneration(IPopulation population);
    }
}
