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
            _firstDate = "1-January-2000";

            string _lastDate;
            _lastDate = "22-February-2000";

            string _weekDay;
            _weekDay = "Monday";

            openAndClosePrices(_firstDate, _lastDate, _weekDay);

            Console.ReadLine();
        }

        static void openAndClosePrices(string firstDate, string lastDate, string weekDay)
        {
            var url = "https://jsonmock.hackerrank.com/api/stocks";
            var pagedUrl = "https://jsonmock.hackerrank.com/api/stocks?page={0}";
            var client = new HttpClient();
            var content = client.GetAsync(url).Result;
            var stocks = new List<stock>();
            var stocksData = Newtonsoft.Json.JsonConvert.DeserializeObject<stocks>(content.Content.ReadAsStringAsync().Result);

            stocks.AddRange(stocksData.data);

            for (int i = 2; i < stocksData.total_pages; i++)
            {
                content = client.GetAsync(string.Format(pagedUrl, i)).Result;
                stocks.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<stocks>(content.Content.ReadAsStringAsync().Result).data);
            }

            var resultStocks = stocks.Where(x => x.dayOfWeek == weekDay && x.parsedDate >= DateTime.Parse(firstDate) && x.parsedDate <= DateTime.Parse(lastDate)).ToList();

            resultStocks.ForEach(x => Console.WriteLine(string.Format("{0} {1} {2}", x.date, x.open, x.close)));           

        }

        class stocks
        {
            public int page { get; set; }
            public int per_page { get; set; }
            public int total { get; set; }
            public int total_pages { get; set; }
            public List<stock> data { get; set; }
        }

        class stock
        {
            public string date { get; set; }
            public DateTime parsedDate
            {
                get
                {
                    return DateTime.Parse(date);
                }
            }
            public float open { get; set; }
            public float high { get; set; }
            public float low { get; set; }
            public float close { get; set; }
            public string dayOfWeek
            {
                get
                {
                    return parsedDate.ToString("dddd");
                }
            }
        }
    }

    
}
