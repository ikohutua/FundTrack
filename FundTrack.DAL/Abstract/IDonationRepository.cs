using FundTrack.DAL.Entities;
using System.Linq;

namespace FundTrack.DAL.Abstract
{
    public interface IDonationRepository
    {
        Donation Create(Donation item);
        IQueryable<Donation> Read();
        Donation Get(int id);
        Donation Update(Donation item);
    }
}
