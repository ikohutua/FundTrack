using FundTrack.Infrastructure.ViewModel;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or Sets Id of User
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets User Login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or Sets User Email
        /// </summary>       
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets User Salt
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// Gets or Sets User Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets User First Name
        /// </summary>       
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets User Last Name
        /// </summary>       
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets Id of Facebook Link
        /// </summary>
        public string FB_Link { get; set; }

        /// <summary>
        /// Gets or Sets Url of User Photo
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// Gets or Sets User Authorization Token
        /// </summary>
        public string AuthorizationToken { get; set; }
        /// <summary>
        /// Gets or Sets Navigation property Banned user
        /// </summary>
        public virtual BannedUser BannedUser { get; set; }

        /// <summary>
        /// Gets or Sets PasswordReset Navigation property
        /// </summary>
        public virtual PasswordReset PasswordReset { get; set; }

        /// <summary>
        /// Gets or Sets Phone navigation property
        /// </summary>
        public virtual ICollection<Phone> Phones { get; set; }

        /// <summary>
        /// Gets or Sets Phone navigation property
        /// </summary>
        public virtual ICollection<OrgAccount> OrgAccounts { get; set; }

        /// <summary>
        /// Gets or Sets Membership navigation property
        /// </summary>
        public virtual Membership Membership { get; set; }

        /// <summary>
        /// Gets or Sets Subscribe organization navigation property
        /// </summary>
        public virtual ICollection<SubscribeOrganization> SubscribeOrganization { get; set; }

        /// <summary>
        /// Gets or Sets UserAddress navigation property
        /// </summary>
        public virtual ICollection<UserAddress> UserAddresses { get; set; }

        /// <summary>
        /// Gets or Sets FinOp navigation property
        /// </summary>
        public virtual ICollection<FinOp> FinOps { get; set; }

        /// <summary>
        /// Gets or Sets ExternalContact navigation property
        /// </summary>
        public virtual ICollection<ExternalContact> ExternalContacts { get; set; }

        /// <summary>
        /// Gets or Sets Offer navigation property
        /// </summary>
        public virtual ICollection<OfferedItem> OfferedItems { get; set; }

        /// <summary>
        /// Gets or Sets Complaint navigation property
        /// </summary>
        public virtual ICollection<Complaint> Complaints { get; set; }

        /// <summary>
        /// Gets or Sets Complaint navigation property
        /// </summary>
        public virtual ICollection<UserResponse> UserResponses { get; set; }

        public virtual ICollection<Donation> UserDonations { get; set; }

        /// <summary>
        /// Convert User model to RegistartionViewModel
        /// </summary>
        /// <param name="user">User model</param>
        public static implicit operator RegistrationViewModel(User user)
        {
            return new RegistrationViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email
            };
        }

        /// <summary>
        /// Convert RegistrationViewModel to User model
        /// </summary>
        /// <param name="registrationViewModel">Registration view model</param>
        public static implicit operator User(RegistrationViewModel registrationViewModel)
        {
            return new User
            {
                FirstName = registrationViewModel.FirstName,
                LastName = registrationViewModel.LastName,
                Login = registrationViewModel.Login,
                Password = registrationViewModel.Password,
                Email = registrationViewModel.Email
            };
        }
        /// <summary>
        /// Convert LoginFacebookViewModel to User model
        /// </summary>
        /// <param name="registrationViewModel">Registration view model</param>
        public static implicit operator User(LoginFacebookViewModel loginFacebookViewModel)
        {
            return new User
            {
                FirstName = loginFacebookViewModel.FirstName,
                LastName = loginFacebookViewModel.LastName,
                Login = loginFacebookViewModel.Login,
                Password = loginFacebookViewModel.Password,
                Email = loginFacebookViewModel.Email,
                PhotoUrl = loginFacebookViewModel.PhotoUrl,
                FB_Link = "facebook.com/" + loginFacebookViewModel.FbLink
            };
        }

        /// <summary>
        /// Convert User model to RegistartionViewModel
        /// </summary>
        /// <param name="user">User model</param>
        public static implicit operator LoginFacebookViewModel(User user)
        {
            return new LoginFacebookViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                PhotoUrl = user.PhotoUrl,
                FbLink = user.FB_Link.Substring(13)
            };
        }

        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_User");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Login).IsRequired().HasMaxLength(100);

                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.PhotoUrl).HasDefaultValue("https://s3.eu-central-1.amazonaws.com/fundtrack/default-user-image.png");
            });
        }
    }
}
