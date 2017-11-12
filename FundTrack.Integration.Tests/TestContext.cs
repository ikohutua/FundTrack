using FundTrack.DAL.Concrete;
using FundTrack.WebUI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FundTrack.Integration.Tests
{
    public class TestContext : IDisposable
    {
        private TestServer _server;
        public HttpClient Client { get; private set; }
        public FundTrackContext DbContext { get; private set; }

        public TestContext()
        {
            var builder = new WebHostBuilder()
          .UseEnvironment("Testing")
          .UseStartup<Startup>();

            _server = new TestServer(builder);
            DbContext = _server.Host.Services.GetService(typeof(FundTrackContext)) as FundTrackContext;
            Client = _server.CreateClient();
        }

        public void Dispose()
        {
            _server?.Dispose();
            Client?.Dispose();
            DbContext?.Dispose();
        }
    }
}
