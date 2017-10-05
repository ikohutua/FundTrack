using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.AutoImportService
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static async Task<Uri> CreateProductAsync(BalanceViewModel balance)
        {

            HttpResponseMessage response = await client.PostAsJsonAsync<BalanceViewModel>("api/FixingBalance/", balance);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }
        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://fundtrack.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                BalanceViewModel balance = new BalanceViewModel { Amount = 1111, BalanceDate = DateTime.Now, OrgAccountId = 97 };
                var url = await CreateProductAsync(balance);
 
            }
            catch (Exception e)
            {
                
            }
           
        }
    }
    public class BalanceViewModel
    {
        public decimal Amount { get; set; }
        public DateTime BalanceDate { get; set; }
        public int OrgAccountId { get; set; }
    }
}
