using FundTrack.DAL.Entities;
using System.Linq;

namespace FundTrack.DAL.Abstract
{
    /// <summary>
    /// Interface which works with User Response
    /// </summary>
    /// <seealso cref="FundTrack.DAL.Abstract.IRepository{FundTrack.DAL.Entities.UserResponse}" />
    public interface IUserResponseRepository:IRepository<UserResponse>
    {
        /// <summary>
        /// Reads as queryable.
        /// </summary>
        /// <returns></returns>
        IQueryable<UserResponse> ReadAsQueryable();
    }
}
