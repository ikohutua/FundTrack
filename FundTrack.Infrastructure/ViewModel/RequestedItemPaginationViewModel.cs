using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    public class RequestedItemPaginationViewModel
    {
        public int TotalRequestedItemsCount { get; set; }
        public int RequestedItemsPerPage { get; set; }
    }
}
