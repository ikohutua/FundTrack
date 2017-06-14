using FundTrack.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using FundTrack.Infrastructure.Models;
using System.Linq;

namespace FundTrack.DAL.Repositories
{
    public class FakeUserRepository : IUserRepository
    {
        private static List<User> _users;

        static FakeUserRepository()
        {
            _users = new List<User>{
                new User
                {
                    Id = 1,
                    FirstName = "Vasyl",
                    SecondName = "Sypa",
                    Email = "vasyl@gmail.com",
                    Login = "Vasilio",
                    Password = "12345"
                },
                new User
                {
                    Id = 2,
                    FirstName = "Ihor",
                    SecondName = "Vavrynyk",
                    Email = "ihor@gmail.com",
                    Login = "Ihorio",
                    Password = "7890"
                }
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
