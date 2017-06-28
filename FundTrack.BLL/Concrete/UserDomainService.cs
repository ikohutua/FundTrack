using FundTrack.BLL.Abstract;
using System;
using FundTrack.DAL.Entities;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using FundTrack.Infrastructure.ViewModel.SuperAdminViewModels;
using FundTrack.BLL.Concrete;
using FundTrack.Infrastructure;
using FundTrack.BLL.Concrete;


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

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDomainService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserDomainService(IUnitOfWork unitOfWorkParam)
        {
            _unitOfWork = unitOfWorkParam;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="rawPassword">The raw password.</param>
        /// <returns></returns>
        public User GetUser(string login, string rawPassword)
        {
            var hashedPassword = PasswordHashManager.GetPasswordHash(rawPassword);

            return this._unitOfWork.UsersRepository
                             .Read()
                             .FirstOrDefault(u => u.Login.ToUpper() == login.ToUpper()
                              && u.Password == hashedPassword);
        }
        public User GetUser(string login)
        {
            return this._unitOfWork.UsersRepository
                            .Read()
                            .FirstOrDefault(a => a.Login.ToUpper() == login.ToUpper());
        }

        /// <summary>
        /// Gets user info view model
        /// </summary>
        /// <param name="userLogin">User login</param>
        /// <param name="rawPassword">User raw password</param>
        /// <returns>User info view model</returns>
        public UserInfoViewModel GetUserInfoViewModel(string login, string rawPassword)
        {
            return this.InitializeUserInfoViewModel(this.GetUser(login, rawPassword));
        }

        /// <summary>
        /// Gets user info view model
        /// </summary>
        /// <param name="userLogin">User login</param>
        /// <returns>User info view model</returns>
        public UserInfoViewModel GetUserInfoViewModel(string login)
        {
            return this.InitializeUserInfoViewModel(this.GetUser(login));
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
                //Membership memberShip = new Membership();
                
                User addedUser = this._unitOfWork.UsersRepository.Create(userToAdd);
                //memberShip.UserId = addedUser.Id;
                //memberShip.RoleId = 1;
                this._unitOfWork.SaveChanges();

                return addedUser;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.AddUserMessage, ex);
            }
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
                var userInfoView = new UserInfoViewModel
                {
                    id = user.Id,
                    login = user.Login,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    email = user.Email,
                    photoUrl = user.PhotoUrl
                };

                if (this._unitOfWork.MembershipRepository.IsUserHasRole(user.Id))
                {
                    userInfoView.role = this._unitOfWork.MembershipRepository.GetRole(user.Id);
                }
                return userInfoView;
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
            if (changePasswordViewModel!=null)
            {
                if (changePasswordViewModel.login!=null&&changePasswordViewModel.newPassword!=null&changePasswordViewModel.oldPassword!=null)
                {
                    User user = this.GetUser(changePasswordViewModel.login);
                    if (user.Password==PasswordHashManager.GetPasswordHash(changePasswordViewModel.oldPassword))
                    {
                        user.Password = PasswordHashManager.GetPasswordHash(changePasswordViewModel.newPassword);
                        _unitOfWork.UsersRepository.Update(user);
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
            return this.InitializeUserInfoViewModel(this._unitOfWork.UsersRepository.Get(userModel.id));
        }
    }
}
