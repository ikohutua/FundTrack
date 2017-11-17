using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PrivatService.Response;
using PrivatService.ViewModels;

namespace PrivatService
{
    public static class PrivatImporter
    {
        private static readonly string _connectionString = "Server=fundtrackss.database.windows.net;Initial Catalog=fundtrackss;Persist Security Info=False;User ID=adminss;Password=fund_track0ITA;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;MultipleActiveResultSets=True;";

        #region xml creating
        private static string ImportXmlData(string cardnumber, string merchantId, string password, DateTime dateFrom, DateTime dateTo)
        {
            const string xmlVersion = "\"1.0\"";
            const string encodingVersion = "\"UTF-8\"";
            const string startDate = "\"sd\"";
            const string endDate = "\"ed\"";
            const string card = "\"card\"";
            const string empty = "\"\"";
            var data =
    @"<oper>cmt</oper>
        <wait>0</wait>
        <test>0</test>  
        <payment id=" + empty + @">
            <prop name= " + startDate + @" value=" + "\"" + dateFrom.ToString("d") + @"""/>
            <prop name= " + endDate + @" value=" + "\"" + dateTo.ToString("d") + @"""/>
            <prop name= " + card + @" value=" + "\"" + cardnumber + @"""/>
        </payment>";

            var sign = HashFunctions.CalculateSha1Hash(HashFunctions.CalculateMd5Hash(data + password));
            var body = @"<?xml version=" + xmlVersion + @" encoding=" + encodingVersion + @"?>
<request version=" + xmlVersion + @">
    <merchant>
        <id>" + merchantId + @"</id>
        <signature>" + sign + @"</signature>
    </merchant>
    <data>
        " + data + @"
    </data>
</request>";

            return body;
        }
        #endregion

        private static async Task<string> PrivatRequestAsync(string cardNumber, string merchantId, string merchantPassword, DateTime dateFrom, DateTime dateTo)
        {
            var dataToBeSent = ImportXmlData(cardNumber, merchantId, merchantPassword, dateFrom, dateTo);
            var content = new StringContent(dataToBeSent);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("tetx/xml"));
                //var url = ConfigurationManager.AppSettings[0];
                var response = await client.PostAsync("https://api.privatbank.ua/p24api/rest_fiz", content);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                return response.StatusCode.ToString();
            }
        }
        private static List<int> GetAllAppCodes()
        {
            var appCodes = new List<int>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(Constants.AllAppCodes, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appCodes.Add((int)reader["AppCode"]);
                    }
                }
            }
            return appCodes;
        }

        private static void AddBankImportDetail(BankImportDetailsViewModel model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(Constants.InsertBankImport, connection);
                command.Parameters.Add("@amount", SqlDbType.NVarChar).Value = model.Amount;
                command.Parameters.Add("@appCode", SqlDbType.Int).Value = model.AppCode;
                command.Parameters.Add("@card", SqlDbType.NVarChar).Value = model.Card;
                command.Parameters.Add("@cardAmount", SqlDbType.NVarChar).Value = model.CardAmount;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = model.Description;
                command.Parameters.Add("@rest", SqlDbType.NVarChar).Value = model.Rest;
                command.Parameters.Add("@terminal", SqlDbType.NVarChar).Value = model.Terminal;
                command.Parameters.Add("@trandate", SqlDbType.DateTime).Value = model.Trandate;
                command.Parameters.Add("@isLooked", SqlDbType.Bit).Value = model.IsLooked;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private static List<BankAccountsViewModel> GetAllBankAccounts(int orgId)
        {
            var bankAccounts = new List<BankAccountsViewModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(Constants.AllBankAccounts, connection);
                command.Parameters.Add("@orgId", SqlDbType.Int).Value = orgId;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BankAccountsViewModel model = new BankAccountsViewModel
                        {
                            CardNumber = (string)reader["CardNumber"],
                            ExtractMerchantId = (int?)reader["ExtractMerchantId"],
                            ExtractMerchantPassword = (string)reader["ExtractMerchantPassword"]
                        };
                        bankAccounts.Add(model);
                    }
                }
            }
            return bankAccounts;
        }

        /// <summary>
        /// Import data from Privat24 for card from some date to now
        /// </summary>
        /// <param name="cardNumber">Privat merchant card number</param>
        /// <param name="merchantId">Merchant Id from Privat24</param>
        /// <param name="merchantPassword">Merchant password from Privat24</param>
        /// <param name="dateFrom"></param>
        public static async Task<string> Import(string cardNumber, string merchantId, string merchantPassword, DateTime dateFrom)
        {
            var appcodes = GetAllAppCodes();
            var response = await PrivatRequestAsync(cardNumber, merchantId, merchantPassword, dateFrom, DateTime.Now.Date);
            return await ResponseProcessing(response, appcodes);
            
        }
        
        /// <summary>
        /// Import data from Privat24 for card from some date to end date
        /// </summary>
        /// <param name="cardNumber">Privat merchant card number</param>
        /// <param name="merchantId">Merchant Id from Privat24</param>
        /// <param name="merchantPassword">Merchant password from Privat24</param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        public static async Task<string> Import(string cardNumber, string merchantId, string merchantPassword, DateTime dateFrom, DateTime dateTo)
        {
            var appcodes = GetAllAppCodes();
            var response = await PrivatRequestAsync(cardNumber, merchantId, merchantPassword, dateFrom, dateTo);
            return ResponseProcessing(response, appcodes).Result;
        }

        /// <summary>
        /// Import data from Privat24 for all accounts of Organization
        /// </summary>
        /// <param name="orgId">Organization Id </param>
        /// <param name="minutes">Interval (in minutes)</param>
        public static async Task Import(int orgId, int minutes)
        {
            DateTime dateTo = DateTime.Now;
            DateTime dateFrom = dateTo.AddMinutes(-minutes).Date;
            dateTo = dateTo.Date;
            var bankAccounts = GetAllBankAccounts(orgId);
            if (bankAccounts.Count == 0)
            {
                return;
            }
            var appcodes = GetAllAppCodes();
            var responses = bankAccounts
                .Select(bankAccount =>
                PrivatRequestAsync(bankAccount.CardNumber, bankAccount.ExtractMerchantId.ToString(), bankAccount.ExtractMerchantPassword, dateFrom, dateTo)).ToList();
            await Task.WhenAll(responses.ToArray());
            foreach (var task in responses)
            {
                ResponseProcessing(task.Result, appcodes);
            }
        }

        private static async Task<string> ResponseProcessing(string task, List<int> appcodes)
        {
            var response = task?.ParseXmlTo<Response.Response>();
            if (response?.Merchant == null)
            {
                return task;
            }

            var privatImport = response.Data.Info.Statements.Statement;
            if (privatImport == null)
            {
                return task;
            }

            privatImport = privatImport.Where(bi => !appcodes.Contains((int)bi.Appcode)).ToArray();
            if (privatImport.Length == 0)
            {
                return task;
            }

            var dataForImportDetails = PrivatToBankImportDetails(privatImport);
            foreach (var detail in dataForImportDetails)
            {
                AddBankImportDetail(detail);
            }
            return task;
        }

        private static List<BankImportDetailsViewModel> PrivatToBankImportDetails(IEnumerable<Statement> import)
        {
            return import.Select(bankImport => new BankImportDetailsViewModel()
            {
                AppCode = (int)bankImport.Appcode,
                Card = bankImport.Card.ToString(),
                Amount = bankImport.Amount,
                Description = bankImport.Description,
                Trandate = new DateTime(bankImport.Trandate.Year, bankImport.Trandate.Month, bankImport.Trandate.Day,
                                        bankImport.Trantime.Hour, bankImport.Trantime.Minute, bankImport.Trantime.Second),
                Terminal = bankImport.Terminal,
                CardAmount = bankImport.Cardamount,
                Rest = bankImport.Rest,
                IsLooked = false
            }).ToList();
        }
    }
}
