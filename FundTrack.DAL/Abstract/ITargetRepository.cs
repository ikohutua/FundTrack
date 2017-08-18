using FundTrack.DAL.Entities;
using System.Collections.Generic;

namespace FundTrack.DAL.Abstract
{
    public interface ITargetRepository
    {
        IEnumerable<Target> Read();

        Target GetTargetByName(string name);
    }
}
