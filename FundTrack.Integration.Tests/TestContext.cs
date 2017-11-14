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

        public TestContext()
        {
            var builder = new WebHostBuilder()
          .UseEnvironment("Testing")
          .UseStartup<TestStartup>();

            _server = new TestServer(builder);
            Client = _server.CreateClient();
        }

        public FundTrackContext GetNewDbContext()
        {
           return _server.Host.Services.GetService(typeof(FundTrackContext)) as FundTrackContext;
        }

        public void Dispose()
        {
            _server?.Dispose();
            Client?.Dispose();

        }
    }
}
