using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class ListExtensions
    {
        public static void ShuffleList<T>(this List<T> list)
        {
            Random rng = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                // Swap list[k] with list[n]
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}