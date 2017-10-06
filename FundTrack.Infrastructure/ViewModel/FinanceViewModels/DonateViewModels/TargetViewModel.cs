using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels
{
    public class TargetViewModel
    {
        public int TargetId { get; set; }
        public string Name { get; set; }
        public int OrganizationId { get; set; }
        public int? ParentTargetId { get; set; }
        public Boolean IsDeletable { get; set; } = true;
    }
}
