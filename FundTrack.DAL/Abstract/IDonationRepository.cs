using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundTrack.DAL.Abstract
{
    public interface IDonationRepository
    {
        Donation Create(Donation item);
        IQueryable<Donation> Read();
        Donation Get(int id);
    }
}
