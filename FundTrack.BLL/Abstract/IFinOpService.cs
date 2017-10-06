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
        /// Creates the fin op from bank account.
        /// </summary>
        /// <param name="finOpModel">The fin op model.</param>
        /// <returns></returns>
        FinOpFromBankViewModel CreateFinOp(FinOpFromBankViewModel finOpModel);
        /// <summary>
        /// Creates the income fin op from cash account
        /// </summary>
        /// <param name="finOpModel"></param>
        /// <returns></returns>
        FinOpViewModel CreateIncome(FinOpViewModel finOpModel);

        /// <summary>
        /// Creates the spending fin op from cash account
        /// </summary>
        /// <param name="finOpModel"></param>
        /// <returns></returns>
        FinOpViewModel CreateSpending(FinOpViewModel finOpModel);

        /// <summary>
        /// Creates the transfer fin op from cash account
        /// </summary>
        /// <param name="finOpModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets all finOps by organizationId
        /// </summary>
        /// <param name="orgId">Id of organization</param>
        /// <returns>collection of finOps</returns>
        IEnumerable<FinOpViewModel> GetAllFinOpsByOrgId(int orgId);
    }
}
