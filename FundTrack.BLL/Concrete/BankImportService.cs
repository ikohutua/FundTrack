using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;
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
        public ImportPrivatViewModel RegisterBankExtracts(ImportPrivatViewModel privatViewModel)
        {
            try
            {
                for (int i = 0; i < privatViewModel.importsDetail.Length; ++i)
                {
                    int appcode = 0;
                    int.TryParse(privatViewModel.importsDetail[i].appCode, out appcode);
                    if (this._unitOfWork.BankImportDetailRepository.GetBankImportDetail(appcode) == null)
                    {
                        var bankImportDetail = new BankImportDetail
                        {
                            Card = privatViewModel.importsDetail[i].card,
                            Trandate = privatViewModel.importsDetail[i].trandate,
                            AppCode = appcode,
                            Amount = privatViewModel.importsDetail[i].amount,
                            CardAmount = privatViewModel.importsDetail[i].cardAmount,
                            Rest = privatViewModel.importsDetail[i].rest,
                            Terminal = privatViewModel.importsDetail[i].terminal,
                            Description = privatViewModel.importsDetail[i].description,
                            IsLooked=false
                        };
                        this._unitOfWork.BankImportDetailRepository.Create(bankImportDetail);
                    }
                }
                this._unitOfWork.SaveChanges();
                return privatViewModel;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

    }
}
