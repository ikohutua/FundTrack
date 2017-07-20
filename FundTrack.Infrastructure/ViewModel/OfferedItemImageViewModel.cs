using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.Infrastructure.ViewModel
{
    public class OfferedItemImageViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int OfferedItemId { get; set; }
        public bool IsMain { get; set; }
    }
}
