using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Interface for work with organization profile
    /// </summary>
    public interface IOrganizationProfileService
    {
        /// <summary>
        /// Gets OrganizationViewModel by its id
        /// </summary>
        /// <param name="id">Id of organization</param>
        /// <returns>OrganizationViewModel</returns>
        OrganizationViewModel GetOrganizationById(int id);
    }
}
