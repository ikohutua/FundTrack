using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public IEnumerable<TargetViewModel> GetTargets()
        {
            try
            {
                return this._unitOfWork.TargetRepository.Read()
                                       .Select(item => new TargetViewModel
                                       {
                                           TargetId = item.Id,
                                           Name = item.TargetName
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
        public FinOpViewModel CreateFinOp(FinOpViewModel finOpModel)
        {
            try
            {
                var finOp = new FinOp
                {
                    AccToId = this._unitOfWork.OrganizationAccountRepository.GetOrgAccountByName(finOpModel.OrgId, finOpModel.AccToName).Id,
                    TargetId = this._unitOfWork.TargetRepository.GetTargetByName(finOpModel.TargetName).Id,
                    Amount = finOpModel.Amount,
                    Description = finOpModel.Description,
                    FinOpDate = DateTime.Now
                };

                var finOpEntity = this._unitOfWork.FinOpRepository.Create(finOp);
                this._unitOfWork.SaveChanges();

                var createdFinOp = this._unitOfWork.FinOpRepository.GetById(finOpEntity.Id);
                createdFinOp.OrgAccountTo.CurrentBalance += finOpModel.Amount;
                this._unitOfWork.FinOpRepository.Update(createdFinOp);

                var bankImportDetail = this._unitOfWork.BankImportDetailRepository.GetById(finOpModel.BankImportId);
                bankImportDetail.IsLooked = true;
                this._unitOfWork.BankImportDetailRepository.ChangeBankImportState(bankImportDetail);

                this._unitOfWork.SaveChanges();
                return finOpModel;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
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
                var finOps= this._unitOfWork.FinOpRepository.GetFinOpByOrgAccount(orgAccountId)

                                                        .Select(f => new FinOpListViewModel
                                                        {
                                                            Date = f.FinOpDate,
                                                            Description = f.Description,
                                                            Amount = f.Amount,
                                                            CurrencyShortName =  f.AccToId.HasValue?f.OrgAccountTo.Currency.ShortName: f.OrgAccountFrom.Currency.ShortName,
                                                            CurrencyFullName = f.AccToId.HasValue ? f.OrgAccountTo.Currency.FullName : f.OrgAccountFrom.Currency.FullName
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

