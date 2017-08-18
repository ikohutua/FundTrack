using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.DAL.Abstract
{
    public interface IOrganizationAccountRepository
    {
        OrgAccount Create(OrgAccount item);
        void Delete(int orgAccountId);
        OrgAccount Edit(OrgAccount item);
        OrgAccount Read(int orgAccountId);
        IEnumerable<OrgAccount> ReadAllOrgAccounts(int organizationId);
        IEnumerable<OrgAccount> GetAllOrgAccounts();
        IEnumerable<OrgAccount> ReadOrgAccountsForDonations(int organizationId);

        OrgAccount GetOrgAccountByCardNumber(int orgId, string card);

        OrgAccount GetOrgAccountByName(int orgId, string orgAccountName);
    }
}
