using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.EditOrganizationViewModels
{
    public class TargetViewModel
    {
        public int Id { get; set; }

        public string TargetName { get; set; }

        public int OrganizationId { get; set; }
    }
}
