using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels
{
    public class TargetReportViewModel
    {
        public int Id { get; set; }
        public string TargetName { get; set; }
        public decimal Sum { get; set; }
    }
}
