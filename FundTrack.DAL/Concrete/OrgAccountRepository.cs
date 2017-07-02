using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Organization Account Repository
    /// </summary>
    public class OrgAccountRepository : IRepository<OrgAccount>
    {        
        private readonly FundTrackContext _context;

        /// <summary>
        /// Creates new instance of organization account repository
        /// </summary>
        /// <param name="context">Db context</param>
        public OrgAccountRepository(FundTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates organization account.
        /// </summary>
        /// <param name="item">Item to create</param>
        /// <returns></returns>
        public OrgAccount Create(OrgAccount item)
        {
            _context.OrgAccounts.Add(item);
            return item;
        }

        /// <summary>
        /// Deletes organization account from data base
        /// </summary>
        /// <param name="id">Recives id of organization account</param>
        public void Delete(int id)
        {
            OrgAccount _orgAccount = _context.OrgAccounts.FirstOrDefault(c => c.Id == id);
            if (_orgAccount != null)
                _context.OrgAccounts.Remove(_orgAccount);
        }

        /// <summary>
        /// Gets organization account by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public OrgAccount Get(int id)
        {
            return _context.OrgAccounts.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Gets all organization accounts in database
        /// </summary>
        /// <returns>
        /// Collection all organization accounts
        /// </returns>
        public IEnumerable<OrgAccount> Read()
        {
            return _context.OrgAccounts;
        }

        /// <summary>
        /// Updates the specified organization account.
        /// </summary>
        /// <param name="item">Organization account to update.</param>
        public OrgAccount Update(OrgAccount item)
        {
            _context.Update(item);
            return item;
        }
    }
    }
