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

        private static async Task<string> PrivatRequestAsync(BankAccounts bankAccount, DateTime dateFrom, DateTime dateTo)
        {

            var dataToBeSent = ImportXmlData(bankAccount.CardNumber, bankAccount.ExtractMerchantId.ToString(), bankAccount.ExtractMerchantPassword, new DateTime(2017,09,19), new DateTime(2017, 10, 19));
            //var datatobeSent = MakeRequest("4149497823331790", 130668, "0Iyb6lMa6cO8490h959L01PXG3eB5VF2", new DateTime(2017, 10, 01), new DateTime(2017, 10, 13));
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

        private static void AddBankImportDetails(BankImportDetails item, FundTrackSS context)
        {
            {
                context.BankImportDetails.Add(item);
            }
        }
        public static async void Run(int orgId, int minutes)
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

            var responses = bankAccounts.Select(bankAccount => PrivatRequestAsync(bankAccount, dateFrom, dateTo)).ToList();
            await Task.WhenAll(responses.ToArray());
            foreach (var task in responses)
            {
                var response = task.Result?.ParseXmlTo<Response>();
                if (response?.merchant == null)
                {
                    continue;
                }

                var privatImport = response.data.info.statements.statement;
                if (privatImport == null)
                {
                    continue;
                }
                var appcodes = fundTrackEntitiesContext.BankImportDetails.Select(bid => bid.AppCode).ToList();
                privatImport = privatImport.Where(bi => !appcodes.Contains((int?)bi.appcode)).ToArray();
                if (privatImport.Length == 0)
                {
                    continue;
                }
                var dataForImportDetails = PrivatToBankImportDetails(privatImport, fundTrackEntitiesContext);
                foreach (var detail in dataForImportDetails)
                {
                    AddBankImportDetails(detail, fundTrackEntitiesContext);
                }
                fundTrackEntitiesContext.SaveChanges();
            }
        }

        private static List<BankImportDetails> PrivatToBankImportDetails(IEnumerable<Statement> import, FundTrackSS context)
        {
            return import.Select(bankImport => new BankImportDetails
            {
                AppCode = (int?)bankImport.appcode,
                Card = bankImport.card.ToString(),
                Amount = bankImport.amount,
                Description = bankImport.description,
                Trandate = new DateTime(bankImport.trandate.Year, bankImport.trandate.Month, bankImport.trandate.Day,
                                        bankImport.trantime.Hour, bankImport.trantime.Minute, bankImport.trantime.Second),
                Terminal = bankImport.terminal,
                CardAmount = bankImport.cardamount,
                Rest = bankImport.rest,
                IsLooked = false
            }).ToList();
        }
    }
}
