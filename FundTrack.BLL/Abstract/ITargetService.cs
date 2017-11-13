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

        bool DeleteTarget(int id);

        IEnumerable<TargetViewModel> GetTargets(int orgId, int parentId = 0);

        IEnumerable<TargetViewModel> GetTargetsByOrganizationIdWithEditableField(int id);
    }
}
