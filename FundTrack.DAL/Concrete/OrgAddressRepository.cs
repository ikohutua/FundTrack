using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Organization Address Repository
    /// </summary>
    public class OrgAddressRepository : IRepository<OrgAddress>
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Creates new instance of organization address repository
        /// </summary>
        /// <param name="context">Db context</param>
        public OrgAddressRepository(FundTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates organization address.
        /// </summary>
        /// <param name="item">Item to create</param>
        /// <returns></returns>
        public OrgAddress Create(OrgAddress item)
        {
            var added = _context.OrgAddresses.Add(item);
            return added.Entity;
        }

        /// <summary>
        /// Deletes organization address from data base
        /// </summary>
        /// <param name="id">Recives id of address</param>
        public void Delete(int id)
        {
            var orgAddress = _context.OrgAddresses.Include(a => a.Address).First();
            //OrgAddress _orgAddress = _context.OrgAddresses.FirstOrDefault(c => c.Id == id);
            if (orgAddress != null)
                _context.OrgAddresses.Remove(orgAddress);
        }

        /// <summary>
        /// Gets organization address by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public OrgAddress Get(int id)
        {
            return _context.OrgAddresses.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Gets all organization addresses in database
        /// </summary>
        /// <returns>
        /// Collection all addresses
        /// </returns>
        public IEnumerable<OrgAddress> Read()
        {
            return _context.OrgAddresses;
        }

        /// <summary>
        /// Updates the specified organization adrress.
        /// </summary>
        /// <param name="item">Organization Address.</param>
        public OrgAddress Update(OrgAddress item)
        {
            _context.Update(item);
            return item;
        }
    }
}
