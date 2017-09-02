using FundTrack.DAL.Entities;
using System.Collections.Generic;

namespace FundTrack.DAL.Abstract
{
    public interface ITargetRepository
    {
        /// <summary>
        /// Get all targets.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Target> Read();

        /// <summary>
        /// Gets the target by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Target GetTargetByName(string name);

        IEnumerable<Target> GetTargetsByOrganizationId(int id);
    }
}
