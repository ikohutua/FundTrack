using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xunit;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using FundTrack.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.Integration.Tests.Controllers
{
    public class FinOpControllerTests
    {
        private readonly TestContext _testContext;
        public FinOpControllerTests()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public async Task Get_FinOps_By_Id_Ok_Response()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            int testFinOpId = 1;
            var testFinOp = _testContext.TestFinOpRepository.GetTestFinOps().Where(f => f.Id == testFinOpId).FirstOrDefault();
            _testContext.TestFinOpRepository.AddFinOpToDb(testFinOp);

            //Act
            var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpsById/{testFinOpId}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultFinOp = new JsonSerializer().Deserialize<FinOpViewModel>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.NotNull(resultFinOp);
            Assert.Equal(testFinOp.Id, resultFinOp.Id);
            Assert.Equal(testFinOp.AccFromId.GetValueOrDefault(0), resultFinOp.AccFromId);
            Assert.Equal(testFinOp.AccToId.GetValueOrDefault(0), resultFinOp.AccToId);
            Assert.Equal(testFinOp.Amount, resultFinOp.Amount);
            Assert.Equal(testFinOp.Description, resultFinOp.Description);
            Assert.Equal(testFinOp.FinOpDate, resultFinOp.Date);
            Assert.Equal(testFinOp.FinOpType, resultFinOp.FinOpType);
            Assert.Equal(testFinOp.DonationId, resultFinOp.DonationId);
        }

        [Fact]
        public async Task Get_FinOps_By_Id_Wrong_Id()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            int wrongId = 100;
            var testFinOps = _testContext.TestFinOpRepository.GetTestFinOps();
            _testContext.TestFinOpRepository.AddRangeOfFinOpsToDb(testFinOps);

            //Act
            var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpsById/{wrongId}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultFinOp = new JsonSerializer().Deserialize<FinOpViewModel>(new JsonTextReader(reader));

            //Assert
            Assert.True(HttpStatusCode.InternalServerError == response.StatusCode);
        }

        [Fact]
        public async Task Get_FinOps_By_OrgAccountId_Ok_Response()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            int testOrgAccountId = 1;
            var testFinOps = _testContext.TestFinOpRepository.GetTestFinOps();
            _testContext.TestFinOpRepository.AddRangeOfFinOpsToDb(testFinOps);

            var testOrgAccounts = _testContext.TestFinOpRepository.GetTestOrgAccounts();
            _testContext.TestFinOpRepository.AddRangeOfOrgAccountsToDb(testOrgAccounts);

            var testBalances = _testContext.TestFinOpRepository.GetTestBalances();
            _testContext.TestFinOpRepository.AddRangeOfBalancesToDb(testBalances);

            //Act
            var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpsByOrgAccId/{testOrgAccountId}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultFinOps = new JsonSerializer().Deserialize<IEnumerable<FinOpViewModel>>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.NotNull(resultFinOps);
            Assert.True(resultFinOps.Count() == 6);
            Assert.False(resultFinOps.Count() == 8);
            Assert.True(resultFinOps.Where(f => f.IsEditable == true).Count() == 3);
        }

        [Fact]
        public async Task Get_FinOps_By_OrgAccountId_Wrong_Id()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            int testOrgAccountId = 100;
            var testFinOps = _testContext.TestFinOpRepository.GetTestFinOps();
            _testContext.TestFinOpRepository.AddRangeOfFinOpsToDb(testFinOps);

            var testOrgAccounts = _testContext.TestFinOpRepository.GetTestOrgAccounts();
            _testContext.TestFinOpRepository.AddRangeOfOrgAccountsToDb(testOrgAccounts);

            var testBalances = _testContext.TestFinOpRepository.GetTestBalances();
            _testContext.TestFinOpRepository.AddRangeOfBalancesToDb(testBalances);

            //Act
            var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpsByOrgAccId/{testOrgAccountId}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultFinOps = new JsonSerializer().Deserialize<IEnumerable<FinOpViewModel>>(new JsonTextReader(reader));

            //Assert

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.Empty(resultFinOps);
        }

        [Fact]
        public async Task Get_FinOps_By_OrgAccountId_For_Page_Ok_Response()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            int testOrgAccountId = 1;
            int testFinOpType = 1;
            int testCurrentPage = 1;
            int testPageSize = 5;
            var testFinOps = _testContext.TestFinOpRepository.GetTestFinOps();
            _testContext.TestFinOpRepository.AddRangeOfFinOpsToDb(testFinOps);

            var testOrgAccounts = _testContext.TestFinOpRepository.GetTestOrgAccounts();
            _testContext.TestFinOpRepository.AddRangeOfOrgAccountsToDb(testOrgAccounts);

            var testBalances = _testContext.TestFinOpRepository.GetTestBalances();
            _testContext.TestFinOpRepository.AddRangeOfBalancesToDb(testBalances);

            //Act
            var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpsByIdForPage/{testOrgAccountId}/{testFinOpType}" + "?currentPage=" + testCurrentPage + "&pageSize=" + testPageSize);
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultFinOps = new JsonSerializer().Deserialize<IEnumerable<FinOpViewModel>>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.NotNull(resultFinOps);
            Assert.True(resultFinOps.Count() == 3);
            Assert.False(resultFinOps.Count() == 8);
            Assert.True(resultFinOps.Where(f => f.IsEditable == true).Count() == 2);
        }

        [Fact]
        public async Task Get_FinOps_By_OrgAccountId_For_Page_Wrong_Id()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            int testOrgAccountId = 100;
            int testFinOpType = 1;
            int testCurrentPage = 1;
            int testPageSize = 5;
            var testFinOps = _testContext.TestFinOpRepository.GetTestFinOps();
            _testContext.TestFinOpRepository.AddRangeOfFinOpsToDb(testFinOps);

            var testOrgAccounts = _testContext.TestFinOpRepository.GetTestOrgAccounts();
            _testContext.TestFinOpRepository.AddRangeOfOrgAccountsToDb(testOrgAccounts);

            var testBalances = _testContext.TestFinOpRepository.GetTestBalances();
            _testContext.TestFinOpRepository.AddRangeOfBalancesToDb(testBalances);

            //Act
            var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpsByIdForPage/{testOrgAccountId}/{testFinOpType}" + "?currentPage=" + testCurrentPage + "&pageSize=" + testPageSize);
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultFinOps = new JsonSerializer().Deserialize<IEnumerable<FinOpViewModel>>(new JsonTextReader(reader));

            //Assert

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.Empty(resultFinOps);
        }

        [Fact]
        public async Task Get_FinOps_Init_Data_Ok_Response()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            int testOrgAccountId = 1;
            var testFinOps = _testContext.TestFinOpRepository.GetTestFinOps();
            _testContext.TestFinOpRepository.AddRangeOfFinOpsToDb(testFinOps);

            //Act
            var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpInitData/{testOrgAccountId}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultData = new JsonSerializer().Deserialize<IEnumerable<int>>(new JsonTextReader(reader)).ToList();

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.NotNull(resultData);
            Assert.True(resultData.Count() == 4);
            Assert.True(resultData[0] == resultData[1] + resultData[2] + resultData[3]);
            Assert.True(resultData[1] == 1);
            Assert.True(resultData[2] == 3);
            Assert.True(resultData[3] == 2);
        }

        [Fact]
        public async Task Get_FinOps_Init_Data_Wrong_Id()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            int testOrgAccountId = 100;
            var testFinOps = _testContext.TestFinOpRepository.GetTestFinOps();
            _testContext.TestFinOpRepository.AddRangeOfFinOpsToDb(testFinOps);

            //Act
            var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpInitData/{testOrgAccountId}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultData = new JsonSerializer().Deserialize<IEnumerable<int>>(new JsonTextReader(reader)).ToList();

            //Assert

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.True(resultData[1] == 0);
            Assert.True(resultData[2] == 0);
            Assert.True(resultData[3] == 0);
        }

        [Fact]
        public async Task Create_Income_Operation_Ok_Response()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            FinOpViewModel incomeModel = new FinOpViewModel() {Amount = 100, AccToId = 1, Description = "TestAddDescription", TargetId = 1, Date = new DateTime(2017, 11, 5), FinOpType = 1, UserId = 1 };
            var notModifiedOrgAccount =_testContext.TestFinOpRepository.GetTestOrgAccountById(1);

            var testOrgAccounts = _testContext.TestFinOpRepository.GetTestOrgAccounts();
            _testContext.TestFinOpRepository.AddRangeOfOrgAccountsToDb(testOrgAccounts);

            //Act
            var content = new StringContent(JsonConvert.SerializeObject(incomeModel), Encoding.UTF8, "application/json");
            var response = await _testContext.Client.PostAsync($"/api/FinOp/Income", content);
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultFinOps = new JsonSerializer().Deserialize<FinOpViewModel>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode(); 

            var modifiedOrgAccount = dbContext.OrgAccounts.Where(acc => acc.Id == 1).FirstOrDefault();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.NotNull(resultFinOps);
            Assert.True(dbContext.FinOps.Count() == 1);
            Assert.False(dbContext.FinOps.Count() == 8);
            Assert.True(modifiedOrgAccount.CurrentBalance == 1100);
            Assert.NotEqual(notModifiedOrgAccount, modifiedOrgAccount);
            Assert.True(modifiedOrgAccount.CurrentBalance == notModifiedOrgAccount.CurrentBalance + resultFinOps.Amount);
        }

        //    [Fact]
        //    public async Task Create_Spending_Operation_Ok_Response()
        //    {
        //        //Arrange
        //        var dbContext = _testContext.GetClearDbContext();

        //        var testImagesForInsert = new List<FinOpImageViewModel>
        //        {
        //            new FinOpImageViewModel() {Base64Data = "Cobra", imageExtension = "jpeg"},
        //            new FinOpImageViewModel() {Base64Data = "Python", imageExtension = "jpeg"},
        //        };
        //        FinOpViewModel spendingModel = new FinOpViewModel() { Amount = 100, AccFromId = 1, Description = "TestSpendingDescription", TargetId = 1, Date = new DateTime(2017, 11, 3), FinOpType = 0, Images = testImagesForInsert, UserId = 1 };
        //        var notModifiedOrgAccount = _testContext.TestFinOpRepository.GetTestOrgAccountById(1);
        //        var testOrgAccounts = _testContext.TestFinOpRepository.GetTestOrgAccounts();
        //        _testContext.TestFinOpRepository.AddRangeOfOrgAccountsToDb(testOrgAccounts);


        //        //Act
        //        var content = new StringContent(JsonConvert.SerializeObject(spendingModel), Encoding.UTF8, "application/json");
        //        var response = await _testContext.Client.PostAsync($"/api/FinOp/Spending", content);
        //        var stream = await response.Content.ReadAsStreamAsync();
        //        var reader = new StreamReader(stream, Encoding.UTF8);
        //        var resultFinOps = new JsonSerializer().Deserialize<FinOpViewModel>(new JsonTextReader(reader));

        //        //Assert
        //        response.EnsureSuccessStatusCode();

        //        var modifiedOrgAccount = dbContext.OrgAccounts.Where(acc => acc.Id == 1).FirstOrDefault();

        //        Assert.True(HttpStatusCode.OK == response.StatusCode);
        //        Assert.NotNull(resultFinOps);
        //        Assert.True(dbContext.FinOps.Count() == 1);
        //        Assert.False(dbContext.FinOps.Count() == 8);
        //        Assert.True(modifiedOrgAccount.CurrentBalance == 900);
        //        Assert.NotEqual(notModifiedOrgAccount, modifiedOrgAccount);
        //        Assert.True(modifiedOrgAccount.CurrentBalance == notModifiedOrgAccount.CurrentBalance - resultFinOps.Amount);
        //    }

        [Fact]
        public async Task Create_Transfer_Operation_Ok_Response()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            FinOpViewModel transferModel = new FinOpViewModel() { Amount = 100, AccFromId = 1, AccToId = 2, Description = "TestTransferDescription", TargetId = null, Date = new DateTime(2017, 11, 3), FinOpType = 2, UserId = 1 };
            var notModifiedOrgAccountFirst = _testContext.TestFinOpRepository.GetTestOrgAccountById(1);
            var notModifiedOrgAccountSecond = _testContext.TestFinOpRepository.GetTestOrgAccountById(2);

            var testOrgAccounts = _testContext.TestFinOpRepository.GetTestOrgAccounts();
            _testContext.TestFinOpRepository.AddRangeOfOrgAccountsToDb(testOrgAccounts);

            //Act
            var content = new StringContent(JsonConvert.SerializeObject(transferModel), Encoding.UTF8, "application/json");
            var response = await _testContext.Client.PostAsync($"/api/FinOp/Transfer", content);
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultFinOps = new JsonSerializer().Deserialize<FinOpViewModel>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            var modifiedOrgAccountFirst = dbContext.OrgAccounts.Where(acc => acc.Id == 1).FirstOrDefault();
            var modifiedOrgAccountSecond = dbContext.OrgAccounts.Where(acc => acc.Id == 2).FirstOrDefault();


            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.NotNull(resultFinOps);
            Assert.True(dbContext.FinOps.Count() == 1);
            Assert.False(dbContext.FinOps.Count() == 8);
            Assert.True(modifiedOrgAccountFirst.CurrentBalance == 900);
            Assert.True(modifiedOrgAccountSecond.CurrentBalance == 1100);
            Assert.NotEqual(notModifiedOrgAccountFirst, modifiedOrgAccountFirst);
            Assert.NotEqual(notModifiedOrgAccountSecond, modifiedOrgAccountSecond);
            Assert.True(modifiedOrgAccountFirst.CurrentBalance == notModifiedOrgAccountFirst.CurrentBalance - resultFinOps.Amount);
            Assert.True(modifiedOrgAccountSecond.CurrentBalance == notModifiedOrgAccountSecond.CurrentBalance + resultFinOps.Amount);
        }

    }
}
