using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Threading.Tasks;


namespace FundTrack.WebUI.Middlewares.Logging
{
    public sealed class ErrorLogger: IErrorLogger
    {
        public void WriteLogInFile(Exception ex)
        {
            string path = @"C:\FundTrack\FundTrack.WebUI\Middlewares\Logging\" + DateTime.Now.Date + "_Exception_Logs.txt";
            {
                using (var file = new FileStream(path, FileMode.Append))
                using (var fw = new StreamWriter(file))
                {
                    string log = "Date and time:  " + DateTime.Now + "\n" + "Error message:  " + ex.Message +
                        "\n" + "Source:  " + ex.Source + "\n" + "InnerException:  " + ex.InnerException +
                        "\n" + "StackTrace:  " + ex.StackTrace + "\n" +
                         new string('_', 50) + "\n";
                    fw.WriteLine(log);
                }
            }
        }
    }
}