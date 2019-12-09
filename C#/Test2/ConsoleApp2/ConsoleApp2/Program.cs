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
            int n = Convert.ToInt32(3);
            int m = Convert.ToInt32(3);
            int affectedCount = Convert.ToInt32(m);

            List<int> affected = new List<int>();

            affected.Add(2);
            affected.Add(1);
            affected.Add(3);

            int poisonousCount = Convert.ToInt32(m);

            List<int> poisonous = new List<int>();

            poisonous.Add(3);
            poisonous.Add(3);
            poisonous.Add(1);

            long result = bioHazard(n, affected, poisonous);

            Console.WriteLine(result);
            Console.ReadLine();
        }

        
        public static long bioHazard(int n, List<int> affected, List<int> poisonous)
        {
            var commonList = new List<int>();
            var nonValidByIndexes = new List<int[]>();
            var duplicatedValues = new List<int[]>();

            for (int i = 1; i <= n; i++)
            {
                commonList.Add(i);
            }

            var permutations = Permutations(commonList).ToList();

            for (int i = 0; i < affected.Count(); i++)
            {
                nonValidByIndexes.Add(new int[] { affected[i], poisonous[i] });
            }

            permutations.RemoveAt(0);

            for (int i = 0; i < permutations.Count - 1; i++)
            {
                //Consecutive validation
                if (permutations[i].ToList().Select((h, k) => h - k).Distinct().Skip(1).Any() && permutations[i].ToList().Count > 1)
                {
                    permutations.RemoveAt(i);
                    i -= 1;
                }
                else
                {
                    //Non valid indexes removal
                    for (int j = 0; j < nonValidByIndexes.Count; j++)
                    {
                        if (i <= permutations.Count - 1)
                        {
                            var total = 0;
                            total += (from q1 in permutations[i].ToList()
                                      join q2 in nonValidByIndexes[j].ToList() on q1 equals q2
                                      select q1).Count();

                            if (total == 2)
                            {
                                permutations.RemoveAt(i);
                                j = -1;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }


            return permutations.Count();
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
