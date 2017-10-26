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
        public void WriteErrorLogInFile(Exception ex)
        {
            string path = @"C:\FundTrack\FundTrack.WebUI\Middlewares\Logging\" + DateTime.Now.Day + "."
                + DateTime.Now.Month + "." + DateTime.Now.Year + "_Exception_Logs.txt";
            {
                using (var file = new FileStream(path, FileMode.Append))
                using (var fw = new StreamWriter(file))
                {
                    string log = "Date and time:  " + DateTime.Now + "\r\n" + "Error message:  " + ex.Message +
                        "\r\n" + "Source:  " + ex.Source + "\r\n" + "InnerException:  " + ex.InnerException +
                        "\r\n" + "StackTrace:  " + ex.StackTrace + "\r\n" +
                         new string('_', 140) + "\r\n";
                    fw.WriteLine(log);
                }
            }
        }
    }
}