using System.Collections.Generic;
using FundTrack.Infrastructure.ViewModel.EditOrganizationViewModels;

namespace FundTrack.BLL.Abstract
{
    public interface ITargetService
    {
        TargetViewModel GetTargetById(int id);
        IEnumerable<TargetViewModel> GetTargetsByOrganizationId(int id);

        TargetViewModel EditTarget(TargetViewModel item);

        TargetViewModel AddTarget(TargetViewModel addresses);

        void DeleteTarget(int id);

    }
}
