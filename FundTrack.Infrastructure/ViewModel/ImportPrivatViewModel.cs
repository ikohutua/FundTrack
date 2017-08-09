using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    public class ImportPrivatViewModel
    {
        public int idMerchant { get; set; }
        public string credit { get; set; }
        public string debet { get; set; }
        public ImportDetailPrivatViewModel[] importsDetail { get; set; }
    }
}
