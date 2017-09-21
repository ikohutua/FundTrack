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
    /// Test class for OrganizationDetailController
    /// </summary>
    public sealed class OrganizationDetailControllerTest
    {
        /// <summary>
        /// Tests AllOrganizations method in the OrganizationDetailController
        /// SCRIPT - Setup service.
        /// RESULT - The result is collection of OrganizationViewModel
        /// </summary>
        [Fact]
        public void AllOrganizationsTest()
        {
            //Arrange
            var service = new Mock<IOrganizationProfileService>();
            service.Setup(s => s.GetAllOrganizations()).Returns(new List<OrganizationViewModel>()
            {
                new OrganizationViewModel() { Id = 1,  Description = "Some description 1",  Name = "Organizagtion1"},
                new OrganizationViewModel() { Id = 2,  Description = "Some description 2",  Name = "Organizagtion2"}
            });

            var controller = new OrganizationDetailController(service.Object);

            //Act
            var result = controller.AllOrganizations() as List<OrganizationViewModel>;
            var acceptedResult = controller.Accepted();

            //Assert
            Assert.True(202 == acceptedResult.StatusCode);
            Assert.Equal(result[0].Id, 1);
            Assert.Equal(result[1].Id, 2);
            Assert.Equal(result.Count, 2);
        }
    }
}
