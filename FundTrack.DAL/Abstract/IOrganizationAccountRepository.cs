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
        IQueryable<OrgAccount> ReadAllOrgAccounts(int organizationId);
    }
}
