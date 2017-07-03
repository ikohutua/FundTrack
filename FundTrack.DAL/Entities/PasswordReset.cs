using System;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// PasswordReset Entity
    /// </summary>
    public class PasswordReset
    {
        /// <summary>
        /// Gets or Sets Id of PasswordReset
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of User
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or Sets Globally unique identifier
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Gets or Sets Exxpire date of reset link
        /// </summary>
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// Gets or Sets User Navigation Property
        /// </summary>
        public virtual User User { get; set; }
    }
}
