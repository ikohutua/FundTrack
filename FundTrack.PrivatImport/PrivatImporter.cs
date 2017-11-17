using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FundTrack.PrivatImport
{
    public static class PrivatImporter
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString;

        #region xml creating
        private static string ImportXmlData(string cardnumber, string merchantId, string password, DateTime dateFrom, DateTime dateTo)
        {
            const string xmlVersion = "1.0";
            const string encodingVersion = "UTF-8";
            const string startDate = "sd";
            const string endDate = "ed";
            const string card = "card";
            var data = string.Format(
    @"<oper>cmt</oper>
        <wait>0</wait>
        <test>0</test>  
        <payment id=\""\"">
            <prop name=\""{0}\"" value=\""{1}\""/>
            <prop name=\""{2}\"" value=\""{3}\""/>
            <prop name=\""{4}\"" value=\""{5}\""/>
        </payment>", startDate, dateFrom.ToShortDateString(), endDate, dateTo.ToShortDateString(), card, cardnumber);

            var sign = HashFunctions.CalculateSha1Hash(HashFunctions.CalculateMd5Hash(data + password));
            var body = string.Format(@"<?xml version={0} encoding={1}?>
<request version={0}>
    <merchant>
        <id>\"" {2}\""</id>
        <signature>\""{3}\""</signature>
    </merchant>
    <data>
        {4}
    </data>
</request>", xmlVersion, encodingVersion, merchantId, sign, data);

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
                var url = ConfigurationManager.AppSettings["PrivatApiUrl"];
                var response = await client.PostAsync(url, content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                return null;
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
                        BankAccountsViewModel model = new BankAccountsViewModel();
                        model.CardNumber = (string)reader["CardNumber"];
                        model.ExtractMerchantId = (int?)reader["ExtractMerchantId"];
                        model.ExtractMerchantPassword = (string)reader["ExtractMerchantPassword"];
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
        public static async Task Import(string cardNumber, string merchantId, string merchantPassword, DateTime dateFrom)
        {
            var appcodes = GetAllAppCodes();
            var response = await PrivatRequestAsync(cardNumber, merchantId, merchantPassword, dateFrom, DateTime.Now.Date);
            ResponseProcessing(response, appcodes);
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
            ResponseProcessing(response, appcodes);
            return response;
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

        private static void ResponseProcessing(string task, List<int> appcodes)
        {
            var response = task?.ParseXmlTo<Response>();
            if (response?.Merchant == null)
            {
                return;
            }

            var privatImport = response.Data.Info.Statements.Statement;
            if (privatImport == null)
            {
                return;
            }

            privatImport = privatImport.Where(bi => !appcodes.Contains((int)bi.Appcode)).ToArray();
            if (privatImport.Length == 0)
            {
                return;
            }

            var dataForImportDetails = PrivatToBankImportDetails(privatImport);
            foreach (var detail in dataForImportDetails)
            {
                AddBankImportDetail(detail);
            }
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
