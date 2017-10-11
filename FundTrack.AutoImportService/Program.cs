using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace FundTrack.AutoImportService
{
    class Program
    {
        static HttpClient client = new HttpClient();
 //       static Dictionary<int, Timer> dict = new Dictionary<int, Timer>{
 //           { 97,  new Timer(TimerCallback, 97, 0, 60000)},
 //           { 107,  new Timer(TimerCallback, 107, 0, 30000)},
 //           { 108,  new Timer(TimerCallback, 108, 0, 15000)},
 //           { 109,  new Timer(TimerCallback, 109, 0, 90000)}
 //};
        static async Task<Uri> CreateProductAsync(BalanceViewModel balance)
        {

            HttpResponseMessage response = await client.PostAsJsonAsync<BalanceViewModel>("api/FixingBalance/", balance);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        
        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("http://fundtrack.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            TimerWithInterval timer = new TimerWithInterval( new Timer(TimerCallback, 97, 0, 15000), 15000);
            Thread.Sleep(52000);
            Console.WriteLine("change");
            timer.ChangeInterval(30000);
            while (true) ;
        }
        static async Task RunAsync(int id)
        {
           
            try
            {
                BalanceViewModel balance = new BalanceViewModel { Amount = 1111, BalanceDate = DateTime.Now, OrgAccountId = id };
                var url = await CreateProductAsync(balance);
 
            }
            catch (Exception e)
            {
                
            }
           
        }
        private static void TimerCallback(object o)
        {
            int id = (int)o;
            Console.WriteLine(DateTime.Now);
        //    RunAsync(id).Wait();
            GC.Collect();
        }
    }
    public class BalanceViewModel
    {
        public decimal Amount { get; set; }
        public DateTime BalanceDate { get; set; }
        public int OrgAccountId { get; set; }
    }
    public class TimerWithInterval
    {
        private Timer timerInstance { get; set; }
        private int interval { get; set; }
        public TimerWithInterval (Timer timer, int interval)
        {
            this.interval = interval;
            timerInstance = timer;
        }
        public void ChangeInterval(int interval)
        {
            if (this.interval == interval)
                return;
            timerInstance.Change(0, interval);
        }
    }
}
