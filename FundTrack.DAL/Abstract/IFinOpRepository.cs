using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
        IEnumerable<FinOp> Read();

        /// <summary>
        /// Gets the fin op by org account.
        /// </summary>
        /// <param name="orgAccountId">The org account identifier.</param>
        /// <returns></returns>
        IEnumerable<FinOp> GetFinOpByOrgAccount(int orgAccountId); 
    }
}
