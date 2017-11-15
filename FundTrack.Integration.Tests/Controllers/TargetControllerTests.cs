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
                new Target(){ Id = 2, OrganizationId = 2, TargetName = "Ліки" },
                new Target(){ Id = 3, OrganizationId = 3, TargetName = "Одяг" },
                new Target(){ Id = 4, OrganizationId = 4, TargetName = "Електроніка" },
                new Target(){ Id = 5, OrganizationId = 5, TargetName = "Боєприпаси" }
            }.AsQueryable();
        }

        private Target GetTestTargetById(int id)
        {
            return GetTestTargets().Where(t => t.Id == id).Single();
        }

        [Fact]
        public async Task Get_Target_Bu_Id_Ok_Response()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<FundTrackContext>()
            .UseInMemoryDatabase(databaseName: "Get_Target_Bu_Id_Ok_Response")
            .Options;

            var testTarget = GetTestTargetById(1);
            using (var context = new FundTrackContext(options))
            {
                context.Targets.Add(testTarget);
                context.SaveChanges();
            }

            _testContext.DbContext.Targets.Add(testTarget);
            _testContext.DbContext.SaveChanges();

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
        }
    }
}
