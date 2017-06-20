using FundTrack.BLL.Abstract;
using System;
using FundTrack.DAL.Entities;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System.Security.Claims;
using System.Collections.Generic;
using FundTrack.DAL.Concrete;
using System.Linq;

namespace FundTrack.BLL.DomainServices
{
    /// <summary>
    /// service for authorization and registration
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IUserDomainService" />
    public class UserDomainService : IUserDomainService
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
        /// <returns></returns>
        public User GetUser(AuthorizeViewModel user)
        {
            User inputUser = new User();
            inputUser.Login = user.Login;
            inputUser.Password = user.Password;
            return unitOfWork.UsersRepository.Read().FirstOrDefault(u => u.Login.ToLower() == user.Login.ToLower()
             && u.Password.ToLower() == user.Password.ToLower());
        }
        /// <summary>
        /// Сreate claims identity for search user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Login or password are not correct</exception>
        public ClaimsIdentity RegisterUserClaim(AuthorizeViewModel user)
        {
            var searchUser = this.GetUser(user);
            if (searchUser != null)
            {

                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,searchUser.Login)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            else
            {
                throw new Exception("Incorrect");
            }
        }
    }
}
