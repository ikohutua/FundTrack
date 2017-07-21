using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.Infrastructure.ViewModel
{
    public class OfferItemChangeStatusViewModel
    {
        public int OfferItemId { get; set; }
        public string OfferItemStatus { get; set; }
        public int UserId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
