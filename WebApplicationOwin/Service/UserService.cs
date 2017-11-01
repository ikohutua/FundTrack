using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationOwin.Models;

namespace WebApplicationOwin.Service
{
    public class UserService
    {
        public User GetUserByCredentials(string email, string password)
        {
            User user = new User() { Id = "1", Email = "email@gmail.com", Password = "password", Name = "Andriy" };
            if (user != null)
            {
                user.Password = string.Empty;
            }
            return user;
        }
    }
}