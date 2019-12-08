using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(4);

            int affectedCount = Convert.ToInt32(2);

            List<int> affected = new List<int>();
                        
            affected.Add(1);            
            affected.Add(2);

            int poisonousCount = Convert.ToInt32(2);

            List<int> poisonous = new List<int>();

            poisonous.Add(3);
            poisonous.Add(4);

            long result = bioHazard(n, affected, poisonous);

            Console.WriteLine(result);
            Console.ReadLine();
        }

        
        public static long bioHazard(int n, List<int> affected, List<int> poisonous)
        {
            int validCombinations = 0;
            var combinationsA = Permutations(affected).ToList();
            var combinationsP = Permutations(poisonous).ToList();

            //combinations.AddRange(poisonous.SelectMany(a => poisonous.Where(p => p != a).Select(p => new { a = a, b = p })).ToList());

            //for (int i = 0; i < combinations.Count; i++)
            //{
            //    var row = combinations[i];
            //    combinations.Remove(combinations.Where(x => x.a == row.b && x.b == row.a).FirstOrDefault());
            //}

            //combinations.AddRange(affected.SelectMany(a => poisonous.Select(p => new { a = a, b = p })).ToList());

            //int validCombinations = 0;

            for (int i = 0; i < combinationsA.Count; i++)
            {
                var validCombination = true;
                for (int j = 0; j < combinationsA[i].Count(); j++)
                {
                    var index = affected.IndexOf(combinationsA[i][j]);
                    if (index >= 0 && poisonous[index] == combinationsP[i][index] && combinationsA[i].Count() > 1)
                    {
                        validCombination = false;
                    }
                }

                if (validCombination && combinationsA[i].Count() > 0)
                {
                    //Console.WriteLine(row);
                    validCombinations += 1;
                }
            }
            

            return 0;
        }

        public static IEnumerable<T[]> Permutations<T>(IEnumerable<T> source)
        {
            T[] data = source.ToArray();

            return Enumerable
              .Range(0, 1 << (data.Length))
              .Select(index => data
                 .Where((v, i) => (index & (1 << i)) != 0)
                 .ToArray());
        }
    }
}
