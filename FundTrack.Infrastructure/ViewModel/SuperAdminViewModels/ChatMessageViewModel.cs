namespace FundTrack.Infrastructure.ViewModel.SuperAdminViewModels
{
    /// <summary>
    /// View Model for Super admin chat
    /// </summary>
    public sealed class ChatMessageViewModel
    {
        /// <summary>
        /// Gets or Sets message in the chat
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets Login of User
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or Sets Connection Id of user
        /// </summary>
        public string ConnectionId { get; set; }
    }
}
