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

namespace ConsoleApp1
{
    public class CusComparer : IEqualityComparer<List<int>>
    {
        public bool Equals(List<int> x, List<int> y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(List<int> obj)
        {
            int hashCode = 0;

            for (var index = 0; index < obj.Count; index++)
            {
                hashCode ^= new { Index = index, Item = obj[index] }.GetHashCode();
            }

            return hashCode;
        }
    }
    class Program
    {

        public static int countMeetings(List<int> arrival, List<int> departure)
        {
            List<int> meetArray = new List<int>();
            List<int> tempArray = departure.Zip(arrival, (x, y) => x - y).ToList();
            var newList = arrival.Zip(departure, (a, d) => new { arrival = a, departure = d, iguales = a - d }).OrderByDescending(a => a.iguales).ToList();
            while (newList.Count != 0)
            {
                if (newList[0].iguales == 0)
                {
                    meetArray.Add(newList[0].arrival);
                    newList = newList.Where(n => !(n.arrival.Equals(newList[0].arrival) && n.departure.Equals(newList[0].arrival))).ToList();
                }
                else
                {
                    var a = !meetArray.Contains(newList[0].arrival);
                    var d = !meetArray.Contains(newList[0].departure);
                    if (a)
                    {
                        meetArray.Add(newList[0].arrival);

                    }
                    if (d)
                    {
                        meetArray.Add(newList[0].departure);
                    }
                }
            }


            return meetArray.Count();
        }
        static void Main(string[] args)
        {
            int arrivalCount = Convert.ToInt32(2);

            List<int> arrival = new List<int>();
            List<int> departure = new List<int>();

            //arrival.Add(1);
            //arrival.Add(2);
            //arrival.Add(3);
            //arrival.Add(1);
            //arrival.Add(1);
            //arrival.Add(1);
            //arrival.Add(1);

            //arrival.Add(1);
            //arrival.Add(1);
            //arrival.Add(2);

            arrival.Add(1);
            arrival.Add(2);
            arrival.Add(1);
            arrival.Add(2);
            arrival.Add(2);
            //1, 2, 1, 2, 2

            //departure.Add(1);
            //departure.Add(2);
            //departure.Add(3);
            //departure.Add(1);
            //departure.Add(1);
            //departure.Add(1);
            //departure.Add(1);

            //departure.Add(1);
            //departure.Add(2);
            //departure.Add(2);

            departure.Add(3);
            departure.Add(2);
            departure.Add(1);
            departure.Add(3);
            departure.Add(3);
            //3, 2, 1, 3, 3


            int result = countMeetings(arrival, departure);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
