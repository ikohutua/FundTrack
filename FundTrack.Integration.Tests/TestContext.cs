using FundTrack.DAL.Concrete;
using FundTrack.Integration.Tests.TestRepositories;
using FundTrack.WebUI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FundTrack.Integration.Tests
{
    public class TestContext 
    {
        private TestServer _server;
        private FundTrackContext _dbContect;
        public HttpClient Client { get; private set; }
        public TestTargetRepository TestTargetRepository { get; private set; }

        public TestContext()
        {
            var builder = new WebHostBuilder()
          .UseEnvironment("Testing")
          .UseStartup<TestStartup>();

            _server = new TestServer(builder);
            Client = _server.CreateClient();
            _dbContect = _server.Host.Services.GetService(typeof(FundTrackContext)) as FundTrackContext;

            TestTargetRepository = new TestTargetRepository(_dbContect);
        }

        public FundTrackContext GetClearDbContext()
        {
            _dbContect.Database.EnsureDeleted();
            return _dbContect;
        }       
    }
}
