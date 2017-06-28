using System;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// PasswordReset Entity
    /// </summary>
    public class PasswordReset
    {
        /// <summary>
        /// Id of PasswordReset
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of User
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Globally unique identifier
        /// </summary>
        public string GUID { get; set; }

        /// <summary>
        /// Exxpire date of reset link
        /// </summary>
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// User Navigation Property
        /// </summary>
        public virtual User User { get; set; }
    }
}
