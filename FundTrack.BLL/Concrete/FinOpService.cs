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

        /// <summary>
        /// Creates new instance of FinOpService
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public FinOpService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the targets.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessLogicException"></exception>
        public IEnumerable<TargetViewModel> GetTargets(int id)
        {
            try
            {
                return _unitOfWork.TargetRepository.GetTargetsByOrganizationId(id)
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

        /// <summary>
        /// Creates the fin op.
        /// </summary>
        /// <param name="finOpModel">The fin op model.</param>
        /// <returns></returns>
        public FinOpFromBankViewModel CreateFinOp(FinOpFromBankViewModel finOpModel)
        {
            try
            {
                var orgAccFrom = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardFromId);
                var orgAccTo = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(finOpModel.CardToId);
                var finOp = new FinOp
                {
                    AccFromId =orgAccFrom.Id,
                    AccToId = orgAccTo.Id,
                    TargetId = finOpModel.Targetid,
                    Amount = finOpModel.Amount,
                    Description = finOpModel.Description,
                    FinOpDate = DateTime.Now,
                };

                 _unitOfWork.FinOpRepository.Create(finOp);
                orgAccTo.CurrentBalance += finOpModel.Amount;
                _unitOfWork.OrganizationAccountRepository.Edit(orgAccTo);
                var bankImportDetail = _unitOfWork.BankImportDetailRepository.GetById(finOpModel.BankImportId);
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
            if (finOpModel.Sum <= 0 || finOpModel.Sum > 1000000)
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
                    Amount = finOpModel.Sum,
                    AccToId = orgAccTo.Id,
                    Description = finOpModel.Description,
                    TargetId = finOpModel.TargetId,
                    FinOpDate = DateTime.Now,
                };
                _unitOfWork.FinOpRepository.Create(finOp);

                orgAccTo.CurrentBalance += finOpModel.Sum;
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
                if (finOpModel.Sum > orgAccFrom.CurrentBalance)
                {
                    throw new ArgumentException("Витрати не можуть перебільшувати баланс рахунку");
                }
                var finOp = new FinOp
                {
                    Amount = finOpModel.Sum,
                    AccFromId = orgAccFrom.Id,
                    Description = finOpModel.Description,
                    TargetId = finOpModel.TargetId,
                    FinOpDate = DateTime.Now,
                };
                _unitOfWork.FinOpRepository.Create(finOp);
                orgAccFrom.CurrentBalance -= finOpModel.Sum;
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
                if (finOpModel.Sum > orgAccFrom.CurrentBalance)
                {
                    throw new ArgumentException("Витрати не можуть перебільшувати баланс рахунку");
                }
                var finOp = new FinOp
                {
                    Amount = finOpModel.Sum,
                    AccToId = orgAccTo.Id,
                    AccFromId = orgAccFrom.Id,
                    Description = finOpModel.Description,
                    FinOpDate = DateTime.Now,
                };
                _unitOfWork.FinOpRepository.Create(finOp);
                orgAccFrom.CurrentBalance -= finOpModel.Sum;
                _unitOfWork.OrganizationAccountRepository.Edit(orgAccFrom);
                orgAccTo.CurrentBalance += finOpModel.Sum;
                _unitOfWork.OrganizationAccountRepository.Edit(orgAccTo);
                _unitOfWork.SaveChanges();
                return finOpModel;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException("Щось пішло не так....О_о", ex);
            }
        }
        /// <summary>
        /// Gets the fin ops by org account.
        /// </summary>
        /// <param name="orgAccountId">The org account identifier.</param>
        /// <returns></returns>
        public IEnumerable<FinOpListViewModel> GetFinOpsByOrgAccount(int orgAccountId)
        {
            try
            {
                var finOps = _unitOfWork.FinOpRepository.GetFinOpByOrgAccount(orgAccountId)
                    .Select(f => new FinOpListViewModel
                    {
                        Date = f.FinOpDate,
                        Description = f.Description,
                        Amount = f.Amount,
                        Target = f.Target.TargetName,
                        CurrencyShortName = f.AccToId.HasValue ? f.OrgAccountTo.Currency.ShortName : f.OrgAccountFrom.Currency.ShortName,
                        CurrencyFullName = f.AccToId.HasValue ? f.OrgAccountTo.Currency.FullName : f.OrgAccountFrom.Currency.FullName,
                        FinOpType= f.FinOpType
                    });
                return finOps;
            }
            catch (Exception ex)
            {
                return new FinOpListViewModel
                {
                    Error = "Список фінансових операцій порожній."
                } as IEnumerable<FinOpListViewModel>;
            }
        }

    }
}

