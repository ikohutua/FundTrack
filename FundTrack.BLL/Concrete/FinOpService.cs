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
        private readonly IUnitOfWork unitOfWork;

        /// <Amountmary>
        /// Creates new instance of FinOpService
        /// </Amountmary>
        /// <param name="unitOfWork">Unit of work</param>
        public FinOpService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <Amountmary>
        /// Gets the targets.
        /// </Amountmary>
        /// <returns></returns>
        /// <exception cref="BusinessLogicException"></exception>
        public IEnumerable<TargetViewModel> GetTargets(int id)
        {
            try
            {
                return unitOfWork.TargetRepository.GetTargetsByOrganizationId(id)
                                       .Select(item => new TargetViewModel
                                       {
                                           TargetId = item.Id,
                                           Name = item.TargetName,
                                           OrganizationId = item.OrganizationId
                                       });
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        /// <Amountmary>
        /// Creates the fin op.
        /// </Amountmary>
        /// <param name="finOpModel">The fin op model.</param>
        /// <returns></returns>
        public FinOpFromBankViewModel CreateFinOp(FinOpFromBankViewModel finOpModel)
        {
            try
            {
                var orgAccFrom = unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardFromId);
                var orgAccTo = unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardToId);
                var finOp = new FinOp
                {
                    AccFromId =orgAccFrom.Id,
                    AccToId = orgAccTo.Id,
                    TargetId = finOpModel.Targetid,
                    Amount = finOpModel.Amount,
                    Description = finOpModel.Description,
                    FinOpDate = DateTime.Now,
                };

                 unitOfWork.FinOpRepository.Create(finOp);
                orgAccTo.CurrentBalance += finOpModel.Amount;
                unitOfWork.OrganizationAccountRepository.Edit(orgAccTo);
                var bankImportDetail = unitOfWork.BankImportDetailRepository.GetById(finOpModel.BankImportId);
                bankImportDetail.IsLooked = true;
                unitOfWork.BankImportDetailRepository.ChangeBankImportState(bankImportDetail);
                unitOfWork.SaveChanges();
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
                var orgAccTo = unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardToId);
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
                unitOfWork.FinOpRepository.Create(finOp);

                orgAccTo.CurrentBalance += finOpModel.Amount;
                unitOfWork.OrganizationAccountRepository.Edit(orgAccTo);
                unitOfWork.SaveChanges();
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
                var orgAccFrom = unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardFromId);
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
                unitOfWork.FinOpRepository.Create(finOp);
                orgAccFrom.CurrentBalance -= finOpModel.Amount;
                unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                unitOfWork.SaveChanges();
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
                var orgAccFrom = unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardFromId);
                var orgAccTo = unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardToId);
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
                unitOfWork.FinOpRepository.Create(finOp);
                orgAccFrom.CurrentBalance -= finOpModel.Amount;
                unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                orgAccTo.CurrentBalance += finOpModel.Amount;
                unitOfWork.OrganizationAccountRepository.Edit(orgAccTo);
                unitOfWork.SaveChanges();
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
                var finOps = unitOfWork.FinOpRepository.GetFinOpByOrgAccount(orgAccountId)
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
                var f = unitOfWork.FinOpRepository.GetById(id);
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
                var orgAccFrom = unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardFromId);

                var finOp = unitOfWork.FinOpRepository.GetById(finOpModel.Id);
                finOp.Amount = finOpModel.Amount;
                finOp.Description = finOpModel.Description;
                finOp.TargetId = finOpModel.TargetId;
                finOp.FinOpDate = finOpModel.Date;
                finOp.UserId = finOpModel.UserId;
                unitOfWork.FinOpRepository.Update(finOp);

                switch (finOpModel.FinOpType)
                {
                    case 0:
                        orgAccFrom.CurrentBalance -= finOpModel.Difference;
                        unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                        break;
                    case 1:
                        orgAccFrom.CurrentBalance += finOpModel.Difference;
                        unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                        break;
                    case 2:
                        var orgAccTo = unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardToId);
                        orgAccFrom.CurrentBalance -= finOpModel.Difference;
                        orgAccTo.CurrentBalance += finOpModel.Difference;
                        unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                        unitOfWork.OrganizationAccountRepository.Edit(orgAccTo);
                        break;

                }
                unitOfWork.SaveChanges();
                return finOpModel;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException("Щось пішло не так....О_о", ex);
            }
        }

    }
}

