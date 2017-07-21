using FundTrack.DAL.Entities;

namespace FundTrack.DAL.Abstract
{
    /// <summary>
    /// Repository from entity-Membership
    /// </summary>
    /// <seealso cref="FundTrack.DAL.Abstract.IRepository{FundTrack.DAL.Entities.Membership}" />
    public interface IMembershipRepository : IRepository<Membership>
    {
        /// <summary>
        /// Gets the user role.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>user role in db</returns>
        string GetRole(int userId);

        /// <summary>
        /// Determines whether is  user has role in membership table
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// <c>true</c> if is user has role; otherwise, <c>false</c>.
        /// </returns>
        bool IsUserHasRole(int userId);

        /// <summary>
        /// Gets the organization identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        int GetOrganizationId(int userId);

    }
}
