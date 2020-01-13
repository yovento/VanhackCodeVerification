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

    class Program
    {

        class meeting
        {
            public int arrival { get; set; }
            public int departure { get; set; }
            public int index { get; set; }
        }

        public static int countMeetings(List<int> arrival, List<int> departure)
        {
            var n = arrival.Count();
            meeting[] meet = new meeting[n];

            for (int i = 0; i < n; i++)
            {
                meet[i] = new meeting { arrival = arrival[i], departure = departure[i], index = i } ;                
            }

            meet = meet.OrderBy(x => x.departure - x.arrival).ThenBy(x => x.departure).ToArray();

            List<int> m = new List<int>();
            
            for (int i = 0; i < n; i++)
            {
                int arr = meet[i].arrival;
                int dep = meet[i].departure;
                int freeDay = nextGreater(m, m.Count(), arr - 1);

                Console.WriteLine(arr.ToString() + " " + dep.ToString());
                if (freeDay != 0 && freeDay <= dep)
                {
                    m.Add(freeDay);
                }
            }
            return m.Count();
        }

        static int nextGreater(List<int> arr, int lenght, int start)
        {
            int low = 0, high = lenght - 1, ans = start + 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;

                if (arr[mid] <= ans)
                {

                    if (arr[mid] == ans)
                    {
                        ans++;
                        high = lenght - 1;
                    }
                    low = mid + 1;
                }
                else
                    high = mid - 1;
            }

            return ans;
        }

        static void Main(string[] args)
        {
            int arrivalCount = Convert.ToInt32(2);

            List<int> arrival = new List<int>();
            List<int> departure = new List<int>();

   
            //arrival.Add(1);
            //arrival.Add(1);

            arrival.Add(99848);
            arrival.Add(99849);
            arrival.Add(99848);

            arrival.Add(99940);
            arrival.Add(99982);
            arrival.Add(23936);
            //arrival.Add(11);

            //arrival.Add(1);
            //arrival.Add(2);
            //arrival.Add(1);
            //arrival.Add(2);
            //arrival.Add(2);
            //1, 2, 1, 2, 2


            departure.Add(99848);
            departure.Add(99849);
            departure.Add(99849);

            departure.Add(99941);
            departure.Add(99983);
            departure.Add(23938);
            //departure.Add(11);

            //departure.Add(3);
            //departure.Add(1000000);

            //departure.Add(3);
            //departure.Add(2);
            //departure.Add(1);
            //departure.Add(3);
            //departure.Add(3);
            //3, 2, 1, 3, 3

            int result = countMeetings(arrival, departure);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }


}
