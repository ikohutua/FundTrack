using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.DAL.Concrete
{
    public class OrganizationRepository : IRepository<Organization>
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public OrganizationRepository(FundTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates the organization.
        /// </summary>
        /// <param name="item">The organization.</param>
        public Organization Create(Organization item)
        {
            _context.Organizations.Add(item);
            return item;
        }

        /// <summary>
        /// Deletes organization from data base
        /// </summary>
        /// <param name="id">Recives id of organization</param>
        public void Delete(int id)
        {
            Organization _organization = _context.Organizations.FirstOrDefault(c => c.Id == id);
            if (_organization != null)
                _context.Organizations.Remove(_organization);
        }

        /// <summary>
        /// Gets the organization by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Organization Get(int id)
        {
            return _context.Organizations.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Gets all organizations in database
        /// </summary>
        /// <returns>
        /// Collection all organizations
        /// </returns>
        public IEnumerable<Organization> Read()
        {
            return _context.Organizations;
        }

        /// <summary>
        /// Updates the specified organization.
        /// </summary>
        /// <param name="item">The organization.</param>
        public Organization Update(Organization item)
        {
            _context.Update(item);
            return item;
        }
    }
}

