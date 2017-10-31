﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FundTrack.WebUI.Middlewares
{
    public class MyAuthorize
    {
        private readonly RequestDelegate _next;
        public MyAuthorize(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var auth = httpContext.Request?.Headers["Authorization"];
            var token = auth?.ToString();
            //Debug.WriteLine(auth);
            //Debug.WriteLine(a);
            if (!string.IsNullOrEmpty(token))
            {
                var authorizeTicket = new SecureTokenFormatter().Unprotect(token);
            }

            await _next(httpContext);
        }
    }

    public class SecureTokenFormatter
    {
        private TicketSerializer serializer;

        public SecureTokenFormatter()
        {
            serializer = new TicketSerializer();
        }

        public string Protect(AuthenticationTicket ticket)
        {
            var ticketData = serializer.Serialize(ticket);
            var protectedString = Convert.ToBase64String(ticketData);
            return protectedString;
        }

        public AuthenticationTicket Unprotect(string text)
        {
            var protectedData = Convert.FromBase64String(text);
            var ticket = serializer.Deserialize(protectedData);
            return ticket;
        }
    }
}
