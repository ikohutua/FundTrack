using FundTrack.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.DAL.Abstract;
using System.Linq;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure;

namespace FundTrack.BLL.Concrete
{
    public class FixingBalanceService : IFixingBalanceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FixingBalanceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Get data for filtering on fixing balance component
        /// </summary>
        /// <param name="accountId">Id of account</param>
        /// <returns>Filtering for fixing balance</returns>
        public FixingBalanceFilteringViewModel GetFilteringByAccId(int accountId)
        {
            var finOps = _unitOfWork.FinOpRepository.GetFinOpByOrgAccount(accountId);
            FixingBalanceFilteringViewModel fixing = new FixingBalanceFilteringViewModel();
            fixing.ServerDate = DateTime.Now.Date;
            fixing.FirstDayForFixingBalance = finOps
                                    .OrderByDescending(x => x.FinOpDate)
                                    .FirstOrDefault()
                                    ?.FinOpDate
                                    .Date
                                    .AddDays(1);
            var allFixingForAccount = _unitOfWork.BalanceRepository.GetAllBalancesByAccountId(accountId);
            var lastFix = allFixingForAccount.OrderByDescending(x => x.BalanceDate).FirstOrDefault();
            if (lastFix != null)
            {
                fixing.LastFixing = ConvertToBalanceViewModel(lastFix);
                fixing.HasFinOpsAfterLastFixing = HasFinOpsAfterLastFixing(fixing.LastFixing);
            }
            else
            {
                fixing.HasFinOpsAfterLastFixing = _unitOfWork.FinOpRepository.GetFinOpByOrgAccount(accountId).Any();
            }
            return fixing;
        }

        /// <summary>
        /// Fixing balance
        /// </summary>
        /// <param name="balance">Instance with info ablout fixing.</param>
        /// <returns>Added data</returns>
        public BalanceViewModel AddNewBalance(BalanceViewModel balance)
        {
            if (IsFinOpsInBalanceDay(balance))
            {
                throw new BusinessLogicException(ErrorMessages.AddNewBalanceMessage);
            }
            decimal amount = _unitOfWork.OrganizationAccountRepository.Read(balance.OrgAccountId).CurrentBalance;
            balance.Amount = amount;
            var addedBalanceEntity = _unitOfWork.BalanceRepository.Add(balance);
            _unitOfWork.SaveChanges();
            BalanceViewModel addedBalanceViewModel = ConvertToBalanceViewModel(addedBalanceEntity);
            return addedBalanceViewModel;
        }

        private BalanceViewModel ConvertToBalanceViewModel(Balance balance)
        {
            return new BalanceViewModel
            {
                Amount = balance.Amount,
                BalanceDate = balance.BalanceDate,
                OrgAccountId = balance.OrgAccountId
            };
        }
        
        private bool HasFinOpsAfterLastFixing (BalanceViewModel lastBalanceFixing)
        {
           return _unitOfWork.FinOpRepository
              .GetFinOpByOrgAccount(lastBalanceFixing.OrgAccountId)
              .Where(x => x.FinOpDate >= lastBalanceFixing.BalanceDate)
              .Any();
        }

        private bool IsFinOpsInBalanceDay (BalanceViewModel balance)
        {
            var finops = _unitOfWork.FinOpRepository.GetFinOpByOrgAccount(balance.OrgAccountId);
            return finops.Where(x => x.FinOpDate.Date == balance.BalanceDate.Date).Any();
        }
    }
}
