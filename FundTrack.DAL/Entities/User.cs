using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id of User
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User Login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// User Email
        /// </summary>       
        public string Email { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User SaltKey to hash password
        /// </summary>
        public string SaltKey { get; set; }

        /// <summary>
        /// User First Name
        /// </summary>       
        public string FirstName { get; set; }

        /// <summary>
        /// User Last Name
        /// </summary>       
        public string LastName { get; set; }

        /// <summary>
        /// Id of Facebook Link
        /// </summary>
        public string FB_Link { get; set; }

        /// <summary>
        /// Url of User Photo
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// Indicates if the user is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Phone navigation property
        /// </summary>
        public virtual ICollection<Phone> Phones { get; set; }

        /// <summary>
        /// Membership navigation property
        /// </summary>
        public virtual ICollection<Membership> Memberships { get; set; }

        /// <summary>
        /// UserAddress navigation property
        /// </summary>
        public virtual ICollection<UserAddress> UserAddresses { get; set; }

        /// <summary>
        /// FinOp navigation property
        /// </summary>
        public virtual ICollection<FinOp> FinOps { get; set; }

        /// <summary>
        /// ExternalContact navigation property
        /// </summary>
        public virtual ICollection<ExternalContact> ExternalContacts { get; set; }

        /// <summary>
        /// Offer navigation property
        /// </summary>
        public virtual ICollection<Offer> Offers { get; set; }

        /// <summary>
        /// Complaint navigation property
        /// </summary>
        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}
