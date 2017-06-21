using FundTrack.BLL.Abstract;
using System;
using FundTrack.DAL.Entities;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System.Linq;

namespace FundTrack.BLL.DomainServices
{
    /// <summary>
    /// Service for authorization and registration
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IUserDomainService" />
    public sealed class UserDomainService : IUserDomainService
    {
        private readonly IUnitOfWork unitOfWork;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDomainService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserDomainService(IUnitOfWork unitOfWorkParam)
        {
            this.unitOfWork = unitOfWorkParam;
        }

        /// <summary>
        /// Get user for DAL which come from parameter
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User from db</returns>
        public User GetUser(AuthorizeViewModel user)
        {
            var inputUser = new User
            {
                Login = user.Login,
                Password = user.Password
            };
            return unitOfWork.UsersRepository
                             .Read()
                             .FirstOrDefault(u => u.Login.ToLower() == user.Login.ToLower()
                              && u.Password.ToLower() == user.Password.ToLower());
        }

        /// <summary>
        /// Call method GetUser from db and map to type UserInfoViewModel
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Model which contain information about user</returns>
        /// <exception cref="System.Exception">Login or password are not correct</exception>
        public UserInfoViewModel GetUserInfoViewModel(AuthorizeViewModel user)
        {
            var searchUser = this.GetUser(user);
            if (searchUser != null)
            {
                var userInfoView = new UserInfoViewModel
                {
                    userId = searchUser.Id,
                    userLogin = searchUser.Login,
                    userFirstName = searchUser.FirstName,
                    userLastName = searchUser.LastName,
                    userEmail = searchUser.Email,
                    userPhotoUrl = searchUser.PhotoUrl
                };
                return userInfoView;
            }
            else
            {
                throw new Exception("Incorrect login or password");
            }
        }
    }
}
