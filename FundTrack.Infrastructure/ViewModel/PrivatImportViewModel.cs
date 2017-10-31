using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    public class PrivatImportViewModel
    {
        public int IdMerchant { get; set; }
        public string Password { get; set; }
        public string Card { get; set; }
        public string DataFrom { get; set; }
        public string DataTo { get; set; }
    }
}
