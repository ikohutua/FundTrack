using System.Collections.Generic;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels
{
    public class OrganizationDonateAccountsViewModel
    {
        public int OrganizationId { get; set; }
        public string OrgName { get; set; }
        public List<DonateAccountViewModel> Accounts { get; set; }
    }
}
