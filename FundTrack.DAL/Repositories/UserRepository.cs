using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static List<User> _users;

        static UserRepository()
        {
            _users = new List<User>{
                new User
                {
                    Id = 1,
                    Login = "Vasilio",
                    Email = "vasyl@gmail.com",
                    Password = "12345",
                    FirstName = "Vasya",
                    LastName = "Sypa",
                    FB_Link = "aaa",
                    PhotoUrl = "bbb",
                },
                new User
                {
                    Id = 2,
                    Login = "Ihorko",
                    Email = "ihor@gmail.com",
                    Password = "67890",
                    FirstName = "Ihor",
                    LastName = "Vavrunyk",
                    FB_Link = "aaa",
                    PhotoUrl = "bbb",
                },
            };
        }
        public User CreateUser(User user)
        {
            _users.Add(user);

            return user;
        }

        public bool IsUserExists(User user)
        {
            bool isUserExists = _users.Any(u => u.Email.ToLower() == user.Email.ToLower()
                                        || u.Login.ToLower() == user.Login.ToLower());

            return isUserExists;
        }
    }
}
