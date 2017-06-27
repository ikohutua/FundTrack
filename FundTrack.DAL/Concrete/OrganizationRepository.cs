using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Concrete
{
    public class OrganizationRepository : IOrganizationRepository
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

        /// <summary>
        /// Gets Organizations with their ban status
        /// </summary>
        /// <returns>Organizations with ban status</returns>
        public IEnumerable<Organization> GetOrganizationsWithBanStatus()
        {
            return _context.Organizations.Include(o => o.BannedOrganization);
        }

        /// <summary>
        /// Unbans organization with concrete id
        /// </summary>
        /// <param name="id">Id of User</param>
        public void UnBanOrganization(int id)
        {
            var bannedOrg = _context.BannedOrganizations.FirstOrDefault(o => o.Id == id);

            _context.Remove(bannedOrg);
        }

        /// <summary>
        /// Bans organization
        /// </summary>
        /// <param name="organization">Organization to Ban</param>
        /// <returns>Banned organization</returns>
        public void BanOrganization(BannedOrganization organization)
        {
            _context.BannedOrganizations.Add(organization);
        }
    }
}

