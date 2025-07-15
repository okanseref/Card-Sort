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
        
        public static List<List<T>> GetAllFullAndSubsetPermutations<T>(this List<T> list, int minSubsetLength = 3)
        {
            var result = new List<List<T>>();

            // Full list permutations
            result.AddRange(GetPermutations(list));

            // All combinations of 3 elements
            var subsets = GetCombinations(list, minSubsetLength);
            foreach (var subset in subsets)
            {
                result.AddRange(GetPermutations(subset));
            }

            return result;
        }

        // Generate all permutations of a list
        private static List<List<T>> GetPermutations<T>(List<T> list)
        {
            var result = new List<List<T>>();
            Permute(list, 0, result);
            return result;
        }

        private static void Permute<T>(List<T> list, int start, List<List<T>> result)
        {
            if (start >= list.Count)
            {
                result.Add(new List<T>(list));
                return;
            }

            for (int i = start; i < list.Count; i++)
            {
                Swap(list, start, i);
                Permute(list, start + 1, result);
                Swap(list, start, i); // backtrack
            }
        }

        private static void Swap<T>(List<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        // Generate all combinations of `k` elements from a list
        private static List<List<T>> GetCombinations<T>(List<T> list, int k)
        {
            var result = new List<List<T>>();
            Combine(list, 0, k, new List<T>(), result);
            return result;
        }

        private static void Combine<T>(List<T> list, int start, int k, List<T> current, List<List<T>> result)
        {
            if (current.Count == k)
            {
                result.Add(new List<T>(current));
                return;
            }

            for (int i = start; i < list.Count; i++)
            {
                current.Add(list[i]);
                Combine(list, i + 1, k, current, result);
                current.RemoveAt(current.Count - 1);
            }
        }
    }
}