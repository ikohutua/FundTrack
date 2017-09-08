using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    public class ReportOutcomeViewModel
    {
        public string Target { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal MoneyAmount { get; set; }

    }
}
