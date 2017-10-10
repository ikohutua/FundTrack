namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// view model for change status on user response
    /// </summary>
    public class UserResponseChangeStatusViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the new status identifier.
        /// </summary>
        /// <value>
        /// The new status identifier.
        /// </value>
        public int NewStatusId { get; set; }
    }
}
