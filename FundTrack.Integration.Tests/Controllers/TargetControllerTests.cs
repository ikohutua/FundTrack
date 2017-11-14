using FundTrack.DAL.Concrete;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Target = FundTrack.DAL.Entities.Target;

namespace FundTrack.Integration.Tests.Controllers
{
    public class TargetControllerTests
    {
        private readonly TestContext _testContext;

        public TargetControllerTests()
        {
            _testContext = new TestContext();
        }

        private IQueryable<Target> GetTestTargets()
        {
            return new List<Target>()
            {
                new Target(){ Id = 1, OrganizationId = 1, TargetName = "Продукти" },
                new Target(){ Id = 2, OrganizationId = 1, TargetName = "Ліки" },
                new Target(){ Id = 3, OrganizationId = 1, TargetName = "Одяг" },
                new Target(){ Id = 4, OrganizationId = 2, TargetName = "Електроніка" },
                new Target(){ Id = 5, OrganizationId = 2, TargetName = "Боєприпаси" }
            }.AsQueryable();
        }

        private Target GetTestTargetByField(System.Func<Target,bool> predicate)
        {
            return GetTestTargets().Where(predicate).FirstOrDefault();
        }

        [Fact]
        public async Task Get_Target_Bu_Id_Ok_Response_Valid_Data()
        {
            //Arrange
            var dbContext = _testContext.GetNewDbContext();

            var testTarget = GetTestTargetByField(t => t.Id == 1);
            dbContext.Targets.Add(testTarget);
            dbContext.SaveChanges();

            //Act
            var response = await _testContext.Client.GetAsync($"/api/Target/GetTarget/{testTarget.Id}");
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

            dbContext.Dispose();
        }

        [Fact]
        public async Task Get_Targets_By_Organization_Id_Ok_Response_Valid_Data()
        {
            //Arrange
            var dbContext = _testContext.GetNewDbContext();

            int testOrgId = 1;
            dbContext.Targets.AddRange(GetTestTargets());
            dbContext.SaveChanges();

            //Act
            var response = await _testContext.Client.GetAsync($"/api/Target/GetAllTargetsOfOrganization/{testOrgId}");
            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var resultTargets = new JsonSerializer().Deserialize<IEnumerable<TargetViewModel>>(new JsonTextReader(reader));

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.True(HttpStatusCode.OK == response.StatusCode);
            Assert.True(resultTargets.All( t => t.OrganizationId == testOrgId));

            dbContext.Dispose();
        }
    }
}
