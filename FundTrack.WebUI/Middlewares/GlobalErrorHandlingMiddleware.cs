using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;
using FundTrack.WebUI.Middlewares.Logging;
using System.Data;
using System.Diagnostics;

namespace FundTrack.WebUI.Middlewares
{
    public sealed class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LoggingMiddleware _logging;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
            //_logging = logging;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var response = new
                {
                    Message = ex.Message
                };

                var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                //_logging.WriteLogInFile(ex);
                LoggingMiddleware logging = new LoggingMiddleware();
                logging.WriteLogInFile(ex);
                

                await context.Response.WriteAsync(json);
            }
        }
    }
}
