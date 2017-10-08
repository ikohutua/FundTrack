using FundTrack.DAL.Entities;
using System.Collections.Generic;

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

        OrgAccount GetOrgAccountById(int orgAccountId);
    }
}
