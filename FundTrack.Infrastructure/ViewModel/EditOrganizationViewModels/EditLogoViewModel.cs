using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.EditOrganizationViewModels
{
    public class EditLogoViewModel
    {
        public int OrganizationId { get; set; }
        public string Base64Code { get; set; }
        public string LogoUrl { get; set; }
    }
}
