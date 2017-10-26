using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.PrivatImport
{
    public static class PrivatImporter
    {
        #region Md5/Sha1 cryptographic hash functions
        private static string CalculateMd5Hash(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            using (var md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(inputBytes);
                return HexStringFromBytes(hashBytes);
            }
        }

        private static string CalculateSha1Hash(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            using (var sha1 = SHA1.Create())
            {
                var hashBytes = sha1.ComputeHash(inputBytes);
                return HexStringFromBytes(hashBytes);
            }
        }

        private static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
        #endregion

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
            <prop name= " + startDate + @" value=" + "\"" + dateFrom.ToShortDateString() + @"""/>
            <prop name= " + endDate + @" value=" + "\"" + dateTo.ToShortDateString() + @"""/>
            <prop name= " + card + @" value=" + "\"" + cardnumber + @"""/>
        </payment>";

            var sign = CalculateSha1Hash(CalculateMd5Hash(data + password));
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

        /*  #region Serialized version(needs work)
          private static string MakeRequest(string cardnumber, uint merchantId, string password, DateTime satartDate, DateTime endDate)
          {
              var re = new request
              {
                  version = 1.0m
              };
              var data = new data
              {
                  oper = "cmt",
                  test = 0,
                  wait = 0
              };
              var payment = new payment
              {
                  id = ""
              };
              var prop = new prop[3];
              for (var i = 0; i < prop.Length; i++)
              {
                  prop[i] = new prop();
              }
              prop[0].name = "sd";
              prop[0].value = satartDate.ToShortDateString();
              prop[1].name = "ed";
              prop[1].value = endDate.ToShortDateString();
              prop[2].name = "card";
              prop[2].value = cardnumber;
              payment.prop = prop;
              data.payment = payment;
              var merchant = new merchant
              {
                  id = merchantId
              };
              var serializedData = data.Serialize();
              merchant.signature = CalculateSha1Hash(CalculateMd5Hash(serializedData + password));
              re.merchant = merchant;
              re.data = data;
              return re.Serialize();
          }

          #endregion*/

        private static async Task<string> PrivatRequestAsync(string cardNumber, string merchantId, string merchantPassword, DateTime dateFrom, DateTime dateTo)
        {
            var dataToBeSent = ImportXmlData(cardNumber, merchantId, merchantPassword, dateFrom, dateTo);
            var content = new StringContent(dataToBeSent);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("tetx/xml"));
                var responce = await client.PostAsync("https://api.privatbank.ua/p24api/rest_fiz ", content);
                if (responce.StatusCode == HttpStatusCode.OK)
                {
                    return await responce.Content.ReadAsStringAsync();
                }
                return null;
            }
        }

        private static List<BankAccounts> GetAllBankAccounts(int orgId, FundTrackSS context)
        {
            return context.BankAccounts.Where(ba => ba.OrgId == orgId
                                                            && ba.ExtractMerchantId != null
                                                            && ba.ExtractMerchantPassword != null
                                                            && ba.IsExtractEnabled == true).ToList();
        }

        /// <summary>
        /// Import data from Privat24 for card from some date to now
        /// </summary>
        /// <param name="cardNumber">Privat merchant card number</param>
        /// <param name="merchantId">Merchant Id from Privat24</param>
        /// <param name="merchantPassword">Merchant password from Privat24</param>
        /// <param name="dateFrom"></param>
        public static async void Import(string cardNumber, string merchantId, string merchantPassword, DateTime dateFrom)
        {
            var fundTrackEntitiesContext = new FundTrackSS();
            var responce = PrivatRequestAsync(cardNumber, merchantId, merchantPassword, dateFrom, DateTime.Now.Date);
            await Task.WhenAll(responce);
            ResponceProcessing(responce, fundTrackEntitiesContext);
        }
        /// <summary>
        /// Import data from Privat24 for card from some date to end date
        /// </summary>
        /// <param name="cardNumber">Privat merchant card number</param>
        /// <param name="merchantId">Merchant Id from Privat24</param>
        /// <param name="merchantPassword">Merchant password from Privat24</param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>

        public static async void Import(string cardNumber, string merchantId, string merchantPassword, DateTime dateFrom, DateTime dateTo)
        {
            var fundTrackEntitiesContext = new FundTrackSS();
            var responce = PrivatRequestAsync(cardNumber, merchantId, merchantPassword, dateFrom, dateTo);
            await Task.WhenAll(responce);
            ResponceProcessing(responce, fundTrackEntitiesContext);
        }
        /// <summary>
        /// Import data from Privat24 for all accounts of Organization
        /// </summary>
        /// <param name="orgId">Organization Id </param>
        /// <param name="minutes">Interval (in minutes)</param>
        public static async void Import(int orgId, int minutes)
        {
            var fundTrackEntitiesContext = new FundTrackSS();
            DateTime dateTo = DateTime.Now;
            DateTime dateFrom = dateTo.AddMinutes(-minutes).Date;
            dateTo = dateTo.Date;
            var bankAccounts = GetAllBankAccounts(orgId, fundTrackEntitiesContext);
            if (bankAccounts.Count == 0)
            {
                return;
            }

            var responses = bankAccounts
                .Select(bankAccount =>
                PrivatRequestAsync(bankAccount.CardNumber, bankAccount.ExtractMerchantId.ToString(), bankAccount.ExtractMerchantPassword, dateFrom, dateTo)).ToList();
            await Task.WhenAll(responses.ToArray());
            foreach (var task in responses)
            {
                ResponceProcessing(task, fundTrackEntitiesContext);
            }
        }

        private static void ResponceProcessing(Task<string> task, FundTrackSS context)
        {
            var response = task.Result?.ParseXmlTo<Response>();
            if (response?.Merchant == null)
            {
                return;
            }

            var privatImport = response.Data.Info.Statements.Statement;
            if (privatImport == null)
            {
                return;
            }

            var appcodes = context.BankImportDetails.Select(bid => bid.AppCode).ToList();
            privatImport = privatImport.Where(bi => !appcodes.Contains((int?)bi.Appcode)).ToArray();
            if (privatImport.Length == 0)
            {
                return;
            }

            var dataForImportDetails = PrivatToBankImportDetails(privatImport);
            foreach (var detail in dataForImportDetails)
            {
                context.BankImportDetails.Add(detail);
            }

            context.SaveChanges();
        }

        private static List<BankImportDetails> PrivatToBankImportDetails(IEnumerable<Statement> import)
        {
            return import.Select(bankImport => new BankImportDetails
            {
                AppCode = (int?)bankImport.Appcode,
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
