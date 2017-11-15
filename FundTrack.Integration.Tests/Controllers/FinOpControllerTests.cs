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
                new FinOp() {Id = 2, AccFromId = 2, AccToId = null, Amount = 200, Description = "Description2", FinOpDate = new DateTime(2017,11,2), DonationId = 1, TargetId = 2, UserId = 2, FinOpType = 0},
                new FinOp() {Id = 3, AccFromId = null, AccToId = 1, Amount = 300, Description = "Description3", FinOpDate = new DateTime(2017,11,3), DonationId = null, TargetId = 1, UserId = 3, FinOpType = 1},
                new FinOp() {Id = 4, AccFromId = null, AccToId = 2, Amount = 250, Description = "Description4", FinOpDate = new DateTime(2017,11,1), DonationId = 2, TargetId = 2, UserId = 2, FinOpType = 1},
                new FinOp() {Id = 5, AccFromId = 1, AccToId = 2, Amount = 100, Description = "Description5", FinOpDate = new DateTime(2017,11,2), DonationId = null, TargetId = null, UserId = 3, FinOpType = 2},
                new FinOp() {Id = 6, AccFromId = 2, AccToId = 1, Amount = 400, Description = "Description6", FinOpDate = new DateTime(2017,11,4), DonationId = null, TargetId = null, UserId = 2, FinOpType = 2},
                new FinOp() {Id = 7, AccFromId = null, AccToId = 3, Amount = 220, Description = "Description7", FinOpDate = new DateTime(2017,11,1), DonationId = 3, TargetId = 2, UserId = 2, FinOpType = 1},
                new FinOp() {Id = 8, AccFromId = 3, AccToId = null, Amount = 200, Description = "Description8", FinOpDate = new DateTime(2017,11,2), DonationId = null, TargetId = 1, UserId = 1, FinOpType = 0},
            }.AsQueryable();
        }

        //public void Initialize()
        //{
        //    var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        //    {
        //        new Claim(ClaimTypes.Name, "UserName"),
        //        new Claim(ClaimTypes.Role, "Admin")
        //    }));

        //    var _ctrl = new FinOpControllerTests();
        //    _ctrl.ControllerContext = new FinOpController(finOpService, organizationStatisticsService)
        //    {            
        //        HttpContext = new DefaultHttpContext() { User = user }
        //    };
        //}

        public FinOp GetTestFinOpsById(int id)
        {
            return GetTestFinOps().Where(f => f.Id == id).Single();
        }

        public IEnumerable<FinOp> GetTestFinOpsByOrgAccountId(int orgAccId)
        {
            return GetTestFinOps().Where(f => (f.AccFromId == orgAccId) || (f.AccToId == orgAccId));
        }

        [Fact]
        public async Task Get_FinOps_By_Id_Ok_Response()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<FundTrackContext>()
            .UseInMemoryDatabase(databaseName: "Get_FinOps_By_Id_Ok_Response")
            .Options;

            var testFinOp = GetTestFinOpsById(1);
            using (var context = new FundTrackContext(options))
            {
                context.FinOps.Add(testFinOp);
                context.SaveChanges();
            }

            _testContext.DbContext.FinOps.Add(testFinOp);
            _testContext.DbContext.SaveChanges();

            //Act
            var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpsById/{testFinOp.Id}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultFinOp = new JsonSerializer().Deserialize<FinOpViewModel>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.Equal(testFinOp.Id, resultFinOp.Id);
            Assert.Equal(testFinOp.AccFromId.GetValueOrDefault(0), resultFinOp.CardFromId);
            Assert.Equal(testFinOp.AccToId.GetValueOrDefault(0), resultFinOp.CardToId);
            Assert.Equal(testFinOp.Amount, resultFinOp.Amount);
            Assert.Equal(testFinOp.Description, resultFinOp.Description);
            Assert.Equal(testFinOp.FinOpDate, resultFinOp.Date);
            Assert.Equal(testFinOp.FinOpType, resultFinOp.FinOpType);
            Assert.Equal(testFinOp.DonationId, resultFinOp.DonationId);
        }

        //[Fact]
        //public async Task Get_FinOps_By_OrgAccountId_Ok_Response()
        //{
        //    //Arrange
        //    var options = new DbContextOptionsBuilder<FundTrackContext>()
        //    .UseInMemoryDatabase(databaseName: "Get_All_FinOps_By_Id_Ok_Response")
        //    .Options;

        //    int orgAccountId = 1;
        //    var testFinOp = GetTestFinOpsByOrgAccountId(orgAccountId);

        //    _testContext.DbContext.FinOps.AddRange(testFinOp);
        //    _testContext.DbContext.SaveChanges();

        //    //Act
        //    var response = await _testContext.Client.GetAsync($"/api/FinOp/GetFinOpsByOrgAccId/{orgAccountId}");
        //    var stream = await response.Content.ReadAsStreamAsync();
        //    var reader = new StreamReader(stream, Encoding.UTF8);
        //    var resultFinOp = new JsonSerializer().Deserialize<IEnumerable<FinOpViewModel>>(new JsonTextReader(reader));

        //    //Assert
        //    response.EnsureSuccessStatusCode();

        //    Assert.True(HttpStatusCode.OK == response.StatusCode);
        //    Assert.Equal(resultFinOp.Count(), 6);

        //}
    }
}
