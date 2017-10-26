using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Bcpg;
using FundTrack.Infrastructure;
using FundTrack.Infrastructure.ViewModel.SuperAdminViewModels;

namespace FundTrack.BLL.Concrete
{
    public class FinOpService : IFinOpService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <Summary>
        /// Creates new instance of FinOpService
        /// </Amountmary>
        /// <param name="_unitOfWork">Unit of work</param>
        public FinOpService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Creates the fin op.
        /// </Summary>
        /// <param name="finOpModel">The fin op model.</param>
        /// <returns></returns>
        public FinOpFromBankViewModel CreateFinOp(FinOpFromBankViewModel finOpModel)
        {
            try
            {
                var bankImportDetail = _unitOfWork.BankImportDetailRepository.GetById(finOpModel.BankImportId);
                var finOp = ConvertFinOpFromBankViewModelToFinOpViewModel(finOpModel);

                switch(finOp.FinOpType)
                {
                    case Constants.FinOpTypeSpending:
                        CreateSpending(finOp);
                        break;
                    case Constants.FinOpTypeIncome:
                        CreateIncome(finOp);
                        break;
                    case Constants.FinOpTypeTransfer:
                        CreateTransfer(finOp);
                        break;
                }
                bankImportDetail.IsLooked = true;
                _unitOfWork.BankImportDetailRepository.ChangeBankImportState(bankImportDetail);
                _unitOfWork.SaveChanges();
                return finOpModel;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        private FinOpViewModel ConvertFinOpFromBankViewModelToFinOpViewModel(FinOpFromBankViewModel finOpModel)
        {
            var finOp = new FinOpViewModel
            {
                Amount = finOpModel.Amount,
                CardFromId = finOpModel.CardFromId.GetValueOrDefault(0),
                CardToId = finOpModel.CardToId.GetValueOrDefault(0),
                Description = finOpModel.Description,
                Date = finOpModel.FinOpDate,
                FinOpType = finOpModel.FinOpType,
                UserId = finOpModel.UserId,
                OrgId = finOpModel.OrgId,
                TargetId = finOpModel.TargetId
            };
            return finOp;
        }

        private void FinOpInputDataValidation(FinOpViewModel finOpModel)
        {
            if (finOpModel.Amount <= 0 || finOpModel.Amount > 1000000)
            {
                throw new ArgumentException(ErrorMessages.MoneyFinOpLimit);
            }
        }
        ///
        public FinOpViewModel CreateIncome(FinOpViewModel finOpModel)
        {
            FinOpInputDataValidation(finOpModel);
            try
            {
                var orgAccTo = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardToId);
                var finOp = new FinOp
                {
                    Amount = finOpModel.Amount,
                    AccToId = orgAccTo.Id,
                    Description = finOpModel.Description,
                    TargetId = finOpModel.TargetId,
                    FinOpDate = finOpModel.Date,
                    FinOpType = finOpModel.FinOpType,
                    UserId = finOpModel.UserId
                };
                _unitOfWork.FinOpRepository.Create(finOp);

                orgAccTo.CurrentBalance += finOpModel.Amount;
                _unitOfWork.OrganizationAccountRepository.Edit(orgAccTo);
                _unitOfWork.SaveChanges();
                return finOpModel;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.OperationIncomeError, ex);
            }
        }

