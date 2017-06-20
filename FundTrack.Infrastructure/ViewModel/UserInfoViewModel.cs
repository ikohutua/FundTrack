using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.Infrastructure.ViewModel
{
    //dummy model for verification that userInfo component works
    public class UserInfoViewModel
    {
        public int userId { get; set; }
        public string userLogin { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string userEmail { get; set; }
        public string userAddress { get; set; }
        public string userPhotoUrl { get; set; }

    }
}
