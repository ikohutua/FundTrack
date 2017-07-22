using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.EditOrganizationViewModels
{
    /// <summary>
    /// EditAddressViewModel
    /// </summary>
   public class EditAddressViewModel
    {
        public int OrgId { get; set; }
        public string ErrorMessage { get; set; }
        public AddressViewModel[] Addresses { get; set; }
    }
}
