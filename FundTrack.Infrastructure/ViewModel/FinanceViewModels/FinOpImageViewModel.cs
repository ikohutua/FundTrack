using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels
{
    public class FinOpImageViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Base64Data { get; set; }
        public string imageExtension { get; set; }
    }
}
