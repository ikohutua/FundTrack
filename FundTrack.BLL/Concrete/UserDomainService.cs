using FundTrack.BLL.Abstract;
using System;
using FundTrack.DAL.Entities;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System.Collections.Generic;
using System.Linq;
using FundTrack.BLL.Concrete;
using FundTrack.Infrastructure;
using FundTrack.Infrastructure.ViewModel.ResetPassword;

namespace FundTrack.BLL.DomainServices
{
    /// <summary>
    /// Service for authorization and registration
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IUserDomainService" />
    public sealed class UserDomainService : IUserDomainService
    {
        // unit of work instance
        private readonly IUnitOfWork _unitOfWork;

        // email sender instance
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDomainService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserDomainService(IUnitOfWork unitOfWorkParam, IEmailSender emailSenderParam)
        {
            _unitOfWork = unitOfWorkParam;
            _emailSender = emailSenderParam;
        }

        /// <summary>
        /// Gets user info view model
        /// </summary>
        /// <param name="userLogin">User login</param>
        /// <param name="rawPassword">User raw password</param>
        /// <returns>User info view model</returns>
        public UserInfoViewModel GetUserInfoViewModel(string login, string rawPassword)
        {
            if (login.Length != 0 && rawPassword.Length != 0)
            {
                var hashedPassword = PasswordHashManager.GetPasswordHash(rawPassword);
                return this.InitializeUserInfoViewModel(this._unitOfWork.UsersRepository.GetUser(login, hashedPassword));
            }
            else
            {
                throw new BusinessLogicException(ErrorMessages.MissedEnterData);
            }
        }

        /// <summary>
        /// Gets user info view model
        /// </summary>
        /// <param name="userLogin">User login</param>
        /// <returns>User info view model</returns>
        public UserInfoViewModel GetUserInfoViewModel(string login)
        {
            return this.InitializeUserInfoViewModel(this._unitOfWork.UsersRepository.GetUser(login));
        }

        /// <summary>
        /// Authorize facebook user in system.
        /// </summary>
        /// <param name="loginFacebookViewModel">The login facebook view model.</param>
        /// <returns></returns>
        public UserInfoViewModel LoginFacebook(LoginFacebookViewModel loginFacebookViewModel)
        {
            User user = this._unitOfWork.UsersRepository.GetFacebookUser(loginFacebookViewModel.Email);
            if (user == null)
            {
                user = loginFacebookViewModel;
                user.Password = PasswordHashManager.GetPasswordHash(loginFacebookViewModel.Password);
                User addedUser = this._unitOfWork.UsersRepository.Create(user);
                this._unitOfWork.SaveChanges();
                return this.InitializeUserInfoViewModel(addedUser);
            }
            else
            {
                if (user.FB_Link == null)
                {
                    user.FB_Link = "facebook.com/" + loginFacebookViewModel.FbLink;
                    this._unitOfWork.UsersRepository.Update(user);
                    this._unitOfWork.SaveChanges();
                    return this.InitializeUserInfoViewModel(user);
                }
            }
            return this.InitializeUserInfoViewModel(user);
        }

        /// <summary>
        /// Creates user in database
        /// </summary>
        /// <param name="registrationViewModel">RegistrationViewModel</param>
        /// <returns>Added user model</returns>
        public RegistrationViewModel CreateUser(RegistrationViewModel registrationViewModel)
        {
            bool isUserExists = this._unitOfWork.UsersRepository.isUserExisted(registrationViewModel.Email,
                                                                               registrationViewModel.Login);
            if (isUserExists)
            {
                throw new BusinessLogicException(ErrorMessages.UserExistsMessage);
            }
            try
            {
                User userToAdd = registrationViewModel;
                userToAdd.Password = PasswordHashManager.GetPasswordHash(registrationViewModel.Password);
                User addedUser = this._unitOfWork.UsersRepository.Create(userToAdd);
                this._unitOfWork.SaveChanges();
                this.CreateUserRole(addedUser.Login);
                return addedUser;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.AddUserMessage, ex);
            }
        }

        /// <summary>
        /// Creates the user role.
        /// </summary>
        /// <param name="login">The login.</param>
        private void CreateUserRole(string login)
        {
            var membership = new Membership
            {
                RoleId = this._unitOfWork.RoleRepository.Read().Where(r => r.Name == UserRoles.Partner).FirstOrDefault().Id,
                UserId = this._unitOfWork.UsersRepository.GetUser(login).Id,
                OrgId = 1
            };
            this._unitOfWork.MembershipRepository.Create(membership);
            this._unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Gets all users from database
        /// </summary>
        /// <returns>List of users</returns>
        public List<User> GetAllUsers()
        {
            try
            {
                return this._unitOfWork.UsersRepository.Read().ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.GetAllUsersMessage, ex);
            }
        }

        /// <summary>
        /// Initializes the user information view model.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Неправильний логін чи пароль.</exception>
        public UserInfoViewModel InitializeUserInfoViewModel(User user)
        {
            if (user != null)
            {
                var bannedUser = this._unitOfWork.UsersRepository.GetAllUsersWithBanStatus()
                                                    .FirstOrDefault(u => u.Id == user.Id)
                                                    .BannedUser;
                if (bannedUser != null)
                {
                    throw new BusinessLogicException(ErrorMessages.UserIsBaned +
                                                        bannedUser.Description);
                }
                else
                {
                    var userInfoView = new UserInfoViewModel
                    {
                        id = user.Id,
                        login = user.Login,
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        email = user.Email,
                        photoUrl = user.PhotoUrl
                    };

                    if (!this._unitOfWork.MembershipRepository.IsUserHasRole(user.Id))
                    {
                        this.CreateUserRole(userInfoView.login);
                    }
                    userInfoView.role = this._unitOfWork.MembershipRepository.GetRole(user.Id);
                    return userInfoView;
                }
            }
            else
            {
                throw new BusinessLogicException(ErrorMessages.IncorrectCredentials);
            }
        }

