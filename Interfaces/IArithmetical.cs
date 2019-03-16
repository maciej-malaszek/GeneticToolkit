using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticToolkit.Interfaces
{

    public interface IArithmetical<T>
    {
        T Add(T l,T r);
        T Subtract(T l,T r);
        T Divide(T l,T r);
        T Multiply(T l,T r);
        T Abs(T x);
        T Random();
        T Random(T max);
        T Random(T min, T max);
    }
}
