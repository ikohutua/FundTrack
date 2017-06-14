using FundTrack.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.BLL.Abstract
{
    public interface IUserDomainService
    {
        User CreateUser(User user);
    }
}
