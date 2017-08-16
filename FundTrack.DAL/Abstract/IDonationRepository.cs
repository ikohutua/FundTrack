using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.DAL.Abstract
{
    public interface IDonationRepository
    {
        Donation Create(Donation item);
        IEnumerable<Donation> Read();
    }
}
