using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using FundTrack.Infrastructure.Attributes;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels
{
    public class FinOpViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredFieldMessage)]
        public string Description { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredFieldMessage)]
        [Range(0.01, 100.00, ErrorMessage = ErrorMessages.MoneyFinOpLimit)]
        public decimal Amount { get; set; }

        public int AccFromId { get; set; }

        public int AccToId { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredFieldMessage)]
        [MinValue(1)]
        public int OrgId { get; set; }

        public string Target { get; set; }

        public int? TargetId { get; set; }

        public int? UserId { get; set; }

        public IEnumerable<FinOpImageViewModel> Images { get; set; }

        public int FinOpType { get; set; }

        public DateTime Date { get; set; }

        public bool IsEditable { get; set; }

        public string Error { get; set; }

        public int? DonationId { get; set; }

    }
}
