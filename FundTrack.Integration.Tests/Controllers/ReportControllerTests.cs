using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FundTrack.Integration.Tests.Controllers
{
    public class ReportControllerTests
    {
        private readonly TestContext _testContext;
        public readonly string _route = "/api/reports/";

        public ReportControllerTests()
        {
            _testContext = new TestContext();
        }

     

        [Fact]
        public async Task Get_Income_Report_Test()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();
            var testTargets = _testContext.TestIncomeReportRepository.GetTestTargets();
            var testFinOps = _testContext.TestIncomeReportRepository.GetTestFinOps();
            var testOrgAccounts = _testContext.TestIncomeReportRepository.GetTestOrgAccounts();

            _testContext.TestIncomeReportRepository.AddTargetsToDb(testTargets);
            _testContext.TestIncomeReportRepository.AddFinOpsToDb(testFinOps);
            _testContext.TestIncomeReportRepository.AddOrgAccountsToDb(testOrgAccounts);


            //Act
            var response = await _testContext.Client.GetAsync($"{_route}IncomeReport/1?datefrom=2017-09-08&dateto=2017-09-30");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultReport = new JsonSerializer().Deserialize<IEnumerable<ReportIncomeViewModel>>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.NotNull(resultReport);
            Assert.True(resultReport.Count() == 13);
          
        }
    }
}
