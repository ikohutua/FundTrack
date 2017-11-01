using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.SuperAdminViewModels;
using System.Collections.Generic;

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
        /// Creates the fin ops from bank account.
        /// </summary>
        /// <param name="finOpModel">The fin op model.</param>
        /// <returns></returns>
        IEnumerable<FinOpFromBankViewModel> ProcessMultipleFinOp(int orgAccId);

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

        IEnumerable<FinOpViewModel> GetFinOpByOrgAccountIdForPage(int orgAccountId, int currentPage, int itemsPerPage, int finOpType);

        IEnumerable<int> GetFinOpInitData(int accountId);

        /// <summary>
        /// Gets all finOps by organizationId
        /// </summary>
        /// <param name="orgId">Id of organization</param>
        /// <returns>collection of finOps</returns>
        IEnumerable<FinOpViewModel> GetAllFinOpsByOrgId(int orgId);

        /// <summary>
        /// Bind donation and relative finOp
        /// </summary>
        /// <param name="finOp"></param>
        /// <returns></returns>
        FinOpViewModel BindDonationAndFinOp(FinOpViewModel finOp);
    }
}
