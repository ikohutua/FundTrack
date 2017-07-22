namespace FundTrack.Infrastructure.ViewModel.EditOrganizationViewModels
{
    /// <summary>
    /// Moderator view model
    /// </summary>
    public class ModeratorViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
