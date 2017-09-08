using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FundTrack.Infrastructure.ViewModel
{
    public class ReportIncomeViewModel
    {
        public string Description { get; set; }
        public string From { get; set; }
        public string TargetTo { get; set; }
        public decimal MoneyAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
