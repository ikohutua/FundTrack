using FundTrack.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundTrack.DAL.Entities;
using Microsoft.EntityFrameworkCore;

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
            var orgAccount = this._context.OrgAccounts
                .Include(a=>a.BankAccount)
                .Include(a=>a.FinOpsFrom)
                .Include(a=>a.FinOpsTo)
                .Include(a=>a.Balances)
                .FirstOrDefault(i => i.Id == orgAccountId);
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

        public IEnumerable<OrgAccount> GetAllOrgAccounts()
        {
            return this._context.OrgAccounts.Include(a=>a.BankAccount).Include(a=>a.FinOpsFrom).Include(a=>a.FinOpsTo);
        }

        public OrgAccount Read(int orgAccountId)
        {
            return this._context.OrgAccounts
                .Include(a => a.BankAccount)
                .Include(a => a.Balances)
                .Include(a => a.Currency)
                .Include(a => a.FinOpsFrom)
                .Include(a => a.FinOpsTo)
                .FirstOrDefault(i => i.Id == orgAccountId);
        }

        public IEnumerable<OrgAccount> ReadAllOrgAccounts(int organizationId)
        {
            return this._context.OrgAccounts
                .Include(a => a.BankAccount)
                .Include(a => a.Balances)
                .Include(a => a.Currency)
                .Include(a => a.FinOpsFrom)
                .Include(a => a.FinOpsTo)
                .Where(a => a.OrgId == organizationId);
        }

        public IEnumerable<OrgAccount> ReadOrgAccountsForDonations(int organizationId)
        {
            return this._context.OrgAccounts
                .Include(a => a.BankAccount)
                .Include(a => a.Currency)
                .Where(a => a.OrgId == organizationId)
                .Where(a => a.BankAccount.MerchantPassword != null);
        }

        public OrgAccount GetOrgAccountByCardNumber(int orgId, string card)
        {
            return this._context.OrgAccounts
                                .Include(a => a.BankAccount)
                                .FirstOrDefault(a => a.OrgId == orgId && a.BankAccount.CardNumber == card);
        }

        public OrgAccount GetOrgAccountByName(int orgId,string orgAccountName)
        {
            return this._context.OrgAccounts
                       .FirstOrDefault(a => a.OrgId == orgId && a.OrgAccountName == orgAccountName);
        }
    }
}
