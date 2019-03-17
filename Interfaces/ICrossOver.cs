using System.Collections.Generic;

namespace GeneticToolkit.Interfaces
{
    public interface ICrossOver
    {
        int ParentsCount { get; }

        int ChildrenCount { get; }

        IList<IGenotype> Cross(IList<IGenotype> parents);
    }
}
