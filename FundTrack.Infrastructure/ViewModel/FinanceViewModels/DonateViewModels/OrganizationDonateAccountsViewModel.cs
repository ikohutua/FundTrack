using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels
{
    public class OrganizationDonateAccountsViewModel
    {
        public int OrganizationId { get; set; }
        public string OrgName { get; set; }
        public List<DonateAccountViewModel> Accounts { get; set; }
    }
}
