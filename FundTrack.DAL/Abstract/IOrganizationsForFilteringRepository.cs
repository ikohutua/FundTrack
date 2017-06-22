using FundTrack.DAL.Entities;
using System.Collections.Generic;

namespace FundTrack.DAL.Abstract
{
    /// <summary>
    /// Interface for work whith list of organizations.
    /// </summary>
    public interface IOrganizationsForFilteringRepository
    {
        /// <summary>
        /// Gets the collection of organizations from database
        /// </summary>
        /// <value>
        /// Organization
        /// </value>
        IEnumerable<Organization> GetAll { get; }
    }
}
