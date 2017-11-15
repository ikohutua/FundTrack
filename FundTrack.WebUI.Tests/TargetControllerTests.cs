using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using FundTrack.WebUI.Controllers;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace FundTrack.WebUI.Tests
{
    public sealed class TargetControllerTests
    {
        [Fact]
        public void GetTargetById()
        {
            //Arrange
            var service = new Mock<ITargetService>();
            service.Setup(s => s.GetTargetById(1)).Returns(new TargetViewModel() { TargetId = 1, Name = "їжа", OrganizationId = 1 });

            var controller = new TargetController(service.Object);

            //Act
            var result = controller.GetTarget(1);
            var acceptedResult = controller.Accepted();

            //Assert
            Assert.Equal(1, result.TargetId);
            Assert.Equal("їжа", result.Name);
            Assert.Equal(1, result.OrganizationId);
            Assert.True(202 == acceptedResult.StatusCode);
            service.Verify();
        }

        [Fact]
        public void GetTargetsByOrganizationId()
        {
            //Arrange
            var service = new Mock<ITargetService>();
            service.Setup(s => s.GetTargetsByOrganizationId(1)).Returns(GetTestTargetsViewModel());

            var controller = new TargetController(service.Object);

            //Act
            var result = controller.GetTargetsByOrganizationId(1);
            var acceptedResult = controller.Accepted();

            //Assert
            Assert.True(202 == acceptedResult.StatusCode);
            Assert.Equal(GetTestTargetsViewModel().Count, 3);

            foreach (var item in result)
            {
                Assert.Equal(1,item.OrganizationId);
            }

            service.Verify(s => s.GetTargetsByOrganizationId(1));
        }

        private List<TargetViewModel> GetTestTargetsViewModel()
        {
            return new List<TargetViewModel>()
            {
                new TargetViewModel() { TargetId = 1, Name = "їжа", OrganizationId = 1 },
                new TargetViewModel() { TargetId = 3, Name = "Одяг", OrganizationId = 1 },
                new TargetViewModel() { TargetId = 8, Name = "їжа", OrganizationId = 1 }
            };
        }

        [Fact]
        public void AddTargetReturnsViewResultWithTargetViewModel()
        {
            // Arrange
            var trg = GetTempTarget();

            var mock = new Mock<ITargetService>();
            mock.Setup(ts => ts.CreateTarget(trg)).Returns(trg);
            var controller = new TargetController(mock.Object);

            // Act
            var result = controller.AddTarget(trg);

            // Assert
            Assert.NotNull(result);
          //  Assert.Equal(trg.Name, result.Name);
          //  Assert.Equal(trg.OrganizationId, result.OrganizationId);
        }

        [Fact]
        public void Update_TargetFieldsAreEqualWithResultFields()
        {
            // Arrange
            var mock = new Mock<ITargetService>();
            var trg = GetTempTarget();
            mock.Setup(ts => ts.EditTarget(trg)).Returns(trg);
            var controller = new TargetController(mock.Object);
            

            // Act
            var result = controller.EditTarget(trg);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(trg.TargetId, result.TargetId);
            Assert.Equal(trg.Name, result.Name);
            Assert.Equal(trg.OrganizationId, result.OrganizationId);
        }

        private TargetViewModel GetTempTarget()
        {
            return new TargetViewModel() { TargetId = 1, Name = "їжа", OrganizationId = 1 };
        }
    }
}
