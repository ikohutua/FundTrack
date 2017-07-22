using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.EditOrganizationViewModels;
using System.Collections.Generic;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Controller that manages operations with moderators of organization
    /// </summary>
    [Route("api/[controller]")]
    public class ModeratorController
    {
        private readonly IModeratorService _moderatorService;

        public ModeratorController(IModeratorService moderatorService)
        {
            _moderatorService = moderatorService;
        }

        [HttpGet("GetModerators/{id}")]
        public IEnumerable<ModeratorViewModel> GetModerators(int id)
        {
            return _moderatorService.GetOrganizationModerators(id);
        }

        [HttpPost("[action]")]
        public ModeratorViewModel AddModerator([FromBody]CreateModeratorViewModel moderator)
        {
            return _moderatorService.CreateModerator(moderator);
        }

        [HttpDelete("DeactivateModerator/{login}")]
        public ModeratorViewModel DeactivateModerator(string login)
        {
            return _moderatorService.DeactivateModerator(login);
        }

        [HttpGet("GetAvailableUsers/{id}")]
        public IEnumerable<CreateModeratorViewModel> GetAvailableUsers(int id)
        {
            return _moderatorService.GetAvailableUsers(id);
        }
    }
}
