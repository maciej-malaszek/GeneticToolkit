using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticToolkit.Utils.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Resize<T>(this T[] array)
        {
            var temp = new T[array.Length * 2];
            array.CopyTo(temp, 0);
            return temp;
        }

        public static bool Any<T>(this IEnumerable<T> array, Predicate<T> predicate)
        {
            return Enumerable.Any(array, predicate.Invoke);
        }

        public static bool All<T>(this IEnumerable<T> array, Predicate<T> predicate)
        {
            return Enumerable.All(array, predicate.Invoke);
        }

        public static bool Contains<T>(this IEnumerable<T> array, T value)
        {
            return Enumerable.Contains(array, value);
        }

        public static T Last<T>(this T[] array)
        {
            return array[array.Length - 1];
        }

        public static void Swap<T>(this T[] array, int index0, int index1)
        {
            var t = array[index0];
            array[index0] = array[index1];
            array[index1] = t;
        }
    }
}