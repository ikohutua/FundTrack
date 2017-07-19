using FundTrack.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    public class RequestedItemViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string GoodsCategory { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        [MinValue(1)]
        public int GoodsCategoryId { get; set; }

        //[MinValue(1)]
        public int OrganizationId { get; set; }
        public string ErrorMessage { get; set; }

        [MinValue(1)]
        public int GoodsTypeId { get; set; }
        public IEnumerable<RequestedImageViewModel> Images { get; set; }
    }
}
