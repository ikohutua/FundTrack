using FundTrack.BLL.Abstract;
using FundTrack.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FundTrack.WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserDomainService _userDomainService;

        public UserController(IUserDomainService userDomainService)
        {
            this._userDomainService = userDomainService;
        }

        [HttpPost]
        public User CreateUser(User user)
        {
            return _userDomainService.CreateUser(user);
        }
    }
}