using System;
using System.Collections.Generic;
using System.Text;
using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.BLL.Concrete
{
    public class BankService : IBankService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BankService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BankViewModel> GetAllBanks()
        {
            var banks = _unitOfWork.BankRepository.Read();
            var banksVm = new List<BankViewModel>();
            foreach (var bank in banks)
            {
                banksVm.Add(bank);
            }
            return banksVm;
        }

        public Bank GetBankById(int bankId)
        {
            return _unitOfWork.BankRepository.Get(bankId);
        }
    }
}
