namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Interface for Sending Emails
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Send email to givend email
        /// </summary>
        /// <param name="currentHost">Current application host</param>
        /// <param name="email">Addres to send Email</param>
        /// <param name="guid">Guid of user</param>
        void SendMail(string path, string email, string guid);
    }
}
