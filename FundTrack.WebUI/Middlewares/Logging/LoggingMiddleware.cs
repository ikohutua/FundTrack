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
    public sealed class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        //public LoggingMiddleware(RequestDelegate next)
        //{
        //    _next = next;
        //}

        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        await _next.Invoke(context);
        //    }
        //    catch (Exception ex)
        //    {
        //        context.Response.ContentType = "application/json";
        //        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        //        var response = new
        //        {
        //            Message = ex.Message
        //        };

        //        var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
        //        {
        //            ContractResolver = new CamelCasePropertyNamesContractResolver()
        //        });


        //        await context.Response.WriteAsync(json);
        //    }
        //}

        public void WriteLogInFile(Exception ex)
        {
            string path = @"C:\FundTrack\FundTrack.WebUI\Middlewares\Logging\Exception_Logs.txt";
            //try
            {
                using (var fs = new FileStream(path, FileMode.Append))
                using (var fw = new StreamWriter(fs))
                {
                    fw.WriteLine("Date and time:  " + DateTime.Now);
                    fw.WriteLine("Error message:  " + ex.Message);
                    fw.WriteLine("Source:  " + ex.Source);
                    fw.WriteLine("InnerException:  " + ex.InnerException);
                    fw.WriteLine();
                    fw.WriteLine("StackTrace:  " + ex.StackTrace);
                    fw.WriteLine("-------------------------------------------------------------------------------------");
                    fw.WriteLine();
                }
            }
        }
    }
}