namespace FundTrack.Infrastructure.Models
{
    /// <summary>
    /// Model for super admin chat
    /// </summary>
    public class SuperAdminChatUserModel
    {
        /// <summary>
        /// Gets or Sets Name of user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or Sets User's connection id
        /// </summary>
        public string ConnectionId { get; set; }
    }
}
