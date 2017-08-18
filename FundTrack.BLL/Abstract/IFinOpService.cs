using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.BLL.Abstract
{
    public interface IFinOpService
    {
        IEnumerable<TargetViewModel> GetTargets();

        FinOpViewModel CreateFinOp(FinOpViewModel finOpModel);
    }
}
