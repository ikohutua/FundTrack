using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    public class GoodsTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<GoodsCategoryViewModel> TypeCategories { get; set; }
    }
}
