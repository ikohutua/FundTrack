using FundTrack.Infrastructure.ViewModel;
using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Interface for work with OrganizationsForLayout
    /// Create view model
    /// </summary>
    public interface IOrganizationsForLayoutService
    {
        /// <summary>
        /// Gets all organizations for search
        /// </summary>
        /// <returns> Collection of OrganizationsForLayout </returns>
        IEnumerable<OrganizationsForLayout> GetAll();
    }
}
