using FundTrack.DAL.Concrete;
using FundTrack.Integration.Tests.TestRepositories;
using FundTrack.WebUI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using FundTrack.DAL.Abstract;
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
        //public readonly IUnitOfWork _unitOfWork;
        public HttpClient Client { get; private set; }
        public TestTargetRepository TestTargetRepository { get; private set; }
        public TestIncomeReportRepository TestIncomeReportRepository { get; private set; }
        public TestFinOpRepository TestFinOpRepository { get; private set; }

        public TestContext()
        {
            var builder = new WebHostBuilder()
          .UseEnvironment("Testing")
          .UseStartup<TestStartup>();

            _server = new TestServer(builder);
            Client = _server.CreateClient();
            _dbContect = _server.Host.Services.GetService(typeof(FundTrackContext)) as FundTrackContext;
            //_unitOfWork = _server.Host.Services.GetService(typeof(IUnitOfWork)) as IUnitOfWork;

            TestTargetRepository = new TestTargetRepository(_dbContect);
            TestIncomeReportRepository = new TestIncomeReportRepository(_dbContect);
            TestFinOpRepository = new TestFinOpRepository(_dbContect);
        }

        public FundTrackContext GetClearDbContext()
        {
            _dbContect.Database.EnsureDeleted();
            return _dbContect;
        }       
    }
}
