namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// View Model that contains information for user password changing
    /// </summary>
    public class ChangePasswordViewModel
    {
        public string login { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string errorMessage { get; set; }
    }
}
