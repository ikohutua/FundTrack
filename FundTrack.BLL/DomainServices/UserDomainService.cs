using FundTrack.BLL.Abstract;
using System;
using FundTrack.DAL.Entities;
using FundTrack.DAL.Abstract;

namespace FundTrack.BLL.DomainServices
{
    public class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository _userRepository;

        public UserDomainService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public User CreateUser(User user)
        {
            bool isUserExist = _userRepository.IsUserExists(user);

            if (isUserExist)
            {
                //ask nazar
                throw new Exception("User with the same email or login already exists");
            }
            else
            {
                return _userRepository.CreateUser(user);           
            }          
        }
    }
}
