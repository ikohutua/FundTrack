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
        /// <returns>List user responses</returns>
        IQueryable<UserResponse> ReadAsQueryable();

        /// <summary>
        /// Reads as queryable for pagination
        /// </summary>
        /// <returns>List user response</returns>
        IQueryable<UserResponse> ReadForPagination(int OrganizationId, int ItemsPerPage, int CurrentPage);
    }
}
