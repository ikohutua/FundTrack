using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.DAL.Abstract
{
    /// <summary>
    /// Interface for Organization Repository
    /// </summary>
    public interface IOrganizationRepository : IRepository<Organization>
    {
        /// <summary>
        /// Gets Organization with their ban status
        /// </summary>
        /// <returns>Organization with ban status</returns>
        IEnumerable<Organization> GetOrganizationsWithBanStatus();

        /// <summary>
        /// Unbans organization with concrete id
        /// </summary>
        /// <param name="id">Id of User</param>
        void UnBanOrganization(int id);

        /// <summary>
        /// Bans organization
        /// </summary>
        /// <param name="organization">Organization to Ban</param>
        /// <returns>Banned organization</returns>
        void BanOrganization(BannedOrganization organization);
    }
}
