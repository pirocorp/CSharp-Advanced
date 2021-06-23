﻿namespace _08._Custom_Comparator
{
    using System;
    using System.Linq;

    public static class Program
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            //Func<int, int, int> comparerForArraySort = (x, y) =>
            //    Math.Abs(x % 2) == Math.Abs(y % 2) ? (x == y ? 0 : (x < y ? -1 : 1)) : (Math.Abs(x % 2) == 0 ? -1 : 1);

            //Array.Sort(numbers, (x, y) => comparerForArraySort(x, y));

            int ComparerForOrderBy(int x) => Math.Abs(x % 2);

            numbers = numbers
                .OrderBy(ComparerForOrderBy)
                .ThenBy(x => x)
                .ToArray();

            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}