namespace FundTrack.WebUI.Controllers
{
    internal class AuthorizationType
    {
        public string access_token { get; set; }
        public string login { get; set; }
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string photoUrl { get; set; }
        public string errorMessage { get; internal set; }
    }
}