using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Class for work whith list of organizations.
    /// </summary>
    /// <seealso cref="FundTrack.DAL.Abstract.IOrganizationsForFilteringRepository" />
    public sealed class OrganizationsForFilteringRepository : IOrganizationsForFilteringRepository
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationsForFilteringRepository"/> class.
        /// </summary>
        /// <param name="contextParam">The context parameter.</param>
        public OrganizationsForFilteringRepository(FundTrackContext contextParam)
        {
            this._context = contextParam;
        }

        /// <summary>
        /// Gets the collection of organizations from database
        /// </summary>
        /// <value>
        /// Organization
        /// </value>
        public IEnumerable<Organization> GetAll
        {
            get
            {
                return _context.Organizations;
            }
        }
    }
}
