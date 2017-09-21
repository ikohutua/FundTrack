using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using FundTrack.Infrastructure.Attributes;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels
{
    public class FinOpViewModel
    {
        [Required(ErrorMessage = ErrorMessages.RequiredFieldMessage)]
        public string Description { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredFieldMessage)]
        [Range(0.01, 100.00, ErrorMessage = ErrorMessages.MoneyFinOpLimit)]
        public decimal Amount { get; set; }

        public string AccNameTo { get; set; }

        public string AccNameFrom { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredFieldMessage)]
        [MinValue(1)]
        public int OrgId { get; set; }

        public int? TargetId { get; set; }

        public int UserId { get; set; }

        public IEnumerable<string> PhotoUrls { get; set; }

        public string Currency { get; set; }
    }
}
