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
                var finOp = new FinOp
                {
                    TargetId = finOpModel.TargetId,
                    Amount = finOpModel.AbsoluteAmount,
                    Description = finOpModel.Description,
                    FinOpDate = bankImportDetail.Trandate
                };
                OrgAccount orgAccFrom = new OrgAccount();
                OrgAccount orgAccTo = new OrgAccount();
                if (finOpModel.CardFromId != null)
                {
                    orgAccFrom = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById((int)finOpModel.CardFromId);
                    finOp.AccFromId = orgAccFrom.Id;
                    finOp.FinOpType = Constants.FinOpSpending;
                    orgAccFrom.CurrentBalance += finOpModel.Amount;
                    _unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                }
                if (finOpModel.CardToId != null)
                {
                    orgAccTo = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById((int)finOpModel.CardToId);
                    finOp.AccToId = orgAccTo.Id;
                    orgAccTo.CurrentBalance += finOpModel.Amount;
                    finOp.FinOpType = Constants.FinOpIncome;
                    _unitOfWork.OrganizationAccountRepository.Edit(orgAccTo);
                }
                _unitOfWork.FinOpRepository.Create(finOp);
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
                var finOps = _unitOfWork.FinOpRepository.GetFinOpByOrgAccount(orgAccountId)
                    .OrderByDescending(f => f.Id)
                    .Select(f => new FinOpViewModel
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
                        IsEditable = true,
                        DonationId = f.DonationId
                    });
                return finOps;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.EmptyFinOpList, ex);
            }
        }
        /// <Amountmary>
        /// Gets the fin ops by id.
        /// </Amountmary>
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

        public FinOpViewModel EditFinOperation(FinOpViewModel finOpModel)
        {
            try
            {
                var orgAccFrom = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardFromId);

                var finOp = _unitOfWork.FinOpRepository.GetById(finOpModel.Id);
                finOp.Amount = finOpModel.Amount;
                finOp.Description = finOpModel.Description;
                finOp.TargetId = finOpModel.TargetId;
                finOp.FinOpDate = finOpModel.Date;
                finOp.UserId = finOpModel.UserId;
                finOp.DonationId = finOpModel.DonationId;
                _unitOfWork.FinOpRepository.Update(finOp);

                switch (finOpModel.FinOpType)
                {
                    case Constants.FinOpSpending:
                        orgAccFrom.CurrentBalance -= finOpModel.Difference;
                        _unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                        break;
                    case Constants.FinOpIncome:
                        orgAccFrom.CurrentBalance += finOpModel.Difference;
                        _unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                        break;
                    case Constants.FinOpTransfer:
                        var orgAccTo = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardToId);
                        orgAccFrom.CurrentBalance -= finOpModel.Difference;
                        orgAccTo.CurrentBalance += finOpModel.Difference;
                        _unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                        _unitOfWork.OrganizationAccountRepository.Edit(orgAccTo);
                        break;
                    default:
                        throw new BusinessLogicException(ErrorMessages.InvalidFinanceOperation);

                }
                _unitOfWork.SaveChanges();
                return finOpModel;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.UpdateDataError, ex);
            }
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

