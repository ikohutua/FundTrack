using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels
{
    public class FinOpViewModel
    {
        public int Id { get; set; }
        public int BankImportId { get; set; }
        public string TargetName { get; set; }
        public string AccFromName { get; set; }
        public string AccToName { get; set; }
        public decimal Amount { get; set; }
        public decimal AbsoluteAmount { get; set; }
        public string Description { get; set; }
        public int OrgId { get; set; }
    }
}
