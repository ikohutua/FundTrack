using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
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
        void DeleteOrganizationAccount(int organizationAccountId);
        OrgAccountViewModel CreateOrganizationAccount(OrgAccountViewModel model);


    }
}
