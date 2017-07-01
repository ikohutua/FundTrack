using System.Collections.Generic;
using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.WebUI.Controllers
{
    internal class AuthorizeUserModel
    {
        public string login { get; set; }
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string photoUrl { get; set; }
        public string role { get; set; }
    }

    internal class AuthorizationType
    {
        public AuthorizeUserModel userModel { get; set; }
        public string access_token { get; set; }
        public string errorMessage { get; internal set; }
        public List<ValidationViewModel> validationSummary { get; internal set; }
    }
}