using FundTrack.BLL.Abstract;
using System;
using FundTrack.DAL.Entities;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System.Linq;
using System.Collections.Generic;
using FundTrack.Infrastructure;

namespace FundTrack.BLL.DomainServices
{
    /// <summary>
    /// Service for authorization and registration
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IUserDomainService" />
    public sealed class UserDomainService : IUserDomainService
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDomainService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserDomainService(IUnitOfWork unitOfWorkParam)
        {
            this._unitOfWork = unitOfWorkParam;
        }

        /// <summary>
        /// Get user for DAL which come from parameter
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User from db</returns>
        public User GetUser(string login, string rawPassword)
        {
            var hashedPassword = PasswordHashManager.GetPasswordHash(rawPassword);

            return this._unitOfWork.UsersRepository
                             .Read()
                             .FirstOrDefault(u => u.Login.ToLower() == login.ToLower()
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
            var searchUser = this.GetUser(login, rawPassword);
            return this.InitializeUserInfoViewModel(searchUser);
        }
        /// <summary>
        /// Gets user info view model
        /// </summary>
        /// <param name="userLogin">User login</param>
        /// <returns>User info view model</returns>
        public UserInfoViewModel GetUserInfoViewModel(string login)
        {
            var searchUser = this.GetUser(login);
            return this.InitializeUserInfoViewModel(searchUser);
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
                throw new Exception("User with that email alreafy existed");
            }

            try
            {
                User userToAdd = registrationViewModel;
                userToAdd.Password = PasswordHashManager.GetPasswordHash(registrationViewModel.Password);
                User addedUser = this._unitOfWork.UsersRepository.Create(userToAdd);
                this._unitOfWork.SaveChanges();

                return addedUser;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not add link", ex);
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
                throw new Exception("Could not receieve users", ex);
            }
        }
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
                return userInfoView;
            }
            else
            {
                throw new Exception("Incorrect login or password");
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
