using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;
using FundTrack.WebUI.Middlewares.Logging;
using Microsoft.Extensions.Logging;

namespace FundTrack.WebUI.Middlewares
{
    public sealed class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IErrorLogger _logging;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, IErrorLogger logging, ILogger<GlobalErrorHandlingMiddleware> logger)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
        {
            _next = next;
            _logging = logging;
            _logger = logger;
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

                _logging.WriteErrorLogInFile(ex);
                _logger.LogError(ex.ToString());
                

                await context.Response.WriteAsync(json);
            }
        }
    }
}
