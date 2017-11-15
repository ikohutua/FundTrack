using FundTrack.DAL.Concrete;
using Microsoft.EntityFrameworkCore;
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
using FinOp = FundTrack.DAL.Entities.FinOp;
using OrgAccount = FundTrack.DAL.Entities.OrgAccount;
using Balance = FundTrack.DAL.Entities.Balance;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using FundTrack.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using FundTrack.BLL.Abstract;

namespace FundTrack.Integration.Tests.Controllers
{
    public class FinOpControllerTests
    {
        private readonly TestContext _testContext;
        //private readonly IFinOpService finOpService;
        //private readonly IOrganizationStatisticsService organizationStatisticsService;

        public FinOpControllerTests()
        {
            _testContext = new TestContext();
        }

        private IQueryable<FinOp> GetTestFinOps()
        {
            return new List<FinOp>()
            {
                new FinOp() {Id = 1, AccFromId = 1, AccToId = null, Amount = 100, Description = "Description1", FinOpDate = new DateTime(2017,11,1), DonationId = null, TargetId = 1, UserId = 1, FinOpType = 0},
                new FinOp() {Id = 2, AccFromId = 2, AccToId = null, Amount = 200, Description = "Description2", FinOpDate = new DateTime(2017,11,2), DonationId = null, TargetId = 2, UserId = 2, FinOpType = 0},
                new FinOp() {Id = 3, AccFromId = null, AccToId = 1, Amount = 300, Description = "Description3", FinOpDate = new DateTime(2017,11,3), DonationId = 1, TargetId = 1, UserId = 3, FinOpType = 1},
                new FinOp() {Id = 4, AccFromId = null, AccToId = 2, Amount = 250, Description = "Description4", FinOpDate = new DateTime(2017,11,1), DonationId = 2, TargetId = 2, UserId = 2, FinOpType = 1},
                new FinOp() {Id = 5, AccFromId = 1, AccToId = 2, Amount = 100, Description = "Description5", FinOpDate = new DateTime(2017,11,2), DonationId = null, TargetId = null, UserId = 3, FinOpType = 2},
                new FinOp() {Id = 6, AccFromId = 2, AccToId = 1, Amount = 400, Description = "Description6", FinOpDate = new DateTime(2017,11,4), DonationId = null, TargetId = null, UserId = 2, FinOpType = 2},
                new FinOp() {Id = 7, AccFromId = null, AccToId = 3, Amount = 220, Description = "Description7", FinOpDate = new DateTime(2017,11,1), DonationId = 3, TargetId = 2, UserId = 2, FinOpType = 1},
                new FinOp() {Id = 8, AccFromId = 3, AccToId = null, Amount = 200, Description = "Description8", FinOpDate = new DateTime(2017,11,2), DonationId = null, TargetId = 2, UserId = 2, FinOpType = 0},
                new FinOp() {Id = 9, AccFromId = null, AccToId = 1, Amount = 100, Description = "Description9", FinOpDate = new DateTime(2017,11,4), DonationId = null, TargetId = 1, UserId = 3, FinOpType = 1},
                new FinOp() {Id = 10, AccFromId = null, AccToId = 1, Amount = 300, Description = "Description10", FinOpDate = new DateTime(2017,11,1), DonationId = null, TargetId = 1, UserId = 3, FinOpType = 1},
            }.AsQueryable();
        }

        private IQueryable<OrgAccount> GetTestOrgAccounts()
        {
            return new List<OrgAccount>()
            {
                new OrgAccount() {Id = 1, AccountType = "Готівка", BankAccId = null, CurrencyId = 2, CurrentBalance = 1000, Description = "Description1", OrgAccountName = "Рахунок1", OrgId = 1, TargetId = 1, CreationDate = new DateTime(2017,10,28), UserId = 1 },
                new OrgAccount() {Id = 2, AccountType = "Готівка", BankAccId = null, CurrencyId = 2, CurrentBalance = 1000, Description = "Description2", OrgAccountName = "Рахунок2", OrgId = 1, TargetId = 2, CreationDate = new DateTime(2017,10,29), UserId = 2 },
                new OrgAccount() {Id = 3, AccountType = "Готівка", BankAccId = null, CurrencyId = 2, CurrentBalance = 1000, Description = "Description3", OrgAccountName = "Рахунок3", OrgId = 1, TargetId = 1, CreationDate = new DateTime(2017,10,28), UserId = 2 },
                new OrgAccount() {Id = 4, AccountType = "Готівка", BankAccId = null, CurrencyId = 2, CurrentBalance = 1000, Description = "Description4", OrgAccountName = "Рахунок4", OrgId = 2, TargetId = 2, CreationDate = new DateTime(2017,10,30), UserId = 4 }

            }.AsQueryable();
        }

