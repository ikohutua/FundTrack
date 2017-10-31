﻿using Microsoft.AspNetCore.Builder;


namespace FundTrack.WebUI.Middlewares
{
    public static class MiddlewaresExtensions
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

        /// <summary>
        /// Custom autorization middlewar
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMyAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyAuthorize>();
        }
    }
}
