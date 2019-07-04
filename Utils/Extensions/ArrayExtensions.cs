using System;

namespace GeneticToolkit.Utils.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Resize<T>(this T[] array)
        {
            var temp = new T[array.Length * 2];
            array.CopyTo(temp,0);
            return temp;
        }

        public static bool Any<T>(this T[] array, Predicate<T> predicate)
        {
            foreach (T t in array)
                if (predicate.Invoke(t))
                    return true;
            return false;
        }

        public static bool All<T>(this T[] array, Predicate<T> predicate)
        {
            foreach (T t in array)
                if (predicate.Invoke(t) == false)
                    return false;

            return true;
        }

        public static bool Contains<T>(this T[] array, T value)
        {
            foreach (T item in array)
                if (item.Equals(value))
                    return true;
            return false;
        }

        public static T Last<T>(this T[] array)
        {
            return array[array.Length-1];
        }

        public static void Swap<T>(this T[] array, int index0, int index1)
        {
            T t = array[index0];
            array[index0] = array[index1];
            array[index1] = t;
        }
    }
}
