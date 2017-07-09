using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FundTrack.Infrastructure.ViewModel
{
    public sealed class OfferedItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string[] ImageUrl { get; set; }
        public string StatusName { get; set; }
        public string GoodsCategoryName { get; set; }
        public string GoodsTypeName { get; set; }
        public string Error { get; set; }
        

        
    }
}
