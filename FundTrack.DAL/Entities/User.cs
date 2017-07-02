using FundTrack.Infrastructure.ViewModel;
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
        /// Navigation property Banned user
        /// </summary>
        public virtual BannedUser BannedUser { get; set; }

        /// <summary>
        /// PasswordReset Navigation property
        /// </summary>
        public virtual PasswordReset PasswordReset { get; set; }

        /// <summary>
        /// Phone navigation property
        /// </summary>
        public virtual ICollection<Phone> Phones { get; set; }

        /// <summary>
        /// Membership navigation property
        /// </summary>
        public virtual Membership Membership { get; set; }

        /// <summary>
        /// Subscribe organization navigation property
        /// </summary>
        public virtual ICollection<SubscribeOrganization> SubscribeOrganization { get; set; }

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
    }
}
