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
            int n = Convert.ToInt32(5);
            int m = Convert.ToInt32(2);
            int affectedCount = Convert.ToInt32(m);

            List<int> affected = new List<int>();

            affected.Add(1);
            affected.Add(2);
            //affected.Add(3);
            //affected.Add(3);
            //affected.Add(3);

            int poisonousCount = Convert.ToInt32(m);

            List<int> poisonous = new List<int>();

            poisonous.Add(3);
            poisonous.Add(5);
            //poisonous.Add(1);
            //poisonous.Add(6);
            //poisonous.Add(4);
            //poisonous.Add(1);

            long result = bioHazard(n, affected, poisonous);

            Console.WriteLine(result);
            Console.ReadLine();
        }


        public static long bioHazard(int n, List<int> affected, List<int> poisonous)
        {
            int[] arr = Enumerable.Range(1, n).ToArray();

            var permutations = Enumerable.Range(0, 1 << (arr.Length)).Select(index => arr.Where((v, i) => (index & (1 << i)) != 0).ToArray()).ToList();

            permutations.RemoveAt(0);

            //Consecutive validation
            permutations.RemoveAll(x => x.ToList().Select((h, k) => h - k).Distinct().Skip(1).Any() && x.ToList().Count > 1);

            //Indexes validation
            permutations.RemoveAll(x => (affected.Select((g, idx) => new int[] { g, poisonous[idx] }).ToList()).Any(y =>
                                  (from q1 in x.ToList()
                                   join q2 in y.ToList() on q1 equals q2
                                   select q1).Count() == 2

                                    ));

            return permutations.Count();
        }
    }
}
