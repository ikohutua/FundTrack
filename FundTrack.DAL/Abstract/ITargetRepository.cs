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
       
        /// <summary>
        /// Get all targets by Organization Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<Target> GetTargetsByOrganizationId(int id);

        /// <summary>
        /// Get target by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Target GetTargetById(int id);

        Target Create(Target item);

        Target Update(Target item);

        void Delete(int id);


    }
}
