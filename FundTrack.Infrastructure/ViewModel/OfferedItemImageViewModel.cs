using System;

namespace FundTrack.Infrastructure.ViewModel
{
    public class OfferedItemImageViewModel :  IEquatable<OfferedItemImageViewModel>
    {
        public int Id { get; set; }
        public int OfferedItemId { get; set; }
        public string ImageUrl { get; set; }
        public string Base64Data { get; set; }
        public bool IsMain { get; set; }
        public string ImageExtension { get; set; }

        public bool Equals(OfferedItemImageViewModel other)
        {
            return Id == other.Id;
        }
    }
}
