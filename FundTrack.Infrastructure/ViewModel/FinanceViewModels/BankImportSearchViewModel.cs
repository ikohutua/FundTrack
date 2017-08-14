using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels
{
    public class BankImportSearchViewModel
    {
        public DateTime DataFrom { get; set; }
        public DateTime DataTo { get; set; }
        public string Card { get; set; }
        public bool State { get; set; }
    }
}
