using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.BLL.Abstract
{
    public interface IOrganizationAccountService
    {
        OrgAccountViewModel GetOrganizationAccountById(int organizationAccountId);
        IEnumerable<OrgAccountViewModel> GetAccountsByOrganizationId(int organizationId);
        OrgAccountViewModel UpdateOrganizationAccount(OrgAccountViewModel model);
        DeleteOrgAccountViewModel DeleteOrganizationAccount(DeleteOrgAccountViewModel model);
        OrgAccountViewModel CreateOrganizationAccount(OrgAccountViewModel model);

        IEnumerable<OrgAccountSelectViewModel> GetAccountsForSelectByOrganizationId(int organizationId);
        OrgAccountSelectViewModel GetAccountForSelect(int organizationId, string card);
    }
}
