using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticToolkit.Interfaces
{
    public interface IPhenotype
    {
        IGenotype Genotype { get; set; } 

        IPhenotype ShallowCopy();
    }
}
