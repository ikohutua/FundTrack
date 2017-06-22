using FundTrack.Infrastructure.ViewModel;
using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Interface for work with OrganizationForFilteringViewModel
    /// Create view model
    /// </summary>
    public interface IOrganizationsForFilteringService
    {
        /// <summary>
        /// Gets all organizations for search
        /// </summary>
        /// <returns> Collection of OrganizationForFilteringViewModel </returns>
        IEnumerable<OrganizationForFilteringViewModel> GetAll();
    }
}
