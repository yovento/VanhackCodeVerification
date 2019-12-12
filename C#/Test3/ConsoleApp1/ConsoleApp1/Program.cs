using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string _firstDate;
            _firstDate = DateTime.Today.AddDays(-15).ToShortDateString();

            string _lastDate;
            _lastDate = DateTime.Today.ToShortDateString();

            string _weekDay;
            _weekDay = "friday";

            openAndClosePrices(_firstDate, _lastDate, _weekDay);

            Console.ReadLine();
        }

        static void openAndClosePrices(string firstDate, string lastDate, string weekDay)
        {
            var client = new HttpClient();

            var content = client.GetAsync("https://jsonmock.hackerrank.com/api/stocks").Result;

            var stocks = content.Content.ReadAsStringAsync().Result;

            Console.WriteLine(content);

        }
    }
}
