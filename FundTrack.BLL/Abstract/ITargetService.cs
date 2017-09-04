using System.Collections.Generic;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;

namespace FundTrack.BLL.Abstract
{
    public interface ITargetService
    {
        TargetViewModel GetTargetById(int id);
        IEnumerable<TargetViewModel> GetTargetsByOrganizationId(int id);

        TargetViewModel EditTarget(TargetViewModel item);

        TargetViewModel CreateTarget(TargetViewModel addresses);

        void DeleteTarget(int id);

    }
}
