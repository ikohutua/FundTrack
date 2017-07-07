using FundTrack.Infrastructure.ViewModel.EventViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.BLL.Abstract
{
    public interface IEventManagementService
    {
        IEnumerable<EventManagementViewModel> GetAllEventsByOrganizationId(int id);
    }
}
