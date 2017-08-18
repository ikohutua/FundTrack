using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundTrack.BLL.Concrete
{
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
                        int.TryParse(importsDetail[i].appCode, out appcode);
                        if (this._unitOfWork.BankImportDetailRepository.GetBankImportDetail(appcode) == null)
                        {
                            var bankImportDetail = new BankImportDetail
                            {
                                Card = importsDetail[i].card,
                                Trandate = importsDetail[i].trandate,
                                AppCode = appcode,
                                Amount = importsDetail[i].amount,
                                CardAmount = importsDetail[i].cardAmount,
                                Rest = importsDetail[i].rest,
                                Terminal = importsDetail[i].terminal,
                                Description = importsDetail[i].description,
                                IsLooked = importsDetail[i].isLooked
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

        private IEnumerable<ImportDetailPrivatViewModel> ConvertFromEntityToModel(IEnumerable<BankImportDetail> bankImportDetails)
        {
            try
            {
                return bankImportDetails.Select(item => new ImportDetailPrivatViewModel
                {
                    id = item.Id,
                    isLooked = item.IsLooked,
                    trandate = item.Trandate,
                    appCode = item.AppCode.ToString(),
                    amount = item.Amount,
                    rest = item.Rest,
                    cardAmount = item.CardAmount,
                    description = item.Description,
                    terminal = item.Terminal,
                    card = item.Card,
                    tempState = item.IsLooked
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
