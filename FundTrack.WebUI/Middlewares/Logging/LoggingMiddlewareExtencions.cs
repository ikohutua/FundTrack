using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;


namespace FundTrack.WebUI.Middlewares.Logging
{
    public static class LoggingExtencions
    {
        /// <summary>
        /// Use global error handling as middleware
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder LoggingHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}