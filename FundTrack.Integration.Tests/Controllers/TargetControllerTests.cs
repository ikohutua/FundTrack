using FundTrack.DAL.Concrete;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using FundTrack.Integration.Tests.TestRepositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FundTrack.Integration.Tests.Controllers
{
    public class TargetControllerTests
    {
        private readonly TestContext _testContext;
        public readonly string _route = "/api/Target";
        public TargetControllerTests()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public async Task Get_Target_Bu_Id_Ok_Response_Valid_Data()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            var testTarget = _testContext.TestTargetRepository.GetTestTargetByField(t => t.Id == 1);
            _testContext.TestTargetRepository.AddTargetToDb(testTarget);

            //Act
            var response = await _testContext.Client.GetAsync($"{_route}/GetTarget/{testTarget.Id}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultTarget = new JsonSerializer().Deserialize<TargetViewModel>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.Equal(testTarget.Id, resultTarget.TargetId);
            Assert.Equal(testTarget.TargetName, resultTarget.Name);
            Assert.Equal(testTarget.OrganizationId, resultTarget.OrganizationId);
            Assert.True(resultTarget.IsDeletable);
        }

        [Fact]
        public async Task Get_Targets_By_Organization_Id_Ok_Response_Valid_Data()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            int testOrgId = 1;
            var testTargets = _testContext.TestTargetRepository.GetTestTargets();
            _testContext.TestTargetRepository.AddRangeOfTargetsToDb(testTargets);

            //Act
            var response = await _testContext.Client.GetAsync($"{_route}/GetAllTargetsOfOrganization/{testOrgId}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultTargets = new JsonSerializer().Deserialize<IEnumerable<TargetViewModel>>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.True(resultTargets.All(t => t.OrganizationId == testOrgId));
            Assert.True(resultTargets.Count() == testTargets.Where(t => t.OrganizationId == testOrgId).Count());
        }

        [Fact]
        public async Task Add_Target_Ok_Response_Valid_Data()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();
            var testTarget = _testContext.TestTargetRepository.GetTestTargetByField(t => t.Id == 1);
            var testTargetVm = _testContext.TestTargetRepository.ConvertToTargetViewModel(testTarget);

            var content = new StringContent(JsonConvert.SerializeObject(testTargetVm), Encoding.UTF8, "application/json");

            //Act
            var response = await _testContext.Client.PostAsync($"{_route}/CreateTarget", content);
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultTarget = new JsonSerializer().Deserialize<TargetViewModel>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.Equal(testTargetVm.TargetId, resultTarget.TargetId);
            Assert.Equal(testTargetVm.Name, resultTarget.Name);
            Assert.Equal(testTargetVm.OrganizationId, resultTarget.OrganizationId);
        }

        [Fact]
        public async Task Delete_Target_Ok_Response_Valid_Data()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();
            var testTargets = _testContext.TestTargetRepository.GetTestTargets();
            _testContext.TestTargetRepository.AddRangeOfTargetsToDb(testTargets);

            var testTarget = _testContext.TestTargetRepository.GetTestTargetByField(t => t.Id == 1);
            var testTargetVm = _testContext.TestTargetRepository.ConvertToTargetViewModel(testTarget);

            //Act
            var response = await _testContext.Client.GetAsync($"{_route}/DeleteTarget/{testTargetVm.TargetId}");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task Edit_Target_Ok_Response_Valid_Data()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            var testTarget = _testContext.TestTargetRepository.GetTestTargetByField(t => t.Id == 1);
            _testContext.TestTargetRepository.AddTargetToDb(testTarget);

            var testTargetVm = _testContext.TestTargetRepository.ConvertToTargetViewModel(testTarget);
            testTargetVm.Name = "Спец. обладнання";

            //Act
            var content = new StringContent(JsonConvert.SerializeObject(testTargetVm), Encoding.UTF8, "application/json");

            var response = await _testContext.Client.PutAsync($"{_route}/EditTarget", content);
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultTarget = new JsonSerializer().Deserialize<TargetViewModel>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.Equal(testTargetVm.TargetId, resultTarget.TargetId);
            Assert.Equal(testTargetVm.Name, resultTarget.Name);
            Assert.Equal(testTargetVm.OrganizationId, resultTarget.OrganizationId);
        }

        [Fact]
        public async Task Get_Targets_Bu_OrgId_And_ParentId_Ok_Response_Valid_Data()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            var testTargets = _testContext.TestTargetRepository.GetTestTargets();
            _testContext.TestTargetRepository.AddRangeOfTargetsToDb(testTargets);

            int orgId = 1;
            int parentId = 1;
            var expectedResult = _testContext.TestTargetRepository.GetTestTargets()
                                .Where(t => t.OrganizationId == orgId && t.ParentTargetId == parentId);

            //Act
            var response = await _testContext.Client.GetAsync($"{_route}/{orgId}/{parentId}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultTargets = new JsonSerializer().Deserialize<IEnumerable<TargetViewModel>>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.Equal(expectedResult.Count(), resultTargets.Count());
        }

        [Fact]
        public async Task Get_Parent_Targets_Without_Parents_Bu_OrgId_Ok_Response_Valid_Data()
        {
            //Arrange
            var dbContext = _testContext.GetClearDbContext();

            var testTargets = _testContext.TestTargetRepository.GetTestTargets();
            _testContext.TestTargetRepository.AddRangeOfTargetsToDb(testTargets);


            int orgId = 1;
            var expectedResult = _testContext.TestTargetRepository.GetTestTargets()
                                .Where(t => t.OrganizationId == orgId && t.ParentTargetId == null);

            //Act
            var response = await _testContext.Client.GetAsync($"{_route}/{orgId}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultTargets = new JsonSerializer().Deserialize<IEnumerable<TargetViewModel>>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.Equal(expectedResult.Count(), resultTargets.Count());
        }
    }
}