        /// <summary>
        /// Changes password of specified User, by its login
        /// </summary>
        /// <param name="changePasswordViewModel">View model containing login, old password, new password and error message</param>
        /// <returns>Empty userInfoViewModel with errors in case if any arised</returns>
        public UserInfoViewModel ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (changePasswordViewModel != null)
            {
                if (changePasswordViewModel.login != null && changePasswordViewModel.newPassword != null & changePasswordViewModel.oldPassword != null)
                {
                    User user = this._unitOfWork.UsersRepository.GetUser(changePasswordViewModel.login);
                    if (user.Password == PasswordHashManager.GetPasswordHash(changePasswordViewModel.oldPassword))
                    {
                        user.Password = PasswordHashManager.GetPasswordHash(changePasswordViewModel.newPassword);
                        _unitOfWork.UsersRepository.Update(user);
                        _unitOfWork.SaveChanges();
                        return this.InitializeUserInfoViewModel(user);
                    }
                    else
                    {
                        throw new Exception("Старий пароль невірний");
                    }
                }
                else
                {
                    throw new Exception("Заповнені не всі поля");
                }
            }
            else
            {
                throw new Exception("Не намагайтеся нас обманути!");
            }
        }

        public UserInfoViewModel UpdateUser(UserInfoViewModel userModel)
        {
            var user = new User();
            user = this._unitOfWork.UsersRepository.Get(userModel.id);
            user.Email = userModel.email;
            user.FirstName = userModel.firstName;
            user.LastName = userModel.lastName;
            user.PhotoUrl = userModel.photoUrl;
            this._unitOfWork.UsersRepository.Update(user);
            this._unitOfWork.SaveChanges();
            return this.InitializeUserInfoViewModel(this._unitOfWork.UsersRepository.Get(userModel.id));
        }

        /// <summary>
        /// Checks if the user with email exists
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <returns>User email status</returns>
        public bool IsValidUserEmail(string email)
        {
            return _unitOfWork.UsersRepository.Read().FirstOrDefault(u => u.Email == email) == null ? false : true;
        }

        /// <summary>
        /// Sends Email with recovery password link
        /// </summary>
        /// <param name="currentHost">current host</param>
        /// <param name="email">email to send address</param>
        public void SendPasswordRecoveryEmail(string currentHost, string email)
        {
            var user = _unitOfWork.UsersRepository.Read().FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                var guid = Guid.NewGuid().ToString("D");

                if (_unitOfWork.UsersRepository.HasUserResetLink(user))
                {
                    _unitOfWork.UsersRepository.RemoveUserRecoveryLink(user.Id);
                    _unitOfWork.SaveChanges();
                }

                _unitOfWork.UsersRepository.AddUserRecoveryLink(new PasswordReset
                {
                    GUID = guid,
                    User = user,
                    UserID = user.Id,
                    ExpireDate = DateTime.Now
                });

                _emailSender.SendMail(currentHost, email, guid);

                _unitOfWork.SaveChanges();
            }
            else
            {
                throw new BusinessLogicException(ErrorMessages.NoUserWithEmail);
            }
        }

        /// <summary>
        /// Checks if input guid is a valid user guid
        /// </summary>
        /// <param name="guid">Input User guid</param>
        /// <returns>Guid status</returns>
        public bool IsValidUserGuid(string guid)
        {
            return _unitOfWork.UsersRepository.GetUserByGuid(guid) != null ? true : false;
        }

        /// <summary>
        /// Resets user password
        /// </summary>
        /// <param name="passwordReset">View model of PasswordResetViewModel</param>
        public void ResetPassword(PasswordResetViewModel passwordReset)
        {
            var user = _unitOfWork.UsersRepository.GetUserByGuid(passwordReset.Guid);

            if (user != null)
            {
                user.Password = PasswordHashManager.GetPasswordHash(passwordReset.NewPassword);
                _unitOfWork.UsersRepository.Update(user);
                _unitOfWork.UsersRepository.RemoveUserRecoveryLink(user.Id);

                _unitOfWork.SaveChanges();
            }
            else
            {
                throw new BusinessLogicException(ErrorMessages.InvalidGuid);
            }
        }

        /// <summary>
        /// Gets organization Id with bann status
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>Organization Id with ban status</returns>
        public OrganizationIdViewModel GetOrganizationId(string login)
        {
            var user = new User();
            user = _unitOfWork.UsersRepository.Read().Where(u => u.Login == login).FirstOrDefault();
            if (user != null)
            {
                var membership = _unitOfWork.MembershipRepository.Read().Where(m => m.UserId == user.Id).FirstOrDefault();
                if (membership != null)
                {
                    int roleId = membership.RoleId;

                    var bannedOrg = _unitOfWork.OrganizationRepository.GetOrganizationsWithBanStatus().FirstOrDefault(o => o.Id == membership.OrgId).BannedOrganization;

                    var userRole = _unitOfWork.RoleRepository.Read().Where(r => r.Id == roleId).FirstOrDefault().Name;
                    if (userRole == "admin" || userRole == "superadmin")
                    {
                        int organizationId = membership.OrgId;
                        return new OrganizationIdViewModel
                        {
                            OrganizationId = organizationId,
                            BannedDescription = bannedOrg == null ? string.Empty : bannedOrg.Description
                        };
                    }
                    throw new BusinessLogicException(ErrorMessages.InvalidUserRole);
                }
            }
            throw new BusinessLogicException(ErrorMessages.InvalidUser);
        }
    }
}
