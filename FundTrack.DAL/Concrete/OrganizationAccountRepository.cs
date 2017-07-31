using FundTrack.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundTrack.DAL.Entities;

namespace FundTrack.DAL.Concrete
{
    public class OrganizationAccountRepository : IOrganizationAccountRepository
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public OrganizationAccountRepository(FundTrackContext context)
        {
            _context = context;
        }

        public OrgAccount Create(OrgAccount item)
        {
            var orgAccount = this._context.OrgAccounts.Add(item);
            return orgAccount.Entity;
        }

        public void Delete(int orgAccountId)
        {
            var orgAccount = this._context.OrgAccounts.FirstOrDefault(i => i.Id == orgAccountId);
            if (orgAccount!=null)
            {
                this._context.OrgAccounts.Remove(orgAccount); 
            }
        }

        public OrgAccount Edit(OrgAccount item)
        {
            var updatedItem = this._context.OrgAccounts.Update(item);
            return updatedItem.Entity;
        }

        public OrgAccount Read(int orgAccountId)
        {
            return this._context.OrgAccounts.FirstOrDefault(i => i.Id == orgAccountId);
        }

        public IQueryable<OrgAccount> ReadAllOrgAccounts(int organizationId)
        {
            return this._context.OrgAccounts.Where(i => i.OrgId == organizationId);
        }
    }
}