        private IQueryable<Balance> GetTestBalances()
        {
            return new List<Balance>()
            {
                new Balance() {Id = 1, Amount = 1000, BalanceDate = new DateTime(2017,11,2), OrgAccountId = 1},
                new Balance() {Id = 2, Amount = 1000, BalanceDate = new DateTime(2017,11,1), OrgAccountId = 2},
                new Balance() {Id = 3, Amount = 1000, BalanceDate = new DateTime(2017,11,3), OrgAccountId = 3},
                new Balance() {Id = 4, Amount = 1000, BalanceDate = new DateTime(2017,11,1), OrgAccountId = 4}

            }.AsQueryable();
        }

        public FinOp GetTestFinOpsById(int id)
        {
            return GetTestFinOps().Where(f => f.Id == id).Single();
        }

        [Fact]
        public async Task Get_FinOps_By_Id_Ok_Response()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            var testFinOp = GetTestFinOpsById(1);
            dbContext.FinOps.AddRange(GetTestFinOps());
            dbContext.SaveChanges();

            //Act
            var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpsById/{testFinOp.Id}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultFinOp = new JsonSerializer().Deserialize<FinOpViewModel>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
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
        public async Task Get_FinOps_By_OrgAccountId_Ok_Response()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            int testOrgAccountId = 1;
            dbContext.FinOps.AddRange(GetTestFinOps());
            dbContext.OrgAccounts.AddRange(GetTestOrgAccounts());
            dbContext.Balances.AddRange(GetTestBalances());
            dbContext.SaveChanges();

            //Act
            var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpsByOrgAccId/{testOrgAccountId}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultFinOps = new JsonSerializer().Deserialize<IEnumerable<FinOpViewModel>>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.True(resultFinOps.Count() == 6);
            Assert.False(resultFinOps.Count() == 8);
            Assert.True(resultFinOps.Where(f => f.IsEditable == true).Count() == 3);
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
            dbContext.FinOps.AddRange(GetTestFinOps());
            dbContext.OrgAccounts.AddRange(GetTestOrgAccounts());
            dbContext.Balances.AddRange(GetTestBalances());
            dbContext.SaveChanges();

            //Act
            var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpsByIdForPage/{testOrgAccountId}/{testFinOpType}" + "?currentPage=" + testCurrentPage + "&pageSize=" + testPageSize);
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultFinOps = new JsonSerializer().Deserialize<IEnumerable<FinOpViewModel>>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.True(resultFinOps.Count() == 3);
            Assert.False(resultFinOps.Count() == 8);
            Assert.True(resultFinOps.Where(f => f.IsEditable == true).Count() == 2);
        }

        [Fact]
        public async Task Get_FinOps_Init_Data_Ok_Response()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            int testOrgAccountId = 1;
            dbContext.FinOps.AddRange(GetTestFinOps());
            dbContext.OrgAccounts.AddRange(GetTestOrgAccounts());
            dbContext.Balances.AddRange(GetTestBalances());
            dbContext.SaveChanges();

            //Act
            var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpInitData/{testOrgAccountId}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultData = new JsonSerializer().Deserialize<IEnumerable<int>>(new JsonTextReader(reader)).ToList();

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.True(resultData.Count() == 4);
            Assert.True(resultData[0] == resultData[1] + resultData[2] + resultData[3]);
            Assert.True(resultData[1] == 1);
            Assert.True(resultData[2] == 3);
            Assert.True(resultData[3] == 2);
        }

        [Fact]
        public async Task Create_Income_Operation_Ok_Response()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            FinOpViewModel incomeModel = new FinOpViewModel() {Amount = 100, AccToId = 1, Description = "TestAddDescription", TargetId = 1, Date = new DateTime(2017, 11, 5), FinOpType = 1, UserId = 1 };
            dbContext.FinOps.AddRange(GetTestFinOps());
            dbContext.OrgAccounts.AddRange(GetTestOrgAccounts());
            dbContext.SaveChanges();

            //Act
            var content = new StringContent(JsonConvert.SerializeObject(incomeModel), Encoding.UTF8, "application/json");
            var response = await _testContext.Client.PostAsync($"/api/FinOp/Income", content);
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultFinOps = new JsonSerializer().Deserialize<IEnumerable<FinOpViewModel>>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.True(resultFinOps.Count() == 7);
            Assert.False(resultFinOps.Count() == 8);
            Assert.True(resultFinOps.Where(f => f.IsEditable == true).Count() == 4);
        }
    }
}
