using System;

namespace FundTrack.Infrastructure.ViewModel
{
    public class ImportDetailPrivatViewModel
    {
        public int id { get; set; }
        public string card { get; set; }
        public DateTime trandate { get; set; }
        public string amount { get; set; }
        public string appCode { get; set; }
        public string cardAmount { get; set; }
        public string rest { get; set; }
        public string terminal { get; set; }
        public string description { get; set; }
        public bool isLooked { get; set; }
    }
}
