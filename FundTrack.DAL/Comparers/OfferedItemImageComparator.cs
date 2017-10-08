using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;

namespace FundTrack.DAL.Comparers
{
    public class OfferedItemImageComparator : IEqualityComparer<OfferedItemImage>
    {
        public bool Equals(OfferedItemImage x, OfferedItemImage y)
        {
            return x != null && y != null && x.Id == y.Id && x.ImageUrl == y.ImageUrl;
        }

        public int GetHashCode(OfferedItemImage obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;
            int hashOfferUrl = obj.ImageUrl == null ? 0 : obj.ImageUrl.GetHashCode();
            int hashOfferId = obj.Id.GetHashCode();
            return hashOfferUrl ^ hashOfferId;
        }
    }
}
