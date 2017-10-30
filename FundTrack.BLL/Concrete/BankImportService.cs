using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// BankImportService
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IBankImportService" />
    public class BankImportService : IBankImportService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Creates new instance of BankImportService
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public BankImportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Registers the bank extracts.
        /// </summary>
        /// <param name="privatViewModel">The privat view model.</param>
        /// <returns></returns>
        public IEnumerable<ImportDetailPrivatViewModel> RegisterBankExtracts(ImportDetailPrivatViewModel[] importsDetail)
        {
            try
            {
                if (importsDetail != null)
                {
                    for (int i = 0; i < importsDetail.Length; ++i)
                    {
                        int appcode = 0;
                        int.TryParse(importsDetail[i].AppCode, out appcode);
                        if (this._unitOfWork.BankImportDetailRepository.GetBankImportDetail(appcode) == null)
                        {
                            var bankImportDetail = new BankImportDetail
                            {
                                Card = importsDetail[i].Card,
                                Trandate = importsDetail[i].Trandate,
                                AppCode = appcode,
                                Amount = importsDetail[i].Amount,
                                CardAmount = importsDetail[i].CardAmount,
                                Rest = importsDetail[i].Rest,
                                Terminal = importsDetail[i].Terminal,
                                Description = importsDetail[i].Description,
                                IsLooked = importsDetail[i].IsLooked
                            };

                            if (bankImportDetail != null)
                            {
                                this._unitOfWork.BankImportDetailRepository.Create(bankImportDetail);
                            }
                        }
                    }
                    this._unitOfWork.SaveChanges();
                    return importsDetail;
                }
                else
                {
                    var message = "Список виписок порожній.";
                    throw new BusinessLogicException(message);
                }
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        public IEnumerable<ImportDetailPrivatViewModel> getAllSuggestedBankImports(decimal amount, DateTime date)
        {
            try
            {
                var bankImports = _unitOfWork.BankImportDetailRepository.GetNotLookedBankImportsDetail();
                DateTime minOperationTime = date.AddMinutes(-15);
                DateTime maxOperationTime = date.AddMinutes(15);
                decimal amountWithComission;
                if (amount < 0)
                {
                    amountWithComission = Convert.ToDecimal(0.9) * amount;
                    bankImports = bankImports.Where(import => CutCurrencyFromPrivate24(import.CardAmount) <= -amount &&
                                                              CutCurrencyFromPrivate24(import.CardAmount) >= -amountWithComission);
                }
                else
                {
                    amountWithComission = Convert.ToDecimal(10.0 / 9.0) * amount;
                    bankImports = bankImports.Where(import => CutCurrencyFromPrivate24(import.CardAmount) <= -amount &&
                                                              CutCurrencyFromPrivate24(import.CardAmount) >= -amountWithComission);
                }
                bankImports = bankImports.Where(import => import.Trandate >= minOperationTime &&
                                            import.Trandate <= maxOperationTime);
                return ConvertFromEntityToModel(bankImports.ToList());
            }
            catch(Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        public decimal CutCurrencyFromPrivate24(string cardAmount)
        {
            return Convert.ToDecimal(cardAmount.Split(' ').First().Replace('.', ','));
        }

        /// <summary>
        /// Gets the  extracts with filter options.
        /// </summary>
        /// <param name="bankSearchModel">The bank search model.</param>
        /// <returns></returns>
        /// <exception cref="BusinessLogicException">
        /// </exception>
        public IEnumerable<ImportDetailPrivatViewModel> GetRawExtracts(BankImportSearchViewModel bankSearchModel)
        {
            try
            {
                var rawExtracts = this.ConvertFromEntityToModel(this._unitOfWork.BankImportDetailRepository.FilterBankImportDetail(bankSearchModel));
                if (rawExtracts != null)
                {
                    return rawExtracts;
                }
                else
                {
                    var message = "Немає виписок за даний період.";
                    throw new BusinessLogicException(message);
                }
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets all extracts in one org accounts
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns></returns>
        /// <exception cref="BusinessLogicException"></exception>
        public IEnumerable<ImportDetailPrivatViewModel> GetAllExtracts(string card)
        {
            try
            {
                return this.ConvertFromEntityToModel(this._unitOfWork.BankImportDetailRepository.GetBankImportDetailsOneCard(card));
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the count extracts in one org accounts
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns></returns>
        /// <exception cref="BusinessLogicException"></exception>
        public int GetCountExtracts(String card)
        {
            try
            {
                return this._unitOfWork.BankImportDetailRepository.GetBankImportDetailsOneCard(card).Count();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Converts from bankImportentity to ImportDetailPrivalViewModel.
        /// </summary>
        /// <param name="bankImportDetails">The bank import details.</param>
        /// <returns></returns>
        /// <exception cref="BusinessLogicException"></exception>
        private IEnumerable<ImportDetailPrivatViewModel> ConvertFromEntityToModel(IEnumerable<BankImportDetail> bankImportDetails)
        {
            try
            {
                return bankImportDetails.Select(item => new ImportDetailPrivatViewModel
                {
                    Id = item.Id,
                    IsLooked = item.IsLooked,
                    Trandate = item.Trandate,
                    AppCode = item.AppCode.ToString(),
                    Amount = item.Amount,
                    Rest = item.Rest,
                    CardAmount = item.CardAmount,
                    Description = item.Description,
                    Terminal = item.Terminal,
                    Card = item.Card
                });
            }
            catch (Exception ex)
            {
                var message = "Немає виписок.";
                throw new BusinessLogicException(message, ex);
            }
        }
    }
}
