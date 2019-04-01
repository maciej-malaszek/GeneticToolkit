using System.Collections.Generic;

namespace GeneticToolkit.Interfaces
{
    public interface IHeavenPolicy : IGeneticSerializable
    {
        ICollection<IIndividual> Memory { get; }

        void HandleGeneration(IPopulation population);
    }
}
