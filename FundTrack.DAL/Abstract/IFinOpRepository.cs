using FundTrack.DAL.Entities;
using System.Linq;

namespace FundTrack.DAL.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFinOpRepository
    {
        /// <summary>
        /// Creates the specified fin op.
        /// </summary>
        /// <param name="finOp">The fin op.</param>
        /// <returns></returns>
        FinOp Create(FinOp finOp);

        /// <summary>
        /// Updates the specified fin op.
        /// </summary>
        /// <param name="finOp">The fin op.</param>
        /// <returns></returns>
        FinOp Update(FinOp finOp);

        /// <summary>
        /// Gets finOp the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        FinOp GetById(int id);

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns></returns>
        IQueryable<FinOp> Read();

        /// <summary>
        /// Gets the fin op by org account.
        /// </summary>
        /// <param name="orgAccountId">The org account identifier.</param>
        /// <returns></returns>
        IQueryable<FinOp> GetFinOpByOrgAccount(int orgAccountId);

        /// <summary>
        /// Gets the amount of fin operation for the page by the organization ID
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns> IEnumerable<Event> </returns>
        IQueryable<FinOp> GetFinOpByOrgAccountIdForPage(int orgAccountId, int currentPage, int itemsPerPage);

        /// <summary>
        /// Gets the amount of fin operations for the page by the organization ID and finOpType
        /// Join with event images
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns> IEnumerable<Event> </returns>
        IQueryable<FinOp> GetFinOpByOrgAccountIdForPageByCritery(int orgAccountId, int currentPage, int itemsPerPage, int finOpType);
    }
}