        public FinOpViewModel CreateSpending(FinOpViewModel finOpModel)
        {
            FinOpInputDataValidation(finOpModel);
            try
            {
                var orgAccFrom = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardFromId);
                if (finOpModel.Amount > orgAccFrom.CurrentBalance)
                {
                    throw new ArgumentException(ErrorMessages.SpendingIsExceeded);
                }
                var finOp = new FinOp
                {
                    Amount = finOpModel.Amount,
                    AccFromId = orgAccFrom.Id,
                    Description = finOpModel.Description,
                    TargetId = finOpModel.TargetId,
                    FinOpDate = finOpModel.Date,
                    FinOpType = finOpModel.FinOpType,
                    UserId = finOpModel.UserId
                };
                _unitOfWork.FinOpRepository.Create(finOp);
                orgAccFrom.CurrentBalance -= finOpModel.Amount;
                _unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                _unitOfWork.SaveChanges();
                return finOpModel;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.OperationSpendingError, ex);
            }
        }

        public FinOpViewModel CreateTransfer(FinOpViewModel finOpModel)
        {
            FinOpInputDataValidation(finOpModel);
            try
            {
                var orgAccFrom = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardFromId);
                var orgAccTo = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardToId);
                if (finOpModel.Amount > orgAccFrom.CurrentBalance)
                {
                    throw new ArgumentException(ErrorMessages.SpendingIsExceeded);
                }
                var finOp = new FinOp
                {
                    Amount = finOpModel.Amount,
                    AccToId = orgAccTo.Id,
                    AccFromId = orgAccFrom.Id,
                    Description = finOpModel.Description,
                    FinOpDate = finOpModel.Date,
                    FinOpType = finOpModel.FinOpType,
                    UserId = finOpModel.UserId
                };
                _unitOfWork.FinOpRepository.Create(finOp);
                orgAccFrom.CurrentBalance -= finOpModel.Amount;
                _unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                orgAccTo.CurrentBalance += finOpModel.Amount;
                _unitOfWork.OrganizationAccountRepository.Edit(orgAccTo);
                _unitOfWork.SaveChanges();
                return finOpModel;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.OperationTransferError, ex);
            }
        }
        /// <Summary>
        /// Gets the fin ops by org account.
        /// </Summary>
        /// <param name="orgAccountId">The org account identifier.</param>
        /// <returns></returns>
        public IEnumerable<FinOpViewModel> GetFinOpsByOrgAccount(int orgAccountId)
        {
            try
            {
                var finOps = _unitOfWork.FinOpRepository.GetFinOpByOrgAccount(orgAccountId);
                return InitializeFinOps(finOps);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.EmptyFinOpList, ex);
            }
        }

        public IEnumerable<FinOpViewModel> GetFinOpByOrgAccountIdForPage(int orgAccountId, int currentPage, int itemsPerPage, int finOpType)
        {
            try
            {
                if (finOpType == Constants.AnyFinOpType)
                {
                    var finOps = _unitOfWork.FinOpRepository.GetFinOpByOrgAccountIdForPage(orgAccountId, currentPage, itemsPerPage);
                    return InitializeFinOps(finOps);
                }
                else
                {
                    var finOps = _unitOfWork.FinOpRepository.GetFinOpByOrgAccountIdForPage(orgAccountId, currentPage, itemsPerPage)
                        .Where(f => f.FinOpType == finOpType);
                    return InitializeFinOps(finOps);
                }
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.EmptyFinOpList, ex);
            }
        }

        private IEnumerable<FinOpViewModel> InitializeFinOps(IQueryable<FinOp> finOps)
        {
            try
            {
                var finOpList = finOps.Select(f => new FinOpViewModel
                {
                    Id = f.Id,
                    CardFromId = f.AccFromId.GetValueOrDefault(0),
                    CardToId = f.AccToId.GetValueOrDefault(0),
                    Date = f.FinOpDate,
                    Description = f.Description,
                    Amount = f.Amount,
                    TargetId = f.TargetId,
                    Target = f.Target.TargetName,
                    FinOpType = f.FinOpType,
                    DonationId = f.DonationId
                }).ToList();
                foreach (var f in finOpList)
                {
                    f.IsEditable = IsEditable(f.Date, f.CardFromId, f.CardToId);
                }
                return finOpList;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.EmptyFinOpList, ex);
            }
        }

        public bool IsEditable(DateTime finOpDate, int accountFromId, int accountToId)
        {
            if ((accountFromId > 0) && (accountToId > 0))
            {
                var balanceDateFrom = _unitOfWork.BalanceRepository.GetAllBalancesByAccountId(accountFromId).FirstOrDefault().BalanceDate;
                if (finOpDate <= balanceDateFrom)
                {
                    return false;
                }
            }
            if (accountFromId > 0)
            {
                var balanceDate = _unitOfWork.BalanceRepository.GetAllBalancesByAccountId(accountFromId).FirstOrDefault().BalanceDate;
                if (finOpDate <= balanceDate)
                {
                    return false;
                }
            }
            if (accountToId > 0)
            {
                var balanceDate = _unitOfWork.BalanceRepository.GetAllBalancesByAccountId(accountToId).FirstOrDefault().BalanceDate;
                if (finOpDate <= balanceDate)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<int> GetFinOpInitData(int accountId)
        {
            try
            {
                var finOp = _unitOfWork.FinOpRepository.GetFinOpByOrgAccount(accountId);
                List<int> TotalItemsForFinOpType = new List<int>
                {
                    finOp.Count(),
                    finOp.Where(f => f.FinOpType == Constants.FinOpTypeSpending).Count(),
                    finOp.Where(f => f.FinOpType == Constants.FinOpTypeIncome).Count(),
                    finOp.Where(f => f.FinOpType == Constants.FinOpTypeTransfer).Count()
                };
                return TotalItemsForFinOpType;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.GetOrganizationAccount, ex);
            }
        }

        /// <Summary>
        /// Gets the fin ops by id.
        /// </Summary>
        /// <param name="id">The fin ops identifier.</param>
        /// <returns></returns>
        public FinOpViewModel GetFinOpsById(int id)
        {
            try
            {
                if ((id <= 0))
                {
                    throw new BusinessLogicException(ErrorMessages.InvalidIdentificator);
                }
                var finOpFromDataBase = _unitOfWork.FinOpRepository.GetById(id);
                var finOp = new FinOpViewModel
                {
                    Id = finOpFromDataBase.Id,
                    CardFromId = finOpFromDataBase.AccFromId.GetValueOrDefault(0),
                    CardToId = finOpFromDataBase.AccToId.GetValueOrDefault(0),
                    Date = finOpFromDataBase.FinOpDate,
                    Description = finOpFromDataBase.Description,
                    Amount = finOpFromDataBase.Amount,
                    TargetId = finOpFromDataBase.TargetId,
                    Target = finOpFromDataBase.Target?.TargetName,
                    FinOpType = finOpFromDataBase.FinOpType,
                    DonationId = finOpFromDataBase.DonationId,
                    IsEditable = true
                };
                return finOp;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.EmptyFinOpList, ex);
            }
        }

        /// <Summary>
        /// Edit the finance operation.
        /// </Summary>
        /// <param name="finOpModel">The finance operation model.</param>
        /// <returns></returns>
        public FinOpViewModel EditFinOperation(FinOpViewModel finOpModel)
        {
            try
            {
                var finOp = _unitOfWork.FinOpRepository.GetById(finOpModel.Id);
                if (finOpModel.Amount != finOp.Amount)
                {
                    switch (finOpModel.FinOpType)
                    {
                        case Constants.FinOpTypeSpending:
                            OrgAccFromChangeBalance(finOpModel, finOp.Amount);
                            break;
                        case Constants.FinOpTypeIncome:
                            OrgAccToChangeBalance(finOpModel, finOp.Amount);
                            break;
                        case Constants.FinOpTypeTransfer:
                            OrgAccFromChangeBalance(finOpModel, finOp.Amount);
                            OrgAccToChangeBalance(finOpModel, finOp.Amount);
                            break;
                        default:
                            throw new BusinessLogicException(ErrorMessages.InvalidFinanceOperation);

                    }
                }
                if (finOpModel.FinOpType == Constants.FinOpTypeTransfer)
                {
                    if (finOp.FinOpType == Constants.FinOpTypeSpending )
                    {
                        Windtrhaw(finOpModel, finOp);
                    }

                    if (finOp.FinOpType == Constants.FinOpTypeIncome)
                    {
                        Deposite(finOpModel, finOp);
                    }
                }

                finOp.Description = finOpModel.Description;
                finOp.TargetId = finOpModel.TargetId;
                finOp.FinOpDate = finOpModel.Date;
                finOp.UserId = finOpModel.UserId;
                finOp.DonationId = finOpModel.DonationId;
                finOp.Amount = finOpModel.Amount;
                _unitOfWork.FinOpRepository.Update(finOp);
                _unitOfWork.SaveChanges();
                return finOpModel;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.UpdateDataError, ex);
            }
        }

        public void OrgAccFromChangeBalance(FinOpViewModel finOpModel, decimal originalAmount)
        {
            var orgAccFrom = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardFromId);
            orgAccFrom.CurrentBalance -= finOpModel.Amount - originalAmount;
            _unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
        }

        public void OrgAccToChangeBalance(FinOpViewModel finOpModel, decimal originalAmount)
        {
            var orgAccTo = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardToId);
            orgAccTo.CurrentBalance += finOpModel.Amount - originalAmount;
            _unitOfWork.OrganizationAccountRepository.Edit(orgAccTo);
        }

        public void Windtrhaw(FinOpViewModel finOpModel, FinOp finOp)
        {
            var orgAccTo = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardToId);
            orgAccTo.CurrentBalance += finOpModel.Amount;
            finOp.FinOpType = finOpModel.FinOpType;
            finOp.AccToId = finOpModel.CardToId;
            _unitOfWork.OrganizationAccountRepository.Edit(orgAccTo);
        }

        public void Deposite(FinOpViewModel finOpModel, FinOp finOp)
        {
            var orgAccFrom = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardFromId);
            orgAccFrom.CurrentBalance -= finOpModel.Amount;
            finOp.FinOpType = finOpModel.FinOpType;
            finOp.AccToId = finOpModel.CardToId;
            _unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
        }

        public IEnumerable<FinOpViewModel> GetAllFinOpsByOrgId(int orgId)
        {
            try
            {
                var finOps = _unitOfWork.FinOpRepository.Read().ToList();
                return finOps.Select(f => new FinOpViewModel
                {
                    Id = f.Id,
                    CardFromId = f.AccFromId.GetValueOrDefault(0),
                    CardToId = f.AccToId.GetValueOrDefault(0),
                    Date = f.FinOpDate,
                    Description = f.Description,
                    Amount = f.Amount,
                    TargetId = f.TargetId,
                    Target = f.Target?.TargetName,
                    FinOpType = f.FinOpType,
                    IsEditable = true,
                    OrgId = f.OrgAccountTo?.OrgId ?? f.OrgAccountFrom.OrgId,
                    DonationId = f.DonationId
                }).Where(f => f.OrgId == orgId);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(ErrorMessages.GetFinOpWithoutAccount, e);
            }
            
        }
        
        public FinOpViewModel BindDonationAndFinOp(FinOpViewModel finOp)
        {
            try
            {
                var donation = _unitOfWork.DonationRepository.Get((int)finOp.DonationId);
                donation.UserId = finOp.UserId;
                _unitOfWork.DonationRepository.Update(donation);
                var finOpEntity = _unitOfWork.FinOpRepository.GetById(finOp.Id);
                finOpEntity.UserId = finOp.UserId;
                finOpEntity.DonationId = finOp.DonationId;
                _unitOfWork.FinOpRepository.Update(finOpEntity);
                _unitOfWork.SaveChanges();
                return finOp;
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(ErrorMessages.BindingDonationToFinOp, e);
            }
        }
    }
}

