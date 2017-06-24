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
        public User GetUser(string userLogin, string rawPassword)
        {
            var hashedPassword = PasswordHashManager.GetPasswordHash(rawPassword);

            return this._unitOfWork.UsersRepository
                             .Read()
                             .FirstOrDefault(u => u.Login.ToLower() == userLogin.ToLower()
                              && u.Password == hashedPassword);
        }

        /// <summary>
        /// Gets user info view model
        /// </summary>
        /// <param name="userLogin">User login</param>
        /// <param name="rawPassword">User raw password</param>
        /// <returns>User info view model</returns>
        public UserInfoViewModel GetUserInfoViewModel(string userLogin, string rawPassword)
        {
            var searchUser = this.GetUser(userLogin, rawPassword);
            //var searchUserRole = this._unitOfWork.MembershipRepository.GetRole(searchUser.Id);
            if (searchUser != null)
            {
                var userInfoView = new UserInfoViewModel
                {
                    userId = searchUser.Id,
                    userLogin = searchUser.Login,
                    userFirstName = searchUser.FirstName,
                    userLastName = searchUser.LastName,
                    userEmail = searchUser.Email,
                    userPhotoUrl = searchUser.PhotoUrl,
                    userRole=searchUserRole
                };
                return userInfoView;
            }
            else
            {
                throw new Exception("Incorrect login or password");
            }
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
                throw new Exception("Could not recieve users", ex);
            }
        }
    }
}
