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
            int uid = 4;
            string txnType = "credit";
            string monthYear = "09-2019";

            var result = getUserTransaction(uid, txnType, monthYear);

        }

        public static List<int> getUserTransaction(int uid, string txnType, string monthYear)
        {
            Console.WriteLine(uid);
            Console.WriteLine(txnType);
            Console.WriteLine(monthYear);
            string _serviceUrl = "https://jsonmock.hackerrank.com/api/transactions/search?userId={0}&page={1}";
            var client = new HttpClient();
            var average = new float();
            var date = DateTime.Parse(monthYear);
            var content = client.GetAsync(string.Format(_serviceUrl, uid, 1)).Result;
            var transactions = new List<transaction>();
            var transactionsData = Newtonsoft.Json.JsonConvert.DeserializeObject<transactions>(content.Content.ReadAsStringAsync().Result);

            transactions.AddRange(transactionsData.data);

            for (int i = 2; i <= transactionsData.total_pages; i++)
            {
                content = client.GetAsync(string.Format(_serviceUrl, uid, i)).Result;
                transactions.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<transactions>(content.Content.ReadAsStringAsync().Result).data);
            }

            var debitTransactions = transactions.Where(x => x.date.Month.Equals(date.Month) && x.date.Year.Equals(date.Year) && x.txnType.Equals("debit")).ToList();
            transactions = transactions.Where(x => x.date.Month.Equals(date.Month) && x.date.Year.Equals(date.Year) && x.txnType.Equals(txnType)).ToList();
            average = debitTransactions.Select(x => x.amountNumber).Sum() / debitTransactions.Count();

            var result = transactions.Where(x => x.amountNumber > average).Select(x => x.id).OrderBy(x => x).ToList();

            if (result.Count() == 0)
                result.Add(-1);
            return result;
        }
    }

    class transactions
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<transaction> data { get; set; }
    }

    class location
    {
        public int id { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public float zipCode { get; set; }
    }

    class transaction
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public double timestamp { get; set; }
        public DateTime date
        {
            get
            {
                System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime();
                return dateTime.AddMilliseconds(this.timestamp).ToUniversalTime();
            }
        }
        public string txnType { get; set; }
        public string amount { get; set; }
        public float amountNumber
        {
            get
            {
                return float.Parse(amount.Replace("$", "").Replace(",", ""));
            }
        }
        public location location { get; set; }
        public string ip { get; set; }

    }
}
