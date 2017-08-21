using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.BLL.Abstract
{
    public interface IFinOpService
    {
        /// <summary>
        /// Gets the targets with name and id .
        /// </summary>
        /// <returns></returns>
        IEnumerable<TargetViewModel> GetTargets();

        /// <summary>
        /// Creates the fin op.
        /// </summary>
        /// <param name="finOpModel">The fin op model.</param>
        /// <returns></returns>
        FinOpViewModel CreateFinOp(FinOpViewModel finOpModel);

        /// <summary>
        /// Gets the fin ops by org account.
        /// </summary>
        /// <param name="orgAccountId">The org account identifier.</param>
        /// <returns></returns>
        IEnumerable<FinOpListViewModel> GetFinOpsByOrgAccount(int orgAccountId);
    }
}
