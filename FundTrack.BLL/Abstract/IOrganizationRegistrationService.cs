using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Interface for organization registration
    /// </summary>
    public interface IOrganizationRegistrationService
    {
        /// <summary>
        /// Registers new organizations
        /// </summary>
        /// <param name="item"> Item to register</param>
        /// <returns>Item to register</returns>
        OrganizationRegistrationViewModel RegisterOrganization(OrganizationRegistrationViewModel item);
    }
}
