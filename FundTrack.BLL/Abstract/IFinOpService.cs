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
        /// Gets the targets with name and id .
        /// </summary>
        /// <returns></returns>
        IEnumerable<TargetViewModel> GetTargets(int id);

        /// <summary>
        /// Creates the fin op.
        /// </summary>
        /// <param name="finOpModel">The fin op model.</param>
        /// <returns></returns>
        FinOpFromBankViewModel CreateFinOp(FinOpFromBankViewModel finOpModel);

        FinOpViewModel CreateIncome(FinOpViewModel finOpModel);

        FinOpViewModel CreateSpending(FinOpViewModel finOpModel);

        FinOpViewModel CreateTransfer(FinOpViewModel finOpModel);

        FinOpViewModel EditFinOperation(FinOpViewModel finOpModel);

        /// <summary>
        /// Gets the fin op by id.
        /// </summary>
        /// <param name="orgAccountId">The fin op identifier.</param>
        /// <returns></returns>
        FinOpViewModel GetFinOpsById(int id);

        /// <summary>
        /// Gets the fin ops by org account.
        /// </summary>
        /// <param name="orgAccountId">The org account identifier.</param>
        /// <returns></returns>
        IEnumerable<FinOpViewModel> GetFinOpsByOrgAccount(int orgAccountId);
    }
}
