using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    public class RequestedItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GoodsCategory { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int GoodsCategoryId { get; set; }
        public int OrganizationId { get; set; }
        public IEnumerable<RequestedImageViewModel> Images { get; set; }
    }
}
