using Xunit;
using Moq;
using System.Collections.Generic;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.WebUI.Controllers;
using System.Linq;

namespace FundTrack.WebUI.Tests
{
    /// <summary>
    /// Test class for OrganizationsListController
    /// </summary>
    public sealed class OrganizationsListControllerTest
    {
        /// <summary>
        /// Tests AllOrganizations method in the OrganizationsListController
        /// SCRIPT - Setup service. Service returns collection of OrganizationForFilteringViewModel
        /// RESULT - The result is collection of OrganizationsForFilteringViewModel
        /// </summary>
        [Fact]
        public void AllOrganizations_SetupRepository_CollectionOfOrganizationForFilteringViewModel()
        {
            //Arrange
            var service = new Mock<IOrganizationsForFilteringService>();
            service.Setup(s => s.GetAll()).Returns(new List<OrganizationForFilteringViewModel>()
            {
                new OrganizationForFilteringViewModel() { Id = 1, Name = "Name1" },
                new OrganizationForFilteringViewModel() { Id = 2, Name = "Name2" }
            });

            var controller = new OrganizationsListController(service.Object);

            //Act
            var result = controller.AllOrganizations().ToList();
            var acceptedResult = controller.Accepted();
           
            //Assert
            Assert.True(202 == acceptedResult.StatusCode);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("Name2", result[1].Name);
            service.Verify(s => s.GetAll());
        }
    }
}
