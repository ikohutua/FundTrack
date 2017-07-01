namespace FundTrack.Infrastructure.ViewModel.ResetPassword
{
    public class PasswordResetViewModel
    {
        /// <summary>
        /// Gets or Sets Guid of user
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Gets of Sets New User Password
        /// </summary>     
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or Sets Confirm User Password 
        /// </summary>
        public string NewPasswordConfirm { get; set; }
    }
}
