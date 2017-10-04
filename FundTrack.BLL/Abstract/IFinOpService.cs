using FundTrack.DAL.Entities;
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
        /// Creates the fin op.
        /// </summary>
        /// <param name="finOpModel">The fin op model.</param>
        /// <returns></returns>
        FinOpFromBankViewModel CreateFinOp(FinOpFromBankViewModel finOpModel);

        FinOpViewModel CreateIncome(FinOpViewModel finOpModel);

        FinOpViewModel CreateSpending(FinOpViewModel finOpModel);

        FinOpViewModel CreateTransfer(FinOpViewModel finOpModel);

        /// <summary>
        /// Gets the fin ops by org account.
        /// </summary>
        /// <param name="orgAccountId">The org account identifier.</param>
        /// <returns></returns>
        IEnumerable<FinOpListViewModel> GetFinOpsByOrgAccount(int orgAccountId);

        /// <summary>
        /// Gets all finOps by organizationId
        /// </summary>
        /// <param name="orgId">Id of organization</param>
        /// <returns>collection of finOps</returns>
        //IEnumerable<FinOpViewModel> GetAllFinOpsByOrgId(int orgId);
    }
}
