namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels
{
    public class DeleteOrgAccountViewModel
    {
        public int OrgAccountId { get; set; }
        public string AdministratorPassword { get; set; }
        public int UserId { get; set; }
        public string Error { get; set; }
        public int OrganizationId { get; set; }

    }
}
