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

        /// <Amountmary>
        /// Creates new instance of FinOpService
        /// </Amountmary>
        /// <param name="_unitOfWork">Unit of work</param>
        public FinOpService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        /// <summary>
        /// Creates the fin op.
        /// </Amountmary>
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
                    finOp.FinOpType = 0;
                    orgAccFrom.CurrentBalance += finOpModel.Amount;
                    _unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                }
                if (finOpModel.CardToId != null)
                {
                    orgAccTo = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById((int)finOpModel.CardToId);
                    finOp.AccToId = orgAccTo.Id;
                    orgAccTo.CurrentBalance += finOpModel.Amount;
                    finOp.FinOpType = 1;
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
                throw new BusinessLogicException("Щось пішло не так....О_о", ex);
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
                    throw new ArgumentException("Витрати не можуть перебільшувати баланс рахунку");
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
                throw new BusinessLogicException("Щось пішло не так....О_о", ex);
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
                    throw new ArgumentException("Витрати не можуть перебільшувати баланс рахунку");
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
                throw new BusinessLogicException("Щось пішло не так....О_о", ex);
            }
        }
        /// <Amountmary>
        /// Gets the fin ops by org account.
        /// </Amountmary>
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
                        IsEditable = true
                    });
                return finOps;
            }
            catch (Exception ex)
            {
                return new FinOpViewModel
                {
                    Error = "Список фінансових операцій порожній."
                } as IEnumerable<FinOpViewModel>;
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
                var f = _unitOfWork.FinOpRepository.GetById(id);
                var finOp = new FinOpViewModel
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
                    IsEditable = true
                };
                return finOp;
            }
            catch (Exception ex)
            {
                return new FinOpViewModel
                {
                    Error = "Список фінансових операцій порожній."
                };
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
                _unitOfWork.FinOpRepository.Update(finOp);

                switch (finOpModel.FinOpType)
                {
                    case 0:
                        orgAccFrom.CurrentBalance -= finOpModel.Difference;
                        _unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                        break;
                    case 1:
                        orgAccFrom.CurrentBalance += finOpModel.Difference;
                        _unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                        break;
                    case 2:
                        var orgAccTo = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardToId);
                        orgAccFrom.CurrentBalance -= finOpModel.Difference;
                        orgAccTo.CurrentBalance += finOpModel.Difference;
                        _unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                        _unitOfWork.OrganizationAccountRepository.Edit(orgAccTo);
                        break;

                }
                _unitOfWork.SaveChanges();
                return finOpModel;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException("Щось пішло не так....О_о", ex);
            }
        }

        
        public IEnumerable<FinOpViewModel> GetAllFinOpsByOrgId(int orgId)
        {
            try
            {
                var finOps = _unitOfWork.FinOpRepository.Read();
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
                    OrgId = (f.OrgAccountTo != null ? f.OrgAccountTo.OrgId : f.OrgAccountFrom.OrgId)
                }).Where(f => f.OrgId == orgId);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(ErrorMessages.GetFinOpWithoutAccount, e);
            }
            
        }
    }
}

