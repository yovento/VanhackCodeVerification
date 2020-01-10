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

        public static int countMeetings(List<int> arrival, List<int> departure)
        {
            int maxMeetingDays = 0;
            int actualMeetingDay = 0;

            List<int> meetArray = new List<int>();
            List<int> tempArray = departure.Zip(arrival, (x, y) => x - y).ToList();

            maxMeetingDays = arrival.Select(x => x).Max() == departure.Select(x => x).Max() ? arrival.Select(x => x).Max() : departure.Select(x => x).Max();

            for (int i = 0; i < tempArray.Count; i++)
            {
                if (tempArray[i] == actualMeetingDay)
                {
                    for (int j = arrival[i]; j < departure[i]; j++)
                    {

                    }
                }
            }
            foreach (var item in tempArray)
            {
                
                

                
                if (item <= actualMeetingDay && meetArray[actualMeetingDay] == 0)
                {
                    meetArray[actualMeetingDay]
                }
            }


            return 0;
        }
        static void Main(string[] args)
        {
            int arrivalCount = Convert.ToInt32(3);

            List<int> arrival = new List<int>();
            List<int> departure = new List<int>();

            arrival.Add(1);
            arrival.Add(1);
            arrival.Add(2);

            departure.Add(1);
            departure.Add(2);
            departure.Add(2);


            int result = countMeetings(arrival, departure);
        }
    }
}
