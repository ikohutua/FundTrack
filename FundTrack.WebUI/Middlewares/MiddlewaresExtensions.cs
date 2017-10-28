using Microsoft.AspNetCore.Builder;


namespace FundTrack.WebUI.Middlewares
{
    public static class GlobalErrorHandlingMiddlewareExtensions
    {
        /// <summary>
        /// Use global error handling as middleware
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalErrorHandling( this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorHandlingMiddleware>();
        }
    }
}
