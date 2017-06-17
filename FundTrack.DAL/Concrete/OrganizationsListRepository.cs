using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Class for work whith list of organizations.
    /// </summary>
    /// <seealso cref="FundTrack.DAL.Abstract.IOrganizationsListRepository" />
    public sealed class OrganizationsListRepository : IOrganizationsListRepository
    {
        private FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationsListRepository"/> class.
        /// </summary>
        /// <param name="contextParam">The context parameter.</param>
        public OrganizationsListRepository(FundTrackContext contextParam)
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
