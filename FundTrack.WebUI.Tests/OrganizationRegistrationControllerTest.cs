using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.WebUI.Controllers;
using Moq;
using Xunit;

namespace FundTrack.WebUI.Tests
{
    /// <summary>
    /// Class for testing Organization Registration Controller
    /// </summary>
    public class OrganizationRegistrationControllerTest
    {
        /// <summary>
        /// Creates new organization 
        /// Script - setups service, which return created item
        /// </summary>
        [Fact]
        public void RegisterOrganizationController_SetupService_CreatedItem()
        {
            var registerServiceMock = new Mock<IOrganizationRegistrationService>();

            var organizationToCreate = new OrganizationRegistrationViewModel
            {
                Name = "Helpful",
                Description = "Volunteers organization",
                AdministratorLogin = "admin2",
                City = "Lviv",
                Country = "Ukraine",
                Street = "Pasichna",
                House = "24a",
                NameError = string.Empty,
                UserError = string.Empty
            };

            registerServiceMock.Setup(o => o.RegisterOrganization(organizationToCreate)).
                Returns(new OrganizationRegistrationViewModel {
                Name = "Helpful",
                Description = "Volunteers organization",
                AdministratorLogin = "admin2",
                City = "Lviv",
                Country = "Ukraine",
                Street = "Pasichna",
                House = "24a",
                NameError = string.Empty,
                UserError = string.Empty
            });

            var controller = new OrganizationRegistrationController(registerServiceMock.Object);

            var result = controller.RegisterNewOrganization(organizationToCreate);

            Assert.NotNull(result);
        }
    }
}
